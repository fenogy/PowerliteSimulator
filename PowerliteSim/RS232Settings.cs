using System;
using System.IO.Ports;

namespace PowerliteSim
{
    public class RS232Settings
    {
        #region Fields

        private int commPortId;
        private int baudRate;
        private int dataBits;
        private Parity parity;
        private StopBits stopBits;
        private int interCharGap;
        private int responseDelay;
        private int deviceId;
        private int Test;

        Configuration portConfig = Configuration.Deserialize("config.xml");

       #endregion

        #region Constructor
        

        public RS232Settings()
        {

            commPortId = portConfig.ComPort;
            baudRate = 19200;
            dataBits = 8;
            parity = Parity.None;
            stopBits = StopBits.One;
            interCharGap = 0;
            responseDelay = 300;
           
            
        }

        public RS232Settings(int deviceId, int portId, int baudRate, int dataBits, Parity parity, StopBits stopBits, int interCharGap, int responseDelay)
        {

            //this.commPortId = portId;
            //this.baudRate = baudRate;
            //this.dataBits = dataBits;
            //this.parity = parity;
            //this.stopBits = stopBits;
            //this.interCharGap = interCharGap;
            
        }

        //public RS232Settings(RS232Settings settings)
        //{

        //    commPortId = settings.CommunicationPort;
        //    baudRate = (int)settings.BaudRate;
        //    dataBits = settings.DataBits;
        //    parity = (Parity)Enum.Parse(typeof(Parity), settings.Parity);
        //    stopBits = (StopBits)Enum.Parse(typeof(StopBits), settings.StopBits);
        //    interCharGap = settings.InterCharGap;

        //}
        #endregion

        #region Properties

        #region Public

        public int CommunicationPort
        {
            get { return commPortId; }
            set { commPortId = value; }
        }


        public BaudRate BaudRate
        {
            get { return (BaudRate)baudRate; }
            set { baudRate = (int)value; }
        }

        public int DataBits
        {
            get { return dataBits; }
            set { dataBits = value; }
        }

        public Parity Parity
        {
            get { return (Parity)parity; }
            set { parity = value; }
        }

        public StopBits StopBits
        {
            get { return stopBits; }
            set { stopBits = value; }
        }

        public int InterCharGap
        {
            get { return interCharGap; }
            set { interCharGap = value; }
        }


        public int ResponseDelay
        {
            get { return responseDelay; }
            set { responseDelay = value; }
        }

        //public int DeviceId
        //{
        //    get { return deviceId; }
        //    set { deviceId = value; }
        //}

        #endregion

        #endregion

        public object Clone()
        {
            RS232Settings db = new RS232Settings();
            //db.DeviceId = DeviceId;
            db.CommunicationPort = CommunicationPort;
            db.BaudRate = BaudRate;
            db.DataBits = DataBits;
            db.Parity = Parity;
            db.StopBits = StopBits;
            db.ResponseDelay = responseDelay;
            db.InterCharGap = interCharGap;
           


            return db;
        }
	}

    public enum BaudRate
    {
        BR110 = 110,
        BR300 = 300,
        BR600 = 600,
        BR1200 = 1200,
        BR2400 = 2400,
        BR4800 = 4800,
        BR9600 = 9600,
        BR14400 = 14400,
        BR19200 = 19200,
        BR38400 = 38400,
        BR56000 = 56000,
        BR57600 = 57600,
        BR115200 = 115200,
        BR128000 = 128000,
        BR256000 = 256000,
    }
}
