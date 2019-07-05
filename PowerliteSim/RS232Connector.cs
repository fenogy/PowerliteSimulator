using System;
using System.Threading;
using System.IO.Ports;

namespace PowerliteSim
{
    public delegate void WriteEventHandler(string msg);
    public delegate string ReadEventHandler(string msg);

	public class RS232Connector : IDisposable
	{
        private RS232Settings settings;
        //private bool isConnected;
        private SerialPort serialPort;
        private string lastErrorMsg;
        //private 
        private event ReadEventHandler onReadEvent;
        private event WriteEventHandler onWriteEvent;


        public RS232Connector()
        {
            //isConnected = false;            
        }

		public string GetResponse(string message)
		{                        
            WritePort(message);
            Thread.Sleep(settings.ResponseDelay);
            return ReadPort();
		}
        public string GetResponse(byte[] command)
        {
            WritePort(command);

            Thread.Sleep(settings.ResponseDelay);

            return ReadPort();
        }
        public bool Init(RS232Settings settings)
        {
            string portName;

            this.settings = (RS232Settings)settings;
            
            if (serialPort == null || !serialPort.IsOpen)// !isConnected)
            {
                portName = "COM" + this.settings.CommunicationPort.ToString();
                serialPort = new SerialPort(portName, (int)this.settings.BaudRate, this.settings.Parity, this.settings.DataBits,this.settings.StopBits);

                serialPort.ReadTimeout = Timeout.Infinite;
                serialPort.WriteTimeout = Timeout.Infinite;
                serialPort.Encoding = System.Text.Encoding.ASCII;

                try
                {
                    serialPort.Open();
                    //if (serialPort.IsOpen)                    
                    //    isConnected = true;                    
                }                
                catch (UnauthorizedAccessException uaExc)
                {
                    lastErrorMsg = uaExc.Message;
                }
                catch (System.IO.IOException ioExc)
                {
                    lastErrorMsg = ioExc.Message;

                }
            }
            return serialPort.IsOpen;
        }

        public bool GetConnectionStatus()
        {
            if (serialPort == null)
                return false;

            return serialPort.IsOpen;
        }

		public string ReadPort()
		{
            string data = "";
            if (serialPort.IsOpen)
            {
                try
                {
                    data = serialPort.ReadExisting();
                }
                catch(InvalidOperationException e)
                {

                }
                    if (onReadEvent != null)
                        onReadEvent(data);

            }
            return data;
		}
        //public void SubscribeReadEvent(ReadEventHandler readUpdateMethod)
        //{
        //    onReadEvent += readUpdateMethod;
        //}

        //public void SubscribeWriteEvent(WriteEventHandler writeUpdateMethod)
        //{
        //    onWriteEvent += writeUpdateMethod;
        //}

		public void Uninit()
		{
            if(serialPort != null)
            serialPort.Close();
		}

        //public void UnsubscribeReadEvent(ReadEventHandler readUpdateMethod)
        //{
        //    onReadEvent -= readUpdateMethod;
        //}

        //public void UnsubscribeWriteEvent(WriteEventHandler writeUpdateMethod)
        //{
        //    onWriteEvent -= writeUpdateMethod;
        //}

		public void WritePort(string message)
		{
            char[] data;
            if (serialPort.IsOpen)
            {
                if (settings.InterCharGap == 0)
                {
                    try
                    {
                        serialPort.Write(message);
                    }
                    catch (InvalidOperationException e)
                    {

                    }
                    catch (ArgumentException e)
                    {
                    }
                    catch (TimeoutException e)
                    {

                    }

                }
                else
                {
                    data = message.ToCharArray();
                    Console.WriteLine(">"+message);
                    for (int i = 0; i < data.Length; i++)
                    {
                        try
                    {
                        serialPort.Write(data, i, 1);
                    }
                    catch (InvalidOperationException e)
                    {

                    }
                    catch (ArgumentException e)
                    {
                    }
                    catch (TimeoutException e)
                    {

                    }
                        Thread.Sleep(settings.InterCharGap);
                    }
                }
            }

            if (onWriteEvent != null)
                onWriteEvent(message);
		}
        // Write the desired command bytes to communication port
        public void WritePort(byte[] command)
        {
            if (serialPort.IsOpen)
            {
                if (settings.InterCharGap == 0)
                {
                    try
                    {
                        serialPort.Write(command, 0, command.Length);
                    }
                    catch (InvalidOperationException e)
                    {

                    }
                    catch (ArgumentException e)
                    {
                    }
                    catch (TimeoutException e)
                    {

                    }
                }
                else
                {
                    for (int i = 0; i < command.Length; i++)
                    {
                        try
                        {
                            serialPort.Write(command, i, 1);
                        }
                        catch (InvalidOperationException e)
                        {

                        }
                        catch (ArgumentException e)
                        {
                        }
                        catch(TimeoutException e)
                        {

                        }
                        Thread.Sleep(settings.InterCharGap);
                    }
                }
            }
        }

        //public int CommId
        //{
        //    get {return settings.DeviceId;}
        //}

        public int ResponseDelay
        {
            get{return settings.ResponseDelay;}
        }
        
        public string LastErrorMsg
        {
            get{return lastErrorMsg;}
        }

        public RS232Settings Settings
        {
            set { settings = (RS232Settings)value; }
            get { return settings; }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                serialPort.Close();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        string lastCmd = "";
        internal bool Listen(out string newCommand)
        {
            string ss = ReadPort();
            lastCmd += ss;
            //newCommand = ReadPort();
            newCommand = "";
            int lfLocation = lastCmd.IndexOf("\n");

            if (lfLocation > 0)
            {
                try
                {
                    //lastCmd.Remove(lastCmd.IndexOf((char)0x0a));
                    lastCmd.Remove(lfLocation);
                }
                catch (Exception e)
                {
                }
            }
            //if(lastCmd.IndexOf(lastCmd.Length - 1)
            //if (/*newCommand != ""*/lastCmd != "" && (lastCmd.Contains("\r") || lastCmd.Contains("\n")))
            if (/*newCommand != ""*/lastCmd != "" && (lastCmd.Contains("\r") || lastCmd.Contains("\n")))
            {
                newCommand = lastCmd;
                lastCmd = "";
                return true;
            }
            else
            {
                WritePort(ss);
                ss = "";
                return false;
            }

        }

        internal void SendResponse(string p)
        {
            WritePort(p);
        }

    }
}
