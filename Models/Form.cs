using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManager.Models
{
    public class Form
    {
        public Customer customer { get; set; }
        [DisplayName("Invoice Date")]
        public string invoiceDate { get; set; }
        public InvoiceDetail detail { get; set; }
    }
}
