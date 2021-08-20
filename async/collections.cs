//
// CONCURRENT QUEUE DEMO
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace ConcurrentQueueDemo
{
    class Program
    {
        static Random rnd = new Random();
        static ConcurrentQueue<ulong> cq = new ConcurrentQueue<ulong>();

        static void Main(string[] args)
        {
            Thread threadFib = new Thread(new ThreadStart(GenerateFib));
            threadFib.IsBackground = false;
            threadFib.Start();

            Thread threadReader = new Thread(new ThreadStart(ReadFib));
            threadReader.IsBackground = false;
            threadReader.Start();
        }

        private static void ReadFib()
        {
            Console.WriteLine("Starting to read from the queue...");

            do
            {
                if (cq.TryDequeue(out ulong n))
                {
                    Console.Write("[Fib {0}]", n);
                }
                else
                {
                    //Console.Write(".");
                }
                Thread.Sleep(10);
            } while (true);
        }

        private static void GenerateFib()
        {
            for (ushort ix = 0; ix < 50; ix++)
            {
                Thread.Sleep(rnd.Next(1, 500));
                cq.Enqueue(Fibonacci(ix));
            }
        }

        private static ulong Fibonacci(ushort n)
        {
            return (n == 0) ? 0 : Fib(n);
        }
        private static ulong Fib(ushort n)
        {
            ulong a = 0;
            ulong b = 1;
            for (uint ix = 0; ix < n - 1; ix++)
            {
                ulong next = checked(a + b);
                a = b;
                b = next;
            }
            return b;
        }
    }
}


//
// CONCURRENT DICTIONARY DEMO
//

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentDictionaryTest
{
    class Program
    {
        static ConcurrentDictionary<int, string> cd = new ConcurrentDictionary<int, string>();
        static void Main()
        {
            //if (cd.TryAdd(1, "one"))
            //{
            //    Console.WriteLine("KVP 1:one Was Added!");
            //}

            //string val = cd.GetOrAdd(1, "ONE");
            //Console.WriteLine("Existing 1: {0}", val);

            //string val2 = cd.AddOrUpdate(1, "_ONE_",
            //    (int existingKey, string existingValue) => 
            //    {
            //        return existingValue.ToUpper();
            //    });
            //Console.WriteLine("Existing 1: {0}", val2);

            //if (cd.TryGetValue(1, out string val3))
            //{
            //    Console.WriteLine("Existing value: ", val3);
            //}

            //cd.GetOrAdd()

            string filename = @"d:\source\sp.txt";
            string[] lines = File.ReadAllLines(filename);

            Parallel.ForEach<string>(lines,
                (string line) =>
                {
                    string[] words = line.Split(' ');
                    foreach (string word in words)
                    {
                        if (string.IsNullOrWhiteSpace(word)) continue;

                        string canonicalWord = word.Trim(' ', ',', '.', '?', '!', ';','-', ':', '\'', '\"').ToLowerInvariant();

                        wordCount.AddOrUpdate(canonicalWord, 1, (k, currentCount) => { return currentCount + 1; });
                    }
                });

        }
        static ConcurrentDictionary<string, uint> wordCount = new ConcurrentDictionary<string, uint>();
    }
}

//
// BLOCKING COLLECTION DEMO
//

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerExample
{
    class Program
    {
        static Random rnd = new Random();
        static BlockingCollection<ulong> numbers = new BlockingCollection<ulong>(10);

        static void Main(string[] args)
        {
            Thread threadFib = new Thread(new ThreadStart(GenerateFib));
            threadFib.IsBackground = false;
            threadFib.Start();

            Thread threadReader = new Thread(new ThreadStart(ReadFib));
            threadReader.IsBackground = false;
            threadReader.Start();
        }

        private static void GenerateFib()
        {
            for (ushort ix = 0; ix < 50; ix++)
            {
                Thread.Sleep(rnd.Next(1, 500));
                Console.WriteLine("Adding next Fib...");
                numbers.Add(Fibonacci(ix));
            }
        }
        private static void ReadFib()
        {
            Thread.Sleep(10000);
            do
            {
                var n = numbers.Take();
                Console.Write("[Fib {0}]", n);
            } while (true);
        }

        private static ulong Fibonacci(ushort n)
        {
            return (n == 0) ? 0 : Fib(n);
        }
        private static ulong Fib(ushort n)
        {
            ulong a = 0;
            ulong b = 1;
            for (uint ix = 0; ix < n - 1; ix++)
            {
                ulong next = checked(a + b);
                a = b;
                b = next;
            }
            return b;
        }
    }
}
