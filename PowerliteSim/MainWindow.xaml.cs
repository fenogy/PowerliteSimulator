using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace PowerliteSim
{

    public partial class MainWindow : Window
    {


        private Thread processThread;
        private Thread imageChanger;

        private Statemachine machine = new Statemachine();
        private RS232Connector controller;

        private DispatcherTimer updateTimer;
        private DispatcherTimer TransitionTimer;
        private DispatcherTimer timerFlasher;
        private DispatcherTimer FlashingTimer;
        private LaserSettings LaserSettings;

        private int hg1Count = 0;
        private int hg2Count = 0;
        private bool hg1ena = false;
        private bool hg2ena = false;

        int begin1 = 0;
        int end1 = 0;
        int begin2 = 0;
        int end2 = 0;
        int rotAngle = 20;
        DoubleAnimation da1;
        DoubleAnimation da2;
        RotateTransform rt1 = new RotateTransform();
        RotateTransform rt2 = new RotateTransform();

        private bool started = false;
        public  bool settingsOK = false;
        public bool isHvpsorEocBlink = false;
        public bool isCoolantResistanceBlink = false;
        public bool isWaterLevelBlink = false;
        public int evenOdd = 0;

        public delegate void GUIRotationHandler(int message, bool dir, int index);
        public delegate void FireBurstHandler(int status);
        public GUIRotationHandler GUIRotDelegate;
      
        public delegate void FlashingHandeler();
        public FlashingHandeler FlashingDelegate;
        private bool isExpanded = false;

        public MainWindow()
        {

            InitializeComponent();
            
            controller = new RS232Connector();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //////UI Compression///
            BackgroundRect.Visibility = System.Windows.Visibility.Hidden;
            BackgroundRectCompressed.Visibility = System.Windows.Visibility.Visible;
            //label51.Content = "Less";


            startButtonImage.Visibility = System.Windows.Visibility.Hidden;
            image5.Visibility = System.Windows.Visibility.Hidden;
            closeImage.Visibility = System.Windows.Visibility.Hidden;
            rectangle17.Visibility = System.Windows.Visibility.Hidden;
            rectangle18.Visibility = System.Windows.Visibility.Hidden;
            grid4.Visibility = System.Windows.Visibility.Hidden;
            rectangle19.Visibility = System.Windows.Visibility.Hidden;
            image3.Visibility = System.Windows.Visibility.Hidden;
            image3_comp.Visibility = System.Windows.Visibility.Visible;//logo
            label50.Visibility = System.Windows.Visibility.Hidden;//command display

            label4.Visibility = System.Windows.Visibility.Hidden;
            label5.Visibility = System.Windows.Visibility.Hidden;
            label7.Visibility = System.Windows.Visibility.Hidden;
            label8.Visibility = System.Windows.Visibility.Hidden;
            label9.Visibility = System.Windows.Visibility.Hidden;
            label10.Visibility = System.Windows.Visibility.Hidden;
            label11.Visibility = System.Windows.Visibility.Hidden;
            label12.Visibility = System.Windows.Visibility.Hidden;
            label13.Visibility = System.Windows.Visibility.Hidden;
            label43.Visibility = System.Windows.Visibility.Hidden;

            label47.Visibility = System.Windows.Visibility.Hidden;
            label48.Visibility = System.Windows.Visibility.Hidden;

            label6.Visibility = System.Windows.Visibility.Hidden;
            label13.Visibility = System.Windows.Visibility.Hidden;
            label14.Visibility = System.Windows.Visibility.Hidden;
            label16.Visibility = System.Windows.Visibility.Hidden;
            label15.Visibility = System.Windows.Visibility.Hidden;
            label17.Visibility = System.Windows.Visibility.Hidden;
            label1.Visibility = System.Windows.Visibility.Hidden;
            label2.Visibility = System.Windows.Visibility.Hidden;
            textBoxVoltage.Visibility = System.Windows.Visibility.Hidden;
            textBoxOscDelay.Visibility = System.Windows.Visibility.Hidden;
            textBoxAmpVoltage.Visibility = System.Windows.Visibility.Hidden;
            textBoxAmpDelay.Visibility = System.Windows.Visibility.Hidden;
            textBoxQDelay.Visibility = System.Windows.Visibility.Hidden;
            textBoxQDivision.Visibility = System.Windows.Visibility.Hidden;
            textBoxQTrigger.Visibility = System.Windows.Visibility.Hidden;
            textBoxLampTrigger.Visibility = System.Windows.Visibility.Hidden;
            textBoxSeederDelay.Visibility = System.Windows.Visibility.Hidden;
            textBoxSyncDelay.Visibility = System.Windows.Visibility.Hidden;
            textBoxOscTemp.Visibility = System.Windows.Visibility.Hidden;
            textBoxAmp1Temp.Visibility = System.Windows.Visibility.Hidden;
            textBoxAmp2temp.Visibility = System.Windows.Visibility.Hidden;
            textBoxWaterTemp.Visibility = System.Windows.Visibility.Hidden;
            textBoxOscShotCnt.Visibility = System.Windows.Visibility.Hidden;
            textBoxAmpShotCnt.Visibility = System.Windows.Visibility.Hidden;
            textBoxSysShotCnt.Visibility = System.Windows.Visibility.Hidden;
            textBoxCurrentMode.Visibility = System.Windows.Visibility.Hidden;
            textBoxCurrentState.Visibility = System.Windows.Visibility.Hidden;
            textBoxPRF.Visibility = System.Windows.Visibility.Hidden;
            textBoxQramp.Visibility = System.Windows.Visibility.Hidden;
            textBoxVramp.Visibility = System.Windows.Visibility.Hidden;
            label18.Visibility = System.Windows.Visibility.Hidden;
            label19.Visibility = System.Windows.Visibility.Hidden;
            label20.Visibility = System.Windows.Visibility.Hidden;

            //extra small
            label21.Visibility = System.Windows.Visibility.Hidden;
            label45.Visibility = System.Windows.Visibility.Hidden;
            headTempFlt.Visibility = System.Windows.Visibility.Hidden;
            coolanResFlt.Visibility = System.Windows.Visibility.Hidden;
            coolanOTPFlt.Visibility = System.Windows.Visibility.Hidden;
            coolantLvlLow.Visibility = System.Windows.Visibility.Hidden;
            HvpsFlt.Visibility = System.Windows.Visibility.Hidden;
            EocFlt.Visibility = System.Windows.Visibility.Hidden;
            ExtIlkChkBox.Visibility = System.Windows.Visibility.Hidden;
            HeadCableIlk.Visibility = System.Windows.Visibility.Hidden;
            CoverIlk.Visibility = System.Windows.Visibility.Hidden;
            textBoxCurrentModeDesc.Visibility = System.Windows.Visibility.Hidden;
            label50.Visibility = System.Windows.Visibility.Hidden;//command
            rectangle30.Visibility = System.Windows.Visibility.Hidden;//smaller one
            rectangle31.Visibility = System.Windows.Visibility.Hidden;// largerone

            rectangle30_comp.Visibility = System.Windows.Visibility.Visible;//smaller one
            textBoxCurrentModeDesc_comp.Visibility = System.Windows.Visibility.Visible;

            image6_comp.Visibility = System.Windows.Visibility.Hidden; //compand button

            ///////UI compression finished///////////////

            amp2.Visibility = System.Windows.Visibility.Hidden;
            amp1.Visibility = System.Windows.Visibility.Hidden;
            osc.Visibility = System.Windows.Visibility.Hidden;

            updateTimer = new DispatcherTimer();
            updateTimer.Interval = TimeSpan.FromMilliseconds(800);
            updateTimer.Tick += UpdateTimer_Tick;

            updateTimer.Start();

            TransitionTimer = new DispatcherTimer();
            TransitionTimer.Interval = TimeSpan.FromSeconds(1);
            TransitionTimer.Tick += TransitionTimer_Tick;

            TransitionTimer.Start();

            FlashingTimer = new DispatcherTimer();
            FlashingTimer.Interval = TimeSpan.FromMilliseconds(800);
            FlashingTimer.Tick += FlashingTimer_Tick;
            FlashingTimer.Start();

            light1.Visibility = System.Windows.Visibility.Hidden;
            light2.Visibility = System.Windows.Visibility.Hidden;
            light3.Visibility = System.Windows.Visibility.Hidden;

            beampathanimation.Visibility = System.Windows.Visibility.Hidden;

            //Rotation handling code
            machine.Rot += new Statemachine.RotationHandler(invokeRotation);
            machine.StartBurst += new Statemachine.BurstHandler(invokeBurstMode);

            da1 = new DoubleAnimation(begin1, new Duration(TimeSpan.FromSeconds(1.5)));
            da1.AccelerationRatio = 0;
            da1.DecelerationRatio = 0;
            Hg1Image.RenderTransform = rt1;
            Hg1Image.RenderTransformOrigin = new Point(0.5, 0.5);

            da2 = new DoubleAnimation(begin2, new Duration(TimeSpan.FromSeconds(1.5)));
            da2.AccelerationRatio = 0;
            da2.DecelerationRatio = 0;
            Hg2Image.RenderTransform = rt2;
            Hg2Image.RenderTransformOrigin = new Point(0.5, 0.5);
            //gIFImageControl1.Visibility = System.Windows.Visibility.Hidden;

            machine.LoadOldConfiguration();

            imageChanger = new Thread(new ThreadStart(invokeFlashing));
            imageChanger.Priority = ThreadPriority.Highest;
            imageChanger.Start();

            startStop();
            //startButtonImage_MouseLeave(sender, null);


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (processThread != null)
            {
                processThread.Abort();
                processThread.Join();

            }
            if (imageChanger != null)
            {
                imageChanger.Abort();
                imageChanger.Join();
            }

            //if ( != null)
            //{
            //    imageChanger.Abort();
            //    imageChanger.Join();
            //}          
            machine.SaveConfiguration();
            controller.Uninit();
        }


        public void updateRotation(int steps, bool direction, int HGIndex)
        {
            if (HGIndex == 1)
            {
                if (direction == true)
                {
                    end1 = begin1 + (steps * rotAngle);
                    da1.From = begin1;
                    da1.To = end1;
                    rt1.BeginAnimation(RotateTransform.AngleProperty, da1);
                    begin1 = end1;

                }
                else if (direction == false)
                {

                    begin1 = end1 - (steps * rotAngle);

                    da1.From = end1;
                    da1.To = begin1;
                    rt1.BeginAnimation(RotateTransform.AngleProperty, da1);
                    end1 = begin1;
                }
            }
            else if (HGIndex == 2)
            {
                if (direction == true)
                {
                    end2 = begin2 + (steps * rotAngle);
                    da2.From = begin2;
                    da2.To = end2;
                    rt2.BeginAnimation(RotateTransform.AngleProperty, da2);
                    begin2 = end2;
                }
                else if (direction == false)
                {

                    begin2 = end2 - (steps * rotAngle);

                    da2.From = end2;
                    da2.To = begin2;
                    rt2.BeginAnimation(RotateTransform.AngleProperty, da2);
                    end2 = begin2;

                }
            }


        }

        public void invokeRotation(int steps, bool direction, int HGIndex)
        {
            //  if (eventTriggerCount == 2)
            // {
            //     eventTriggerCount = 1;
            // }
            // else if (eventTriggerCount == 1)
            // {
            Dispatcher.BeginInvoke(new GUIRotationHandler(updateRotation), steps, direction, HGIndex);
            //  eventTriggerCount++;
            // }

            // this.invoke(
        }
        public void invokeBurstMode(int status)
        {
            //  if (eventTriggerCount == 2)
            // {
            //     eventTriggerCount = 1;
            // }
            // else if (eventTriggerCount == 1)
            // {
            Dispatcher.BeginInvoke(new FireBurstHandler(UpdateBurstStatus), status);
            //  eventTriggerCount++;
            // }

            // this.invoke(
        }

        public void invokeFlashing()
        {
            //  if (eventTriggerCount == 2)
            // {
            //     eventTriggerCount = 1;
            // }
            // else if (eventTriggerCount == 1)
            // {
            while(true){

                DispatcherOperation op = Dispatcher.BeginInvoke(new FlashingHandeler(timerFlasher_Tick));
                //op.Wait(TimeSpan.FromMilliseconds(200));
                 Thread.Sleep(200);
             }
            //  eventTriggerCount++;
            // }

            // this.invoke(
        }

        #region UI Events


        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.DragMove();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(200);
            this.Close();
        }

        private void menu1_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            if (!started)
            {
                //if ( controller.Init((byte)machine.port ,(uint)machine.baudeRate ,(byte)machine.dataBits ,(byte)machine.parity ,(byte)machine.stopBits ) )
                if (controller.Init(machine.Settings))
                {
                    processThread = new Thread(new ThreadStart(Process));
                    processThread.Start();
                    SetStartMode(true);
                }
                else
                {
                    MessageBox.Show("Cannot Initialize the communication, port inaccessible!");
                }
            }
            else
            {
                processThread.Abort();
                processThread.Join();
                controller.Uninit();

                SetStartMode(false);
                processThread = null;
            }
        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {
            LaserSettings dlg = new LaserSettings();

            dlg.Settings = machine.Settings;
            dlg.ShowDialog();
            
            if (dlg.DialogResult.HasValue && dlg.DialogResult.Value)
            {

                machine.Settings = dlg.Settings;
                machine.port = dlg.Settings.CommunicationPort;
            }

        }

        private void SetStartMode(bool state)
        {
            started = state;

            if (state)
            {
                buttonStart.Content = "Stop";
                RectangleStartLED.Fill = new SolidColorBrush(Colors.GreenYellow);

                    Color.FromRgb(0, 255, 0);
                //pictureBoxStartLED.BackColor = Color.Lime;
            }
            else
            {
                buttonStart.Content = "Start";
                RectangleStartLED.Fill = new SolidColorBrush(Colors.Red);
                //pictureBoxStartLED.BackColor = Color.Red;
            }

        }

        #endregion

        #region Simulator

        #region communication &  simulator process

        private void Process()
        {
            while (true)
            {
                string newCommand = "";
                if (controller.Listen(out newCommand))
                {
                    Console.WriteLine(newCommand);
                    string sendBuffer = "\r\n:" + machine.HandleCommand(newCommand) + "\r\n:";
                    controller.SendResponse(sendBuffer);
                    Console.WriteLine(sendBuffer);
                }
                Thread.Sleep(machine.GetResponseDelay());

            }
        }

        #endregion


        #endregion

        #region Timer Events

        private void UpdateTimer_Tick(object sender, System.EventArgs e)
        {
            //UpdateLEDStatus();

            textBoxPRF.Text = machine.GetPRF;
            textBoxPower.Text = machine.GetOutputPower;
            textBoxLampTrigger.Text = machine.GetFlashIFMode;
            textBoxQTrigger.Text = machine.GetQSWIFMode;
            textBoxQDivision.Text = machine.GetQDivision.ToString();
            textBoxVoltage.Text = machine.GetVoltage;

            // new assigns 
            textBoxAmpVoltage.Text = machine.GetAmpVoltage;
            textBoxOscDelay.Text = machine.GetOscDelay;
            textBoxAmpDelay.Text = machine.GetAmpDelay;
            textBoxSeederDelay.Text = machine.GetSeederDelay;
            textBoxSyncDelay.Text = machine.GetSyncDelay;
            textBoxOscTemp.Text = machine.GetOscTemp;
            textBoxAmp1Temp.Text = machine.GetAmp1Temp;
            textBoxAmp2temp.Text = machine.GetAmp2Temp;
            textBoxWaterTemp.Text = machine.GetWaterTemp;
            textBoxQramp.Text = machine.GetQRamp;
            textBoxVramp.Text = machine.GetVRamp;
            textBoxOscShotCnt.Text = Convert.ToString(machine.OscShotCount);
            textBoxAmpShotCnt.Text = Convert.ToString(machine.OscShotCount);
            textBoxSysShotCnt.Text = Convert.ToString(machine.SysShotCount);

            amp2VoltageAux.Text = machine.GetAmpVoltage;
            amp2DelayAux.Text = machine.GetAmpDelay;
            amp2TempAux.Text = machine.GetAmp2Temp;
            amp2shtAux.Text = Convert.ToString(machine.OscShotCount);

            amp1VoltageAux.Text = machine.GetAmpVoltage;
            amp1DelayAux.Text = machine.GetAmpDelay;
            amp1TempAux.Text = machine.GetAmp1Temp;
            amp1shtAux.Text = Convert.ToString(machine.OscShotCount);

            oscVoltageAux.Text = machine.GetVoltage;
            oscDelayAux.Text = machine.GetOscDelay;
            oscTempAux.Text = machine.GetOscTemp;
            oscshtAux.Text = Convert.ToString(machine.OscShotCount);

            label50.Content = machine.receivedCommand;
            UpdateLEDStatus();
        }

        public  void timerFlasher_Tick()    
   {


            if (machine.Mode >= 2 && machine.Mode < 4)
            {
                if (amp2.Visibility == System.Windows.Visibility.Hidden && amp1.Visibility == System.Windows.Visibility.Hidden && osc.Visibility == System.Windows.Visibility.Hidden)
                {

                    amp2.Visibility = System.Windows.Visibility.Visible;
                    amp1.Visibility = System.Windows.Visibility.Visible;
                    osc.Visibility = System.Windows.Visibility.Visible;
                    Thread.Sleep(50);
                }
                else
                {
                    amp2.Visibility = System.Windows.Visibility.Hidden;
                    amp1.Visibility = System.Windows.Visibility.Hidden;
                    osc.Visibility = System.Windows.Visibility.Hidden;
                    Thread.Sleep(50);

                }
            }
            else
            {
                amp2.Visibility = System.Windows.Visibility.Hidden;
  