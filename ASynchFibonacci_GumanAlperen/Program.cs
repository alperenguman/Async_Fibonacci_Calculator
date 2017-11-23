using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;

namespace ASynchFibonacci_GumanAlperen
{
    public class Fibonacci
    {


        public delegate BigInteger GetFiboNumberDelegate(BigInteger count);

        public static BigInteger GetFiboNumber(BigInteger count)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            BigInteger i, f1 = 1, f2 = 1, f3 = 0;
            
            if (count == 1)
            {
                return f1;
            }
            else if (count == 2)
            {
                return f2;
            }

            for (i = 3; i <= count; i++)
            {
                f3 = f1 + f2;
                f1 = f2;
                f2 = f3;
                //Added to slow down this method just a little
                System.Threading.Thread.Sleep(400);
            }
            return f3;   
        }

        public static string WorkInProgress()
        {
            while (true)
            {
                Console.Write("Main Thread ID: {} | ", System.Threading.Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("continuing to do work…");
                System.Threading.Thread.Sleep(1000);
            };
        }

        public static string FiboComplete(int order, int fibonumber)
        {
            return String.Format("DONE. {} is the {}th number in a Fibonacci Sequence.", fibonumber, order);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.Write("Nth number in Fibonacci Sequence. n=");
            BigInteger count = BigInteger.Parse(Console.ReadLine());

            Fibonacci.GetFiboNumberDelegate fibDelegate = new Fibonacci.GetFiboNumberDelegate(Fibonacci.GetFiboNumber);
            IAsyncResult asyncFibResult = fibDelegate.BeginInvoke(count, null, null);

            while (!asyncFibResult.IsCompleted)
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Main thread id: {0}, still working...", Thread.CurrentThread.ManagedThreadId);
            }

            BigInteger fibResult = fibDelegate.EndInvoke(asyncFibResult);
            Console.WriteLine("DONE. The {0}th number in Fibonacci sequence is {1}", count, fibResult);
            Console.ReadLine();

        }
    }
}
