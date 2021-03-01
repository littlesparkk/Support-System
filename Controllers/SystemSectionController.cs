using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
using System.Data.Entity;

namespace Asp.NETMVCCRUD.Controllers
{
    public class SystemSectionController : Controller
    {
        // GET: SystemSections
        public ActionResult IndexSystemSection()
        {
            return View();
        }
        public ActionResult GetSystemSectionData()
        {
            using (DBModel db = new DBModel())
            {
                List<SystemSection> sectionList = db.SystemSections.ToList<SystemSection>();
                return Json(new { data = sectionList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEditSystemSection(int id = 0)
        {
            if (id == 0)
                return View(new SystemSection());

            else
            {
                using (DBModel db = new DBModel())
                {
                    return View(db.SystemSections.Where(x => x.SystemSectionId == id).FirstOrDefault<SystemSection>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEditSystemSection(SystemSection section)
        {
            using (DBModel db = new DBModel())
            {
                if (section.SystemSectionId == 0)
                {
                    db.SystemSections.Add(section);
                    db.SaveChanges();
                    //return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                    return View("IndexSystemSection");
                }
                else
                {
                    db.Entry(section).State = EntityState.Modified;
                    db.SaveChanges();
                    //return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                    return View("IndexSystemSection");
                }
            }

        }
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                SystemSection section = db.SystemSections.Where(x => x.SystemSectionId == id).FirstOrDefault<SystemSection>();
                db.SystemSections.Remove(section);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfuly" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}