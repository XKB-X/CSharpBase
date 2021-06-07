using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10_Collection.CreatePipe
{
    public class PipeSample
    {
        public static async void StartPipeline()
        {
            var fileNames = new BlockingCollection<string>();
            var lines = new BlockingCollection<string>();
            var words = new ConcurrentDictionary<string, int>();
            //var items = new BlockingCollection<Info>();
            //var colorItems = new BlockingCollection<Info>();
            Task t1 = PipeLineStages.ReadFileNameAsync(@"../../..", fileNames);
            ConsoleHelper.WriteLine("Start stage1");
            Task t2 = PipeLineStages.LoadContentAsync(fileNames, lines);
            ConsoleHelper.WriteLine("Start stage2");
            Task t3 = PipeLineStages.ProcessContentAsync(lines, words);
            await Task.WhenAll(t1, t2, t3);
            ConsoleHelper.WriteLine("stage1,2,3完成");
           
        }
    }
}
