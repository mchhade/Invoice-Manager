using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManager.Models
{
    public class Customer
    {
        public Customer()
        {

        }
        [Key]
        public int id { get; set; }
        [DisplayName("Customer name")]
        [Required(ErrorMessage = "Customer name is Required")]
        public string name { get; set; }
        [Required(ErrorMessage = "Address Customer is Required")]
        [DisplayName("Customer Address")]
        public string address { get; set; }
        [Required(ErrorMessage = "Phone Customer is Required")]
        [DisplayName("Customer phone")]
        public string phoneNumber {get;set;}
        
    }
}
