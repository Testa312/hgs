using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.Concurrent;
using System.Data;
using System.Windows.Forms;
using Npgsql;
namespace HGS
    {
    public class Data
    {
        private static Data instance;

        private Data() { }

        public static Data Get()
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
        //最大点id;
        int MAXOFPOINTID = 0;
        public int stat_MAXOFPOINTID
        {
            get { return MAXOFPOINTID; }
        }
        //所有点的字典。----------------------------------
        public ConcurrentDictionary<int, point> cd_Point { set; get; }

        //sis点列表，用于取得实时值------------------------------------------------------------------

        List<int> lssispoint = new List<int>();

        //所点列表，用于进行计算，不使用并发字典的foreach(得到所有锁后才能进行)---------------------

        List<int> lsallpoint = new List<int>();

        //新加点列表，用于填加到DB------------------------------------------------------------------

        List<point> lsNewPoint = new List<point>();

        //修改的点列表，用于更新到DB------------------------------------------------------------------

        List<point> lsModifyPoint = new List<point>();
        //删除的点列表，用于更新到DB------------------------------------------------------------------

        List<point> lsDeletePoint = new List<point>();

        //sis id和point id转换字典。------------------------------------

        static Dictionary<int, int> dic_sisIdtoPointId = new Dictionary<int, int>();
        public Dictionary<int, int> dic_SisIdtoPointId
        {
            //set { dic_sisIdtoPointId = value; }
            get { return dic_sisIdtoPointId; }
        }
        //-------------------------------------临时表

        DataTable dtPoint = new DataTable();
        //-----------------------------------------------
        public List<int> lsAllPoint
        {
            set { lsallpoint = value; }
            get { return lsallpoint; }
        }

        //取得计算点的相关点列表。
        private List<int> GetSubPoint(int pid)
        {
            List<int> lsid = new List<int>();
            string strexp = string.Format("id={0}", pid);
            DataRow[] frow = dtPoint.Select(strexp);
            foreach (DataRow dr in frow)
            {
                lsid.Add((int)dr["pointid"]);
            }
            return lsid;
        }
        //返回大于n的素数。
        private  int AbovePrimes(int n)
        {
            int i = (n % 2) == 0 ? ++n : n + 2;
            int j = 0;
            for (; i <= 100003; i = i + 2)
            {
                int k = (int)Math.Sqrt(i);
                for (j = 2; j <= k; j++)
                {
                    if ((i % j) == 0)
                    {
                        break;
                    }
                }

                if (j > k)
                {
                    return i;
                }
            }
            //最大100003
            return 100003;
        }
        private void GetPointsStat()
        {
            var pgconn = new NpgsqlConnection(Pref.GetInstance().pgConnString);
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
            int initialCapacity = AbovePrimes(NUMPOINTS);//素数
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
        private void LoadSubPointTabel()
        {
            //得到关连的参与计算点。
            string strsql = "select point.id,formula_point.pointid from point,formula_point where point.id = formula_point.id";
            NpgsqlDataAdapter daPoint = new NpgsqlDataAdapter(strsql, Pref.GetInstance().pgConnString);
            dtPoint.Clear();//清空
            daPoint.Fill(dtPoint);
        }
        static HashSet<int> loopvar = new HashSet<int>();
        //返回计算点展开成sis点的列表。
        private List<int> ExpandOrgPointToSisPoint(int id)
        {
            List<int> ExtendPoint = new List<int>();
            if (loopvar.Contains(id))
            {
                StringBuilder sb = new StringBuilder();
                foreach (int sid in loopvar)
                    sb.Append(string.Format("{0},", cd_Point[sid].ed));
                throw new ArgumentException(sb.Append("循环变量引用！").ToString());
            }
            loopvar.Add(id);
            point Point = cd_Point[id];
            foreach (int sid in Point.lsOrgCalcPointID)
            {
                point Pointx = cd_Point[sid];
                if (Pointx.pointsrc == pointsrc.sis)
                {
                    ExtendPoint.Add(Pointx.id);
                }
                else
                    ExtendPoint.AddRange(ExpandOrgPointToSisPoint(Pointx.id));

            }
            loopvar.Clear();
            return ExtendPoint;
        }
        //-------------------------------
        private void LoadData()
        {
            var pgconn = new NpgsqlConnection(Pref.GetInstance().pgConnString);
            try
            {
                pgconn.Open();
                string strsql = "select * from point";
                var cmd = new NpgsqlCommand(strsql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();

                while (pgreader.Read())
                {
                    point Point = new point();
                    Point.id = (int)pgreader["id"];
                    Point.nd = pgreader["nd"].ToString();
                    Point.pn = pgreader["pn"].ToString();
                    Point.eu = pgreader["eu"].ToString();
                    Point.ed = pgreader["ed"].ToString();
                    Point.tv = (double)pgreader["tv"];
                    Point.bv = (double)pgreader["bv"];
                    Point.ll = (double)pgreader["ll"];
                    Point.hl = (double)pgreader["hl"];
                    Point.zl = (double)pgreader["zl"];
                    Point.zh = (double)pgreader["zh"];
                    Point.id_sis = (int)pgreader["id_sis"];
                    Point.pointsrc = (pointsrc)pgreader["pointsrc"];
                    Point.ownerid = (int)pgreader["ownerid"];
                    Point.formula = pgreader["formula"].ToString();
                    //
                    if (Point.pointsrc == pointsrc.calc)
                    {
                        Point.lsOrgCalcPointID = GetSubPoint(Point.id);
                    }
                    cd_Point[Point.id] = Point;
                }
                ///展开计算点到sis点。
                foreach (point v in cd_Point.Values)
                {
                    if (v.pointsrc == pointsrc.calc)
                    {
                        v.listSisCalaPointID = ExpandOrgPointToSisPoint(v.id);
                    }
                }
            }
            catch (Exception) { throw; }
            finally { pgconn.Close(); }
        }
        public void LoadFromPG()
        {
            lsallpoint.Clear();
            lsModifyPoint.Clear();
            lsNewPoint.Clear();
            lssispoint.Clear();
            dic_sisIdtoPointId.Clear();
            if(cd_Point != null) cd_Point.Clear();
            GetPointsStat();
            Initcd_dicCapacity();
            LoadSubPointTabel();
            LoadData();
            dtPoint.Clear();//清空,省内存
        }
        private string dtoNULL(double? d)
        {
            return d.HasValue ? d.ToString() : "NULL";
        }
        public void SavetoPG()
        {
            StringBuilder sb = new StringBuilder();
            foreach (point pt in lsModifyPoint)
            {

                sb.AppendLine(string.Format(@"update point set tv={0},bv={1},ll={2},hl={3},zl={4},zh={5},mt='{6}',eu='{7}',ownerid={8},formula='{9}';" +
                                        "where id = {10}", dtoNULL(pt.tv), dtoNULL(pt.bv), dtoNULL(pt.ll), dtoNULL(pt.hl), dtoNULL(pt.zl),
                                        DateTime.UtcNow, pt.eu, pt.ownerid, pt.formula, pt.id));
                if (pt.pointsrc == pointsrc.calc && pt.lsOrgCalcPointID.Count > 0)
                {
                    sb.AppendLine(string.Format("delete  from formula_point where id = {0};", pt.id));
                    foreach (int id in pt.lsOrgCalcPointID)
                    {
                        sb.AppendLine(string.Format("insert into formula_point (id,pointid) values({0},{1});", pt.id, id));
                    }
                }
            }
            foreach (point pt in lsNewPoint)
            {
                sb.AppendLine(string.Format(@"insert into point (id,nd,pn,ed,eu,tv,bv,ll,hl,zl,zh,id_sis,pointsrc,mt,ownerid,formula)" +
                                    "values ({0},'{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12},'{13}',{14},'{15}');",
                                    ++MAXOFPOINTID, pt.nd, pt.pn, pt.ed, pt.eu, dtoNULL(pt.tv), dtoNULL(pt.bv), dtoNULL(pt.ll),
                                    dtoNULL(pt.hl), dtoNULL(pt.zl), dtoNULL(pt.zh), pt.id_sis,
                                    (int)pt.pointsrc, DateTime.UtcNow, Pref.GetInstance().Owner, pt.formula));
                if (pt.lsOrgCalcPointID.Count > 0)
                {
                    //sb.AppendLine(string.Format("delete  from formula_point where id = {0};", MAXOFPOINTID));//????????????????
                    foreach (int id in pt.lsOrgCalcPointID)
                    {
                        sb.AppendLine(string.Format("insert into formula_point (id,pointid) values({0},{1});", MAXOFPOINTID, id));
                    }
                }
            }
            foreach(point pt in lsDeletePoint)
            {
                sb.AppendLine(string.Format("delete  from formula_point where id = {0};", pt.id));
            }
            var pgconn = new NpgsqlConnection(Pref.GetInstance().pgConnString);
            try
            {
                if (sb.Length < 10) return;
                var cmd = new NpgsqlCommand(sb.ToString(), pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
                pgconn.Close();
                foreach (point pt in lsNewPoint)
                {
                    cd_Point[pt.id] = pt;
                    lsallpoint.Add(pt.id);
                    if (pt.pointsrc == pointsrc.sis)
                    {
                        lssispoint.Add(pt.id);
                        dic_sisIdtoPointId.Add(pt.id_sis, pt.id);
                    }
                }
                foreach (point pt in lsDeletePoint)
                {
                    point  rpt;
                    cd_Point.TryRemove(pt.id ,out rpt);//???????????????
                    lsallpoint.Remove(pt.id);
                    if (pt.pointsrc == pointsrc.sis)
                    {
                        lssispoint.Remove(pt.id);
                        dic_sisIdtoPointId.Remove(pt.id_sis);
                    }
                    }
                lsNewPoint.Clear();
                lsModifyPoint.Clear();              
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                MAXOFPOINTID--;
                pgconn.Close();
            }
        }
        public void Add(point pt)
        {
            foreach (int id in pt.lsOrgCalcPointID)
            {
                if (cd_Point[id].pointsrc == pointsrc.calc)
                    pt.listSisCalaPointID.AddRange(ExpandOrgPointToSisPoint(id));
                else
                    pt.listSisCalaPointID.Add(id);
            }
            lsNewPoint.Add(pt);
           
        }
        public void Delete(point pt)
        {
            lsDeletePoint.Add(pt);//有问题?????????????

        }
        public void Update(point pt)
        {
            pt.listSisCalaPointID = ExpandOrgPointToSisPoint(pt.id);
            lsModifyPoint.Add(pt);
        }
       
    }
    
}
