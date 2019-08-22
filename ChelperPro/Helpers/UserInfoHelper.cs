using System;
using System.Collections.Generic;
using System.Data;
using ChelperPro.Models;
using MySql.Data.MySqlClient;

namespace ChelperPro.Helpers
{
    public class UserInfoHelper
    {
        private readonly string connStr = "server=chdb.cakl0xweapqd.us-west-1.rds.amazonaws.com;port=3306;database=chdb;user=chroot;password=ch123456;charset=utf8";

        internal string GetBioByID(string userId)
        {
            string ubio = "";
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                "SELECT Bio FROM HelperInfo WHERE Uid = @para1";

                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", userId);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ubio = reader.GetString(0);
                }
                return ubio;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ubio;
            }
            finally
            {
                conn.Close();   //关闭连接              
            }
        }

        public UserInfo GetUserInfoByID(string userid)
        {
            UserInfo user = new UserInfo();
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                "SELECT FirstName,LastName,Icon,ChatID,FLanguage,SLanguage,PaymentID,FENum,SENum,Address FROM UserInfo WHERE Uid = @para1";

                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", userid);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.FirstName = reader.GetString(0);
                    user.LastName = reader.GetString(1);
                    user.Icon = reader.GetString(2);
                    user.ChatID = reader.GetString(3);
                    user.FLanguage = reader.GetString(4);
                    user.SLanguage = reader.GetString(5);
                    user.PaymentID = reader.GetString(6);
                    user.FENo = reader.GetString(7);
                    user.SENo = reader.GetString(8);
                    user.Address = reader.GetString(9);
                    user.UserID = userid;
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                conn.Close();   //关闭连接              
            }
        }

        internal void UpdateUserRealNameEmail(string fName, string lName, string email)
        {
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "INSERT INTO UserInfo(Uid,FirstName,LastName,ChatID," +
                        "Flanguage,SLanguage,PaymentID,Icon,FENum,SENum," +
                        "Address,Location,Email,ZipCode) " +
                        "VALUES(@para4, @para1, @para2, 'cycbis', 'English', 'English', 'cycbis0000', 'http', " +
                        "0000000000, 0000000000, 'example', 'example', @para3, 95131)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("para1", fName);
                    cmd.Parameters.AddWithValue("para2", lName);
                    cmd.Parameters.AddWithValue("para3", email);
                    cmd.Parameters.AddWithValue("para4", Settings.UserId);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Connecting to MySQL success");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();//关闭连接              
            }
        }

        internal List<string> GetMyServiceArea()
        {
            var zipslist = new List<string>();
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                "SELECT ServiceZip1,ServiceZip2,ServiceZip3 FROM HelperInfo WHERE Uid = @para1";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", Settings.UserId);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    zipslist.Add(reader.GetString(0));
                    zipslist.Add(reader.GetString(1));
                    zipslist.Add(reader.GetString(2));
                }
                return zipslist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return zipslist;
            }
            finally
            {
                conn.Close();   //关闭连接              
            }
        }

        internal void UpdateMySpecialities(string bio, IList<TagInfo> tags)
        {
            UpdateHelperBio(bio);
            UpdateHelperTags(tags);
        }

        public void UpdateHelperTags(IList<TagInfo> tags)
        {
            DeleteTags();
            foreach(TagInfo taginfo in tags)
            {
                InsertTags(taginfo.TagID);
            }
        }

        private void InsertTags(int tagid)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "INSERT INTO HelperTag(Uid,TagID) VALUES(@para1, @para2) ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("para1", Settings.UserId);
                    cmd.Parameters.AddWithValue("para2", tagid);
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

        private void DeleteTags()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "DELETE From HelperTag Where Uid = @para1";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("para1", Settings.UserId);
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

        internal void UpdateUac(Uac ac)
        {
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                    "UPDATE UserMaster SET " +
                    "ContactNo = @para3, " +
                    "Pwd = @para4" +
                    " WHERE Uid = @para1";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", ac.UserID);
                cmd.Parameters.AddWithValue("para3", ac.ContactNo);
                cmd.Parameters.AddWithValue("para4", ac.Pwd);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接              
            }
        }

        public void UpdateHelperBio(string bio)
        {
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                    "UPDATE HelperInfo SET " +
                    "Bio = @para2" +
                    " WHERE Uid = @para1";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", Settings.UserId);
                cmd.Parameters.AddWithValue("para2", bio);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接              
            }
        }

        internal string GetChatIDByUid(string userID)
        {
            string chatid = "";
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                "SELECT ChatID FROM UserInfo WHERE Uid = @para1";

                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", userID);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    chatid = reader.GetString(0);
                }
                return chatid;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return chatid;
            }
            finally
            {
                conn.Close();   //关闭连接              
            }
        }

        internal void UpdateUserInfo(UserInfo usr)
        {
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                    "UPDATE UserInfo SET " +
                    "FirstName = @para2, " +
                    "LastName = @para3, " +
                    "FLanguage = @para4, " +
                    "SLanguage = @para5, " +
                    "PaymentID = @para6, " +
                    "Icon = @para7, " +
                    "FENum = @para8, " +
                    "SENum = @para9, " +
                    "Address = @para10" +
                    " WHERE Uid = @para1";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", usr.UserID);
                cmd.Parameters.AddWithValue("para2", usr.FirstName);
                cmd.Parameters.AddWithValue("para3", usr.LastName);
                cmd.Parameters.AddWithValue("para4", usr.FLanguage);
                cmd.Parameters.AddWithValue("para5", usr.SLanguage);
                cmd.Parameters.AddWithValue("para6", usr.PaymentID);
                cmd.Parameters.AddWithValue("para7", usr.Icon);
                cmd.Parameters.AddWithValue("para8", usr.FENo);
                cmd.Parameters.AddWithValue("para9", usr.SENo);
                cmd.Parameters.AddWithValue("para10", usr.Address);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接              
            }
        }

        readonly private List<TagInfo> tagInfoList = new List<TagInfo>();
        readonly private List<int> tagslist = new List<int>();

        internal List<TagInfo> GetMyTagsByID(string userId)
        {
            tagInfoList.Clear();
            GetTagByHelperID(userId);
            foreach (int tagid in tagslist)
            {
                GetTagListByID(tagid);
            }
            return tagInfoList;
        }

        private void GetTagByHelperID(string helperID)
        {
            tagslist.Clear();
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr = "SELECT TagID FROM HelperTag WHERE Uid = @para1";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", helperID);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tagslist.Add(reader.GetInt32(0));
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
        }

        private void GetTagListByID(int tagid)
        {
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                "SELECT TagName,TagIcon FROM Tags WHERE TagID = @para1";

                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", tagid);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string Pcategory = reader.GetString(0);
                    string ImageUrl = reader.GetString(1);

                    TagInfo tmp = new TagInfo()
                    {
                        TagID = tagid,
                        Pcategory = Pcategory,
                        ImageUrl = ImageUrl
                    };
                    tagInfoList.Add(tmp);
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
        }

        public UserInfoHelper()
        {
        }
    }
}
