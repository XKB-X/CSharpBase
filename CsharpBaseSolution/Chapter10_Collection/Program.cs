using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10_Collection
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建管道，运行管道
            CreatePipe.PipeSample.StartPipeline();
            Console.ReadKey();
        }
    }
}
