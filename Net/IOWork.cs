using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ReNetDemo
{
    class IOWork
    {
       // DataBase db = new DataBase();
        public byte[] FileReader(string path)
        {
            /////根据位置写入字节数组 权限为读写
            /////文件流：写入        
            //FileStream fileStream = File.Open(paths[0], FileMode.Open, FileAccess.ReadWrite);
            //byte[] array = new byte[fileStream.Length];//初始化字节数组，用来暂存读取到的字节
            //fileStream.Read(array, 0, array.Length);//读取流中数据，写入到字节数组中
            //fileStream.Close(); //关闭流
            //string strr = "../../../Resources/test.txt";
            FileStream fileStream = File.Open(path, FileMode.Open);//初始化文件流
            Console.WriteLine(path);
            byte[] array = new byte[fileStream.Length];//初始化字节数组，用来暂存读取到的字节
            fileStream.Read(array, 0, array.Length);//读取流中数据，写入到字节数组中
            fileStream.Close(); //关闭流
            string str = Encoding.Default.GetString(array);//将字节数组内容转化为字符串
            return array;
            //Console.Write(str+"@");

        }

    }
}
