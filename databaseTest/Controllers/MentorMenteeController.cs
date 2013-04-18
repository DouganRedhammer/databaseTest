using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using databaseTest.Models;

namespace databaseTest.Controllers
{
    public class MentorMenteeController : Controller
    {
        private MentorMenteeContext db = new MentorMenteeContext();

        //
        // GET: /MentorMentee/

        public ActionResult Index()
        {
            return View(db.MentorMentees.ToList());
        }

        //
        // GET: /MentorMentee/Details/5

        public ActionResult Details(int id = 0)
        {
            MentorMentee mentormentee = db.MentorMentees.Find(id);
            if (mentormentee == null)
            {
                return HttpNotFound();
            }
            return View(mentormentee);
        }

        //
        // GET: /MentorMentee/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MentorMentee/Create

        [HttpPost]
        public ActionResult Create(MentorMentee mentormentee)
        {
            if (ModelState.IsValid)
            {
                db.MentorMentees.Add(mentormentee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mentormentee);
        }

        //
        // GET: /MentorMentee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            MentorMentee mentormentee = db.MentorMentees.Find(id);
            if (mentormentee == null)
            {
                return HttpNotFound();
            }
            return View(mentormentee);
        }

        //
        // POST: /MentorMentee/Edit/5

        [HttpPost]
        public ActionResult Edit(MentorMentee mentormentee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mentormentee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mentormentee);
        }

        //
        // GET: /MentorMentee/Delete/5

        public ActionResult Delete(int id = 0)
        {
            MentorMentee mentormentee = db.MentorMentees.Find(id);
            if (mentormentee == null)
            {
                return HttpNotFound();
            }
            return View(mentormentee);
        }

        //
        // POST: /MentorMentee/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            MentorMentee mentormentee = db.MentorMentees.Find(id);
            db.MentorMentees.Remove(mentormentee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}