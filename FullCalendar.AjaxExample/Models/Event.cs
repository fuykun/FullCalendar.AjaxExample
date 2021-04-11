using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCalendar.AjaxExample.Models
{
    public class Event
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public bool AllDay { get; set; }
    }
}
