using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketRaising.Data_Component
{
    public class TicketImpl : TicketInterface
    {
       static TicketEntities context = new TicketEntities();

        public void addResolvedTime(int ticketId , string dateTime)
        {
            var ticket = getTicket(ticketId);
            ticket.ResolvedTime = dateTime;
            context.SaveChanges();
        }

        public void assignTicket(int assignedId, int tktId)
        {
            var tkt = getTicket(tktId);
            tkt.AssignedTo = assignedId;
            
            context.SaveChanges();
        }

        public string calculateTimeBound(string raisedTime, string ResolvedTime)
        {

            DateTime d1 = Convert.ToDateTime(raisedTime);
            DateTime d2 = Convert.ToDateTime(ResolvedTime);
            var timebound = d2.Subtract(d1);

            string temp = timebound.ToString();
           
            string[] tb = temp.Split('.');
            if (tb.Length==2)
            {
            temp = $"{tb[0]} days {tb[1]}";

            }


            if (ResolvedTime==null)
            {
                return "Still not resolved";
            }


            return temp;
        }
        public string getbasedOnAid(string id)
         {
            
            if (id == null || id == "")
            {
                return "Not yet Assigned";
            }
            else
            {
                var cid = int.Parse(id);
                var selectedEmp = context.tktEmployees.FirstOrDefault((p) => p.EmpId == cid);
                return selectedEmp.EmpName;
            }
        }
        public void changeStatus(int tktId, string status)
        {
            var tkt = getTicket(tktId);
            tkt.Status = status;
            context.SaveChanges();
        }

        public List<Ticket> getAllActiveTickets(int assignedId)
        {
            var list = context.Tickets.ToList();
            List<Ticket> activeTkt = new List<Ticket>();

            foreach (var item in list)
            {
                if (item.AssignedTo==assignedId && item.Status=="Assigned")
                {
                    activeTkt.Add(item);
                }

            }
            return activeTkt;
        }


        public List<Ticket> getAllClosedTickets(int assignedId)
        {
            var list = context.Tickets.ToList();
            List<Ticket> activeTkt = new List<Ticket>();

            foreach (var item in list)
            {
                if (item.AssignedTo == assignedId && item.Status == "Closed")
                {
                    activeTkt.Add(item);
                }

            }
            return activeTkt;
        }

        public List<Ticket> getAllEmpTickets(int empId)
        {
            var list = context.Tickets.ToList();
            List<Ticket> emptkt = new List<Ticket>();

            foreach (var item in list)
            {
                if (item.EmpId==empId)
                {
                    emptkt.Add(item);
                }

            }
            return emptkt;
        }

        public List<Ticket> getAllIssdTickets(int AssignedEmpId)
        {
            var list = context.Tickets.ToList();
            List<Ticket> issdtkt = new List<Ticket>();

            foreach (var item in list)
            {
                if (item.AssignedTo==AssignedEmpId)
                {
                    issdtkt.Add(item);
                }

            }
            return issdtkt;
        }

        public List<Ticket> getAllMgrTickets()
        {
            var list = context.Tickets.ToList();
            List<Ticket> mgrtkt = new List<Ticket>();
            foreach (var item in list)
            {
                if (item.Status=="Active")
                {
                    mgrtkt.Add(item);
                }
            }
            return mgrtkt;
        }

        public List<tktEmployee> getIssdEmployees()
        {
            var employees = context.tktEmployees.ToList();
            List<tktEmployee> issdEmps = new List<tktEmployee>();
            foreach (var item in employees)
            {
                if (item.DeptId==3)
                {
                    issdEmps.Add(item);
                }
            }
            return issdEmps;
        }

        public Ticket getTicket(int tktId)
        {
            var ticket = context.Tickets.FirstOrDefault((t) => tktId == t.TicketNo);
            return ticket;
        }

        public tktEmployee Login(string email, string password)
        {
            var emp = context.tktEmployees.FirstOrDefault((e) => e.Email == email);
            if (emp!=null)
            {
                if (emp.Password==password)
                {
                    return emp;
                }
                else
                {
                    throw new Exception("Incorrect Password");
                }

            }
            else
            {
                throw new Exception("Invalid email");
            }
           
               
            

        }

        public void raiseNewTicket(Ticket ticket) {
            context.Tickets.Add(ticket);
            context.SaveChanges();
       }

        public List<Ticket> solvedTicket(int assignedId)
        {
            var list = context.Tickets.ToList();
            List<Ticket> Closed = new List<Ticket>();

            foreach (var item in list)
            {
                if (item.AssignedTo == assignedId && item.Status == "Closed")
                {
                    Closed.Add(item);
                }

            }
            return Closed;
        }
        public string getDept(int deptid)
        {
            var temp = context.tktDepartments.FirstOrDefault((d) => d.DeptId == deptid);
            return temp.DeptName;
        }
        public string getDeptOnEid(int empId)
        {
            var temp = context.tktEmployees.FirstOrDefault((f) => f.EmpId == empId);

            var dept = getDept(temp.DeptId);
            return dept;

        }
    }
}