using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activity_Selection
{
    public class Program
    {
        public class Activity
        {
            public int Start { get; set; }
            public int Finish { get; set; }

            public Activity(int start, int finish)
            {
                Start = start;
                Finish = finish;
            }
        }
        public static List<Activity> SelectActivities(List<Activity> activities)
        {
            // Bước 1: Sắp xếp các hoạt động theo thời gian kết thúc tăng dần
            var sortedActivities = activities.OrderBy(a => a.Finish).ToList();

            // Bước 2: Chọn hoạt động đầu tiên
            List<Activity> result = new List<Activity>();
            result.Add(sortedActivities[0]);

            // Bước 3: Lặp lại và chọn các hoạt động tiếp theo
            int lastFinishTime = sortedActivities[0].Finish;

            for (int i = 1; i < sortedActivities.Count; i++)
            {
                if (sortedActivities[i].Start >= lastFinishTime)
                {
                    result.Add(sortedActivities[i]);
                    lastFinishTime = sortedActivities[i].Finish;
                }
            }

            return result;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            List<Activity> activities = new List<Activity>
            {
                new Activity(1, 2), // A1
                new Activity(3, 4), // A2
                new Activity(0, 6), // A3
                new Activity(5, 7), // A4
                new Activity(8, 9), // A5
                new Activity(5, 9)  // A6
            };

            List<Activity> selectedActivities = SelectActivities(activities);

            Console.WriteLine("Các hoạt động được chọn:");
            foreach (var activity in selectedActivities)
            {
                Console.WriteLine($"Hoạt động bắt đầu: {activity.Start}, kết thúc: {activity.Finish}");
            }
            Console.ReadLine();
        }
    }
}
