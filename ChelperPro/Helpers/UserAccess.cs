using System;
using System.Data;
using ChelperPro.Models;
using MySql.Data.MySqlClient;
using Twilio;
using Twilio.Rest.Verify.V2;
using Twilio.Rest.Verify.V2.Service;
namespace ChelperPro.Helpers
{
    public class UserAccess
    {
        public UserAccess()
        {
        }

        readonly string connStr = "server=chdb.cakl0xweapqd.us-west-1.rds.amazonaws.com;port=3306;database=chdb;user=chroot;password=ch123456;charset=utf8";
        private int currentUid;
        private int userPermission;
        public int CurrentUid { get { return currentUid; } }

        public void TwilioVerifyService(string tempNumber)
        {
            const string accountSid = "AC86ac48ee4086ad028d5c75b60bc28d12";
            const string authToken = "88bde17df05d1be8d26239c2915f0960";
            TwilioClient.Init(accountSid, authToken);
            //var service = ServiceResource.Create(friendlyName: "My First Verify Service");
            //Console.WriteLine(service.Sid);
            var verification = VerificationResource.Create(
                to: tempNumber, //"+14084641309",
                channel: "sms",
                pathServiceSid: "VA0f7950bc53a8d465b01ecff8f8c96f1c"
            );

            Console.WriteLine(verification.Status);
        }



        public void UserRegister(string ContactNo, string Pwd)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "INSERT INTO UserMaster(Uname,Email,ContactNo,Pwd,Permission) VALUES(@para1, @para2, 0) ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("para1", ContactNo);
                    cmd.Parameters.AddWithValue("para2", Pwd);

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

        internal void UpdateEmailNo(string email, string contactNo)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "UPDATE UserMaster SET " +
                                 "Email = @para2 " +
                                 "ContactNo = @para3" +
                                 " WHERE Uid = @para1";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("para1", currentUid);
                    cmd.Parameters.AddWithValue("para2", email);
                    cmd.Parameters.AddWithValue("para3", contactNo);

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

        internal bool IsSSNExist()
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "select 1 from HelperInfo where Uid = @para1 limit 1";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("para1", Settings.UserId);
                    object result = cmd.ExecuteScalar();
                    if (Convert.ToInt32(result) == 1)
                        return true;
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
            return false;
        }

        internal string GetUserIDbyNo(string contactNo)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr = "select * from UserMaster where ContactNo = @para1";// and Pwd = @para2";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", contactNo);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接
            }
            return "";
        }

        internal string GetPermission(string contactNo)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr = "select * from UserMaster where ContactNo = @para1";// and Pwd = @para2";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", contactNo);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetString(3);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接
            }
            return "0";
        }

        internal string GetPWD(string contactNo)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr = "select * from UserMaster where ContactNo = @para1";// and Pwd = @para2";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", contactNo);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetString(2);
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接
            }
            return "";
        }

        internal bool CheckPhoneNoExist(string contactNo)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "select 1 from UserMaster where ContactNo = @para1 limit 1";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("para1", contactNo);
                    object result = cmd.ExecuteScalar();
                    if (Convert.ToInt32(result) == 1)
                        return true;
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
            return false;
        }

        internal bool CheckPermission()
        {
            if (userPermission == 1)
                return true;
            else
                return false;
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
                Console.WriteLine(ex);
                Console.WriteLine("Connection failed");
            }
            finally
            {
                conn.Close();
            }
        }

        internal void UpdateHelperInfo(string email, string phone, string fl, string sl, string social, string address)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "UPDATE UserInfo SET " +
                                 "FLanguage = @para2" +
                                 "SLanguage = @para3" +
                                 "Address = @para4" +
                                 " WHERE Uid = @para1 "+

                                 "UPDATE UserMaster SET " +
                                 "Email = @para5" +
                                 "ContactNo = @para6" +
                                 "Permission = 1" +
                                 " WHERE Uid = @para1 " +

                                 "UPDATE HelperInfo SET " +
                                 "SSN = @para7" +
                                 " WHERE Uid = @para1";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("para1", currentUid);
                    cmd.Parameters.AddWithValue("para2", fl);
                    cmd.Parameters.AddWithValue("para3", sl);
                    cmd.Parameters.AddWithValue("para4", address);
                    cmd.Parameters.AddWithValue("para5", email);
                    cmd.Parameters.AddWithValue("para6", phone);
                    cmd.Parameters.AddWithValue("para7", social);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Connecting to MySQL success");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
        public bool VerifyUser(string phonenumber, string password)
        {
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr = "select * from UserMaster where ContactNo = @para1 and Pwd = @para2";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", phonenumber);
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
            catch (MySqlException ex)
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
                    user.FENo = reader.GetString(8);
                    user.SENo = reader.GetString(9);
                    user.Address = reader.GetString(10);
                    user.Location = reader.GetString(11);
                    user.Email = reader.GetString(12);
                    user.ZipCode = reader.GetString(13);
                    return user;
                }
            }
            catch (MySqlException ex)
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
