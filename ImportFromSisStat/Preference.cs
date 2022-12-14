using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.Concurrent;
using System.Data;
using Npgsql;
namespace ImportFromSisStat
{
    public class Pref
    {
        private static Pref instance;

        private Pref() { }

        public static Pref Inst()
        {
            if (instance == null)
            {
                instance = new Pref();
            }
            return instance;
        }
        //PostgresSQL-----------
        static string pghost = "192.168.1.109";
        string pgconnString = string.Format("Host={0};Username=postgres;Password=hcm1997;Database=hgs", pghost);
        //SIS------------------
        string sishost = "10.122.18.31";
        int sisport = 8200;
        string sisuser = "sis";
        string sispassword = "openplant";

        //计算错误时的结果值
        //public double errorvalue = -1;
        pointsrc Pointsrc = pointsrc.sis;
        //-------------------------
        public string sisHost
        {
            set { sishost = value; }
            get { return sishost; }
        }
        //---------------------
        public string pgConnString
        {
            set { pgconnString = value; }
            get { return pgconnString; }
        }
        public int sisPort
        {
            set { sisport = value; }
            get { return sisport; }
        }
        public string sisUser
        {
            set { sisuser = value; }
            get { return sisuser; }
        }
        public string sisPassword
        {
            set { sispassword = value; }
            get { return sispassword; }
        }
        //------------------------
        
        public pointsrc PointSrc
        {
            set { Pointsrc = value; }
            get { return Pointsrc; }
        }
        //---------------------------
        public void save()
        {
            XmlSettings seting = new XmlSettings();
            seting.SetValue(cnstXml.pgHost, pghost);
            seting.Save(cnstXml.setfilename);
        }
        public void load()
        {
            XmlSettings seting = new XmlSettings();
            seting.Load(cnstXml.setfilename);
            pghost = seting.GetValue<string>(cnstXml.pgHost);
            sishost = seting.GetValue<string>(cnstXml.sisHost);
            sisport = seting.GetValue<int>(cnstXml.sisPort);
        }
    }
    
}
