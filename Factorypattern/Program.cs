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
            ls.Add(new Programms { Duration = 30, Title = "Welcome session" });
            ls.Add(new Programms { Duration = 30, Title = "Welcome session" });
            ls.Add(new Programms { Duration = 60, Title = "Welcome session" });
            ls.Add(new Programms { Duration = 45, Title = "Welcome session" });

            ls.Add(new Programms { Duration = 30, Title = "Welcome session" });
            ls.Add(new Programms { Duration = 15, Title = "Welcome session" });
            ls.Add(new Programms { Duration = 60, Title = "Welcome session" });
            ls.Add(new Programms { Duration = 45, Title = "Welcome session" });

            var alctmrng = new FirstSession(new DateTime(2016, 1, 1, 9, 0, 0), 180)
                                        .AllocateSession(ls);


            var sumOffirst = alctmrng.Sum(g => g.Duration);
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
                //orderSession.Sum(g => g.Duration) <= MaxDuration &&
                if (orderSession.Sum(g => g.Duration) < MaxDuration)
                {
                    if ((orderSession.Sum(g => g.Duration)) + pgm.Duration <= MaxDuration)
                        orderSession.Add(pgm);
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
    }
}
