using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerarClientJava
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

          private void button1_Click(object sender, EventArgs e)
        {
            string saida  = ExecutarCMD(comandoGeracaoClienteJava());

            File.WriteAllText("resultadoJava2.txt", saida);
        }

        private string comandoGeracaoClienteJava()
        {
            string caminhoSaida = output.Text;
            string caminhoPackage = package.Text;
            string caminhoWsdl = wsdl.Text;

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
