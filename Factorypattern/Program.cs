using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorypattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var ls = new List<Programms>();
            ls.Add(new Programms { Duration = 30, Title = "Welcome session 1" });
            ls.Add(new Programms { Duration = 30, Title = "Welcome session 2" });
            ls.Add(new Programms { Duration = 60, Title = "Welcome session 3" });
            ls.Add(new Programms { Duration = 45, Title = "Welcome session 4" });

            ls.Add(new Programms { Duration = 30, Title = "Welcome session 5" });
            ls.Add(new Programms { Duration = 15, Title = "Welcome session 6" });
            ls.Add(new Programms { Duration = 60, Title = "Welcome session 7" });
            ls.Add(new Programms { Duration = 45, Title = "Welcome session 8" });

            var alctmrng = new FirstSession(new DateTime(2016, 1, 1, 9, 0, 0), 180)
                                        .AllocateSession(ls);


            var sumOffirst = alctmrng.Sum(g => g.Duration);

            Console.WriteLine("Monring");
            foreach (var item in alctmrng.OrderBy(g=>g.Time))
            {
                
                Console.WriteLine(string.Format("Time is {0} and program {1} and Duration {2}",item.Time.ToString("HH:mm"),item.Title,item.Duration));
            }

            var second = new  FirstSession(new DateTime(2016, 1, 1, 1, 0, 0), 180)
                                        .AllocateSession(ls.Except(alctmrng).ToList());



            Console.WriteLine("noon");
            foreach (var item in second.OrderBy(g => g.Time))
            {

                Console.WriteLine(string.Format("Time is {0} and program {1} and Duration {2}", item.Time.ToString("HH:mm"), item.Title, item.Duration));
            }


            Console.ReadLine();
        }
    }

    public interface ISession
    {
        DateTime StartTime { get; set; }

        int MaxDuration { get; set; }

        List<Programms> AllocateSession(List<Programms> programs);

    }

    public class FirstSession : ISession
    {
        public FirstSession(DateTime startTime, int maxDuration)
        {
            this.StartTime = startTime;
            this.MaxDuration = maxDuration;
        }

        public DateTime StartTime { get; set; }
        public int MaxDuration
        {
            get;

            set;
        }


        public List<Programms> AllocateSession(List<Programms> programs)
        {
            var orderSession = new List<Programms>();
            foreach (Programms pgm in programs)
            {
                if (orderSession.Sum(g => g.Duration) <= MaxDuration)
                {
                    if ((orderSession.Sum(g => g.Duration)) + pgm.Duration <= MaxDuration)
                    {
                        if (!orderSession.Any())
                            pgm.Time = StartTime;
                        else
                        {
                            pgm.Time = orderSession.Last().Time;
                            pgm.Time= pgm.Time.AddMinutes(orderSession.Last().Duration);
                        }


                        orderSession.Add(pgm);

                    }
                }
            }

            return orderSession;
        }



    }

    public class SecondSession : ISession
    {
        public SecondSession(DateTime startTime)
        {
            this.StartTime = startTime;
        }

        public DateTime StartTime { get; set; }

        public int MaxDuration
        {
            get;

            set;
        }

        public List<Programms> AllocateSession(List<Programms> programs)
        {
            return new List<Programms>();
        }




    }

    enum SessionType
    {
        First,
        Second
    }

    public class Programms
    {
        public int Duration { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
    }
}
