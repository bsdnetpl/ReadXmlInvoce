using Microsoft.EntityFrameworkCore;
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
            // Retrieve the file path and load the XML document
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);
            // Create new instances for Invoice and Product list
            Invoice inv = new();
            List<Product> productList = new List<Product>();
            // Get all nodes with the tag name "DokumentHandlowy" from the XML document
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("DokumentHandlowy");

            foreach (XmlNode node in nodeList)
            {
                // Extract the invoice number and check if it already exists in the database
                XmlNode numerDokumentuNode = node.SelectSingleNode("NumerDokumentu");
                string numerDokumentu = numerDokumentuNode.InnerText;
                // Check if the invoice number already exists in the database
                if (_mssqlConnect.Invoices.Any(i => i.numDock == numerDokumentu))
                {
                    throw new Exception($"Faktura o numerze {numerDokumentu} już istnieje w bazie danych.");
                }
                // Retrieve the sales date information
                XmlNode dataSprzedazyNode = node.SelectSingleNode("DataSprzedazy");
                string dataSprzedazy = dataSprzedazyNode.InnerText;
                // Assign invoice details to the Invoice object
                inv.numDock = numerDokumentu;
                inv.dateSell = dataSprzedazy;
                // Get all nodes with the tag name "PozycjaDokumentu" from the XML document
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
                    // Assign extracted product details to the Product object
                    product.lp = Convert.ToInt32(lp);
                    product.code = kod;
                    product.nameProduct = opis;
                    product.amount = Convert.ToDouble(ilosc);
                    product.price = Convert.ToDouble(cena);
                    product.taxVat = stawkaVAT;
                    product.value = Convert.ToDouble(wartosc);
                    product.valueTax = Convert.ToDouble(wartoscVAT);
                    // Add the Product object to the list of products
                    productList.Add(product); 
                }
            }
            // Assign the list of products to the Invoice object
            inv.product = productList;
            // Add the Invoice object to the database and save changes
            _mssqlConnect.Invoices.Add(inv);
            _mssqlConnect.SaveChanges();
            // Return true to indicate successful XML processing
            return true;
        }

        public void DeleteProduct(int productId)
        {    // Find the product by its ID
            var product = _mssqlConnect.products.Find(productId);

            if (product != null)
            {        // Remove the product from the database and save changes
                _mssqlConnect.products.Remove(product);
                _mssqlConnect.SaveChanges();
            }
        }

        public List<Product> FindProductsByName(string productName)
        {
            // Search for products by exact name (case-sensitive)
            var foundProducts = _mssqlConnect.products.Where(p => p.nameProduct == productName).ToList();

            return foundProducts;
        }

        public bool EditProduct(Product products)
        {    // Find the product by its ID
            var product = _mssqlConnect.products.Find(products.Id);

            if (product != null)
            {
                // Update product fields with the provided values
                product.price = products.price;
                product.taxVat = products.taxVat;
                product.valueTax = products.valueTax;
                product.value = products.value;
                product.amount = products.amount;
                product.code = products.code;
                product.invoceNumber = products.invoceNumber;
                product.nameProduct = products.nameProduct;
                // Save changes to the database after updating the product
                _mssqlConnect.SaveChanges();
                return true; // Return true if the edit was successful
            }

            return false;  // Return false if the product with the given ID was not found
        }
    }

    }
}
