﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.Concurrent;
using System.Data;
using Npgsql;
namespace HGS
    {
    public class Pref
    {
        private static Pref inst;

        private Pref() { }

        public static Pref GetInst()
        {
            if (inst == null)
            {
                inst = new Pref();
            }
            return inst;
        }
        //PostgresSQL-----------
        static string pghost = "192.168.1.109";
        string pgconnString = string.Format("Host={0};Username=postgres;Password=hcm1997;Database=hgs", pghost);
        //SIS------------------
        string sishost = "10.122.18.31";
        int sisport = 8200;
        string sisuser = "sis";
        string sispassword = "openplant";

        //计算点节点名--------------------
        string calcpointnodeName = "CeCalc";
        //--------------------
        public string strOk = "√";
        public string strNo = "×";
        //-----
        const char strCalcVarPfx = 'C';
        const char strSisVarPfx = 'S';
    
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
        public string CalcPointNodeName
        {
            get { return calcpointnodeName; }
        }
        //-------------------------------
        public string GetVarName(point pt)
        {
            switch (pt.pointsrc)
            {
                case pointsrc.sis: return strSisVarPfx + pt.id.ToString();
                case pointsrc.calc: return strCalcVarPfx + pt.id.ToString();
                default: throw new Exception("点计算优先级错误！");
            }
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
