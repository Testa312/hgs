using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
namespace HGS
{
    public class TreeDragData
    {
        public HashSet<int> pointid_set = new HashSet<int>();
        public TreeNode DragSourceNode = null;
    }
    static class DataDeviceTree
    {
        /*
        //临时
        public static void updatedtwth()
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                pgconn.Open();
                string sql = string.Format(@"SELECT * FROM devicetree  order by sort;");
                var cmd = new NpgsqlCommand(sql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();
                StringBuilder sb = new StringBuilder();
                while (pgreader.Read())
                {
                    //DeviceInfo di = new DeviceInfo();

                    int id = (int)pgreader["id"];

                    object ob = pgreader["alarm_th_dis"];
                    if (ob != DBNull.Value)
                    {
                        float[] th = (float[])ob;
                        for (int i = 0; i < th.Length; i++)
                        {
                            th[i] *= 10;
                        }
                        sb.AppendLine(string.Format(@"update devicetree set alarm_th_dis={0} where id = {1};",
                                    ArraytoString(th), id));
                    }
                }
                //
               pgconn.Close();
               cmd = new NpgsqlCommand(sb.ToString(), pgconn);
               pgconn.Open();
               cmd.ExecuteNonQuery();
            }
            catch (Exception e) { throw new Exception(string.Format("读入设备节点时发生错误！"), e); }
            finally { pgconn.Close(); }
        }
        */
        //-------------------
        public static int GetNextTreeNodeId()
        {
            int imax = 0;
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString());
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
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString());
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
                DeviceInfo ttg = (DeviceInfo)tnode.Tag;
                string path = string.Format("/{0}",ttg.id);
                sb.Insert(0,path);
                tnode = tnode.Parent;
            }
            return sb.ToString();
        }
        private static string GetNodeArray(TreeNode tn)
        {
            StringBuilder sb = new StringBuilder();
            DeviceInfo ttg = (DeviceInfo)tn.Tag;

            if (ttg != null  && ttg.Sensors_set().Count > 0)
            {
                sb.Append("'{");
                foreach (int id in ttg.Sensors_set())
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
                    string ay = string.Format("{0},", v);
                    sb.Append(ay);
                }
                if (sb.Length >= 2) sb.Remove(sb.Length - 1, 1);
                sb.Append("}'");
                return sb.ToString();
            }
            else
                return "NULL";
        }
        /*
        private static float? CasttoFloat(object ob)
        {
            if (ob == DBNull.Value)
                return null;
            else
                return (float)ob;
        }*/
        public static List<TreeNode> GetAllSubNode(string path)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString());
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
                    DeviceInfo ttag = null;
                    int tnid = (int)pgreader["id"];
                    if (!Data_Device.dic_Device.TryGetValue(tnid, out ttag))
                    {
                        ttag = new DeviceInfo();
                       
                        ttag.id = tnid;

                        ttag.Name = tn.Text;
                        ttag.sort = (int)pgreader["sort"];
                        ttag.path = pgreader["path"].ToString();
                        ttag.nd = pgreader["nd"].ToString();
                        ttag.pn = pgreader["pn"].ToString();
                        ttag.Orgformula_If = pgreader["alarmif"].ToString();
                        ttag.Sound = (int)pgreader["sound"];
                        ttag.DelayAlarmTime = (int)pgreader["delayalarmtime"];
                        object ob = pgreader["alarm_th_dis"];
                        
                        if (ob != DBNull.Value)
                        {
                            ttag.Alarm_th_dis = (float[])ob;
                        }
                        ob = pgreader["pointid_array"];
                        if (ob != DBNull.Value)
                        {
                            ttag.SensorUnionWith(new HashSet<int>((int[])ob));
                        }
                        ttag.ParseFormula();
                    }
                    tn.ForeColor = ttag.Alarm_th_dis != null ? Color.Red : Color.Black;
                    tn.Tag = ttag;
                    ltn.Add(tn);
                }
            }
            catch (Exception e) { FormBugReport.ShowBug(e,"读入设备子节点时发生错误！"); }
            finally { pgconn.Close(); }
            return ltn;
        }
        public static void InsertNodetoDb(TreeNode tn)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString());
            try
            {
               
                DeviceInfo tag = (DeviceInfo)tn.Tag;
                tag.id = GetNextTreeNodeId();
                if (tag.sort <= 0) tag.sort = GetMaxSortV();
                tag.path = GetNodeFullPath(tn);
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(string.Format(@"insert into devicetree (id,nodename,path,alarm_th_dis,sort,pointid_array,sound,nd,pn,alarmif,delayalarmtime)" +
                                    " values ({0},'{1}','{2}',{3},{4},{5},{6},'{7}','{8}','{9}',{10});",
                                    tag.id, tag.Name, tag.path,ArraytoString(tag.Alarm_th_dis), tag.sort, GetNodeArray(tn),
                                    tag.Sound,tag.nd,tag.pn,tag.Orgformula_If,tag.DelayAlarmTime));
                GetinsertsubSql(sb, tag);
                var cmd = new NpgsqlCommand(sb.ToString(), pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
                //
                tag.ParseFormula();
                tag.initSensorsQ();
            }
            catch (Exception e) { FormBugReport.ShowBug(e,"增加设备节点时发生错误！"); }
            finally { pgconn.Close(); }
        }
        private static void GetinsertsubSql(StringBuilder sb, DeviceInfo di)
        {
            if (di.lsVartoPoint_If is null) return;
            foreach (varlinktopoint supt in di.lsVartoPoint_If)
            {
                sb.AppendLine(string.Format("insert into formula_device (id,pointid,varname) values({0},{1},'{2}');",
                    di.id, supt.sub_id, supt.varname));
            }
        }
        private static string  GetUpdateSql(TreeNode tn)
        {
            DeviceInfo tag = (DeviceInfo)tn.Tag;
            if (tag.id < 0) return "";
            tag.path = GetNodeFullPath(tn);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(@"update devicetree set nodename='{0}',path='{1}',alarm_th_dis={2},sort={3},pointid_array={4},"+
                                    "sound={5},nd='{6}',pn='{7}',alarmif = '{8}',delayalarmtime = {9} where id = {10};",
                                    tag.Name, tag.path, ArraytoString(tag.Alarm_th_dis), tag.sort, GetNodeArray(tn),tag.Sound,
                                    tag.nd,tag.pn,tag.Orgformula_If,tag.DelayAlarmTime, tag.id));
            sb.AppendLine(string.Format("delete  from formula_device where id = {0};", tag.id));
            
            GetinsertsubSql(sb, tag);
            return sb.ToString();
        }
        public static void UpdateNodetoDB(TreeNode tn)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString());
            try
            {
                var cmd = new NpgsqlCommand(GetUpdateSql(tn), pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
                //
                DeviceInfo tag = (DeviceInfo)tn.Tag;
                tag.ParseFormula();
                tag.initSensorsQ();
            }
            catch (Exception e) 
            { 
                FormBugReport.ShowBug(e,"更新设备节点时发生错误！"); 
            }
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
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString());
            try
            {
                var cmd = new NpgsqlCommand(sb.ToString(), pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { FormBugReport.ShowBug(e, "更新设备节点时发生错误！"); }
            finally { pgconn.Close(); }
        }
       
        public static void UpdateNode(string sql)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString());
            try
            {
                var cmd = new NpgsqlCommand(sql, pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { FormBugReport.ShowBug(e, "更新设备节点时发生错误！"); }
            finally { pgconn.Close(); }
        }
        public static void RemoveNode(TreeNode tn)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString());
            try
            {
                string sql = string.Format(@"DELETE  FROM devicetree WHERE path like '{0}%';", ((DeviceInfo)tn.Tag).path);
                var cmd = new NpgsqlCommand(sql, pgconn);
                pgconn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e) { FormBugReport.ShowBug(e, "删除设备节点时发生错误！"); }
            finally { pgconn.Close(); }
        }
    }
}
