using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
namespace HGS
{
    class Auth
    {
        private static Auth inst;
        private Auth() { }
        public static Auth GetInst()
        {
            if (inst == null)
            {
                inst = new Auth();
            }
            return inst;
        }
        //登录ID------------------------
        int loginid = -1;//0为管理员,见表Owner
        string username = "";
        //-----------------------
        public int LoginID
        {
            //set { loginid = value; }
            get { return loginid; }
        }
        public string UserName
        {
            //set { username = value; }
            get { return username; }
        }
        //取得可登录的专业；
        public List<string> GetUser()
        {
            List<string> ls_user = new List<string>();
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            try
            {
                pgconn.Open();
                string strsql = "select name,id  from owner order by id";
                var cmd = new NpgsqlCommand(strsql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();
                while (pgreader.Read())
                {

                    ls_user.Add(pgreader["Name"].ToString());
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
            return ls_user;
        }
        //用户授权
        public bool UserAuthorization(int userid, string pw)
        {
            var pgconn = new NpgsqlConnection(Pref.Inst().pgConnString);
            bool rsl = false;
            try
            {
                pgconn.Open();
                string strsql = string.Format("select (password = crypt('{0}', password)) as password,name from owner where id = {1}"
                    , pw, userid);
                var cmd = new NpgsqlCommand(strsql, pgconn);
                NpgsqlDataReader pgreader = cmd.ExecuteReader();
                pgreader.Read();
                rsl = (bool)pgreader["password"];
                if (rsl)
                {
                    username = pgreader["name"].ToString();
                    loginid = userid;
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
            return rsl;
        }
    }
}
