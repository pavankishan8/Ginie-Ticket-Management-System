using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketRaising.Data_Component
{
    interface TicketInterface
    {
        Ticket getTicket(int tktId);
        void changeStatus(int tktId,string status);

        void raiseNewTicket(Ticket ticket);
        List<Ticket> getAllEmpTickets(int empId);
        List<Ticket> getAllMgrTickets();

        List<Ticket> getAllIssdTickets(int AssignedEmpId);

        void assignTicket(int assignedId, int tktId);
        List<Ticket> getAllActiveTickets(int assignedId);

        List<Ticket> solvedTicket(int assignedId);
        List<tktEmployee>getIssdEmployees();
        tktEmployee Login(string email, string password);

        void addResolvedTime(int ticketId , string dateTime);
        string calculateTimeBound(string raisedTime, string ResolvedTime);
    }
}
