using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
using System.Data.Entity;


namespace Asp.NETMVCCRUD.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult IndexUserManagement()
        {
            return View();
        }
        public ActionResult GetUserManagementData()
        {
            using(DBModel db = new DBModel())
            {
                List<UserManagement> userList = db.UserManagements.ToList<UserManagement>();
                return Json(new { data = userList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEditUserManagement(int id = 0)
        {
            if (id == 0)
                return View(new UserManagement());

            else
            {
                using(DBModel db = new DBModel())
                {
                    return View(db.UserManagements.Where(x => x.UserManagementId == id).FirstOrDefault<UserManagement>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEditUserManagement(UserManagement user)
        {
            using(DBModel db = new DBModel())
            {
                if(user.UserManagementId == 0)
                {
                    db.UserManagements.Add(user);
                    db.SaveChanges();
                    //return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                    return View("IndexUserManagement");
                }
                else
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    //return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                    return View("IndexUserManagement");
                }
            }

        }
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                UserManagement user = db.UserManagements.Where(x => x.UserManagementId == id).FirstOrDefault<UserManagement>();
                db.UserManagements.Remove(user);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfuly" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}