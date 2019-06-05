using System;
using System.Collections.Generic;
using ChelperPro.Models;
using MySql.Data.MySqlClient;

namespace ChelperPro.Helpers
{
    public class TagsHelper
    {
        private readonly string connStr = "server=chdb.cakl0xweapqd.us-west-1.rds.amazonaws.com;port=3306;database=chdb;user=chroot;password=ch123456;charset=utf8";

        readonly private List<TagInfo> tagInfoList = new List<TagInfo>();
        public List<TagInfo> GetTagList()
        {
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                "SELECT * FROM Tags";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int TagID = reader.GetInt32(0);
                    string Pcategory = reader.GetString(1);
                    string ImageUrl = reader.GetString(2);

                    TagInfo tmp = new TagInfo()
                    {
                        TagID = TagID,
                        Pcategory = Pcategory,
                        ImageUrl = ImageUrl
                    };
                    tagInfoList.Add(tmp);
                }
                return tagInfoList;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return tagInfoList;
            }
            finally
            {
                conn.Close();   //关闭连接              
            }
        }

        public string GetTagNameByID(int tagID)
        {
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            string _tagName = "";
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                "SELECT TagName FROM Tags WHERE TagID = @para1";
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                cmd.Parameters.AddWithValue("para1", tagID);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    _tagName = reader.GetString(0);
                }
                return _tagName;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return _tagName;
            }
            finally
            {
                conn.Close();   //关闭连接              
            }
        }

        public TagsHelper()
        {
        }
    }
}
