using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

namespace DeveloperTest2
{
    class Program
    {

        static void Main(string[] args)
        {
            string jsonFile = @"C:\Users\aldo\source\repos\DeveloperTest\DeveloperTest2\tasks.json";
            var json = System.IO.File.ReadAllText(jsonFile);

            var jObject = JObject.Parse(json);

            List<Schedule> tasks = new List<Schedule>();

            if (jObject != null)
            {
                JArray tasksArray = (JArray)jObject["schedules"];
                if (tasksArray != null)
                {

                    foreach (var item in tasksArray)
                    {
                        Schedule schedule = new Schedule();
                        schedule.Id = Convert.ToInt32(item["scheduleid"].ToString());
                        schedule.Description = item["description"].ToString();
                        schedule.Time = item["time"].ToString();
                        schedule.Repeat = Convert.ToBoolean(item["repeat"].ToString());
                        schedule.ScheduleTypes = Convert.ToInt32(item["type"].ToString());
                        schedule.ScheduleTypeValue = (ScheduleType)schedule.ScheduleTypes;
                        tasks.Add(schedule);
                    }

                }
            }
            schedule_Timer(tasks);
        }

        static void schedule_Timer(List<Schedule> tasks)
        {
            foreach (var task in tasks)
            {
                DateTime nowTime = DateTime.Now;
                string[] time = task.Time.Split(':');
                DateTime scheduledTime = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, Convert.ToInt32(time[0]), Convert.ToInt32(time[1]), 0, 0); //Specify your scheduled time HH,MM,SS [8am and 42 minutes]
                if (nowTime > scheduledTime)
                {
                    if (task.ScheduleTypes == 0) {
                        scheduledTime = scheduledTime.AddDays(1);
                    } else if (task.ScheduleTypes == 1) {
                        scheduledTime = scheduledTime.AddDays(7);
                    }
                    else if (task.ScheduleTypes == 2)
                    {
                        scheduledTime = scheduledTime.AddDays(31);
                    }
                }

                double tickTime = (double)(scheduledTime - DateTime.Now).TotalMilliseconds;
               Timer timer = new Timer(tickTime);
                timer.Interval = 2000;

                timer.Elapsed += OnTimedEvent;
                timer.AutoReset = task.Repeat;
               
                timer.Enabled = true;
                timer.Start();

                Console.WriteLine("Task Started "+ task.Description);
            }
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }
    }
}
