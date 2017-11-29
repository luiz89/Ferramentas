using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GerarStubsJava
{
    class Program
    {
        static void Main(string[] args)
        {
            string saida = ExecutarCMD(comandoGeracaoClienteJava());

            File.WriteAllText("resultado.txt", saida);
        }



        private static string comandoGeracaoClienteJava()
        {
            Dictionary<string, string> dados = lendoArquivoConf();
            string caminhoSaida = dados["caminhoOutput"];
            string caminhoPackage = dados["caminhoPackage"]; ;
            string caminhoWsdl = dados["caminhoWsdl"]; ;

            StringBuilder sb = new StringBuilder();
            sb.Append("wsimport -s")
            .Append(" ").Append(caminhoSaida)
            .Append(" ").Append("-extension -verbose -keep")
            .Append(" ").Append("-p")
            .Append(" ").Append(caminhoPackage)
            .Append(" ").Append("-wsdllocation")
            .Append(" ").Append(caminhoWsdl).Append(" ").Append(caminhoWsdl)
            .Append(" ").Append("-B-XautoNameResolution");

            return sb.ToString();

        }

        private static Dictionary<string, string> lendoArquivoConf()
        {
            Dictionary<string, string> dados = new Dictionary<string, string>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"dadosGeracao.xml");
            XmlNode titleNode = xmlDoc.SelectSingleNode("//dados//caminhoOutput");
            if (titleNode != null) { 
                dados.Add("caminhoOutput", titleNode.InnerText);
            }

            titleNode = xmlDoc.SelectSingleNode("//dados//caminhoPackage");
            if (titleNode != null) { 
                dados.Add("caminhoPackage", titleNode.InnerText);
            }

            titleNode = xmlDoc.SelectSingleNode("//dados//caminhoWsdl");
            if (titleNode != null) { 
                dados.Add("caminhoWsdl", titleNode.InnerText);
            }


            return dados;
        }

        public static string ExecutarCMD(string comando)
        {
            using (Process processo = new Process())
            {
                processo.StartInfo.FileName = Environment.GetEnvironmentVariable("comspec");

                // Formata a string para passar como argumento para o cmd.exe
                processo.StartInfo.Arguments = string.Format("/c {0}", comando);

                processo.StartInfo.RedirectStandardOutput = true;
                processo.StartInfo.UseShellExecute = false;
                processo.StartInfo.CreateNoWindow = true;

                processo.Start();
                processo.WaitForExit();

                string saida = processo.StandardOutput.ReadToEnd();
                return saida;
            }
        }

    }
}
