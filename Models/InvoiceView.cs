using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManager.Models
{
    public class InvoiceView
    {
        public Customer customer { get; set; }
        [DisplayName("Invoice Id")]
        public int invoiceId { get; set; }
       
        public InvoiceView(int Customerid,string name,string address,string phoneNumber,int invoiceId)
        {
            this.customer = new Customer();
            this.customer.id = Customerid;
            this.customer.name = name;
            this.customer.phoneNumber = phoneNumber;
            this.customer.address = address;
            this.invoiceId = invoiceId;
       
        }
    }
}
