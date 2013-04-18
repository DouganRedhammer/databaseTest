using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using databaseTest.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Text;

namespace databaseTest.Controllers
{
    public class Default1Controller : Controller
    {
        private ChatStorageEntities1 db = new ChatStorageEntities1();

        //
        // GET: /Default1/

        public ActionResult Index()
        {
            return View(db.MessageStorages.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ActionResult Details(int id = 0)
        {
            MessageStorage messagestorage = db.MessageStorages.Find(id);
            if (messagestorage == null)
            {
                return HttpNotFound();
            }
            return View(messagestorage);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(MessageStorage messagestorage)
        {
            if (ModelState.IsValid)
            {
                db.MessageStorages.Add(messagestorage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(messagestorage);
        }

        //
        // GET: /Default1/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MessageStorage messagestorage = db.MessageStorages.Find(id);
            if (messagestorage == null)
            {
                return HttpNotFound();
            }
            return View(messagestorage);
        }

        //
        // POST: /Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(MessageStorage messagestorage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messagestorage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(messagestorage);
        }

        //
        // GET: /Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MessageStorage messagestorage = db.MessageStorages.Find(id);
            if (messagestorage == null)
            {
                return HttpNotFound();
            }
            return View(messagestorage);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MessageStorage messagestorage = db.MessageStorages.Find(id);
            db.MessageStorages.Remove(messagestorage);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        [HttpGet]
        public JsonResult Insert()
        {
            
            String msg = Request.QueryString["msg"];
            try
            {
                if (ModelState.IsValid)
                {
                    MessageStorage newMessage = new MessageStorage();
                    newMessage.message = msg;
                    newMessage.timeStamp = DateTime.Now.ToString("HH:mm:ss tt");
                    newMessage.userName = User.Identity.Name.ToString();
                    db.MessageStorages.Add(newMessage);
                    db.SaveChanges();
                    return Json(msg, JsonRequestBehavior.AllowGet);
                }

            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        /*
         *   this will return a json string of the message entities
         *   I need to somehow check for any new data since the last call or simply query it every time
         *   and have the jquery replace the entire text area every time this methond is called.
         */
        [HttpGet]
        public JsonResult List()
        {
            StringBuilder sBuilder = new StringBuilder();
            foreach (var item in db.MessageStorages)
            {
                sBuilder.Append(item.userName + ": " + item.timeStamp + "\n" + item.message + "\n\n");
            }
            return Json(sBuilder.ToString(), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult Authenticate()
        {
       
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ListMentors()
        {
            /*
            UsersContext db = new UsersContext();
            UserProfile userprofile = db.UserProfiles.Where(i => i.UserName == User.Identity.Name);
            MentorMenteeContext listMentors = new MentorMenteeContext();
            listMentors.MentorMentees.Where(i => i.MenteeId == userprofile.UserId);
            */
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }

}