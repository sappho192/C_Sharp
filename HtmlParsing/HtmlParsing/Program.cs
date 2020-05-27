using System;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace HtmlParsing
{
    class Program
    {
        private static string loadHtml(string path)
        {
            // 웹 클라이언트 객체 생성
            WebClient client = new WebClient();
            // 주소를 전달하여 내장된 메소드로 문자열 추출
            string code = client.DownloadString(path);
            Console.WriteLine(code);
            return code;
        }

        private static void extractElement
            (string htmlCode, string rootXPath, Dictionary<string, string> targetXPathList)
        {
            HtmlAgilityPack.HtmlDocument mydoc = new HtmlAgilityPack.HtmlDocument();
            mydoc.LoadHtml(htmlCode);
            // div 안에 있는 모든것
            var rootNode = mydoc.DocumentNode.SelectSingleNode(rootXPath);

            foreach (var targetXPath in targetXPathList)
            {   /* 
                div 태그로 묶여있는 것을 nodes로 뽑아내고,
                여러개일 경우 그 요소들에서 해당경로를 SelectSingNode를 이용하여
                경로에대한 node 생성수 Innertext를 이용하여 문자열을 추출한다.
                */
                string content = rootNode.SelectSingleNode(targetXPath.Value).InnerHtml;
                Console.WriteLine($"[{targetXPath.Key} 추출]\n{content}");
            }

        }
        static void Main(string[] args)
        {
            string path = @"http://example.com/";

            string rootXPath = @"/html[1]/body[1]/div[1]";
            var targetPathList = new Dictionary<string, string>();
            targetPathList.Add(@"제목", @"/html[1]/body[1]/div[1]/h1[1]");
            targetPathList.Add(@"내용", @"/html[1]/body[1]/div[1]/p[1]");

            string htmlCode = loadHtml(path);
            extractElement(htmlCode, rootXPath, targetPathList);
        }
    }
}
