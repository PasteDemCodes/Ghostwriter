using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ghostwriter.Models
{
    public class Order
    {
        public string OrderNumber { get; set; }
        public string Title { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}