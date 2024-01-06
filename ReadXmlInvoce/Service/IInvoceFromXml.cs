using ReadXmlInvoce.Models;

namespace ReadXmlInvoce.Service
{
    public interface IInvoceFromXml
    {
        void DeleteProduct(int productId);
        bool EditProduct(Product products);
        List<Product> FindProductsByName(string productName);
        bool ReadXml(string xmlFile);
    }
}