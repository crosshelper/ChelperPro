using System;
using System.Data;
using ChelperPro.Models;
using MySql.Data.MySqlClient;

namespace ChelperPro.Helpers
{
    public class UserAccess
    {
        public UserAccess()
        {
        }

        readonly string connStr = "server=chdb.cakl0xweapqd.us-west-1.rds.amazonaws.com;port=3306;database=chdb;user=chroot;password=ch123456;charset=utf8";
        private int currentUid;
        public int CurrentUid { get { return currentUid; } }

        public void UserRegister(string Uname, string Email, string ContactNo, string Pwd)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "INSERT INTO UserMaster(Uname,Email,ContactNo,Pwd,Permission) VALUES(@para1, @para2, @para3, @para4, 0) ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("para1", Uname);
                    cmd.Parameters.AddWithValue("para2", Email);
                    cmd.Parameters.AddWithValue("para3", ContactNo);
                    cmd.Parameters.AddWithValue("para4", Pwd);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Connecting to MySQL success");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Connection failed");
            }
            finally
            {
                conn.Close();
            }
        }

        internal void SetChatID()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "UPDATE UserInfo SET " +
                                 "ChatID = @para1" +
                                 " WHERE Uid = @para2";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("para1", "cycbis_" + currentUid);
                    cmd.Parameters.AddWithValue("para2", currentUid);
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Connecting to MySQL success");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("Connection failed");
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 验证用户名密码，存放回true，否则为false
        /// </summary>
        public bool VerifyUser(string username, string password)
        {
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr = "select * from UserMaster where Uname = @para1 and Pwd = @para2";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", username);
                cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    currentUid = reader.GetInt32(0);
                    Settings.IsLogin = true;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接              
            }
            return false;
        }

        public UserInfo GetUserInfo(int userid)
        {
            UserInfo user = new UserInfo();
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr = "select * from UserInfo where Uid = @para1";// and Pwd = @para2";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", userid);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user.UserID = reader.GetString(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);
                    user.ChatID = reader.GetString(3);
                    user.FLanguage = reader.GetString(4);
                    user.SLanguage = reader.GetString(5);
                    user.PaymentID = reader.GetString(6);
                    user.Icon = reader.GetString(7);
                    //user.Homeland = reader.GetString(8);
                    user.FENo = reader.GetString(9);
                    user.SENo = reader.GetString(10);
                    user.Address = reader.GetString(11);
                    user.Location = reader.GetString(12);
                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接              
            }
            return user;
        }
    }
}
