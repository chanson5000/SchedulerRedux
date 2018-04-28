using System.Collections.Generic;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.Interfaces
{
    public interface IEmployeeRepository : IRepository
    {
        bool Add(Employee employee);
        bool Add(string firstName, string lastName, string emailAddress, string phoneNumber);
        bool Exists(Employee employee);
        int GetId(Employee employee);
        Employee Get(int id);
        List<Employee> GetAllSorted();
        bool Modify(Employee employee);
        bool Remove(Employee employee);
    }
}
