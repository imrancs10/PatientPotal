using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace PatientPortal.Models
{
    [XmlObjectWrapper(ElementName = "allCustomers")]
    public class MyCustomer
    {
        public string NameAndLastName { get; set; }
        public int Age { get; set; }
        public Double Height { get; set; }
        public Boolean Active { get; set; }

        [XmlElement(ElementName = "WEEKLY_MONEY")]
        public Decimal Allowance { get; set; }

        public MyCustomer ReferredBy { get; set; }
    }
    [XmlRoot(ElementName = "allCustomers")]
    public class ListOfCustomers : List<MyCustomer>
    {
    }
}