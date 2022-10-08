﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.Concurrent;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using Npgsql;
using System.Text.RegularExpressions;
using CalcEngine;
namespace HGS
    {
    public class Data
    {
        private static Data instance;

        private Data() { }

        public static Data inst()
        {
            if (instance == null)
            {
                instance = new Data();
            }
            return instance;
        }
        //点数
        int NUMPOINTS = 0;
        public int stat_NUMPOINTS
        {
            get { return NUMPOINTS; }
        }
        //点id最在值;
        /*int MAXOFPOINTID = 0;
        int stat_MAXOFPOINTID
        {
            get { return MAXOFPOINTID; }
        }
        */
        //所有点的字典,用于数据库中保存的公式id-varname查找point----------------------------------
        public Dictionary<int, point> cd_Point = new Dictionary<int, point>();

        //sis点列表，用于取得实时值------------------------------------------------------------------

        HashSet<point> hs_sispoint = new HashSet<point>();

        //计算点列表，用于取得计算值------------------------------------------------------------------

        HashSet<point> hs_calcpoint = new HashSet<point>();

        //所点列表，用于进行计算，不使用并发字典的foreach(得到所有锁后才能进行)---------------------

        HashSet<point> hs_allpoint = new HashSet<point>();

        //新加点列表，用于填加到DB------------------------------------------------------------------

        HashSet<point> hs_NewPoint = new HashSet<point>();

        //修改的点列表，用于更新到DB------------------------------------------------------------------

        HashSet<point> hs_ModifyPoint = new HashSet<point>();
        //删除的点列表，用于更新到DB------------------------------------------------------------------

        HashSet<point> hs_DeletePoint = new HashSet<point>();

        //sis id和point id转换字典。------------------------------------

        Dictionary<int, point> dic_sisIdtoPointId = new Dictionary<int, point>();

        //公式错误点。-------------------------------
        HashSet<point> hs_formulaErrorPoint = new HashSet<point>();

        //计算
        CalcEngine.CalcEngine _ce = new CalcEngine.CalcEngine();
        //
        public Dictionary<int, point> dic_SisIdtoPoint
        {
            //set { dic_sisIdtoPointId = value; }
            get { return dic_sisIdtoPointId; }
        }
        
        public HashSet<point> hsAllPoint
        {
            set { hs_allpoint = value; }
            get { return hs_allpoint; }
        }
        public HashSet<point> hsSisPoint
        {
            set { hs_sispoint = value; }
            get { return hs_sispoint; }
        }
        
        public HashSet<point> hsCalcPoint
        {
            set { hs_calcpoint = value; }
            get { return hs_calcpoint; }
        }
        
        public IDictionary<string, object> Variables
        {
            get { return _ce.Variables; }
            //set { _vars = value; }
        }
        /*
        public int GetNextPointID()
        {
            return ++MAXOFPOINTID;
        }*/
        public HashSet<point> hs_FormulaErrorPoint
        {
            set { hs_formulaErrorPoint = value; }
            get { return hs_formulaErrorPoint; }
        }          
        public int GetNextPointId()
        {
            int imax = 0;
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                pgconn.Open();
                string strsql = "select count(*) from point";
                var cmd = new NpgsqlCommand(strsql, pgconn);
                imax = (int)(long)cmd.ExecuteScalar();
                if (imax > 0)
                {
                    strsql = "select max(id) from point";
                    cmd.CommandText = strsql;
                    imax = (int)cmd.ExecuteScalar();
                }
                pgconn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                pgconn.Close();
            }
            return ++imax;
        }
        //展开公式为Sis点-------------------------------------------------
        static HashSet<int> loopvar = new HashSet<int>();
        public string  ExpandOrgFormula_Main(point pt)
        {
            if ( pt.orgformula_main.Length == 0) return "";
            if (loopvar.Contains(pt.id))
            {
                StringBuilder sb = new StringBuilder();
                foreach (int sid in loopvar)
                {
                    point Point = cd_Point[sid];
                    sb.Append(string.Format("[{0}]:{1},",Point.id, Point.ed));
                }
                throw new ArgumentException(sb.Append("循环变量引用！").ToString());
            }
            loopvar.Add(pt.id);
            string orgf = pt.orgformula_main;
            // List<varlinktopoint> lsvpt = VartoPointTable.Sub_PointtoVarList(pt, cellid.main);
            if (pt.lsCalcOrgSubPoint_main != null)
            {
                ExpandOrgFormulaSub(ref orgf, pt.lsCalcOrgSubPoint_main);
            }
            loopvar.Clear();
            //dtTempPoint.Clear();
            return orgf;
        }
        private void ExpandOrgFormulaSub(ref string orgf, List<varlinktopoint> lscosp)
        {
            foreach (varlinktopoint subpt in lscosp)
            {
                point Point = cd_Point[subpt.sub_id];
                if (Point.pointsrc == pointsrc.calc)
                {
                    string rpl = string.Format("({0})", ExpandOrgFormula_Main(Point));
                    string pat = string.Format(@"\b{0}\b(?=[^(])|\b{0}$", subpt.varname);

                    orgf = Regex.Replace(orgf, pat, rpl);
                }
                else
                {
                    string rpl = string.Format("{0}", Pref.Inst().GetVarName(Point));
                    string pat = string.Format(@"\b{0}\b(?=[^(])|\b{0}$", subpt.varname);
                    orgf = Regex.Replace(orgf, pat, rpl);
                }
            }
        }
        //展开HL公式为Sis点-------------------------------------------------
        public string ExpandOrgFormula_HL(point pt)
        {
            if (pt.orgformula_hl.Length == 0) return "";
            string orgf = pt.orgformula_hl;
            //List<varlinktopoint> lsvpt = VartoPointTable.Sub_PointtoVarList(pt, cellid.hl);
            if (pt.lsCalcOrgSubPoint_hl != null)
            {

                ExpandOrgFormulaSub(ref orgf, pt.lsCalcOrgSubPoint_hl);
            }
            return orgf;
        }
        //展开LL公式为Sis点-------------------------------------------------
        public string ExpandOrgFormula_LL(point pt)
        {
            if (pt.orgformula_ll.Length == 0) return "";
            string orgf = pt.orgformula_ll;
            //List<varlinktopoint> lsvpt = VartoPointTable.Sub_PointtoVarList(pt, cellid.ll);
            if (pt.lsCalcOrgSubPoint_ll != null)
            {
                ExpandOrgFormulaSub(ref orgf, pt.lsCalcOrgSubPoint_ll);
            }
            return orgf;
        }
        //展开alarmif公式为Sis点-------------------------------------------------
        public string ExpandOrgFormula_AlarmIf(point pt)
        {
            if (pt.alarmif.Length == 0) return "";
            string orgf = pt.alarmif;
            //List<varlinktopoint> lsvpt = VartoPointTable.Sub_PointtoVarList(pt, cellid.ll);
            if (pt.lsCalcOrgSubPoint_alarmif != null)
            {
                foreach (varlinktopoint subpt in pt.lsCalcOrgSubPoint_alarmif)
                {
                    ExpandOrgFormulaSub(ref orgf, pt.lsCalcOrgSubPoint_alarmif);
                }
            }
            return orgf;
        }
        //------------------------------------------------------------
        static HashSet<int> xloopvar = new HashSet<int>();
        //返回计算点展开成sis点的列表,用于检查循环引用问题。
        public List<point> ExpandOrgPointToSisPoint_Main(point pt)
        {
            if (pt.lsCalcOrgSubPoint_main == null) return null;
            List<point> ExpandPoint = new List<point>();
            if (xloopvar.Contains(pt.id))
            {
                StringBuilder sb = new StringBuilder();
                foreach (int sid in xloopvar)
                {
                    point Point = cd_Point[sid];
                    sb.Append(string.Format("[{0}]:{1},", Point.id,Point.ed));
                }
                throw new ArgumentException(sb.Append("循环变量引用！").ToString());
            }
            xloopvar.Add(pt.id);
            //List<varlinktopoint> lsvpt = VartoPointTable.Sub_PointtoVarList(pt, cellid.main);
            ExpandOrgPointToSisPoinSub(ExpandPoint, pt.lsCalcOrgSubPoint_main);
            xloopvar.Clear();
            return ExpandPoint;
        }
        private void ExpandOrgPointToSisPoinSub(List<point> exppt, List<varlinktopoint> lcosp)
        {
            foreach (varlinktopoint subpt in lcosp)
            {
                point Pointx = cd_Point[subpt.sub_id];
                if (Pointx.pointsrc == pointsrc.calc)
                {
                    //ExpanddPoint.AddRange(ExpandOrgPointToSisPoint_Main(Pointx));
                    List<point> lsexp = ExpandOrgPointToSisPoint_Main(Pointx);
                    if (lsexp != null)
                        exppt.AddRange(lsexp);
                }
                else
                {
                    exppt.Add(Pointx);
                }
            }
        }
        //------------------------------------
        //返回HL公式展开成sis点的列表。
        public List<point> ExpandOrgPointToSisPoint_HL(point pt)
        {
            if (pt.lsCalcOrgSubPoint_hl == null) return null;
            List<point> ExpandPoint = new List<point>();
            //List<varlinktopoint> lsvpt = VartoPointTable.Sub_PointtoVarList(pt, cellid.hl);
            ExpandOrgPointToSisPoinSub(ExpandPoint, pt.lsCalcOrgSubPoint_hl);
            return ExpandPoint;
        }
        //返回LL公式展开成sis点的列表。
        public List<point> ExpandOrgPointToSisPoint_LL(point pt)
        {
            if (pt.lsCalcOrgSubPoint_ll == null) return null;
            List<point> ExpandPoint = new List<point>();
            //List<varlinktopoint> lsvpt = VartoPointTable.Sub_PointtoVarList(pt, cellid.ll);
            ExpandOrgPointToSisPoinSub(ExpandPoint, pt.lsCalcOrgSubPoint_ll);
            return ExpandPoint;
        }
        //返回AlarmIf公式展开成sis点的列表。
        public List<point> ExpandOrgPointToSisPoint_AlarmIf(point pt)
        {
            if (pt.lsCalcOrgSubPoint_alarmif == null) return null;
            List<point> ExpandPoint = new List<point>();
            //List<varlinktopoint> lsvpt = VartoPointTable.Sub_PointtoVarList(pt, cellid.ll);
            ExpandOrgPointToSisPoinSub(ExpandPoint, pt.lsCalcOrgSubPoint_alarmif);
            return ExpandPoint;
        }
        //公式解析---------------------------------
        public void GetCalcSubPoint(point pt)
        {
            if (pt.pointsrc == pointsrc.calc)
            {
                pt.lsCalcOrgSubPoint_main = VartoPointTable.Sub_PointtoVarList(pt, cellid.main);
            }
            //if (pt.pointsrc == pointsrc.sis)
            {
                pt.lsCalcOrgSubPoint_hl = VartoPointTable.Sub_PointtoVarList(pt, cellid.hl);
                pt.lsCalcOrgSubPoint_ll = VartoPointTable.Sub_PointtoVarList(pt, cellid.ll);
                pt.lsCalcOrgSubPoint_alarmif = VartoPointTable.Sub_PointtoVarList(pt, cellid.alarmif);
            }
        }
        public void ParsetoSisPoint(point pt)
        {
            if (pt.pointsrc == pointsrc.calc)
            {
                pt.listSisCalaExpPointID_main = ExpandOrgPointToSisPoint_Main(pt);
            }
            //if (pt.pointsrc == pointsrc.sis)
            {
                if (pt.lsCalcOrgSubPoint_hl != null ) pt.listSisCalaExpPointID_hl = ExpandOrgPointToSisPoint_HL(pt);
                if (pt.lsCalcOrgSubPoint_ll != null) pt.listSisCalaExpPointID_ll = ExpandOrgPointToSisPoint_LL(pt);
                if (pt.lsCalcOrgSubPoint_alarmif != null) pt.listSisCalaExpPointID_alarmif = ExpandOrgPointToSisPoint_AlarmIf(pt);
            }
        }
        public void ParseFormula(point pt)
        {
            pt.expression_main = null;
            pt.expression_hl = null;
            pt.expression_ll = null;
            if (pt.pointsrc == pointsrc.calc)
            {
                if (pt.orgformula_main.Length > 0)
                    pt.expression_main = _ce.Parse(ExpandOrgFormula_Main(pt));
            }
            //if (pt.pointsrc == pointsrc.sis)
            {
                if (pt.orgformula_hl.Length > 0)
                {
                    pt.expression_hl = _ce.Parse(ExpandOrgFormula_HL(pt));
                }
                if (pt.orgformula_ll.Length > 0)
                {
                    pt.expression_ll = _ce.Parse(ExpandOrgFormula_LL(pt));
                }
                if (pt.alarmif.Length > 0)
                {
                    pt.expression_alarmif = _ce.Parse(ExpandOrgFormula_AlarmIf(pt));
                }
            }
        }
        //-------------------------------
        private void LoadData()
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            point flagpt = new point();
            try
            {
                pgconn.Open();
                string strsql = "select * from point order by id";
                var cmd = new NpgsqlCommand(strsql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();

                while (pgreader.Read())
                {
                    point Point = new point();
                    flagpt = Point;
                    Point.id = (int)pgreader["id"];
                    Point.nd = pgreader["nd"].ToString();
                    Point.pn = pgreader["pn"].ToString();
                    Point.eu = pgreader["eu"].ToString();
                    Point.ed = pgreader["ed"].ToString();

                    if (pgreader["tv"] == DBNull.Value)
                        Point.tv = null;
                    else
                        Point.tv = (double)pgreader["tv"];

                    if (pgreader["bv"] == DBNull.Value)
                        Point.bv = null;
                    else
                        Point.bv = (double)pgreader["bv"];

                    if (pgreader["ll"] == DBNull.Value)
                        Point.ll = null;
                    else
                        Point.ll = (double)pgreader["ll"];

                    if (pgreader["hl"] == DBNull.Value)
                        Point.hl = null;
                    else
                        Point.hl = (double)pgreader["hl"];

                    if (pgreader["zl"] == DBNull.Value)
                        Point.zl = null;
                    else
                        Point.zl = (double)pgreader["zl"];
                    if (pgreader["zh"] == DBNull.Value)
                        Point.zh = null;
                    else
                        Point.zh = (double)pgreader["zh"];
                    //
                    if (pgreader["skip_pp"] == DBNull.Value)
                        Point.skip_pp = null;
                    else
                        Point.skip_pp = (double)pgreader["skip_pp"];

                    Point.id_sis = (int)pgreader["id_sis"];

                    //object oo = (int)pgreader["pointsrc"];
                    Point.pointsrc = (pointsrc)(short)pgreader["pointsrc"];
                    Point.ownerid = (int)pgreader["ownerid"];
                    Point.orgformula_main = pgreader["orgformula_main"].ToString();
                    //Point.expformula_main = pgreader["expformula"].ToString();
                    Point.isavalarm = (bool)pgreader["isavalarm"];
                    Point.iscalc = (bool)pgreader["iscalc"];
                    Point.fm = (short)pgreader["fm"];
                    Point.isboolvalarm = (bool)pgreader["isboolv"];
                    Point.boolalarminfo = pgreader["boolalarminfo"].ToString();
                    Point.orgformula_hl = pgreader["orgformula_hl"].ToString();
                    Point.orgformula_ll = pgreader["orgformula_ll"].ToString();
                    Point.alarmif = pgreader["alarmif"].ToString();
                    Point.boolalarmif = (bool)pgreader["boolalarmif"];
                    //
                    Point.isalarmskip = (bool)pgreader["isalarmskip"];
                    Point.isalarmwave = (bool)pgreader["isalarmwave"];
                    if ((Point.isalarmwave || Point.isalarmskip) && Point.skip_pp != null)
                        Point.skipCheck = new SkipCheck();
                    //

                    //Point.lsCalcOrgSubPoint_main = VartoPointTable.Sub_PointtoVarList(Point,cellid.main);

                    cd_Point[Point.id] = Point;
                    hs_allpoint.Add(Point);
                    if (Point.pointsrc == pointsrc.sis)
                    {
                        hs_sispoint.Add(Point);
                        dic_sisIdtoPointId.Add(Point.id_sis, Point);
                        _ce.Variables.Add(Pref.Inst().GetVarName(Point), Point.Av);
                    }
                    else
                    {
                        hs_calcpoint.Add(Point);
                    }
                }
                ///展开计算点到sis点。
                foreach (point v in hsAllPoint)
                {
                    flagpt = v;
                    GetCalcSubPoint(v);
                   
                }
                foreach (point v in hsAllPoint)
                {
                    flagpt = v;
                    ParsetoSisPoint(v);
                    ParseFormula(v);
                }
            }
            catch(Exception e)  { throw new Exception(string.Format("装入点id={0}:{1}时发生错误！",flagpt.id,flagpt.ed),e); }
            finally { pgconn.Close(); }
        }
        public void LoadFromPG()
        {
            hs_allpoint.Clear();
            hs_ModifyPoint.Clear();
            hs_NewPoint.Clear();
            hs_sispoint.Clear();
            hs_calcpoint.Clear();
            dic_sisIdtoPointId.Clear();
            cd_Point.Clear();
            //GetPointsStat();
            VartoPointTable.Refresh(true); 
            LoadData();
        }
        /*
        private string dtoNULL(double? d)
        {
            return d.HasValue ? d.ToString() : "NULL";
        }*/
        private void GetinsertsubSql(StringBuilder sb, List<varlinktopoint> lssub,point pt,cellid CellID)
        {
            if (lssub is null) return;
            foreach (varlinktopoint supt in lssub)
            {
                sb.AppendLine(string.Format("insert into formula_point (id,pointid,varname,cellid) values({0},{1},'{2}',{3});",
                    pt.id, supt.sub_id, supt.varname, (int)CellID));              
            }
        }
        private void GetinsertsubSql(StringBuilder sb, point pt)
        {
            //if (pt.pointsrc == pointsrc.calc)
            {
                GetinsertsubSql(sb, pt.lsCalcOrgSubPoint_main, pt,cellid.main);
            }
            //else 
            {
                //if (pt.lsCalcOrgSubPoint_hl.Count > 0)
                {
                    GetinsertsubSql(sb, pt.lsCalcOrgSubPoint_hl, pt,cellid.hl);
                }
                //if (pt.lsCalcOrgSubPoint_ll.Count > 0)
                {
                    GetinsertsubSql(sb, pt.lsCalcOrgSubPoint_ll, pt,cellid.ll);
                }
                //
                {
                    GetinsertsubSql(sb, pt.lsCalcOrgSubPoint_alarmif, pt, cellid.alarmif);
                }
            }
        }
        public void SavetoPG()
        {
            StringBuilder sb = new StringBuilder();
           
            foreach (point pt in hs_ModifyPoint)
            {
                pt.listSisCalaExpPointID_main = ExpandOrgPointToSisPoint_Main(pt);

                sb.AppendLine(string.Format(@"update point set tv={0},bv={1},ll={2},hl={3},zl={4},zh={5},mt='{6}',eu='{7}',"+
                                         "pn='{8}',orgformula_main='{9}',fm={10},iscalc = {11}," +
                                        "isavalarm = {12},ed = '{13}',isboolv = {14},boolalarminfo = '{15}'," +
                                                   " orgformula_hl = '{16}',orgformula_ll = '{17}',alarmif = '{18}' ,boolalarmif = {19} ,isalarmskip = {20},isalarmwave = {21}," +
                                                   "skip_pp = {22} where id = {23};",
                                        Functions.dtoNULL(pt.tv), Functions.dtoNULL(pt.bv), Functions.dtoNULL(pt.ll), Functions.dtoNULL(pt.hl),
                                        Functions.dtoNULL(pt.zl), Functions.dtoNULL(pt.zh),
                                        DateTime.Now,pt.eu, pt.pn, pt.orgformula_main,pt.fm,
                                        pt.iscalc,pt.isavalarm, pt.ed,pt.isboolvalarm,pt.boolalarminfo, pt.orgformula_hl,
                                        pt.orgformula_ll,pt.alarmif,pt.boolalarmif,pt.isalarmskip,pt.isalarmwave, Functions.dtoNULL(pt.skip_pp), pt.id));
                sb.AppendLine(string.Format("delete  from formula_point where id = {0};", pt.id));
                GetinsertsubSql(sb, pt);
            }
            foreach (point pt in hs_NewPoint)
            {
                pt.listSisCalaExpPointID_main = ExpandOrgPointToSisPoint_Main(pt);

                sb.AppendLine(string.Format(@"insert into point (id,nd,pn,ed,eu,tv,bv,ll,hl,zl,"+
                                            "zh,id_sis,pointsrc,mt,ownerid,orgformula_main,fm,iscalc,isavalarm,isboolv,boolalarminfo," +
                                            "orgformula_hl,orgformula_ll,alarmif,boolalarmif,isalarmskip,isalarmwave,skip_pp) " + 
                                    "values ({0},'{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9},"+
                                            "{10},{11},{12},'{13}',{14},'{15}',{16},{17},{18},{19},'{20}','{21}','{22}','{23}',{24},{25},{26},{27});",
                                    pt.id, pt.nd, pt.pn, pt.ed, pt.eu, Functions.dtoNULL(pt.tv), Functions.dtoNULL(pt.bv), 
                                    Functions.dtoNULL(pt.ll), Functions.dtoNULL(pt.hl), Functions.dtoNULL(pt.zl),
                                    Functions.dtoNULL(pt.zh), pt.id_sis,(int)pt.pointsrc, DateTime.Now, Auth.GetInst().LoginID, pt.orgformula_main,
                                    pt.fm,pt.iscalc,pt.isavalarm,pt.isboolvalarm,pt.boolalarminfo, pt.orgformula_hl, pt.orgformula_ll,pt.alarmif,
                                    pt.boolalarmif,pt.isalarmskip,pt.isalarmwave, Functions.dtoNULL(pt.skip_pp)));
                GetinsertsubSql(sb, pt);
               // pt.id = ptid;
               // ptid++;
            }
            foreach(point pt in hs_DeletePoint)
            {
                sb.AppendLine(string.Format("delete  from point where id = {0};", pt.id));
            }
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            point flagpt = new point();
            try
            {
                if (sb.Length < 10) return;
                var cmd = new NpgsqlCommand(sb.ToString(), pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
                pgconn.Close();
                //
                VartoPointTable.Refresh(true);
                foreach (point pt in hs_NewPoint)
                {
                    flagpt = pt;
                    cd_Point[pt.id] = pt;
                    hs_allpoint.Add(pt);
                    GetCalcSubPoint(pt);
                    ParsetoSisPoint(pt);
                    ParseFormula(pt);
                    if (pt.pointsrc == pointsrc.sis)
                    {
                        hs_sispoint.Add(pt);
                        dic_sisIdtoPointId.Add(pt.id_sis, pt);
                    }
                    else
                    {
                        hs_calcpoint.Add(pt);
                    }
                    //ParsePoint(pt);
                    //ParseFormula(pt);
                }
                foreach (point pt in hs_DeletePoint)
                {
                    flagpt = pt;
                    cd_Point.Remove(pt.id);
                    hs_allpoint.Remove(pt);
                    hs_calcpoint.Remove(pt);
                    if (pt.pointsrc == pointsrc.sis)
                    {
                        hs_sispoint.Remove(pt);
                        dic_sisIdtoPointId.Remove(pt.id_sis);
                    }
                    AlarmSet.GetInst().ssAlarmPoint.Remove(pt);
                }
                foreach (point pt in hs_ModifyPoint)
                {
                    flagpt = pt;
                    GetCalcSubPoint(pt);
                    ParsetoSisPoint(pt);
                    ParseFormula(pt);
                }
                hs_NewPoint.Clear();
                hs_ModifyPoint.Clear();
                hs_DeletePoint.Clear();
                //重装子点表
                VartoPointTable.Refresh(true);
            }
            catch (Exception e) { throw new Exception(string.Format("保存点id={0}:{1}时发生错误！", flagpt.id, flagpt.ed), e); }
            finally
            {
                //MAXOFPOINTID--;
                pgconn.Close();
            }
        }
        public void Add(point pt)
        {
            if (hs_ModifyPoint.Contains(pt))
                throw new Exception("修改点中也包括此点！");
            hs_NewPoint.Add(pt);
        }
        public void Delete(point pt)
        {
            hs_DeletePoint.Add(pt);//有问题?????????????
        }
        public void DeleteClear()
        {
            hs_DeletePoint.Clear();
        }
        public void Update(point pt)
        {
            if (hs_NewPoint.Contains(pt))
                throw new Exception("新加点中也包括此点！");
            hs_ModifyPoint.Add(pt);
        }     
    }
    
}
