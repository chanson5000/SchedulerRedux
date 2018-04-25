using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleWhizRedux.Interfaces;

namespace ScheduleWhizRedux.Models
{
    public class Job : IJob
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
    }
}
