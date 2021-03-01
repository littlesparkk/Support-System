using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Asp.NETMVCCRUD.Models;

namespace Asp.NETMVCCRUD.Controllers
{
    public class SupportStatusController : Controller
    {
        // GET: SupportStatus
        public ActionResult IndexSupportStatus()
        {
            return View();
        }
        public ActionResult GetSupportStatusData()
        {
            using (DBModel db = new DBModel())
            {
                List<SupportStat> supportList = db.SupportStats.ToList<SupportStat>();
                return Json(new { data = supportList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEditSupportStatus(int id = 0)
        {
            if (id == 0)
                return View(new SupportStat());

            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.SupportStats.Where(x => x.SupportStatusId == id).FirstOrDefault<SupportStat>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEditSupportStatus(SupportStat support)
        {
            using (DBModel db = new DBModel())
            {
                if (support.SupportStatusId == 0)
                {
                    db.SupportStats.Add(support);
                    db.SaveChanges();
                    //return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                    return View("IndexSupportStatus");
                }
                else
                {
                    db.Entry(support).State = EntityState.Modified;
                    db.SaveChanges();
                    //return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                    return View("IndexSupportStatus");
                }
            }

        }
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                SupportStat support = db.SupportStats.Where(x => x.SupportStatusId == id).FirstOrDefault<SupportStat>();
                db.SupportStats.Remove(support);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfuly" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}