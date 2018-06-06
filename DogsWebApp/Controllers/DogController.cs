using DogsWebApp.DAL;
using DogsWebApp.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DogsWebApp.Controllers
{
    public class DogController : Controller
    {
        private DogsContext db = new DogsContext();

        // GET: Dog
        public ActionResult Index()
        {
            return View(db.Dogs.ToList());
        }

        // GET: Dog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        // GET: Dog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        // POST: Dog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dog dog, string submitDetails)
        {
            if (submitDetails.Equals("Back"))
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                // if the dog has sub types then add the dog object to the type since it won't get it from the model and then make sure the status is changed
                // so that it will be saved 
                if (dog.Types != null)
                {
                    dog.Types.ForEach(t =>
                    {
                        t.Dog = dog;
                        db.Entry(t).State = EntityState.Modified;
                    });

                    // removes any types from the list where the type is empty
                    var emptyTypes = dog.Types.Where(t => string.IsNullOrEmpty(t.Type));
                    if (emptyTypes != null)
                        db.BreedTypes.RemoveRange(emptyTypes);
                }

                db.Entry(dog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dog);
        }

        // GET: Dog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dog dog = db.Dogs.Find(id);
            if (dog == null)
            {
                return HttpNotFound();
            }
            return View(dog);
        }

        // POST: Dog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dog dog = db.Dogs.Find(id);
            db.Dogs.Remove(dog);
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
