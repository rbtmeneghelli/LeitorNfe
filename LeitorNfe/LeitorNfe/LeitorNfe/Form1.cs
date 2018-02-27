using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Globalization;
using LeitorNfe.Classe;

namespace LeitorNfe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Objetos
                Layout objLayout = new Layout();
            
            //Variaveis
                string[] extensions = { ".xml", ".xmls", ".txt" };
                string path = ConfigurationManager.AppSettings["XmlPath"];
            
            //Lista 
                List<TXT> listaTxt = new List<TXT>();
                List<NFE> listaNfe = new List<NFE>();

            //Iniciando Objeto com diretorio
                DirectoryInfo diretorio = new DirectoryInfo(path);

            //FileInfo[] Arquivos = diretorio.GetFiles("*.xml");
                FileInfo[] Arquivos = diretorio.EnumerateFiles().Where(f => extensions.Contains(f.Extension.ToLower())).ToArray();

            //Leitura dos Arquivos no Looping
            foreach (var arq in Arquivos)
            {
                path = string.Format("{0}\\{1}", arq.DirectoryName, arq.Name);

                switch (arq.Extension.ToLower())
                {
                    case ".xml":
                        listaNfe = objLayout.BuildXml(path);

                        foreach (var item in listaNfe) {
                            MessageBox.Show(item.ide.cDV);
                        }

                    break;

                    case ".txt":
                        listaTxt = objLayout.BuildTxt(path);

                        foreach (var item in listaTxt) {
                            MessageBox.Show(item.Id.ToString());
                        }

                    break;
                }
            }
        }
    }
}
