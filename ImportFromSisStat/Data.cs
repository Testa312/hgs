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
namespace ImportFromSisStat
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
        //sis点列表，用于取得实时值------------------------------------------------------------------

        HashSet<string> hs_sispoint = new HashSet<string>();
        public HashSet<string> hsSisPoint
        {
            set { hs_sispoint = value; }
            get { return hs_sispoint; }
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
        private void LoadData()
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                pgconn.Open();
                string strsql = "select * from point order by id";
                var cmd = new NpgsqlCommand(strsql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();

                while (pgreader.Read())
                {
                    string nd = pgreader["nd"].ToString();
                    string pn = pgreader["pn"].ToString();

                    pointsrc pointsrc = (pointsrc)(short)pgreader["pointsrc"];

                    if (pointsrc == pointsrc.sis)
                    {
                        hs_sispoint.Add(string.Format("{0}.{1}",nd,pn));
                    }
                }
 
            }
            catch(Exception e)  { throw new Exception(string.Format("装入数据时发生错误！"); }
            finally { pgconn.Close(); }
        }
        public void LoadFromPG()
        {
            hs_sispoint.Clear();
            LoadData();
        }
        private string dtoNULL(double? d)
        {
            return d.HasValue ? d.ToString() : "NULL";
        }
        public void SavetoPG(List<point> lspt)
        {
            StringBuilder sb = new StringBuilder();
            foreach (point pt in lspt)
            {
                sb.AppendLine(string.Format(@"insert into point (id,nd,pn,ed,eu,ll,hl" +
                                            "id_sis,pointsrc,mt,ownerid,fm,isavalarm" +
                                    "values ({0},'{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},{11},{12});",
                                            pt.id, pt.nd, pt.pn, pt.ed, pt.eu, 
                                    dtoNULL(pt.ll),dtoNULL(pt.hl),pt.id_sis, (int)pt.Pointsrc, DateTime.Now, Auth.GetInst().LoginID, 
                                    pt.fm ,"true"));
            }
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                if (sb.Length < 10) return;
                var cmd = new NpgsqlCommand(sb.ToString(), pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
                pgconn.Close();
            }
            catch (Exception) { throw new Exception(string.Format("保存数据时发生错误！")); }
            finally
            {
                //MAXOFPOINTID--;
                pgconn.Close();
            }
        }   
    }
    
}
