using System;
using System.Collections.Generic;
using System.Data;
using ChelperPro.Models;
using MySql.Data.MySqlClient;

namespace ChelperPro.Helpers
{
    public class TopicInfoHelper
    {
        private readonly List<TopicInfo> topiclist = new List<TopicInfo>();
        private readonly List<TopicInfoLabel> topiclabellist = new List<TopicInfoLabel>();
        private readonly UserInfoHelper uih= new UserInfoHelper();

        private readonly string connStr = "server=chdb.cakl0xweapqd.us-west-1.rds.amazonaws.com;port=3306;database=chdb;user=chroot;password=ch123456;charset=utf8";

        public List<TopicInfoLabel> GetMyTopicList(string zip)
        {
            GetTopicListByZipCode(zip);
            TopicInfoConvertor(topiclist);
            return topiclabellist;
        }

        private void TopicInfoConvertor(List<TopicInfo> list)
        {
            var th = new TagsHelper();
            foreach(TopicInfo tp in list)
            {
                var user = uih.GetUserInfoByID(tp.UserID);
                TopicInfoLabel tmp = new TopicInfoLabel
                {
                    TopicID = tp.TopicID,
                    TagName = th.GetTagNameByID(tp.TagID),
                    UserID = tp.UserID,
                    Name = user.FirstName + " " + user.LastName,
                    Icon = user.Icon,
                    Zipcode = tp.Zipcode,
                    Language = tp.Language,
                    Description = tp.Description,
                    Status = StatusTextConvertor(tp.Status)
                };
                topiclabellist.Add(tmp);
            }
        }

        private string StatusTextConvertor(int status)
        {
            switch (status)
            {
                case 0:
                    return "";
                case 1:
                    return "Emergency";
                default:
                    return "";
            }
        }

        private void GetTopicListByZipCode(string zipCode)
        {
            //建立数据库连接
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {   //建立连接，打开数据库
                conn.Open();
                string sqlstr =
                "SELECT TopicID,TagID,Uid,Language,Description,Status FROM TopicInfo WHERE Zip = @para1";

                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                //通过设置参数的形式给SQL 语句串值
                cmd.Parameters.AddWithValue("para1", zipCode);
                //cmd.Parameters.AddWithValue("para2", password);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int TopicID = reader.GetInt32(0);
                    int TagID = reader.GetInt32(1);
                    string Uid = reader.GetString(2);
                    string Language = reader.GetString(3);
                    string Description = reader.GetString(4);
                    int Status = reader.GetInt32(5);

                    TopicInfo tmp = new TopicInfo()
                    {
                        UserID = Uid,
                        TopicID = TopicID,
                        TagID = TagID,
                        Zipcode = zipCode,
                        Language = Language,
                        Description = Description,
                        Status = Status
                    };
                    topiclist.Add(tmp);
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

        public TopicInfoHelper()
        {
        }
    }
}
