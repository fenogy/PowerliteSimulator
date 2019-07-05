using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PowerliteSim
{
    [Serializable]
    public class Configuration
    {

      int _OscVoltage;

      decimal _AmpVoltage;
      decimal _OscDelay;
      decimal _AmpDelay;
      int _Qdelay;
      int _Qdivision;
      int _Prf;
      int _Qramp;
      int _Vramp;
      decimal _SeederDelay;
      decimal _SyncDelay;

      ulong _ShotCount;
      ulong _SysShotCount;

      int _ComPort;

      int _BurstLength;




      public Configuration()
      {
         _OscVoltage = 1200;
         _AmpVoltage = 1450;
         _OscDelay = 50;
         _AmpDelay = 55;
         _Qdelay = 190;
         _Qdivision = 1;
         _Prf = 10;
         _Qramp = 60 ;
         _Vramp = 300;
         _SeederDelay = 10.5M;
         _SyncDelay = 16.5M;
         _ShotCount = 0;
         _ComPort = 1;
         _BurstLength = 25;
      }

      public static void Serialize(string file, Configuration c)
      {
         System.Xml.Serialization.XmlSerializer xs 
            = new System.Xml.Serialization.XmlSerializer(c.GetType());
         StreamWriter writer = File.CreateText(file);
         xs.Serialize(writer, c);
         writer.Flush();
         writer.Close();
      }

      public static Configuration Deserialize(string file)
      {
         System.Xml.Serialization.XmlSerializer xs 
            = new System.Xml.Serialization.XmlSerializer(
               typeof(Configuration));
         StreamReader reader = File.OpenText(file);
         Configuration c = (Configuration)xs.Deserialize(reader);
         reader.Close();
         return c;
      }

      public int OscVoltage
      {
          get { return _OscVoltage; }
          set { _OscVoltage = value; }
      }
      public decimal AmpVoltage
      {
          get { return _AmpVoltage; }
          set { _AmpVoltage = value; }
      }
      public decimal OscDelay
      {
          get { return _OscDelay; }
          set { _OscDelay = value; }
      }
      public decimal AmpDelay
      {
          get { return _AmpDelay; }
          set { _AmpDelay = value; }
      }
      public int Qdelay
      {
          get { return _Qdelay; }
          set { _Qdelay = value; }
      }
      public int Qdivision
      {
          get { return _Qdivision; }
          set { _Qdivision = value; }
      }
      public int Prf
      {
          get { return _Prf; }
          set { _Prf = value; }
      }
      public int Qramp
      {
          get { return _Qramp; }
          set { _Qramp = value; }
      }
      public int Vramp
      {
          get { return _Vramp; }
          set { _Vramp = value; }
      }
      public decimal SeederDelay
      {
          get { return _SeederDelay; }
          set { _SeederDelay = value; }
      }
      public decimal SyncDelay
      {
          get { return _SyncDelay; }
          set { _SyncDelay = value; }
      }

      public ulong ShotCount
      {
          get { return _ShotCount; }
          set { _ShotCount = value; }
      }

      public ulong SysShotCount
      {
          get { return _SysShotCount; }
          set { _SysShotCount = value; }
      }

      public int ComPort
      {
          get { return _ComPort; }
          set { _ComPort = value; }
      }
      public int BurstLength
      {
          get { return _BurstLength; }
          set { _BurstLength = value; }
      }


   }


}

