using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using LeitorNfe.Outros;
using LeitorNfe.Impostos;

namespace LeitorNfe.Classe
{
    public enum XmlNfeTag
    {
        ide,
        emit,
        dest,
        retirada,
        entrega,
        det,
        total,
        transp,
        infAdic
    }

    public class Layout
    {
        //Objeto de cada etapa de leitura do XML
            private Ide objIde;
            private Emit objEmit;
            private Dest objDest;
            private Retirada objRet;
            private Entrega objEntr;
            //private Prod objProd;
            private Transp objTransp;
            private InfAdic objInfAdic;
            private Tributos objTributo;
            private TribTotal objTotal;

        //Lista de objetos de cada tributo da NFE
            private List<ICMS> listaICMS;
            private List<PIS> listaPIS;
            private List<COFINS> listaCOFINS;
            private List<Prod> listaProd;

        //Lista com o ResultadoFinal da leitura do TXT
            private List<TXT> listaTxt;
            private List<NFE> listaNfe;

        //Lista responsavel por varrer as tags do XML
            private List<XmlNodeList> listaXml;
      
        //Outras variaveis
            private XmlDocument xmlDocument;
            private XmlNode xmlNode;
            private int countXmlTag;
            private int countXmlNodes;

        public Layout()
        {
            //Objetos e Lista responsavel pela filtragem de tributos dentro da tag imposto
                objTributo = new Tributos();
                listaICMS = new List<ICMS>();
                listaPIS = new List<PIS>();
                listaCOFINS = new List<COFINS>();
                listaProd = new List<Prod>();

            //Objetos XML
                objIde  = new Ide();
                objEmit = new Emit();
                objDest = new Dest();
                objRet  = new Retirada();
                objEntr = new Entrega();
                //objProd = new Prod();
                objTransp = new Transp();
                objInfAdic = new InfAdic();
                objTotal = new TribTotal();
                
            //Lista com tags XML
                listaXml = new List<XmlNodeList>();

            //Lista de Resultado
                listaTxt = new List<TXT>();
                listaNfe = new List<NFE>();
           
            //Outras variaveis
                xmlDocument = new XmlDocument(); 
                countXmlTag = 0;
                countXmlNodes = 0;
        }
        
        public List<TXT> BuildTxt(string path)
        {
            CultureInfo c1 = new CultureInfo("pt-BR");
            StreamReader arquivo = new StreamReader(path, Encoding.GetEncoding(c1.TextInfo.ANSICodePage));

            string linha = "";

            listaTxt.Clear();

            while (true)
            {
                linha = arquivo.ReadLine();

                if (linha != null)
                {
                    string[] DadosColetados = linha.Split('|');

                    listaTxt.Add(new TXT()
                    {
                        Id =  string.IsNullOrEmpty(DadosColetados[0]) ? 0 : Convert.ToInt32(DadosColetados[0]),
                        Nome = string.IsNullOrEmpty(DadosColetados[1]) ? "Vazio" : DadosColetados[1],
                        Idade = string.IsNullOrEmpty(DadosColetados[2]) ? 0 : Convert.ToInt32(DadosColetados[2]),
                        Endereço = string.IsNullOrEmpty(DadosColetados[3]) ? "Vazio" : DadosColetados[3],
                        Salario = string.IsNullOrEmpty(DadosColetados[4]) ? 0 : Convert.ToDecimal(DadosColetados[4])
                    });
                    
                }
                else
                    break;
            }

            return listaTxt;
        }

        public List<NFE> BuildXml(string path)
        {
            listaXml.Clear();

            xmlDocument.Load(path);

            foreach(string nfeTag in Enum.GetNames(typeof(XmlNfeTag)))
            {
                countXmlTag = xmlDocument.GetElementsByTagName(nfeTag).Count;

                if(countXmlTag > 0)
                    listaXml.Add(xmlDocument.GetElementsByTagName(nfeTag));
            }

            listaNfe = PrintXml(listaXml);

            return listaNfe;
        }

        protected List<NFE> PrintXml(List<XmlNodeList> pListaXml)
        {

            foreach (XmlNodeList xnl in pListaXml)
            {
                foreach (XmlElement nodo in xnl)
                {
                    if (nodo.Name == "ide") {
                         objIde.cUF = string.IsNullOrEmpty(nodo.GetElementsByTagName("cUF")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("cUF")[0].InnerText.Trim();
                         objIde.cNF = string.IsNullOrEmpty(nodo.GetElementsByTagName("cNF")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("cNF")[0].InnerText.Trim();
                         objIde.natOp = string.IsNullOrEmpty(nodo.GetElementsByTagName("natOp")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("natOp")[0].InnerText.Trim();
                         objIde.indPag = string.IsNullOrEmpty(nodo.GetElementsByTagName("indPag")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("indPag")[0].InnerText.Trim();
                         objIde.mod = string.IsNullOrEmpty(nodo.GetElementsByTagName("mod")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("mod")[0].InnerText.Trim();
                         objIde.serie = string.IsNullOrEmpty(nodo.GetElementsByTagName("serie")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("serie")[0].InnerText.Trim();
                         objIde.nNF = string.IsNullOrEmpty(nodo.GetElementsByTagName("nNF")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("nNF")[0].InnerText.Trim();
                         objIde.dEmi = string.IsNullOrEmpty(nodo.GetElementsByTagName("dEmi")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("dEmi")[0].InnerText.Trim();
                         objIde.dSaiEnt = string.IsNullOrEmpty(nodo.GetElementsByTagName("dSaiEnt")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("dSaiEnt")[0].InnerText.Trim();
                         objIde.tpNF = string.IsNullOrEmpty(nodo.GetElementsByTagName("tpNF")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("tpNF")[0].InnerText.Trim();
                         objIde.cMunFG = string.IsNullOrEmpty(nodo.GetElementsByTagName("cMunFG")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("cMunFG")[0].InnerText.Trim();
                         objIde.tpImp = string.IsNullOrEmpty(nodo.GetElementsByTagName("tpImp")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("tpImp")[0].InnerText.Trim();
                         objIde.tpEmis = string.IsNullOrEmpty(nodo.GetElementsByTagName("tpEmis")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("tpEmis")[0].InnerText.Trim();
                         objIde.cDV = string.IsNullOrEmpty(nodo.GetElementsByTagName("cDV")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("cDV")[0].InnerText.Trim();
                         objIde.tpAmb = string.IsNullOrEmpty(nodo.GetElementsByTagName("tpAmb")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("tpAmb")[0].InnerText.Trim();
                         objIde.finNFe = string.IsNullOrEmpty(nodo.GetElementsByTagName("finNFe")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("finNFe")[0].InnerText.Trim();
                         objIde.procEmi = string.IsNullOrEmpty(nodo.GetElementsByTagName("procEmi")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("procEmi")[0].InnerText.Trim();
                         objIde.verProc = string.IsNullOrEmpty(nodo.GetElementsByTagName("verProc")[0].InnerText.Trim()) ? "Vazio" : nodo.GetElementsByTagName("verProc")[0].InnerText.Trim();
                    }

                    else if (nodo.Name == "emit") {
                        objEmit.CNPJ = nodo.GetElementsByTagName("CNPJ")[0].InnerText.Trim();
                        objEmit.xNome = nodo.GetElementsByTagName("xNome")[0].InnerText.Trim();
                        objEmit.xFant = nodo.GetElementsByTagName("xFant")[0].InnerText.Trim();
                        objEmit.xLgr = nodo.GetElementsByTagName("xLgr")[0].InnerText.Trim();
                        objEmit.nro = nodo.GetElementsByTagName("nro")[0].InnerText.Trim();
                        objEmit.xCpl = nodo.GetElementsByTagName("xCpl")[0].InnerText.Trim();
                        objEmit.xBairro = nodo.GetElementsByTagName("xBairro")[0].InnerText.Trim();
                        objEmit.cMun = nodo.GetElementsByTagName("cMun")[0].InnerText.Trim();
                        objEmit.xMun = nodo.GetElementsByTagName("xMun")[0].InnerText.Trim();
                        objEmit.UF = nodo.GetElementsByTagName("UF")[0].InnerText.Trim();
                        objEmit.CEP = nodo.GetElementsByTagName("CEP")[0].InnerText.Trim();
                        objEmit.cPais = nodo.GetElementsByTagName("cPais")[0].InnerText.Trim();
                        objEmit.xPais = nodo.GetElementsByTagName("xPais")[0].InnerText.Trim();
                        objEmit.fone = nodo.GetElementsByTagName("fone")[0].InnerText.Trim();
                        objEmit.IE = nodo.GetElementsByTagName("IE")[0].InnerText.Trim();
                    }

                    else if (nodo.Name == "dest") {
                        objDest.CNPJ = nodo.GetElementsByTagName("CNPJ")[0].InnerText.Trim();
                        objDest.xNome = nodo.GetElementsByTagName("xNome")[0].InnerText.Trim();
                        objDest.xLgr = nodo.GetElementsByTagName("xLgr")[0].InnerText.Trim();
                        objDest.nro = nodo.GetElementsByTagName("nro")[0].InnerText.Trim();
                        objDest.xCpl = nodo.GetElementsByTagName("xCpl")[0].InnerText.Trim();
                        objDest.xBairro = nodo.GetElementsByTagName("xBairro")[0].InnerText.Trim();
                        objDest.cMun = nodo.GetElementsByTagName("cMun")[0].InnerText.Trim();
                        objDest.xMun = nodo.GetElementsByTagName("xMun")[0].InnerText.Trim();
                        objDest.UF = nodo.GetElementsByTagName("UF")[0].InnerText.Trim();
                        objDest.CEP = nodo.GetElementsByTagName("CEP")[0].InnerText.Trim();
                        objDest.cPais = nodo.GetElementsByTagName("cPais")[0].InnerText.Trim();
                        objDest.xPais = nodo.GetElementsByTagName("xPais")[0].InnerText.Trim();
                        objDest.fone = nodo.GetElementsByTagName("fone")[0].InnerText.Trim();
                        objDest.IE = nodo.GetElementsByTagName("IE")[0].InnerText.Trim();
                     }

                    else if (nodo.Name == "retirada") {
                        objRet.CNPJ = nodo.GetElementsByTagName("CNPJ")[0].InnerText.Trim();
                        objRet.xLgr = nodo.GetElementsByTagName("xLgr")[0].InnerText.Trim();
                        objRet.nro  = nodo.GetElementsByTagName("nro")[0].InnerText.Trim();
                        objRet.xCpl = nodo.GetElementsByTagName("xCpl")[0].InnerText.Trim();
                        objRet.xBairro = nodo.GetElementsByTagName("xBairro")[0].InnerText.Trim();
                        objRet.cMun = nodo.GetElementsByTagName("cMun")[0].InnerText.Trim();
                        objRet.xMun = nodo.GetElementsByTagName("xMun")[0].InnerText.Trim();
                        objRet.UF = nodo.GetElementsByTagName("UF")[0].InnerText.Trim();
                    }

                    else if (nodo.Name == "entrega") {
                        objEntr.CNPJ = nodo.GetElementsByTagName("CNPJ")[0].InnerText.Trim();
                        objEntr.xLgr = nodo.GetElementsByTagName("xLgr")[0].InnerText.Trim();
                        objEntr.nro = nodo.GetElementsByTagName("nro")[0].InnerText.Trim();
                        objEntr.xCpl = nodo.GetElementsByTagName("xCpl")[0].InnerText.Trim();
                        objEntr.xBairro = nodo.GetElementsByTagName("xBairro")[0].InnerText.Trim();
                        objEntr.cMun = nodo.GetElementsByTagName("cMun")[0].InnerText.Trim();
                        objEntr.xMun = nodo.GetElementsByTagName("xMun")[0].InnerText.Trim();
                        objEntr.UF = nodo.GetElementsByTagName("UF")[0].InnerText.Trim();
                    }

                    else if (nodo.Name == "transp")
                    {
                        //Conta a quantidade de tags filhos dentro do pai
                        countXmlNodes = nodo.ChildNodes.Count;
                       
                        for(int i = 0; i < countXmlNodes; i++)
                        {
                            switch(nodo.ChildNodes[i].Name)
                            {
                                case "modFrete":
                                    objTransp.modFrete = nodo.GetElementsByTagName("modFrete")[0].InnerText.Trim();
                                break;

                                case "transporta":
                                    objTransp.transporta = new Transporte.Transporta() { 
                                        CNPJ = nodo.GetElementsByTagName("placa")[0].InnerText.Trim(),
                                        xNome = nodo.GetElementsByTagName("xNome")[0].InnerText.Trim(),
                                        IE = nodo.GetElementsByTagName("IE")[0].InnerText.Trim(),
                                        xEnder = nodo.GetElementsByTagName("xEnder")[0].InnerText.Trim(),
                                        xMun = nodo.GetElementsByTagName("xMun")[0].InnerText.Trim(),
                                        UF = nodo.GetElementsByTagName("UF")[0].InnerText.Trim()
                                    };
                                break;

                                case "veicTransp":
                                    objTransp.veicTransp = new Transporte.VeicTransp() { 
                                        placa = nodo.GetElementsByTagName("placa")[0].InnerText.Trim(), 
                                        RNTC = nodo.GetElementsByTagName("RNTC")[0].InnerText.Trim(), 
                                        UF = nodo.GetElementsByTagName("UF")[0].InnerText.Trim()
                                    };
                                break;

                                case "reboque":
                                    objTransp.reboque = new Transporte.Reboque() {
                                        placa = nodo.GetElementsByTagName("placa")[0].InnerText.Trim(),
                                        RNTC = nodo.GetElementsByTagName("RNTC")[0].InnerText.Trim(),
                                        UF = nodo.GetElementsByTagName("UF")[0].InnerText.Trim()
                                    };
                                break;
                                    
                                case "vol":
                                    objTransp.vol = new Transporte.Vol() {
                                        qVol = nodo.GetElementsByTagName("qVol")[0].InnerText.Trim(),
                                        esp = nodo.GetElementsByTagName("esp")[0].InnerText.Trim(),
                                        marca = nodo.GetElementsByTagName("marca")[0].InnerText.Trim(),
                                        nVol = nodo.GetElementsByTagName("nVol")[0].InnerText.Trim(),
                                        pesoL = nodo.GetElementsByTagName("pesoL")[0].InnerText.Trim(),
                                        pesoB = nodo.GetElementsByTagName("pesoB")[0].InnerText.Trim(),
                                        lacre = new Transporte.Lacres() {
                                            nLacre = nodo.GetElementsByTagName("nLacre")[0].InnerText.Trim()
                                        }
                                    };
                                break;
                            }
                        }
                    }

                    else if (nodo.Name == "infAdic") {
                        objInfAdic.infAdFisco = nodo.GetElementsByTagName("infAdFisco")[0].InnerText.Trim();
                    }

                    else if (nodo.Name == "det") {

                        //Conta a quantidade de tags filhos dentro do pai
                        countXmlNodes = nodo.ChildNodes.Count;

                        for (int i = 0; i < countXmlNodes; i++)
                        {
                            switch (nodo.ChildNodes[i].Name)
                            {
                                case "prod":
                                    listaProd.Add(new Prod() {                        
                                        cProd = nodo.GetElementsByTagName("cProd")[0].InnerText.Trim(),
                                        xProd = nodo.GetElementsByTagName("xProd")[0].InnerText.Trim(),
                                        CFOP = nodo.GetElementsByTagName("CFOP")[0].InnerText.Trim(),
                                        uCom = nodo.GetElementsByTagName("uCom")[0].InnerText.Trim(),
                                        qCom = nodo.GetElementsByTagName("qCom")[0].InnerText.Trim(),
                                        vUnCom = nodo.GetElementsByTagName("vUnCom")[0].InnerText.Trim(),
                                        vProd = nodo.GetElementsByTagName("vProd")[0].InnerText.Trim(),
                                        uTrib = nodo.GetElementsByTagName("uTrib")[0].InnerText.Trim(),
                                        qTrib = nodo.GetElementsByTagName("qTrib")[0].InnerText.Trim(),
                                        vUnTrib = nodo.GetElementsByTagName("vUnTrib")[0].InnerText.Trim()
                                    });
                                break;

                                case "imposto":

                                    xmlNode = nodo.ChildNodes[i];

                                    foreach (XmlElement novoNodo in xmlNode)
                                    {
                                        if (novoNodo.Name == "ICMS")
                                            listaICMS.AddRange(objTributo.TribICMS(novoNodo));

                                        else if (novoNodo.Name == "PIS")
                                            listaPIS.AddRange(objTributo.TribPIS(novoNodo));

                                        else if (novoNodo.Name == "COFINS")
                                            listaCOFINS.AddRange(objTributo.TribCOFINS(novoNodo));
                                    }

                                break;
                            }
                        }
                    }

                    else if(nodo.Name == "total")
                    {
                       //Conta a quantidade de tags filhos dentro do pai
                        countXmlNodes = nodo.ChildNodes.Count;

                        for (int i = 0; i < countXmlNodes; i++)
                        {
                            switch (nodo.ChildNodes[i].Name)
                            {

                                case "ICMSTot":

                                    objTotal.TotalICMS = new Total.ICMSTot()
                                    {
                                        vBC = nodo.GetElementsByTagName("vBC")[0].InnerText.Trim(),
                                        vICMS = nodo.GetElementsByTagName("vICMS")[0].InnerText.Trim(),
                                        vBCST = nodo.GetElementsByTagName("vBCST")[0].InnerText.Trim(),
                                        vST = nodo.GetElementsByTagName("vST")[0].InnerText.Trim(),
                                        vProd = nodo.GetElementsByTagName("vProd")[0].InnerText.Trim(),
                                        vFrete = nodo.GetElementsByTagName("vFrete")[0].InnerText.Trim(),
                                        vSeg = nodo.GetElementsByTagName("vSeg")[0].InnerText.Trim(),
                                        vDesc = nodo.GetElementsByTagName("vDesc")[0].InnerText.Trim(),
                                        vII = nodo.GetElementsByTagName("vII")[0].InnerText.Trim(),
                                        vIPI = nodo.GetElementsByTagName("vIPI")[0].InnerText.Trim(),
                                        vPIS = nodo.GetElementsByTagName("vPIS")[0].InnerText.Trim(),
                                        vCOFINS = nodo.GetElementsByTagName("vCOFINS")[0].InnerText.Trim(),
                                        vOutro = nodo.GetElementsByTagName("vOutro")[0].InnerText.Trim(),
                                        vNF = nodo.GetElementsByTagName("vNF")[0].InnerText.Trim()
                                    };

                                break;
                            }
                        }
                    }

                } //Fim do foreach
            } //Fim do foreach

                    //Montagem da NFE
                    listaNfe.Add(new NFE() {
                        ide = objIde,
                        emit = objEmit,
                        dest = objDest,
                        ret = objRet,
                        entr = objEntr,
                        imposto = new Imposto() { produto = listaProd, ICMS = listaICMS, PIS = listaPIS, COFINS = listaCOFINS },
                        transporte = new Transp() { modFrete = objTransp.modFrete, transporta = objTransp.transporta, veicTransp = objTransp.veicTransp, reboque = objTransp.reboque, vol = objTransp.vol },
                        infAdic = objInfAdic,
                        total = objTotal
                    });

                    //Zera a lista de tributos obtidos dentro da tag Impostos
                        listaICMS = new List<ICMS>();
                        listaPIS = new List<PIS>();
                        listaCOFINS = new List<COFINS>();
                        listaProd = new List<Prod>();

            return listaNfe;
        }
    }
}
