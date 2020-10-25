using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

using System.Text;
public class SendMessage : MonoBehaviour
{
  public  ClientIO io;
    public string msg;
    TcpClient tcpClient;
    NetworkStream networkStream;
    public InputField input;
    public void Init()
    {
        //创建一个TcpClient对象，自动分配主机IP地址和端口号  
        tcpClient = new TcpClient();
        //连接服务器，其IP和端口号为127.0.0.1和888  
        tcpClient.Connect("127.0.0.1", 888);
        networkStream = tcpClient.GetStream();
        
    }
    public void Send(string message)
    {
        if (tcpClient != null)//判断是否连接成功
        {
            //定义流数据写入对象
            BinaryWriter writer = new BinaryWriter(networkStream);

            string localip = "127.0.0.1";//存储本机IP，默认值为127.0.0.1
                                         //获取所有IP地址
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in ips)
            {
                if (!ip.IsIPv6SiteLocal)//如果不是IPV6地址
                    localip = ip.ToString();//获取本机IP地址
            }
            writer.Write(message);//向服务器发送消息  
         
        }
    }




    
    public void Receive()
    {
       
        Debug.Log(networkStream.DataAvailable.ToString());
        BinaryReader reader = new BinaryReader(networkStream);
        byte[] recvBuf = new byte[102400];
        networkStream.ReadTimeout = 2000;


         int bytesRead = networkStream.Read(recvBuf, 0,102400);  //从流中获取数据,阻塞方法(如果没有数据,将阻塞线程)

        if (bytesRead > 0)
        {

            string message = Encoding.UTF8.GetString(recvBuf);
            //string[] msgs= io.SplitMsg(message);


            // io.FileWrite(msgs[0], msgs[1], msgs[2]);
            //string message = reader.ReadString();
            Debug.Log("收到服务器消息:" + message);
            string V = io.SplitMsg(message)[0];
            string S= io.SplitMsg(message)[1];
            string O = io.SplitMsg(message)[2];
            Debug.Log("%%%%%%%%%%%%5"+O);


            io.FileWrite(V, S, O);

            //  string msg=message.Substring(0,20);


            //int Vs=msg.IndexOf("$");

            //int Ve = msg.IndexOf("$", Vs+1);

            //int V = Convert.ToInt32(msg.Substring(Vs + 1, Ve - Vs-1));

            //int Ss = msg.IndexOf("$", Ve+1);
            //int Se = msg.IndexOf("$", Ss+1);

            //int S = Convert.ToInt32(msg.Substring(Ss+1, Se - Ss-1));

            //int Os = msg.IndexOf("$", Se-5);
            //int Oe = msg.IndexOf("$", Os+1);


            //int O = Convert.ToInt32(msg.Substring(Os + 1, Oe - Os - 1));

            //string tosize="$"+V.ToString() + "$" +   S.ToString()+"$" + O.ToString()+"$";


            // io.FileWrite(message,V,S,O, tosize.Length);

            SceneManager.LoadScene(2);
        }

 

        //while (true)
        //{


        //    try
        //    {
        //    }
        //    catch(IOException e)
        //    {
        //        // reader.Close();
        //         Debug.Log(e+"...");
        //        break;
        //    }

        //}
    }
    public void MsgInput()
    {

        msg = input.text;

        Send(msg);
    }
        
        
       
    
    private void Start()
    {
        Init();

    }
}

