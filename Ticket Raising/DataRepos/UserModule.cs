using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketRaising.Data_Component;

namespace TicketRaising.DataRepos
{
    public interface IUserModule
    {
        tktEmployee ValidateUser(string emailAddress, string password);
        
    }
    public class UserModule : IUserModule
    {
        private bool isValidEmail(string emailAddress)
        {
            var context = new TicketEntities();
            var rec = context.tktEmployees.SingleOrDefault(e => e.Email == emailAddress);
            return rec == null;
        }
        public void RegisterCustomer(tktEmployee employee)
        {
            var context = new TicketEntities();
            if (isValidEmail(employee.Email))
            {
                context.tktEmployees.Add(employee);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Employee is already Registered, Please click on Forgot Password");
            }
        }

        public void UpdateCustomer(tktEmployee employee)
        {
            var context = new TicketEntities();
            var selected = context.tktEmployees.FirstOrDefault(e => e.EmpId == employee.EmpId);
            if (isValidEmail(employee.Email))
            {
                selected.Email = employee.Email;
                selected.Password = employee.Password;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Email already Exits");
            }
        }

        public tktEmployee ValidateUser(string emailAddress, string password)
        {
            var context = new TicketEntities();
            var employee = context.tktEmployees.SingleOrDefault(e => e.Email == emailAddress && e.Password == password);
            if (employee == null)
            {
                throw new Exception("Login Failed");
            }
            return employee;
        }
    }
}