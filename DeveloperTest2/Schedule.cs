using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperTest2
{
    public class Schedule
    {
            public int Id { get; set; }
            public string Description { get; set; }
            public int ScheduleTypes { get; set; }
            public ScheduleType ScheduleTypeValue { get; set; }
            public string Time { get; set; }
            public bool Repeat { get; set; }
            
    }
}
