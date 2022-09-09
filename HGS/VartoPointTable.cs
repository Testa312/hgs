using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
namespace HGS
{
    static class VartoPointTable
    {
        //-------------------
        static DataTable dt_vartoPoint = null;
        //-----------------------------------------------
        static DateTime filltime = DateTime.Now;
        static DataTable dt_VartoPoint
        {
            // set { dttempPoint = value; }
            get { return dt_vartoPoint; }
        }
        //取得计算点的相关点列表。
        public static List<int> GetDeletePointIdList(int pointid)
        {
            Refresh();
            List<int> lsptid = new List<int>();
            string strexp = string.Format("pointid={0}", pointid);
            DataRow[] frow = dt_vartoPoint.Select(strexp);
            foreach (DataRow dr in frow)
            {
                lsptid.Add((int)dr["id"]);
            }
            return lsptid;
        }
        //取得计算点的相关点列表。
        public static List<varlinktopoint> Sub_PointtoVarList(point pt, cellid cellId)
        {
            Refresh();
            //if (pt.pointsrc != pointsrc.calc) return null;
            List<varlinktopoint> lssubpt = new List<varlinktopoint>();
            string strexp = string.Format("id={0} and cellId={1}", pt.id, (int)cellId);
            DataRow[] frow = dt_vartoPoint.Select(strexp);
            foreach (DataRow dr in frow)
            {
                varlinktopoint subpt = new varlinktopoint();
                subpt.sub_id = (int)dr["pointid"];
                subpt.varname = dr["varname"].ToString();
                //subpt.PointSrc = (pointsrc)(short)dr["pointsrc"];
                lssubpt.Add(subpt);
            }
            return lssubpt;
        }
        private static void LoadSubPointTable()
        {
            //得到关连的参与计算点。
            //string strsql = "select point.id,formula_point.pointid,cellid,varname from point," +
            // "formula_point where point.id = formula_point.id";
            string strsql = "select * from formula_point order by sort";
            NpgsqlDataAdapter daPoint = new NpgsqlDataAdapter(strsql, Pref.Inst().pgConnString);
            
            daPoint.Fill(dt_vartoPoint);
        }
        public static void Refresh(bool force = false)
        {
            if (dt_vartoPoint == null)
            {
                dt_vartoPoint = new DataTable();
                LoadSubPointTable();
                filltime = DateTime.Now;
            }
            else if (force)
            {
                dt_vartoPoint.Clear();//清空
                LoadSubPointTable();
                filltime = DateTime.Now;
            }
        }
        public static void DelayClear()
        {
            if ((DateTime.Now - filltime).TotalMinutes >= 15)
            {
                dt_vartoPoint.Clear();
                dt_vartoPoint = null;
            }
        }
    } 

}
