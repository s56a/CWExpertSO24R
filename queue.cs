using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApplication
{
 class Program
 {
  static void Main(string[] args)
  {
   Queue qt = new Queue();
   qt.Enqueue(1);
   qt.Enqueue(2);
   qt.Enqueue(3);

   foreach (Object obj in qt)
   {
    Console.WriteLine(obj);
   }
    Console.WriteLine(); Console.WriteLine();
    Console.WriteLine("The number of elements in the Queue " + qt.Count);
    Console.WriteLine("Dose the Queue contain " + qt.Contains(3));
    Console.ReadKey();
   }
 }
} 