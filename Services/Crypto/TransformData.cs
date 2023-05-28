using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Services.Crypto
{
    public static class TransformData
    {
        public static Encoding Encoding
        {
            get
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                return Encoding.GetEncoding(1251);
            }
        }

        #region To<Type>
        public static IEnumerable<byte> ToByte(DataTable table)
        {
            using MemoryStream ms = new();
            table.WriteXml(ms);
            return ms.ToArray();
        }
        public static IEnumerable<byte> ToByte(string data)
        {
            return Encoding.GetBytes(data);
        }
        public static IEnumerable<byte> ToByte(IEnumerable<int> data)
        {
            return data.Select(el => (byte)el);
        }
        public static IEnumerable<int> ToInt(string data)
        {
            return Encoding.GetBytes(data).Select(i => (int)i);
        }
        public static IEnumerable<int> ToInt(IEnumerable<byte> data)
        {
            return data.Select(i => (int)i);
        }
        public static IEnumerable<int> ToInt(DataTable table)
        {
            return ToInt(ToByte(table));
        }
        public static string ToString(IEnumerable<byte> data)
        {
            return Encoding.GetString(data.ToArray());
        }
        public static DataTable ToDataTableFromCFT(IEnumerable<byte> data)
        {

            try
            {
                DataSet ds = new();
                using MemoryStream ms = new(data.ToArray());
                ds.ReadXml(ms);
                return ds.Tables[0];
            }
            catch
            {
                throw new Exception(
                    "Файл формата .ctf поврежден. " +
                    "Чтение данных не возможно.\n" +
                    "Попробуйте поменять теущие настройки преобразования.");
            }
        }
        public static XDocument ToXML<Key, Value>(Dictionary<Key, Value> elements, string rootName = "root") where Key:notnull
        {
            XDocument xdoc = new();
            XElement root = new(rootName);
            foreach (var el in elements)
            {
                XElement element = new("parameter");
                element.Add(new XAttribute("name", el.Key));
                element.Add(new XElement("value", el.Value));
                root.Add(element);
            }
            xdoc.Add(root);
            return xdoc;
        }
        public static Dictionary<string, string> ToDictionary(XDocument xdoc, string rootName = "root")
        {
            Dictionary<string, string> pairs = new();
            XElement? root = xdoc.Element(rootName);
            if (root == null) 
            { 
                throw new Exception($"Корневой элемент {rootName} не найден."); 
            }
            foreach (var el in root.Elements("parameter"))
            {
                pairs.Add(el.Attribute("name").Value, el.Element("value").Value);
            }
            return pairs;
        }
        #endregion
    }
}
