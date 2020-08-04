using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace InvoiceManager.Models
{
    public class Invoice
    {
        public Invoice()
        {
            
        }
        public Invoice(int customerId)
        {
            this.customerId = customerId;
        }
        [Key]
        public int id { get; set; }
        [ForeignKey("Customer")]
        public int customerId { get; set; }
        public List<InvoiceDetail> details { get; set; }
    }
}
