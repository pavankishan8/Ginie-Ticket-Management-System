using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TicketRaising.Data_Component;
using TicketRaising.DataRepos;

namespace TicketRaising.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        static tktEmployee globalemp = null;
        public ActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Login(string email, string pwd)
        {
            var com1 = new TicketImpl();
            var com = new UserModule();
            try
            {
                var emp = com.ValidateUser(email, pwd);
                Session["CurrentUser"] = emp;
                
                FormsAuthentication.SetAuthCookie(emp.Email, false);
                FormsAuthentication.RedirectFromLoginPage(emp.Email, false);
                var selected = com1.Login(email, pwd);
                TempData["selected"] = selected;
                globalemp = selected;
                if (selected.DeptId == 3)
                    return RedirectToAction("ISSDaction");
                if (selected.DeptId == 4)
                    return RedirectToAction("MGRaction");
                else
                    return RedirectToAction("Empaction");

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("LoginError", ex.Message);
                return View();
            }
        }

        public ActionResult ISSDaction()
        {
            ViewBag.Message = globalemp.EmpName;
            return View();
        }

        public ActionResult MGRaction()
        {
            var com = new TicketImpl();
            var all = com.getAllMgrTickets();
            TempData["ISSDemp"] = com.getIssdEmployees();
            TempData["timeBound"] = com;
            ViewBag.Message = globalemp.EmpName;
            return View(all);
        }

        public ActionResult Empaction()
        {
            ViewBag.Message = globalemp.EmpName;
            return View();
        }

        public ActionResult RaiseTicket()
        {
           
            return View(new Ticket());

        }

        [HttpPost]
        public ActionResult RaiseTicket(Ticket tkt)
        {
            var com = new TicketImpl();

            tkt.EmpId = globalemp.EmpId;
            tkt.RaisedTime = DateTime.Now.ToString();
            tkt.Status = "Active";
            com.raiseNewTicket(tkt);
            TempData["alert"] = $"Ticket Number {tkt.TicketNo} Raised Successfully";



            return RedirectToAction("Empaction");
        }

        public ActionResult VeiwTicket()
        {
            var com = new TicketImpl();
            var Allticket = com.getAllEmpTickets(globalemp.EmpId);
            TempData["timeBound"] = com;
            return View(Allticket);
        }

        public ActionResult Assign(int id)
        {
            var com = new TicketImpl();
            var allissd = com.getIssdEmployees();
            var EMplist = new List<SelectListItem>();
            foreach (var item in allissd)
            {
                var one = new SelectListItem { Text = item.EmpName, Value = item.EmpId.ToString() };
                EMplist.Add(one);
            }

            TempData["ISSDemp"] = EMplist;
            var tkt = com.getTicket(id);
            return View(tkt);
        }

        [HttpPost]
        public ActionResult Assign(Ticket tkt)
        {
            var com = new TicketImpl();
            com.changeStatus(tkt.TicketNo, "Assigned");
            com.assignTicket((int)tkt.AssignedTo, tkt.TicketNo);
            TempData["alert"] = $"Ticket Assigned Successfully To {com.getbasedOnAid(tkt.AssignedTo.ToString())}";
            return RedirectToAction("MGRaction");
        }

        public ActionResult ViewAssigned()
        {
            var com = new TicketImpl();
            var Allticket = com.getAllActiveTickets(globalemp.EmpId);
            return View(Allticket);
        }

        public ActionResult ViewAssignedTicket(int id)
        {
            var com = new TicketImpl();
            var ticket = com.getTicket(id);
            
            return View(ticket);
        }
        [HttpPost]
        public ActionResult ViewAssignedTicket(Ticket ticket)
        {
            var com = new TicketImpl();
            com.changeStatus(ticket.TicketNo, "Closed");
            com.addResolvedTime(ticket.TicketNo, DateTime.Now.ToString());
            TempData["alert"] = $"Ticket Number {ticket.TicketNo} closed Successfully";
            return RedirectToAction("ISSDaction");
        }

        public ActionResult ViewClosed()
        {
            var com = new TicketImpl();
            TempData["timeBound"] = com;
            var Allticket = com.getAllClosedTickets(globalemp.EmpId);
            return View(Allticket);
        }
    }
}