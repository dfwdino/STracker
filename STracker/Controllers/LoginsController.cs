using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using STracker;

namespace STracker.Controllers
{
    public class LoginsController : Controller
    {
        private STrackerEntities db = new STrackerEntities();

        // GET: Logins
        public ActionResult Index()
        {
            return View(db.Logins.ToList());
        }

        // GET: Logins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // GET: Logins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Login login)
        {
            if (ModelState.IsValid)
            {
                AesManaged aes = new AesManaged();
                aes.GenerateKey();
                aes.GenerateIV();

                string hash, salt;

                GenerateSaltedHash(login.Password, out hash, out salt);

                login.Hash = hash;
                login.Salt = salt;

                db.Logins.Add(login);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(login);
        }

        public void GenerateSaltedHash(string password, out string hash, out string salt){
            var saltBytes = new byte[64];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            salt = Convert.ToBase64String(saltBytes);
            var rfc289 = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            hash = Convert.ToBase64String(rfc289.GetBytes(256));
        }


        // GET: Logins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Logins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Deleted,UserName,Password,FullName")] Login login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(login);
        }

        // GET: Logins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Login login = db.Logins.Find(id);
            if (login == null)
            {
                return HttpNotFound();
            }
            return View(login);
        }

        // POST: Logins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Login login = db.Logins.Find(id);
            db.Logins.Remove(login);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
