using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManager.Models
{
    public class InvoiceDetail
    {
       
        public int id { get; set; }
        [DisplayName("Description")]
        [Required(ErrorMessage ="Description is Required")]
        public string description { get; set; }
        [Required(ErrorMessage ="Quantity is Required")]
        [DisplayName("Quantity")]
        [Range(1,int.MaxValue,ErrorMessage ="Quanitiy must be positive")]
        public int quantity { get; set; }
        [DisplayName("Unit Price")]
        [Required(ErrorMessage ="Unit Price is Required")]
        [Range(0,double.MaxValue,ErrorMessage ="Unit Price must be positive")]
        [ForeignKey("invoice")]
        public  int invoiceId { get; set; }
        public decimal unitPrice { get; set; }
        public InvoiceDetail(string description, int quantity, decimal unitPrice)
        {
            this.description = description;
            this.quantity = quantity;
            this.unitPrice = unitPrice;
        }
        public InvoiceDetail()
        {

        }
        public InvoiceDetail(int id,string description, int quantity, decimal unitPrice)
        {
            this.id = id;
            this.description = description;
            this.quantity = quantity;
            this.unitPrice = unitPrice;
        }
        public InvoiceDetail( string description, int quantity,decimal unitPrice,int invoiceId)
        {
            this.invoiceId = invoiceId;
            this.description = description;
            this.quantity = quantity;
            this.unitPrice = unitPrice;
        }
    }
}
