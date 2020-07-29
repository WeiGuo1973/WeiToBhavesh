using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Audit.DAL;
using Audit.Migrations;
using Audit.Models;
using Audit.ViewModel;
using System.Linq.Dynamic;


namespace Audit.Controllers
{
    public class StatusController : Controller
    {
        private ContactContext db = new ContactContext();

        // GET: Status
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GetList()
        {
            //Server Side Parameter
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            List<Contact> contList = new List<Contact>();

            contList = db.Contacts.ToList();
            int totalrows = contList.Count;
            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                contList = contList.
                    Where(x => x.CompanyName.ToLower().Contains(searchValue.ToLower()) || x.ContactPerson.ToLower().Contains(searchValue.ToLower()) || x.Email.ToLower().Contains(searchValue.ToLower()) || x.PhoneNumber.ToString().Contains(searchValue.ToLower()) || x.Status.GetDisplayName().ToLower().Contains(searchValue.ToLower())).ToList();
            }
            int totalrowsafterfiltering = contList.Count;
            //sorting
            contList = contList.OrderBy(sortColumnName + " " + sortDirection).ToList();

            //paging
            if (length > 0)
            {
                contList = contList.Skip(start).Take(length).ToList();
            }

            return Json(new
            {
                data = contList.Select(x => new
                {
                    ID = x.ID,
                    CompanyName = x.CompanyName,
                    ContactPerson = x.ContactPerson,
                    PhoneNumber = x.PhoneNumber,
                    //Email=x.Email,
                    LoadDate = x.LoadDate.ToString("MM/dd/yyyy hh:mm tt"),
                    Physicalyear = x.Physicalyear,
                    Status = x.Status.GetDisplayName()
                }),
                draw = Request["draw"],
                recordsTotal = totalrows,
                recordsFiltered = totalrowsafterfiltering
            }, JsonRequestBehavior.AllowGet); ;

        }

        // GET: Status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactViewModel contactViewModel = new ContactViewModel();
            contactViewModel.Contacts = db.Contacts.Find(id);
            contactViewModel.LoadFiles = db.LoadFiles.Where(s => s.CompanyID == id).ToList();
            if (contactViewModel.Contacts == null || contactViewModel.LoadFiles == null)
            {
                return HttpNotFound();
            }
            return View(contactViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(int? id, Status selector)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            contact.Status = selector;
            db.Entry(contact).State = EntityState.Modified;
            db.SaveChanges();            
            return RedirectToAction("Details", new { id = id });
        }

        //GET: Status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Contact contact = db.Contacts.Find(id);
            //if (contact == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(contact);
            Contact contact = db.Contacts.Find(id);
            List<LoadFiles> loadFiles = db.LoadFiles.Where(x => x.CompanyID == contact.ID).ToList();
            db.Contacts.Remove(contact);
            for (var i = 0; i < loadFiles.Count(); i++)
            {
                db.LoadFiles.Remove(loadFiles[i]);
            }
            db.SaveChanges();
            return RedirectToAction("index");
        }

        // POST: Status/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirm(int? id)
        //{

        //    Contact contact = db.Contacts.Find(id);
        //    List<LoadFiles> loadFiles = db.LoadFiles.Where(x => x.CompanyID == contact.ID).ToList();
        //    db.Contacts.Remove(contact);
        //    for (var i = 0; i < loadFiles.Count(); i++)
        //    {
        //        db.LoadFiles.Remove(loadFiles[i]);
        //    }
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
