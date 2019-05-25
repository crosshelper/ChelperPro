using System;
using ChelperPro.Models;
using MySql.Data.MySqlClient;

namespace ChelperPro.Helpers
{
    public class UserInfoHelper
    {
        private readonly string connStr = "server=chdb.cakl0xweapqd.us-west-1.rds.amazonaws.com;port=3306;database=chdb;user=chroot;password=ch123456;charset=utf8";

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




        public UserInfoHelper()
        {
        }
    }
}
