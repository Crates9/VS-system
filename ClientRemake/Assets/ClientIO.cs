using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
public class ClientIO : MonoBehaviour
{

    public string[] SplitMsg(string message)
    {
        string[] result = new string[3];

        int a = message.IndexOf("%YAML 1.1");
        int b = message.IndexOf("%YAML 1.1", a + 100);
        int c = message.IndexOf("%YAML 1.1", b + 100);
        int d = message.LastIndexOf("}");
        result[0] = message.Substring(a, b - a);

        result[1] = message.Substring(b, c - b);

        result[2] = message.Substring(c,d-c+1);

        return result;
    }


    public void FileWrite(string V,string S,string O)
    {
        // //存储字节数据
        //char[] charData;//存储字符数据
       // int pos = size;
       

        try
        {
            
            FileStream vFile = new FileStream(@"Assets\Resources\Verb.prefab", FileMode.Create ,FileAccess.ReadWrite);
            StreamWriter Vwriter = new StreamWriter(vFile);

            // BinaryWriter Vbw = new BinaryWriter(aFile);
            // byte[] Vbyte = new byte[v];

            Vwriter.WriteLine(V);
            Vwriter.Close();
            FileStream sFile = new FileStream(@"Assets\Resources\Subject.prefab", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter Swriter = new StreamWriter(sFile);
            Swriter.WriteLine(S);
            Swriter.Close();

            FileStream oFile = new FileStream(@"Assets\Resources\Object.prefab", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter Owriter = new StreamWriter(oFile);
            Owriter.WriteLine(O);
            Owriter.Close();

            UnityEditor.AssetDatabase.Refresh();



            // 将文件指针移动到文件开始处
            //aFile.Seek(0, SeekOrigin.Begin);
            // aFile.Write(byteData, 0, byteData.Length);//写入数据


        }
        catch (IOException ex)
        {
            Debug.Log("发生IO异常!");
            Debug.Log(ex.ToString());
           
            return;
        }
    }

}
