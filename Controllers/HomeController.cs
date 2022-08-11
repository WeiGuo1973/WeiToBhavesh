using Audit.DAL;
using Audit.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Audit.Migrations;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Audit.ViewModel;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Audit.Controllers
{
    public class HomeController : Controller
    {
        private ContactContext db = new ContactContext();

        public ActionResult Index()
        {

            Contact contactView = new Contact();

            return View(contactView);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SaveData(ContactViewModel contactViewModel, HttpPostedFileBase[] overheadfile, HttpPostedFileBase[] certificationfile)
        public async Task<ActionResult> SaveData()
        {
            string CompanyName = Request.Form[0];
            string contactName = Request.Form[1];
            string email = Request.Form[2];
            string phonenumber = Request.Form[3];
            string revenue = Request.Form[4];
            string fiscalyear = Request.Form[5];
            //int overcount = int.Parse(Request.Form[6]);
            //int cercount = int.Parse(Request.Form[7]);

            if (CompanyName != null && contactName != null && email != null && phonenumber != null && revenue != null && fiscalyear != null && /*overcount != 0 && cercount != 0 && */Request.Files.Count > 0)
            {

                Contact Contacts = new Contact
                {
                    CompanyName = CompanyName,
                    ContactPerson = contactName,
                    Email = email,
                    PhoneNumber = phonenumber,
                    Revenue = (RevenueRange)Enum.Parse(typeof(RevenueRange), revenue),
                    Physicalyear = int.Parse(fiscalyear),
                    LoadDate = DateTime.Now,
                    Status = Status.AwaitingReview
                };

                try
                {                   
                    db.Contacts.Add(Contacts);
                    db.SaveChanges();
                                       
                    await SaveFileAPI(Contacts.ID.ToString());
                    await SendEmail(Contacts.CompanyName, Contacts.Email);

                    return Content("success");
                }
                catch (DbUpdateException ex)
                {
                    //SqlException innerException = ex.InnerException.InnerException as SqlException;
                    return Content("fail");
                }
            }

            return Content("fail");
        }

        public ActionResult DownloadFile(string FileItem)
        {

            string path = AppDomain.CurrentDomain.BaseDirectory + Constants.DownloadFilePath;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + FileItem);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, FileItem);
        }


        //private bool ValidateFile(HttpPostedFileBase file)
        //{
        //    string fileExtension = Path.GetExtension(file.FileName).ToLower();
        //    if ((file.ContentLength > 0 && file.ContentLength < 11242880))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private bool ValidateOverHeadFile(HttpPostedFileBase file)
        //{
        //    string filename = Path.GetFileName(file.FileName).ToLower();
        //    if (filename.Contains("overhead"))
        //    {

        //        return true;
        //    }
        //    var ii = filename.IndexOf("overhead");
        //    return false;

        //}

        //private bool CertificationFile(HttpPostedFileBase file)
        //{
        //    string filename = Path.GetFileName(file.FileName).ToLower();
        //    if (filename.Contains("certification"))
        //    {
        //        return true;
        //    }
        //    return false;

        //}

        private async Task<string> SaveFileAPI(string CompanyID)
        {
            string result = "";
            //HttpResponseMessage result = new HttpResponseMessage();
            using (var client = new HttpClient())
            {
                using (var content = new MultipartFormDataContent())
                {
                    content.Headers.Add("ApplicationName", "Audit");
                    content.Headers.Add("AppSpecificRecord", CompanyID);
                    content.Headers.Add("AppSpecificFlag", "0");
                    for (var i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i];
                        byte[] fileBytes = new byte[file.ContentLength + 1];
                        file.InputStream.Read(fileBytes, 0, fileBytes.Length);
                        var fileContent = new ByteArrayContent(fileBytes);
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = file.FileName };
                        content.Add(fileContent);
                    }
                    var requestUrl = "http://s-tmas-mvc01/FMS/api/StorageFile/";
                    var response = await client.PostAsync(requestUrl, content);

                    result = response.Content.ReadAsStringAsync().Result;
                }
            }
            return result;
        }

        //private string GetFileName(HttpPostedFileBase file)
        //{
        //string fileExtension = Path.GetExtension(file.FileName);
        //string fileMain = Path.GetFileNameWithoutExtension(file.FileName);
        //string FileDate = DateTime.Today.ToString("MM-dd-yy");
        //return (fileMain + "-" + companyName + "(" + FileDate + ")" + "." + fileExtension);

        //    string fileExtension = Path.GetExtension(file.FileName);
        //    string fileMain = Path.GetFileNameWithoutExtension(file.FileName);

        //    return (fileMain + "." + fileExtension);
        //}


        private async Task SendEmail(string companyName, string emailAddress)
        {
            //try
            //{
            //    MessageModel model = new MessageModel();
            //    model.To = emailAddress;
            //    model.Subject = "Audit forms from" + companyName;
            //    model.Body = "Audit forms from" + companyName;
            //    model.Email = "no-reply@la.gov";
            //    model.Password = "";
            //    using (MailMessage mm = new MailMessage(model.Email, model.To))
            //    {
            //        mm.Subject = model.Subject;
            //        mm.Body = model.Body;
            //        mm.IsBodyHtml = false;
            //        SmtpClient smtp = new SmtpClient();
            //        //smtp.Host = "smtp.gmail.com";
            //        smtp.Host = "smtp.gmail.com";
            //        smtp.EnableSsl = true;
            //        NetworkCredential NetworkCred = new NetworkCredential(model.Email, model.Password);
            //        smtp.UseDefaultCredentials = true;
            //        smtp.Credentials = NetworkCred;
            //        smtp.Port = 587;
            //        await Task.Run(() => smtp.Send(mm));
            //    }
            //}
            //catch (Exception em)
            //{
            //    em.ToString();
            // //   ViewBag.Message = "Sorry an error occurred sending the email, please try again";
            //}

            MailMessage msg = new MailMessage("no-reply@la.gov", emailAddress);
            msg.Body = "Audit forms from" + companyName;
            msg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("SMTPOUT.dotd.state.la.us");
            msg.Subject = "Audit forms from" + companyName;
            await Task.Run(() => client.Send(msg));
        }


    }
}



