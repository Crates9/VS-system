using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

using System.IO;
namespace ReNetDemo
{
    class NetWork
    {
        public bool swich = true;
        public string strReader = null;
        public string[] Bag = new string[3];
        DataBase db = new DataBase();
        IOWork io = new IOWork();
        public void InitTCP()
        {


            int port = 888;//端口  
            TcpClient tcpClient;//创建TCP连接对象
                                //定义IP地址
            IPAddress[] serverIP = Dns.GetHostAddresses("127.0.0.1");
            IPAddress localAddress = serverIP[0];//IP地址  
                                                 //监听套接字
            TcpListener tcpListener = new TcpListener(localAddress, port);
            tcpListener.Start(); //开始监听  

            //FileStream afile = new FileStream(@"..\..\Program.cs", FileMode.Create, FileAccess.ReadWrite);

            Console.WriteLine("服务器启动成功，等待用户接入…");//输出消息  
            while (2 - 1 == 1)
            {
                try
                {
                    //每接收一个客户端则生成一个TcpClient  
                    tcpClient = tcpListener.AcceptTcpClient();
                    //获取网络数据流

                    NetworkStream networkStream = tcpClient.GetStream();
                    //定义流数据读取对象
                    BinaryReader reader = new BinaryReader(networkStream);
                    BinaryWriter writer = new BinaryWriter(networkStream);

                    while (1 + 1 == 2)
                    {

                        try
                        {

                            NlpWork nlp = new NlpWork();
                            strReader = reader.ReadString();//接收消息  
                                                            //2.处理输入结果 返回格式为动词+名词(关注顺序 0代表动词， 1，2为名词)
                            string nlpReader = nlp.DepParserDemo(strReader);
                            Bag = nlp.DepResultHandle(nlpReader);
                            //三个词语
                            Console.WriteLine(Bag[0] + "," + Bag[1] + "," + Bag[2]);
                            //3.数据库查询三个词的模型位置
                            string[] paths = new string[3];
                            for (int i = 0; i < 3; i++)
                            {
                                paths[i] = db.SearchPath(Bag[i]); Console.WriteLine(paths[i] + ",");
                            }


                            //4.根据模型位置将字节流写入网络流
                            byte[] VtoStream = io.FileReader(paths[0]);
                           // int intV = VtoStream.Length;
                            byte[] StoStream = io.FileReader(paths[1]);
                           // int intS = StoStream.Length;
                            byte[] OtoStream = io.FileReader(paths[2]);
                            //  int intO = OtoStream.Length;

                            byte[] ConSend = new byte[VtoStream.Length + StoStream.Length + OtoStream.Length];

                            for(int i = 0; i < VtoStream.Length; i++)
                            {
                                ConSend[i] = VtoStream[i];
                            }
                            for(int i = 0; i < StoStream.Length; i++)
                            {
                                ConSend[i + VtoStream.Length] = StoStream[i];
                            }
                            for(int i = 0; i < OtoStream.Length; i++)
                            {
                                ConSend[i + VtoStream.Length + StoStream.Length] = OtoStream[i];
                            }


                            Console.WriteLine(Encoding.Default.GetString(ConSend));
                            writer.Write(ConSend);
                            // writer.Write("$" + Encoding.Default.GetString(VtoStream).Length
                            //    + "$" + Encoding.Default.GetString(StoStream).Length + "$" + Encoding.Default.GetString(OtoStream).Length + "$");

                            //string messageSend = "$"+Encoding.Default.GetString(VtoStream)+"$";
                            //messageSend += Encoding.Default.GetString(StoStream)+"$";
                            //messageSend += Encoding.Default.GetString(OtoStream) + "$";
                            //Console.WriteLine(messageSend);

                          //  writer.Write(VtoStream);

                         //   writer.Write(StoStream);

                         //   writer.Write(OtoStream);



                            //  writer.Write(Encoding.Default.GetString(VtoStream));
                            // writer.Write(Encoding.Default.GetString(StoStream));
                            // writer.Write(Encoding.Default.GetString(OtoStream));


                            writer.Flush();
                            writer.Close();
                            Console.WriteLine("写完了");


                            //else { 
                            //BinaryWriter writer = new BinaryWriter(networkStream);
                            //writer.Write("strReader");
                            //}
                            //定义流数据写入对象


                        }
                        catch
                        {
                            break;
                        }
                    }

                }
                catch
                {
                    break;
                }
            }

        }



    }
}
