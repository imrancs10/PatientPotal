using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace PatientPortal.Infrastructure.Utility
{
    public class Serializer
    {
        public T Deserialize<T>(string input, string rootElementName) where T : class
        {
            //XmlSerializer ser = new XmlSerializer(typeof(T));

            //using (StringReader sr = new StringReader(input))
            //{
            //    return (T)ser.Deserialize(sr);
            //}

            var stringReader = new System.IO.StringReader(input);
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = rootElementName;
            xRoot.IsNullable = true;
            XmlSerializer xs = new XmlSerializer(typeof(T), xRoot);
            return xs.Deserialize(stringReader) as T;
        }

        public string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }
    }
}