using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
namespace HGS
{
    public enum NodeStatus
    {
        isNew,isModify,no
    }
    public class TreeTag
    {
        public string nodeName = "";
        public int id = -1;
        public string path = "";
        public float? start_th = null;
        public float? alarm_th_dis = null;
        public int sort = -1;
        public NodeStatus treenodestatus = NodeStatus.no;
        public int[] pointid_array;
    }
    static class DataDeviceTree
    {
        public static int GetNextPointId()
        {
            int imax = 0;
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                pgconn.Open();
                string strsql = "select count(*) from devicetree";
                var cmd = new NpgsqlCommand(strsql, pgconn);
                imax = (int)(long)cmd.ExecuteScalar();
                if (imax > 0)
                {
                    strsql = "select max(id) from devicetree";
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
        private static string GetNodePath(TreeNode tn)
        {
            StringBuilder sb = new StringBuilder();
            TreeNode tnode = tn;
            while (tnode != null)
            {
                TreeTag ttg = (TreeTag)tnode.Tag;
                string path = string.Format("/{0}",ttg.id);
                sb.Insert(0,path);
                tnode = tnode.Parent;
            }
            return sb.ToString();
        }
        private static string GetNodeArray(TreeNode tn)
        {
            StringBuilder sb = new StringBuilder("'{");
            TreeTag ttg = (TreeTag)tn.Tag;
            if (ttg != null)
            {
                foreach (int id in ttg.pointid_array)
                {
                    string ay = string.Format("{0},", id);
                    sb.Append(ay);
                }
                if (sb.Length >= 3) sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("}'");
            return sb.ToString();
        }
        private static float? CasttoFloat(object ob)
        {
            if (ob == DBNull.Value)
                return null;
            else
                return (float)ob;
        }
        public static List<TreeNode> GetAllSubNode(string path_father)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            List<TreeNode> ltn = new List<TreeNode>();
            try
            {
                pgconn.Open();
                string sql = string.Format(@"SELECT * FROM devicetree WHERE path ~ '^{0}/[0-9]+$'  order by sort;", path_father);
                var cmd = new NpgsqlCommand(sql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();
               
                while (pgreader.Read())
                {
                    TreeNode tn = new TreeNode(pgreader["nodename"].ToString());
                    TreeTag ttag = new TreeTag();
                    tn.Tag = ttag;
                    ttag.id = (int)pgreader["id"];
                    ttag.nodeName = pgreader["nodename"].ToString();
                    ttag.sort = (int)pgreader["sort"];
                    ttag.path = pgreader["path"].ToString();
                    ttag.start_th = CasttoFloat(pgreader["start_th"]);
                    ttag.alarm_th_dis = CasttoFloat(pgreader["alarm_th_dis"]);
                    object ob = pgreader["pointid_array"];
                    if (ob != DBNull.Value)
                        ttag.pointid_array = (int[])pgreader["pointid_array"];
                    ltn.Add(tn);
                    //
                }
            }
            catch (Exception e) { throw new Exception(string.Format("读入设备子节点时发生错误！"), e); }
            finally { pgconn.Close(); }
            return ltn;
        }
        public static void InsertNode(TreeNode tn)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
               
                TreeTag tag = (TreeTag)tn.Tag;
                tag.id = GetNextPointId();
                tag.path = GetNodePath(tn);
                string sql = string.Format(@"insert into devicetree (id,nodename,path,start_th,alarm_th_dis,sort,pointid_array" +
                                    "values ({0},'{1}','{2}',{3},{4},{5},'{6}';",
                                    tag.id, tag.nodeName, tag.path, tag.start_th, tag.alarm_th_dis, tag.sort, GetNodeArray(tn));
                var cmd = new NpgsqlCommand(sql, pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();          
            }
            catch (Exception e) { throw new Exception(string.Format("插入设备子节点时发生错误！"), e); }
            finally { pgconn.Close(); }
        }
        public static void UpdateNode(TreeNode tn)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {

                TreeTag tag = (TreeTag)tn.Tag;
                tag.id = GetNextPointId();
                tag.path = GetNodePath(tn);
                string sql = string.Format(@"update devicetree set nodename={0},path={1},start_th={2},alarm_th={3},sort={4},pointid_array={5} where id = {6};",
                                    tag.nodeName, tag.path, tag.start_th, tag.alarm_th_dis, tag.sort, GetNodeArray(tn), tag.id);
                var cmd = new NpgsqlCommand(sql, pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { throw new Exception(string.Format("更新设备子节点时发生错误！"), e); }
            finally { pgconn.Close(); }
        }
    }
}
