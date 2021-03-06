﻿using System;
using System.Collections.Generic;
using ChelperPro.Models;
using MySql.Data.MySqlClient;

namespace ChelperPro.Helpers
{
    public class UserSettingHelper
    {
        private readonly string connStr = "server=chdb.cakl0xweapqd.us-west-1.rds.amazonaws.com;port=3306;database=chdb;user=chroot;password=ch123456;charset=utf8";

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
                    ac.ContactNo = reader.GetString(1);
                    ac.Pwd = reader.GetString(2);
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

        internal void UpdateZipCode(string zipCode1, string zipCode2, string zipCode3)
        {
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                    "UPDATE HelperInfo SET " +
                    "ServiceZip1 = @para2, " +
                    "ServiceZip2 = @para3, " +
                    "ServiceZip3 = @para4" +
                    " WHERE Uid = @para1";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", Settings.UserId);
                cmd.Parameters.AddWithValue("para2", zipCode1);
                cmd.Parameters.AddWithValue("para3", zipCode2);
                cmd.Parameters.AddWithValue("para4", zipCode3);
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

        internal List<string> GetZipCodeByID(string userId)
        {
            List<string> ziplist = new List<string>();
            //并没有建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                "SELECT ServiceZip1,ServiceZip2,ServiceZip3 FROM HelperInfo WHERE HelperInfo.Uid = @para1";

                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", userId);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ziplist.Add(reader.GetString(0));
                    ziplist.Add(reader.GetString(1));
                    ziplist.Add(reader.GetString(2));
                }
                return ziplist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ziplist;
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

        public UserSettingHelper()
        {
        }
    }
}
