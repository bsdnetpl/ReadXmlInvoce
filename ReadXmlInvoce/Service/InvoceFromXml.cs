using ReadXmlInvoce.DB;
using ReadXmlInvoce.Models;
using System.Xml;

namespace ReadXmlInvoce.Service
{
    public class InvoceFromXml : IInvoceFromXml
    {
        private readonly MssqlConnect _mssqlConnect;

        public InvoceFromXml(MssqlConnect mssqlConnect)
        {
            _mssqlConnect = mssqlConnect;
        }

        public bool ReadXml(string xmlFile)
        {

            string filePath = xmlFile;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);
            Invoce inv = new();
            Product product = new();
            
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("DokumentHandlowy");
            foreach (XmlNode node in nodeList)
            {


                XmlNode numerDokumentuNode = node.SelectSingleNode("NumerDokumentu");
                string numerDokumentu = numerDokumentuNode.InnerText;
                XmlNode dataSprzedazyNode = node.SelectSingleNode("DataSprzedazy");
                string dataSprzedazy = dataSprzedazyNode.InnerText;
               
                inv.numDock = numerDokumentu;
                inv.dateSell = dataSprzedazy;
                _mssqlConnect.Invoces.Add(inv);
                _mssqlConnect.SaveChanges();


                XmlNodeList elemList = xmlDoc.GetElementsByTagName("Towar");
                for (int i = 0; i < elemList.Count; i++)
                {
                    string kod = elemList[i].SelectSingleNode("Kod").InnerText;
                    string opis = elemList[i].SelectSingleNode("Nazwa/Opis").InnerText;
                    string stawkaVAT = elemList[i].SelectSingleNode("StawkaVAT").InnerText;
                    string ilosc = elemList[i].SelectSingleNode("//Ilosc").InnerText;
                    string cena = elemList[i].SelectSingleNode("//Cena").InnerText;
                    string wartosc = elemList[i].SelectSingleNode("//Wartosc").InnerText;
                    string wartoscVAT = elemList[i].SelectSingleNode("//WartoscVAT").InnerText;
                 


                    product.code = kod;
                    product.nameProduct = opis;
                    product.amount = Convert.ToDouble(ilosc);
                    product.price = Convert.ToDouble(cena);
                    product.taxVat = stawkaVAT;
                    product.value = Convert.ToDouble(wartosc);
                    product.taxVat = wartoscVAT;
                    product.lp = i++;
                   

                    _mssqlConnect.products.Add(product);
                    _mssqlConnect.SaveChanges();

                }
            }

            return true;
        }

    }
}
