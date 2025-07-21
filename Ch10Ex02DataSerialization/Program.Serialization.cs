using System.Xml;
using System.Xml.Serialization;

partial class Program
{
    static void SerializingToXml(IQueryable<Product> products)
    {
        string name = "file.xml";
        string path = Path.Combine(Environment.CurrentDirectory, name);


        using (FileStream stream = File.Create(path))
        {
            using (XmlWriter xml = XmlWriter.Create(stream))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("Products");
                foreach (Product p in products)
                {
                    xml.WriteStartElement("Product");
                    xml.WriteElementString("ProductName", p.Name);
                    xml.WriteElementString("ProductCost", p.Cost.ToString());
                    xml.WriteElementString("ProductCategory", p.Category.CategoryName);
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
            }
            
        }

    }
}