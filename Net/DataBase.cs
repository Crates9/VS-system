using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace ReNetDemo { 

class DataBase
    {

    static string connetStr = "server=localhost;port=3306;user=root;" +
             "password=aby608; database=test;";

    public string SearchPath(string word)
    {
        string path = "";

        MySqlConnection conn = new MySqlConnection(connetStr);
            try
            {
                conn.Open();
               // Console.WriteLine("已连接。。。");



                MySqlCommand comd = new MySqlCommand
                {
                    Connection = conn,
                    CommandType = System.Data.CommandType.Text,
                    CommandText = "select DISTINCT   NAME,PATH from base where NAME='" + word+"'"
                };
               // Console.WriteLine("search successed");
                MySqlDataReader mysqldr = comd.ExecuteReader();
                while (mysqldr.Read())
                {
                    //path = "ID:" + mysqldr["ID"] + " 名" +
                    //    mysqldr["NAME"];

                    path = mysqldr["PATH"].ToString();
                    //Console.WriteLine(path);
                    
                }
            }

            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();

            }

        return path;
        
    }

    }
}
