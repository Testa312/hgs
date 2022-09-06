using System;
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
        int MAXOFPOINTID = 0;
        int stat_MAXOFPOINTID
        {
            get { return MAXOFPOINTID; }
        }
        //所有点的字典。----------------------------------
        public ConcurrentDictionary<int, point> cd_Point { set; get; }

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

        Dictionary<int, int> dic_sisIdtoPointId = new Dictionary<int, int>();

        //公式错误点。-------------------------------
        HashSet<point> hs_formulaErrorPoint = new HashSet<point>();

        //计算
        CalcEngine.CalcEngine _ce = new CalcEngine.CalcEngine();
        //
        public Dictionary<int, int> dic_SisIdtoPointId
        {
            //set { dic_sisIdtoPointId = value; }
            get { return dic_sisIdtoPointId; }
        }
        //-------------------------------------临时表

        DataTable dtTempPoint = new DataTable();
        //-----------------------------------------------
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
        public int GetNextPointID()
        {
            return ++MAXOFPOINTID;
        }
        public HashSet<point> hs_FormulaErrorPoint
        {
            set { hs_formulaErrorPoint = value; }
            get { return hs_formulaErrorPoint; }
        }
        //取得计算点的相关点列表。
        public List<int> GetDeletePointIdList(int pointid)
        {
            List<int> lsptid = new List<int>();
            string strexp = string.Format("pointid={0}", pointid);
            DataRow[] frow = dtTempPoint.Select(strexp);
            foreach (DataRow dr in frow)
            {
                lsptid.Add((int)dr["id"]);
            }
            return lsptid;
        }
        //取得计算点的相关点列表。
        private List<subpoint> GetSubPointList(point pt)
        {
            if (pt.pointsrc != pointsrc.calc) return null;
            List<subpoint> lssubpt = new List<subpoint>();
            string strexp = string.Format("id={0}", pt.id);
            DataRow[] frow = dtTempPoint.Select(strexp);
            foreach (DataRow dr in frow)
            {
                subpoint subpt = new subpoint();
                subpt.id = (int)dr["pointid"];
                subpt.varname = dr["varname"].ToString();
                //subpt.PointSrc = (pointsrc)(short)dr["pointsrc"];
                lssubpt.Add(subpt);
            }
            return lssubpt;
        }
        private void GetPointsStat()
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                pgconn.Open();
                string strsql = "select count(*) from point";
                var cmd = new NpgsqlCommand(strsql, pgconn);
                NUMPOINTS = (int)(long)cmd.ExecuteScalar();
                if (NUMPOINTS > 0)
                {
                    strsql = "select max(id) from point";
                    cmd.CommandText = strsql;
                    MAXOFPOINTID = (int)cmd.ExecuteScalar();
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
        }
        private void Initcd_dicCapacity()
        {
            //-------------------------------------------------------------
            //点字典,用于计算
            // We know how many items we want to insert into the ConcurrentDictionary.
            // So set the initial capacity to some prime number above that, to ensure that
            // the ConcurrentDictionary does not need to be resized while initializing it.
            //static int NUMITEMS = 20000;
            int initialCapacity = Functions.inst().AbovePrimes(NUMPOINTS);//素数
            initialCapacity = initialCapacity > 1009 ? initialCapacity : 1009;

            // The higher the concurrencyLevel, the higher the theoretical number of operations
            // that could be performed concurrently on the ConcurrentDictionary.  However, global
            // operations like resizing the dictionary take longer as the concurrencyLevel rises.
            // For the purposes of this example, we'll compromise at numCores * 2.
            int numProcs = Environment.ProcessorCount;
            int concurrencyLevel = numProcs * 2;
            cd_Point = new ConcurrentDictionary<int, point>(concurrencyLevel, initialCapacity);
            //         
        }
        static HashSet<int> loopvar = new HashSet<int>();
        public string  ExpandOrgFormula(point pt)
        {
            //Debug.Assert(pt.pointsrc == pointsrc.calc);
            if (pt.pointsrc != pointsrc.calc || pt.orgformula.Length == 0) return "";

            //if(loopvar.Count == 0) LoadSubPointTabel();

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
            string orgf = pt.orgformula;
            Dictionary<string, int> Var = new Dictionary<string, int>();
            foreach (subpoint subpt in pt.lsCalcOrgSubPoint)
            {
                point Point = cd_Point[subpt.id];
                if(Point.pointsrc == pointsrc.calc)
                {
                    string rpl = string.Format("({0})",ExpandOrgFormula(Point));
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
            loopvar.Clear();
            //dtTempPoint.Clear();
            return orgf;
        }
        private void LoadSubPointTable()
        {
            //得到关连的参与计算点。
            string strsql = "select point.id,formula_point.pointid,varname from point,formula_point where point.id = formula_point.id";
            NpgsqlDataAdapter daPoint = new NpgsqlDataAdapter(strsql, Pref.Inst().pgConnString);
            dtTempPoint.Clear();//清空
            daPoint.Fill(dtTempPoint);
        }
        
        static HashSet<int> xloopvar = new HashSet<int>();
        //返回计算点展开成sis点的列表,用于检查循环引用问题。
        public List<point> ExpandOrgPointToSisPoint(point pt)
        {
            if (pt.pointsrc != pointsrc.calc) return null;
            List<point> ExpanddPoint = new List<point>();
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
            //point Point = cd_Point[id];
            foreach (subpoint subpt in pt.lsCalcOrgSubPoint)
            {
                point Pointx = cd_Point[subpt.id];
                if (Pointx.pointsrc == pointsrc.calc)
                {
                    ExpanddPoint.AddRange(ExpandOrgPointToSisPoint(Pointx));
                }
                else
                {
                    ExpanddPoint.Add(Pointx);
                }
            }
            xloopvar.Clear();
            return ExpanddPoint;
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
 
                    Point.id_sis = (int)pgreader["id_sis"];

                    //object oo = (int)pgreader["pointsrc"];
                    Point.pointsrc = (pointsrc)(short)pgreader["pointsrc"];
                    Point.ownerid = (int)pgreader["ownerid"];
                    Point.orgformula = pgreader["orgformula"].ToString();
                    Point.expformula = pgreader["expformula"].ToString();
                    Point.isavalarm = (bool)pgreader["isavalarm"];
                    Point.iscalc = (bool)pgreader["iscalc"];
                    Point.fm = (short)pgreader["fm"];
                    Point.isboolv = (bool)pgreader["isboolv"];
                    Point.boolalarminfo = pgreader["boolalarminfo"].ToString();
                    //

                    Point.lsCalcOrgSubPoint = GetSubPointList(Point);

                    cd_Point[Point.id] = Point;
                    hs_allpoint.Add(Point);
                    if (Point.pointsrc == pointsrc.sis)
                    {
                        hs_sispoint.Add(Point);
                        dic_sisIdtoPointId.Add(Point.id_sis, Point.id);
                        _ce.Variables.Add(Pref.Inst().GetVarName(Point), Point.av);
                    }
                    else
                    {
                        hs_calcpoint.Add(Point);
                    }
                }
                ///展开计算点到sis点。
                foreach (point v in hs_calcpoint)
                {
                    flagpt = v;
                    v.expformula = ExpandOrgFormula(v);
                    v.listSisCalaExpPointID = ExpandOrgPointToSisPoint(v);
                    if(v.pointsrc == pointsrc.calc && v.expformula.Length > 0)
                        v.expression = _ce.Parse(v.expformula);
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
            if(cd_Point != null) cd_Point.Clear();
            GetPointsStat();
            Initcd_dicCapacity();
            LoadSubPointTable();
            LoadData();
        }
        private string dtoNULL(double? d)
        {
            return d.HasValue ? d.ToString() : "NULL";
        }
        public void SavetoPG()
        {
            StringBuilder sb = new StringBuilder();
            foreach (point pt in hs_ModifyPoint)
            {
                //pt.expformula = ExpandOrgFormula(pt);
                pt.listSisCalaExpPointID = ExpandOrgPointToSisPoint(pt);

                sb.AppendLine(string.Format(@"update point set tv={0},bv={1},ll={2},hl={3},zl={4},zh={5},mt='{6}',eu='{7}',"+
                                                   "ownerid={8},orgformula='{9}',fm={10},iscalc = {11}," +
                                                   "isavalarm = {12},ed = '{13}',isboolv = {14},boolalarminfo = '{15}' where id = {16};",
                                                dtoNULL(pt.tv), dtoNULL(pt.bv), dtoNULL(pt.ll), dtoNULL(pt.hl), dtoNULL(pt.zl), dtoNULL(pt.zh),
                                                DateTime.Now,pt.eu, pt.ownerid, pt.orgformula,pt.fm,
                                                pt.iscalc,pt.isavalarm, pt.ed,pt.isboolv,pt.boolalarminfo,pt.id)); 
                if (pt.pointsrc == pointsrc.calc && pt.lsCalcOrgSubPoint.Count > 0)
                {
                    sb.AppendLine(string.Format("delete  from formula_point where id = {0};", pt.id));
                    foreach (subpoint supt in pt.lsCalcOrgSubPoint)
                    {
                        sb.AppendLine(string.Format("insert into formula_point (id,pointid,varname) values({0},{1},'{2}');", pt.id, supt.id,supt.varname));
                    }
                }
            }
            foreach (point pt in hs_NewPoint)
            {
                //pt.expformula = ExpandOrgFormula(pt);
                pt.listSisCalaExpPointID = ExpandOrgPointToSisPoint(pt);

                sb.AppendLine(string.Format(@"insert into point (id,nd,pn,ed,eu,tv,bv,ll,hl,zl,"+
                                            "zh,id_sis,pointsrc,mt,ownerid,orgformula,fm,iscalc,isavalarm,isboolv,boolalarminfo) "+ 
                                    "values ({0},'{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9},"+
                                            "{10},{11},{12},'{13}',{14},'{15}',{16},{17},{18},{19},'{20}');",
                                    pt.id, pt.nd, pt.pn, pt.ed, pt.eu, dtoNULL(pt.tv), dtoNULL(pt.bv), dtoNULL(pt.ll),dtoNULL(pt.hl), dtoNULL(pt.zl), 
                                    dtoNULL(pt.zh), pt.id_sis,(int)pt.pointsrc, DateTime.Now, Auth.GetInst().LoginID, pt.orgformula,
                                    pt.fm,pt.iscalc,pt.isavalarm,pt.isboolv,pt.boolalarminfo));
                if (pt.pointsrc == pointsrc.calc && pt.lsCalcOrgSubPoint.Count > 0)
                {
                    //sb.AppendLine(string.Format("delete  from formula_point where id = {0};", MAXOFPOINTID));//????????????????
                    foreach (subpoint subpt in pt.lsCalcOrgSubPoint)
                    {
                        sb.AppendLine(string.Format("insert into formula_point (id,pointid,varname) values({0},{1},'{2}');", pt.id, subpt.id,subpt.varname));
                    }
                }
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
                foreach (point pt in hs_NewPoint)
                {
                    flagpt = pt;
                    cd_Point[pt.id] = pt;
                    hs_allpoint.Add(pt);
                    if (pt.pointsrc == pointsrc.sis)
                    {
                        hs_sispoint.Add(pt);
                        dic_sisIdtoPointId.Add(pt.id_sis, pt.id);
                    }
                    else
                    {
                        pt.expression = pt.expformula.Length > 0 ? _ce.Parse(pt.expformula) : new Expression();
                        hs_calcpoint.Add(pt);
                    }
                }
                foreach (point pt in hs_DeletePoint)
                {
                    flagpt = pt;
                    point rpt;
                    cd_Point.TryRemove(pt.id, out rpt);//???????????????
                    hs_allpoint.Remove(pt);
                    //hs_calcpoint.Remove(pt);
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
                    pt.expression = pt.expformula.Length > 0 ? _ce.Parse(pt.expformula) : new Expression() ;
                }
                hs_NewPoint.Clear();
                hs_ModifyPoint.Clear();
                hs_DeletePoint.Clear();
                //重装子点表
                LoadSubPointTable();
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
        public void Update(point pt)
        {
            if (hs_NewPoint.Contains(pt))
                throw new Exception("新加点中也包括此点！");
            hs_ModifyPoint.Add(pt);
        }     
    }
    
}
