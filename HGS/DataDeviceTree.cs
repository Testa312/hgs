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
        Added,Modified,Removed,no
    }
    public class TreeTag
    {
        public string nodeName = "";
        public int id = -1;
        public string path = "";
        public float[] start_th = null;
        public float[] alarm_th_dis = null;
        public int sort = 0;
        //public NodeStatus treenodestatus = NodeStatus.no;
        public HashSet<int> pointid_set = null;
        //public HashSet<object> sisid_set = null;
    }
    public class TreeDragData
    {
        public HashSet<int> pointid_set = new HashSet<int>();
        public TreeNode DragSourceNode = null;
    }
    static class DataDeviceTree
    {
        public static int GetNextTreeNodeId()
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
        public static int GetMaxSortV()
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
                    strsql = "select max(sort) from devicetree";
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
        private static string GetNodeFullPath(TreeNode tn)
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
            StringBuilder sb = new StringBuilder();
            TreeTag ttg = (TreeTag)tn.Tag;

            if (ttg != null && ttg.pointid_set != null && ttg.pointid_set.Count > 0)
            {
                sb.Append("'{");
                foreach (int id in ttg.pointid_set)
                {
                    string ay = string.Format("{0},", id);
                    sb.Append(ay);
                }
                if (sb.Length >= 2) sb.Remove(sb.Length - 1, 1);
                sb.Append("}'");
                return sb.ToString();
            }
            else 
                return "NULL";                
        }
        private static string ArraytoString(float[] fa)
        {
            StringBuilder sb = new StringBuilder();
            if (fa != null && fa.Length > 0)
            {
                sb.Append("'{");
                foreach (float v in fa)
                {
                    string ay = string.Format("{0},", fa);
                    sb.Append(ay);
                }
                if (sb.Length >= 2) sb.Remove(sb.Length - 1, 1);
                sb.Append("}'");
                return sb.ToString();
            }
            else
                return "NULL";
        }
        private static float? CasttoFloat(object ob)
        {
            if (ob == DBNull.Value)
                return null;
            else
                return (float)ob;
        }
        public static List<TreeNode> GetAllSubNode(string path)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            List<TreeNode> ltn = new List<TreeNode>();
            try
            {
                pgconn.Open();
                string sql = string.Format(@"SELECT * FROM devicetree WHERE path ~ '^{0}/[0-9]+$'  order by sort;", path);
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
                    object ob = pgreader["start_th"];
                    if (ob != DBNull.Value)
                    {
                        ttag.start_th = (float[])ob;
                    }
                    ob = pgreader["alarm_th_dis"];
                    if (ob != DBNull.Value)
                    {
                        ttag.alarm_th_dis = (float[])ob;
                    }
                    ob = pgreader["pointid_array"];
                    if (ob != DBNull.Value)
                    {
                        ttag.pointid_set = new HashSet<int>((int[])ob);
                        //ttag.sisid_set = new HashSet<object>();
                        //ttag.sisid_set.UnionWith(Data.inst().GetSisIdSet(ttag.pointid_set));
                    }
                    ltn.Add(tn);
                    //
                }
            }
            catch (Exception e) { throw new Exception(string.Format("读入设备子节点时发生错误！"), e); }
            finally { pgconn.Close(); }
            return ltn;
        }
        public static void InsertNodetoDb(TreeNode tn)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
               
                TreeTag tag = (TreeTag)tn.Tag;
                tag.id = GetNextTreeNodeId();
                if (tag.sort <= 0) tag.sort = GetMaxSortV();
                tag.path = GetNodeFullPath(tn);
                string sql = string.Format(@"insert into devicetree (id,nodename,path,start_th,alarm_th_dis,sort,pointid_array)" +
                                    " values ({0},'{1}','{2}',{3},{4},{5},{6});",
                                    tag.id, tag.nodeName, tag.path, ArraytoString(tag.start_th),ArraytoString(tag.alarm_th_dis), tag.sort, GetNodeArray(tn));
                var cmd = new NpgsqlCommand(sql, pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();          
            }
            catch (Exception e) { throw new Exception(string.Format("增加设备节点时发生错误！"), e); }
            finally { pgconn.Close(); }
        }
        private static string  GetUpdateSql(TreeNode tn)
        {
            TreeTag tag = (TreeTag)tn.Tag;
            tag.path = GetNodeFullPath(tn);
            string sql = string.Format(@"update devicetree set nodename='{0}',path='{1}',start_th={2},alarm_th_dis={3},sort={4},pointid_array={5} where id = {6};",
                                    tag.nodeName, tag.path, ArraytoString(tag.start_th), ArraytoString(tag.alarm_th_dis), tag.sort, GetNodeArray(tn), tag.id);
            return sql;
        }
        public static void UpdateNodetoDB(TreeNode tn)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                /*
                TreeTag tag = (TreeTag)tn.Tag;
                //tag.id = GetNextPointId();
                tag.path = GetNodePath(tn);
                string sql = string.Format(@"update devicetree set nodename='{0}',path='{1}',start_th={2},alarm_th_dis={3},sort={4},pointid_array={5} where id = {6};",
                                    tag.nodeName, tag.path, Functions.dtoNULL(tag.start_th), Functions.dtoNULL(tag.alarm_th_dis), tag.sort, GetNodeArray(tn), tag.id);
                */
                var cmd = new NpgsqlCommand(GetUpdateSql(tn), pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { throw new Exception(string.Format("更新设备节点时发生错误！"), e); }
            finally { pgconn.Close(); }
        }
        public static void UpdateAllSubNodes(TreeNode tn)
        {
            if (tn == null) return;
            StringBuilder sb = new StringBuilder();
            Stack<TreeNode> s_node = new Stack<TreeNode>(20);
            s_node.Push(tn);
            while (s_node.Count > 0)
            {
                TreeNode currentTn = s_node.Pop();
                sb.AppendLine(GetUpdateSql(currentTn));
                foreach (TreeNode tnsub in currentTn.Nodes)
                    s_node.Push(tnsub);
            }
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                var cmd = new NpgsqlCommand(sb.ToString(), pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { throw new Exception(string.Format("更新所有子节点时发生错误！"), e); }
            finally { pgconn.Close(); }
        }
        public static void UpdateNode(string sql)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                var cmd = new NpgsqlCommand(sql, pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { throw new Exception(string.Format("更新设备节点时发生错误！"), e); }
            finally { pgconn.Close(); }
        }
            public static void RemoveNode(TreeNode tn)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                string sql = string.Format(@"DELETE  FROM devicetree WHERE path like '{0}%';",((TreeTag)tn.Tag).path);
                var cmd = new NpgsqlCommand(sql, pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { throw new Exception(string.Format("删除设备节点时发生错误！"), e); }
            finally { pgconn.Close(); }
        }
    }
}
