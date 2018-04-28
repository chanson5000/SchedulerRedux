using System.Collections.Generic;
using ScheduleWhizRedux.Models;

namespace ScheduleWhizRedux.Interfaces
{
    public interface IJobRepository : IRepository
    {
        bool Add(string jobTitle);
        bool Exists(string jobTitle);
        bool Exists(Job job);
        int GetId(string jobTitle);
        Job Get(int id);
        Job GetRecord(string jobTitle);
        bool RecordExists(Job job);
        List<Job> GetAllSorted();
        List<string> GetAllTitles();
        bool Modify(Job job);
        bool Remove(Job job);
        bool Remove(string jobtitle);
    }
}
