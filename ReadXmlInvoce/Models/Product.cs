namespace ReadXmlInvoce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int lp { set; get; }
        public string code { set; get; }
        public string nameProduct { set; get; }
        public string taxVat { set; get; }
        public double amount { set; get; }
        public double price { set; get; }
        public double value { set; get; }
        public double valueTax { set; get; }
        public string invoceNumber { set; get; }
        public Invoice Invoice { set; get; }
    }
}
