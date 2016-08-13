using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queues
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var limitedQueue = new LimitedQueue<int>(3);
            var threads = new List<Thread>();
            var rand = new Random();
            AddEnqueueThreads(threads, limitedQueue);
            AddDequeueThreads(threads, limitedQueue);
            threads.AsParallel().ForAll((t) => t.Start());
            Thread.Sleep(5000);
            limitedQueue.ToList().ForEach((x) => Console.WriteLine(x));
            Console.Read();
        }

        static void AddDequeueThreads(List<Thread> threads, LimitedQueue<int> limitedQueue)
        {
            Enumerable.Range(0, new Random().Next(1, 100)).ToList().ForEach((x) =>
            {
                var t2 = new Thread(() =>
                {
                    try
                    {
                        limitedQueue.Dequeue();
                    }
                    catch (Exception)
                    {
                    }
                });
                threads.Add(t2);
            });
        }
        static void AddEnqueueThreads(List<Thread> threads, LimitedQueue<int> limitedQueue)
        {
            Enumerable.Range(0, new Random().Next(1, 100)).ToList().ForEach((x) =>
            {
                var t1 = new Thread(() => limitedQueue.Enqueue(x));
                threads.Add(t1);
            });
        }
    }
}
