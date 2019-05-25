using System;
using System.Data;
using ChelperPro.Models;
using MySql.Data.MySqlClient;

namespace ChelperPro.Helpers
{
    public class MyWalletHelper
    {
        private readonly string connStr = "server=chdb.cakl0xweapqd.us-west-1.rds.amazonaws.com;port=3306;database=chdb;user=chroot;password=ch123456;charset=utf8";

        internal ReceiptInfo GetReceiptByID(string receiptID)
        {
            ReceiptInfo tmp = new ReceiptInfo();
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                "SELECT ServiceFee,EqFee,Tax,Surcharges,PayTime FROM Receipts WHERE ReceiptID = @para1";

                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", receiptID);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tmp.ServiceFee = reader.GetDouble(0);
                    tmp.EqFee = reader.GetDouble(1);
                    tmp.Tax = reader.GetDouble(2);
                    tmp.Surcharge = reader.GetDouble(3);
                    tmp.PaymentDateTime = reader.GetDateTime(4);
                    tmp.PaymentName = "7785-9085-3425-8797";
                }
                if (tmp != null)
                    return tmp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();   //关闭连接 
            }
            return tmp;
        }

        internal void UpdateBankingInfo(BankingInfo paymentinfo)
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



        internal BankingInfo GetBankingInfoByID(string uid)
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
                var btmp= new BankingInfo();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string PaymentID = reader.GetString(0);
                    string AccountNo = reader.GetString(1);
                    string CName = reader.GetString(2);
                    DateTime ExDate = reader.GetDateTime(3);
                    string CVV = reader.GetString(4);
                    string Zipcode = reader.GetString(5);
                    BankingInfo ptmp = new BankingInfo() { Uid = uid, PaymentID = PaymentID, AccountNo = AccountNo, CName = CName, ExDate = ExDate, CVV = CVV, Zipcode = Zipcode };
                    btmp = ptmp;
                }

                return btmp;
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

        internal void InsertPaymentInfo(BankingInfo pinfo)
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




        public MyWalletHelper()
        {
        }
    }
}
