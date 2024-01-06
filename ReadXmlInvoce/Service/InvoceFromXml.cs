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
            Invoice inv = new();
            List<Product> productList = new List<Product>();
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("DokumentHandlowy");

            foreach (XmlNode node in nodeList)
            {
                XmlNode numerDokumentuNode = node.SelectSingleNode("NumerDokumentu");
                string numerDokumentu = numerDokumentuNode.InnerText;
                XmlNode dataSprzedazyNode = node.SelectSingleNode("DataSprzedazy");
                string dataSprzedazy = dataSprzedazyNode.InnerText;

                inv.numDock = numerDokumentu;
                inv.dateSell = dataSprzedazy;

                XmlNodeList pozycjaNodes = xmlDoc.GetElementsByTagName("PozycjaDokumentu");

                foreach (XmlNode elem in pozycjaNodes)
                {
                    Product product = new Product(); // Tworzenie nowego obiektu Product
                    string lp = elem.SelectSingleNode("Lp").InnerText;
                    string kod = elem.SelectSingleNode("Towar/Kod").InnerText;
                    string opis = elem.SelectSingleNode("Towar/Nazwa/Opis").InnerText;
                    string stawkaVAT = elem.SelectSingleNode("Towar/StawkaVAT").InnerText;
                    string ilosc = elem.SelectSingleNode("Ilosc").InnerText;
                    string cena = elem.SelectSingleNode("Cena").InnerText;
                    string wartosc = elem.SelectSingleNode("Wartosc").InnerText;
                    string wartoscVAT = elem.SelectSingleNode("WartoscVAT").InnerText;
                    
                    product.lp = Convert.ToInt32(lp);
                    product.code = kod;
                    product.nameProduct = opis;
                    product.amount = Convert.ToDouble(ilosc);
                    product.price = Convert.ToDouble(cena);
                    product.taxVat = stawkaVAT;
                    product.value = Convert.ToDouble(wartosc);
                    product.valueTax = Convert.ToDouble(wartoscVAT);

                    productList.Add(product); // Dodanie nowego obiektu Product do listy
                }
            }

            inv.product = productList;

            _mssqlConnect.Invoices.Add(inv);
            _mssqlConnect.SaveChanges();
            return true;
        }

    }
}
