using System;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Linq;
using System.Xml;

namespace HtmlParsing
{
    class Program

    {
        private static string loadHtml(string path)
        {
            WebClient client = new WebClient(); // 웹 클라이언트 객체 생성
            string code = client.DownloadString(path); // 주소를 전달하여 내장된 메소드로 문자열 추출
            Console.WriteLine(code);
            return code;
        }

        private static void extractElement(string htmlCode)
        {
            HtmlAgilityPack.HtmlDocument mydoc = new HtmlAgilityPack.HtmlDocument();
            mydoc.LoadHtml(htmlCode);
            String xpath = "/html[1]/body[1]/div[1]"; // div안에 있는 모든것
            HtmlAgilityPack.HtmlNodeCollection nodes = mydoc.DocumentNode.SelectNodes(xpath);


            foreach (HtmlNode node in nodes)
            {   /* 
                div 태그로 묶여있는 것을 nodes로 뽑아내고 여러개일 경우 그 요소들에서 해당경로를
                SelectSingNode를 이용하여 경로에대한 node 생성수 Innertext를 이용하여 문자열을 추출한다.
                */
                String title = node.SelectSingleNode("/html[1]/body[1]/div[1]/h1[1]").InnerHtml;
                String text = node.SelectSingleNode("/html[1]/body[1]/div[1]/p[1]").InnerHtml;

                Console.WriteLine("제목 추출");
                Console.WriteLine(title);
                Console.WriteLine("텍스트 추출");
                Console.WriteLine(text);

            }

        }
        static void Main(string[] args)
        {
            String path = "http://example.com/";
            string htmlCode = loadHtml(path);
            extractElement(htmlCode);
        }
    }
}
