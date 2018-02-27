using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using LeitorNfe.Impostos;

namespace LeitorNfe.Classe
{
    public class Tributos
    {
        private List<ICMS> listaICMS { get; set; }
        private ICMS objICMS;

        private List<PIS> listaPIS { get; set; }
        private PIS objPIS;

        private List<COFINS> listaCOFINS { get; set; }
        private COFINS objCOFINS;

        private int countXmlNodes;

        public List<ICMS> TribICMS(XmlElement pNodo)
        {
            //Inicia um novo objeto e lista de tributos
                listaICMS = new List<ICMS>();

                objICMS = new ICMS();                  

                foreach (XmlElement novoNodo in pNodo)
                {
                    switch (novoNodo.Name)
                    {
                        case "ICMS00":
                            objICMS.orig = novoNodo.GetElementsByTagName("orig")[0].InnerText.Trim();                              
                        break;

                        case "ICMS40":
                            objICMS.orig = novoNodo.GetElementsByTagName("orig")[0].InnerText.Trim();
                        break;

                        case "ICMS50":
                            objICMS.orig = pNodo.GetElementsByTagName("orig")[0].InnerText.Trim();
                        break;

                        case "ICMS60":
                            objICMS.orig = pNodo.GetElementsByTagName("orig")[0].InnerText.Trim();
                        break;
                    }

                    listaICMS.Add(new ICMS() { tipo = novoNodo.Name, orig = objICMS.orig });
                }

            return listaICMS;
        }

        public List<PIS> TribPIS(XmlElement pNodo)
        {
            //Inicia um novo objeto e lista de tributos
            listaPIS = new List<PIS>();

            objPIS = new PIS();

            foreach (XmlElement novoNodo in pNodo)
            {
                switch (novoNodo.Name)
                {
                    case "PISAliq":
                        objPIS.CST = pNodo.GetElementsByTagName("CST")[0].InnerText.Trim();
                    break;

                }

                listaPIS.Add(new PIS()
                {
                    tipo = novoNodo.Name,
                    CST = objPIS.CST
                });
            }

            return listaPIS;
        }

        public List<COFINS> TribCOFINS(XmlElement pNodo)
        {
            //Inicia um novo objeto e lista de tributos
            listaCOFINS = new List<COFINS>();

            objCOFINS = new COFINS();

            foreach (XmlElement novoNodo in pNodo)
            {
                switch (novoNodo.Name)
                {
                    case "COFINSAliq":
                        objCOFINS.CST = pNodo.GetElementsByTagName("CST")[0].InnerText.Trim();
                        break;
                }

                listaCOFINS.Add(new COFINS()
                {
                    tipo = novoNodo.Name,
                    CST = objCOFINS.CST
                });
            }

            return listaCOFINS;
        }
    }
}
