using System;
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
        public MyWalletHelper()
        {
        }
    }
}
