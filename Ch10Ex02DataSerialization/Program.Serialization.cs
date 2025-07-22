using System.Data.SqlTypes;
using System.Xml;
using System.Xml.Serialization;

delegate void WriteDelegate(string name, string? value);
partial class Program
{
    static void SerializingToXml(IQueryable<Category> categories, bool useAttributes = true)
    {
        string name = "file.xml";
        string path = Path.Combine(Environment.CurrentDirectory, name);


        using (FileStream stream = File.Create(path))
        {
            using (XmlWriter xml = XmlWriter.Create(stream))
            {
                WriteDelegate writeMethod;

                if (useAttributes)
                {
                    writeMethod = xml.WriteAttributeString;
                }
                else
                {
                    writeMethod = xml.WriteElementString;
                }
                xml.WriteStartDocument();
                xml.WriteStartElement("Categories");
                foreach (Category c in categories)
                {
                    xml.WriteStartElement("Category");
                    writeMethod("id", c.CategoryId.ToString());
                    writeMethod("name", c.CategoryName);
                    writeMethod("desc", c.Description);
                    writeMethod("product_count", c.Products.Count.ToString());
                    xml.WriteStartElement("Products");
                    foreach (Product p in c.Products)
                    {
                        xml.WriteStartElement("Product");

                        writeMethod("id", p.ProductId.ToString());
                        writeMethod("name", p.Name);
                        writeMethod("cost", p.Cost.ToString());
                        writeMethod("stock", p.Count.ToString());
                        writeMethod("discontinued", p.ProductId.ToString());

                        xml.WriteEndElement(); //product
                    }

                    xml.WriteEndElement(); // products
                    xml.WriteEndElement(); // category
                }
                xml.WriteEndElement();
            }

        }

    }

    static void XmlSerialize(Product product)
    {
        string name = "file.xml";
        string path = Path.Combine(Environment.CurrentDirectory, name);
        XmlSerializer xs = new(typeof(Product), new[] { product.GetType() });
        using (FileStream stream = File.Create(path))
        {
            xs.Serialize(stream, product);
        }
    }
}