using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScheduleWhizRedux.Interfaces;

namespace ScheduleWhizRedux.Models
{
    public class Shift : IShift
    {
        public string ShiftName { get; set; }
    }
}
