using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public class EmployeeService : IEmployeeService
    {
        EmployeeContext _db;

        public EmployeeService()
        {
            _db = new EmployeeContext();
        }

        public void DeleteAndSave(int id)
        {
            var employee = _db.Employees.Find(id);
            if(employee != null)
            _db.Employees.Remove(employee);
            _db.SaveChanges();
        }
    }
}
