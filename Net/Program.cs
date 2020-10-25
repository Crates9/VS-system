//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ReNetDemo
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            NetWork net = new NetWork();
//            net.InitTCP();
//        }
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReNetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
           // IOWork ios = new IOWork();
           // ios.FileReader();
           // DataBase db = new DataBase();
            //db.SearchPath("Î÷Ò°Æßäþ");
            NetWork net = new NetWork();
            net.InitTCP();
        }
    }
}
