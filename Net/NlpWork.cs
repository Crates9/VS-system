using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baidu.Aip.Nlp;
using System.Text.RegularExpressions;


namespace ReNetDemo
{
    class NlpWork
    {
        public static string API_KEY = "GyBlVd1dvhUii5FckIF7g5ez";
        public static string SECRET_KEY = "BdV2chM75lQrDlaWGhR71cgYH21G0BGT ";
        public Nlp client = new Baidu.Aip.Nlp.Nlp(API_KEY, SECRET_KEY);
        public string DepParserDemo(string input)
        {
            var text = input;

            var options = new Dictionary<string, object> { { "mode", 0 } };

            var result = client.DepParser(text, options);

            return result.ToString();
        }

        public struct DepNode
        {
            public int head;//	头节点
            public string word;//词	
            public int id;//节点编号
            public string deprel;//关系
        };
        private DepNode[] Short2Struct(string input)
        {
            int index = 1;
            string[] baiDuApiJson = new string[20];
            DepNode[] depNode = new DepNode[baiDuApiJson.Length];


            Regex rege = new Regex("{", RegexOptions.Compiled);
            int count = rege.Matches(input.ToString()).Count - 1;//词向量个数，第一个多余
            for (int i = 0; i < count; i++)
            {
                //{}中内容新建字串
                int IndexofA = input.ToString().IndexOf("{", index);
                int IndexofB = input.ToString().IndexOf("}", IndexofA);
                index = IndexofB;
                //截取位数字符串
                baiDuApiJson[i] = input.ToString().Substring(IndexofA, IndexofB - IndexofA + 1);
                // Console.WriteLine(baiDuApiJson[i] + "@@@@@@@@@@@@");

            }

            //小串变结构
            for (int i = 0; i < count; i++)
            {

                int idc = baiDuApiJson[i].IndexOf("head") + 7;
                int idd = baiDuApiJson[i].IndexOf(",", idc);
                int idf = baiDuApiJson[i].IndexOf("word") + 8;
                int idg = baiDuApiJson[i].IndexOf(",", idf) - 1;
                int idh = baiDuApiJson[i].IndexOf("id") + 5;
                int idi = baiDuApiJson[i].IndexOf(",", idh);
                int idj = baiDuApiJson[i].IndexOf("deprel") + 10;
                int idk = baiDuApiJson[i].LastIndexOf("\"");


                depNode[i].head = Convert.ToInt32(baiDuApiJson[i].Substring(idc, idd - idc));
                depNode[i].word = baiDuApiJson[i].Substring(idf, idg - idf);
                depNode[i].id = Convert.ToInt32(baiDuApiJson[i].Substring(idh, idi - idh));
                depNode[i].deprel = baiDuApiJson[i].Substring(idj, idk - idj);

            }
            return depNode;
        }

        public string[] DepResultHandle(string input)
        {
            string myJson = "";
            DepNode[] depNode = Short2Struct(input);
            string[] result = new string[20];

            foreach (DepNode dep in depNode)
            {
                if (Equals(dep.deprel, "SBV") || Equals(dep.deprel, "HED") || Equals(dep.deprel, "VOB") ||
                    Equals(dep.deprel, "POB") || Equals(dep.deprel, "LOC") || Equals(dep.deprel, "BEI") ||
                    Equals(dep.deprel, "QUN") || Equals(dep.deprel, "COO"))
                {
                    myJson += dep.word;

                }

            }

            string myStr = DepParserDemo(myJson);

            DepNode[] Node = Short2Struct(myStr);

            foreach (DepNode d in Node)
            {
                if (d.id != 0)
                {
                    Console.WriteLine("head" + d.head);
                    Console.WriteLine("id" + d.id);
                    Console.WriteLine("word" + d.word);
                    Console.WriteLine("deprel" + d.deprel);
                    Console.WriteLine("!!!!!!!!!!!!11");
                }

            }
            int i = 0;
            for (; i < 3; )
            {
                if (Equals(Node[i].deprel, "HED") || Equals(Node[i].deprel, "BEI") || Equals(Node[i].deprel, "QUN") || Equals(Node[i].deprel, "COO"))
                {
                    result[0] = Node[i].word;
                    Console.WriteLine(i);

                    break;
                }
                else i++;
            }
            if (i == 0)
            { result[1] = Node[1].word; result[2] = Node[2].word; }
            if (i == 1)
            { result[1] = Node[0].word; result[2] = Node[2].word; }
            if (i == 2)
            {
                result[1] = Node[0].word; result[2] = Node[1].word;
            }

            Console.WriteLine("*************************");
            return result;
        }
    }
}
