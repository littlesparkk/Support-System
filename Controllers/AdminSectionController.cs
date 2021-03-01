using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Asp.NETMVCCRUD.Models;
using Asp.NETMVCCRUD.ViewModel;

namespace Asp.NETMVCCRUD.Controllers
{
    public class AdminSectionController : Controller
    {
        // GET: AdminSection
        
        public ActionResult IndexAdminSection()
        {
            return View();
        }
        public ActionResult GetAdminSectionData()
        {
            using (DBModel db = new DBModel())
            {
                var adminList = db.AdminSections.Select(x => new { x.AdminSectionId, x.Ticket_No, x.Title, x.Status, x.Category, x.Severity, x.Priority, x.Raised_By, x.Raised_On, x.Due_On, x.Resolved_On }).ToList();
                return Json(new { data = adminList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEditAdminSection(int id = 0)
        {
            if (id == 0)
                return View(new AdminSection());

            else
            {
                using (DBModel db = new DBModel())
                {
                    ViewBag.Comments = db.UserComments.Where(x => x.AdminSectionId == id && x.ReplyCommentId == null).Select(y => new CommentViewModel { CommentId = y.CommentId, ReplyCommentId = y.ReplyCommentId != null ? y.ReplyCommentId.Value : 0, CommentText = y.CommentText, CreatedAt = y.CreatedAt.Value , CreatedBy = db.RegisterUsers.FirstOrDefault(z => z.AccountId == y.CreatedBy).FirstName }).ToList();
                    ViewBag.ReplyComments = db.UserComments.Where(x => x.AdminSectionId == id && x.ReplyCommentId != null).Select(y => new CommentViewModel { CommentId = y.CommentId, ReplyCommentId = y.ReplyCommentId != null ? y.ReplyCommentId.Value : 0, CommentText = y.CommentText, CreatedAt = y.CreatedAt.Value, CreatedBy = db.RegisterUsers.FirstOrDefault(z => z.AccountId == y.CreatedBy).FirstName }).ToList();
                    return View(db.AdminSections.Where(x => x.AdminSectionId == id).FirstOrDefault<AdminSection>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEditAdminSection(AdminSection admin)
        {
            using (DBModel db = new DBModel())
            {
                if (admin.AdminSectionId == 0)
                {
                    db.AdminSections.Add(admin);
                    db.SaveChanges();
                    //return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                    return View("IndexAdminSection");
                    
                }
                else
                {
                    db.Entry(admin).State = EntityState.Modified;
                    db.SaveChanges();
                    //return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                    return View("IndexAdminSection");
                    
                }
            }

        }
        public ActionResult Delete(int id)
        {
            using (DBModel db = new DBModel())
            {
                AdminSection admin = db.AdminSections.Where(x => x.AdminSectionId == id).FirstOrDefault<AdminSection>();
                db.AdminSections.Remove(admin);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfuly" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteComment(int id)
        {
            using(DBModel db = new DBModel())
            {
                UserComment comment = db.UserComments.Where(x => x.CommentId == id).FirstOrDefault<UserComment>();
                db.UserComments.Remove(comment);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfuly" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SaveComment(int ASID, string CommentText, int? CommentId)
        {
            using (DBModel db = new DBModel())
            {
                var user = User.Identity.Name;
                var userId = db.RegisterUsers.FirstOrDefault(x => x.Email == user).AccountId;
                var username = db.RegisterUsers.FirstOrDefault(x => x.Email == user).FirstName;


                var Comment = new UserComment()
                {
                    AdminSectionId = ASID,
                    CommentText = CommentText,
                    CreatedBy = userId,
                    CreatedAt = DateTime.Now,
                    ReplyCommentId = CommentId
                };

                //db.Comments.Add(Comment);
                //db.SaveChanges();
                db.UserComments.Add(Comment);
                db.SaveChanges();
                
                

                return Json(new { success = true, comment = CommentText, user = username, created = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult testForm()
        {
            return View();
        }
    }
}