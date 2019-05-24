using System;
using MySql.Data.MySqlClient;

namespace ChelperPro.Helpers
{
    public class UserSettingHelper
    {
        private readonly string connStr = "server=chdb.cakl0xweapqd.us-west-1.rds.amazonaws.com;port=3306;database=chdb;user=chroot;password=ch123456;charset=utf8";

        private void GetPaymentByID(string uid)
        {
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                "SELECT Pid,AccountNumber,CName,ExDate,CVV,Zip FROM PaymentInfo WHERE Uid = @para1";

                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", uid);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string PaymentID = reader.GetString(0);
                    string AccountNo = reader.GetString(1);
                    string CName = reader.GetString(2);
                    DateTime ExDate = reader.GetDateTime(3);
                    string CVV = reader.GetString(4);
                    string Zipcode = reader.GetString(5);
                    PaymentInfo ptmp = new PaymentInfo() { Uid = uid, PaymentID = PaymentID, AccountNo = AccountNo, CName = CName, ExDate = ExDate, CVV = CVV, Zipcode = Zipcode };
                    paymentlist.Add(ptmp);
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

        public Uac GetUacByID(string userid)
        {
            Uac ac = new Uac();
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                "SELECT * FROM UserMaster WHERE Uid = @para1";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", userid);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ac.UserID = reader.GetString(0);
                    ac.UserName = reader.GetString(1);
                    ac.Email = reader.GetString(2);
                    ac.ContactNo = reader.GetString(3);
                    ac.Pwd = reader.GetString(4);
                }
                return ac;
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

        public List<PaymentInfo> GetPaymentsList(string userid)
        {
            GetPaymentByID(userid);
            return paymentlist;
        }

        internal void InsertPaymentInfo(PaymentInfo pinfo)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "INSERT INTO PaymentInfo(AccountNumber,CName,ExDate,CVV,Zip,Uid) VALUES(@para1, @para2, @para3, @para4, @para5, @para6) ";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("para1", pinfo.AccountNo);
                    cmd.Parameters.AddWithValue("para2", pinfo.CName);
                    cmd.Parameters.AddWithValue("para3", pinfo.ExDate);
                    cmd.Parameters.AddWithValue("para4", pinfo.CVV);
                    cmd.Parameters.AddWithValue("para5", pinfo.Zipcode);
                    cmd.Parameters.AddWithValue("para6", pinfo.Uid);
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

        internal void UpdatePaymentInfo(PaymentInfo paymentinfo)
        {
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                    "UPDATE PaymentInfo SET " +
                    "AccountNumber = @para1, " +
                    "CName = @para2, " +
                    "ExDate = @para3, " +
                    "CVV = @para4, " +
                    "Zip = @para5" +
                    " WHERE Pid = @para6";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", paymentinfo.AccountNo);
                cmd.Parameters.AddWithValue("para2", paymentinfo.CName);
                cmd.Parameters.AddWithValue("para3", paymentinfo.ExDate);
                cmd.Parameters.AddWithValue("para4", paymentinfo.CVV);
                cmd.Parameters.AddWithValue("para5", paymentinfo.Zipcode);
                cmd.Parameters.AddWithValue("para6", paymentinfo.PaymentID);

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

        internal void UpdateUac(Uac ac)
        {
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                    "UPDATE UserMaster SET " +
                    "Email = @para2, " +
                    "ContactNo = @para3, " +
                    "Pwd = @para4" +
                    " WHERE Uid = @para1";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", ac.UserID);
                cmd.Parameters.AddWithValue("para2", ac.Email);
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

        internal void UpdateUserInfo(User usr)
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

        public UserSettingHelper()
        {
        }
    }
}
