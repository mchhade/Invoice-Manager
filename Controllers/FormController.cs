using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using InvoiceManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InvoiceManager.Controllers
{
   
    public class FormController : Controller
    {
        DbContextOptionsBuilder<ApplicationContext> optionsBuilder=new DbContextOptionsBuilder<ApplicationContext>();
        private ApplicationContext context;
        private DefaulConnection="Data Source=DESKTOP-EBPK77B\\SQLEXPRESS;Initial Catalog=Invoice;Integrated Security=True";
        static List<InvoiceDetail> details = new List<InvoiceDetail>();
        static List<InvoiceView> invoiceViews = new List<InvoiceView>();
       

        public static int id = 0;
        // GET: FormController
        [HttpPost]
        [HttpGet]
        public ActionResult Index()
        {

            optionsBuilder.UseSqlServer();
            context = new ApplicationContext(optionsBuilder.Options);

            invoiceViews = context.customers.Join(context.invoices,customer => customer.id,invoices => invoices.customerId,
                (customer, invoices) => new
               InvoiceView(customer.id,customer.name,customer.address, customer.phoneNumber,invoices.id)
             ).ToList();
            ViewBag.views = invoiceViews;
            return View();
        }
       
        // GET: FormController/Details/5
        public ActionResult Details(int id)
        {
            optionsBuilder.UseSqlServer(DefaulConnection);
            context = new ApplicationContext(optionsBuilder.Options);
            List<InvoiceDetail> invoiceDetails = new List<InvoiceDetail>();
            invoiceDetails = context.details.Where(m=>m.invoiceId == id).ToList();
            ViewBag.details = invoiceDetails;
            return View();
        }

        // GET: FormController/Create
        public ActionResult Create()
        {
            ViewBag.details = details;
            return View();
        }
        //Add Item to List details
        [HttpPost]
        public IActionResult addToList(InvoiceDetail detail)
        {
            ViewBag.details = details;
            if (ModelState.IsValid)
            {
                details.Add(new InvoiceDetail(id,detail.description,detail.quantity,detail.unitPrice));
                id++;
                return RedirectToAction("Create");
            }
            return RedirectToAction("Create");
        }

        // POST: FormController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult saveRecord(Customer customer)
        {
            if (ModelState.IsValid)
            {
                
                optionsBuilder.UseSqlServer(DefaulConnection);
                context = new ApplicationContext(optionsBuilder.Options);     
                context.customers.Add(customer);
                context.SaveChanges();
                Invoice invoice=new Invoice(customer.id);
                context.invoices.Add(invoice);
                context.SaveChanges();
                foreach(InvoiceDetail detail in details)
                {
                    InvoiceDetail invoiceDetail= new InvoiceDetail(detail.description,detail.quantity,detail.unitPrice,invoice.id);
                    context.details.Add(invoiceDetail);
                }
                context.SaveChanges();
                invoiceViews.Add(new InvoiceView(customer.id, customer.name, customer.address, customer.phoneNumber, invoice.id));
                    ViewBag.views = invoiceViews;
                    return View("Index");    
            }
            ViewBag.details = details;
            return View("Create");
        }

        // GET: FormController/Edit/5
        public ActionResult Edit(int id)
        {
            optionsBuilder.UseSqlServer(DefaulConnection);
            context = new ApplicationContext(optionsBuilder.Options);
            Customer customer = context.customers.Where(x=>x.id==id).First();
            return View(customer);
        }

        // POST: FormController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer1)
        {
            try
            {
                optionsBuilder.UseSqlServer(DefaulConnection);
                context = new ApplicationContext(optionsBuilder.Options);
                context.Update(customer1);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FormController/Delete/5
        public ActionResult DeleteView(int id)
        {
            optionsBuilder.UseSqlServer(DefaulConnection);
            context = new ApplicationContext(optionsBuilder.Options);
            Invoice invoice = context.invoices.Where(x => x.id == id).First();
            Customer customer = context.customers.Where(m => m.id == invoice.customerId).First();
            context.RemoveRange(context.details.Where(x=>x.invoiceId==id));
            context.Remove(invoice);
            context.Remove(customer);
            context.SaveChanges();
            for(int i = 0; i < invoiceViews.Count(); i++)
            {
                if (invoiceViews[i].invoiceId == id)
                    invoiceViews.RemoveAt(i);
            }
            ViewBag.views = invoiceViews;
            return RedirectToAction("Index");
        }

        // POST: FormController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Index");
            }
        }
        public IActionResult deleteFromList(int id)
        {
            for(var i=0;i<details.Count();i++)
            {
                if (details[i].id == id)
                    details.RemoveAt(i);
            }
            ViewBag.details = details;
            return View("Create");
        }
        public IActionResult resetList()
        {
            details = new List<InvoiceDetail>();
            ViewBag.details = details;
            return View("Create");
        }
      
     
        public IActionResult DeleteElement(InvoiceDetail detail1)
        {
            optionsBuilder.UseSqlServer(DefaulConnection);
            context = new ApplicationContext(optionsBuilder.Options);
            context.Remove(detail1);
            context.SaveChanges();
            ViewBag.details = details;
            return View("Details");
        }
        public IActionResult EditElement(int id)
        {
            optionsBuilder.UseSqlServer(DefaulConnection);
            context = new ApplicationContext(optionsBuilder.Options);
            InvoiceDetail detail2 = context.details.Where(x => x.id == id).First();
            ViewBag.details = details;
            return View(detail2);
            
        }
        [HttpPost]
        public IActionResult EditElement(InvoiceDetail detail)
        {
            optionsBuilder.UseSqlServer(DefaulConnection);
            context = new ApplicationContext(optionsBuilder.Options);
            context.Update(detail);
            context.SaveChanges();
            ViewBag.details = details;
            return RedirectToAction("details","Form",new { id = detail.invoiceId});
        }
    }
}
