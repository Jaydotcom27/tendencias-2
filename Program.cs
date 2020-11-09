using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
namespace _2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n Insert your students txt file route: ");
            var studentsRoute = Console.ReadLine();
            Console.WriteLine("\n Insert your topics txt file route: ");
            var topicsRoute = Console.ReadLine();
            Console.WriteLine("\n Insert your desired group ammount: ");
            var groupAmmount = Convert.ToInt32(Console.ReadLine());

            string[] students = File.ReadAllLines(@studentsRoute).OrderBy(s => Guid.NewGuid()).ToArray();
            string[] topics = File.ReadAllLines(@topicsRoute).OrderBy(s => Guid.NewGuid()).ToArray();
            Team[] teams = File.ReadAllLines(@topicsRoute).OrderBy(s => Guid.NewGuid()).Select(t => new Team()).ToArray();

            List<int> groupDistribution = DistributeInteger(students.Length, groupAmmount).ToList().OrderBy(s => Guid.NewGuid()).ToList();
            List<int> topicsDistribution = DistributeInteger(topics.Length, groupAmmount).ToList().OrderBy(s => Guid.NewGuid()).ToList();

            if (groupAmmount > students.Length)
            {
                Console.WriteLine("You cant have more groups than students on the files. App terminating now.");
            }

            if (groupAmmount > topics.Length)
            {
                Console.WriteLine("You cant have more groups than topics. App terminating now.");
                System.Environment.Exit(1);
            }
            else
            {
                int counter = 0;
                for (int j = 0; j < groupDistribution.Count; j++)
                {
                    counter = groupDistribution[j];
                    var s = students.Skip(j * students.Length / groupDistribution.Count)
                    .Take(groupDistribution[j]);
                    teams[j].students.AddRange(s);

                    var t = topics.Skip(j * topics.Length / topicsDistribution.Count)
                    .Take(topicsDistribution[j]);
                    teams[j].Topics.AddRange(t);
                }

                var n = 1;
                foreach (Team t in teams)
                {
                    Console.WriteLine("\t Grupo # " + n);
                    string printTopics = string.Join(", ", t.Topics);
                    Console.WriteLine("Topic(s): " + printTopics);
                    string printStudents = string.Join(", ", t.students);
                    Console.WriteLine("Student(s): " + printStudents);
                    Console.WriteLine(" ");
                    n++;
                }
            }
        }

        public static IEnumerable<int> DistributeInteger(int total, int divider)
        {
            if (divider == 0)
            {
                yield return 0;
            }
            else
            {
                int rest = total % divider;
                double result = total / (double)divider;

                for (int i = 0; i < divider; i++)
                {
                    if (rest-- > 0)
                    {
                        yield return (int)Math.Ceiling(result);
                    }
                    else
                    {
                        yield return (int)Math.Floor(result);
                    }
                }
            }
        }

        class Team
        {
            public List<string> Topics { get; set; }
            public List<string> students { get; set; }
            public Team()
            {
                Topics = new List<string>();
                students = new List<string>();
            }
        }

    }
}
