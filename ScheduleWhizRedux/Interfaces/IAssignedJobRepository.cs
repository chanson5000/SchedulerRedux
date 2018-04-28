using System.Collections.Generic;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.Interfaces
{
    public interface IAssignedJobRepository
    {
        bool Add(Job job, Employee employee);
        bool Remove(Job job, Employee employee);
        List<string> Get(int id);
        List<string> Get(Employee employee);
        List<string> GetAvailable(int id);
        List<string> GetAvailable(Employee employee);
        bool IsJobAssignedToEmployee(string jobTitle, Employee employee);
        bool IsJobAssignedToEmployee(Job job, Employee employee);
    }
}
