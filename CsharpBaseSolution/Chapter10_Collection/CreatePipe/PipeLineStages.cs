using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10_Collection.CreatePipe
{
    public static class PipeLineStages
    {
        /// <summary>
        /// 写入输出
        /// </summary>
        /// <param name="path"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static Task ReadFileNameAsync(string path, BlockingCollection<string> output)
        {
            return Task.Run(()=> {
                foreach (string filename in Directory.EnumerateFiles(path, "*.cs",SearchOption.AllDirectories))
                {
                    output.Add(filename);
                    ConsoleHelper.WriteLine(string.Format("stage1:added{0}", filename));
                }
                //通知集合中的读取器不应再等在任何的额外项
                output.CompleteAdding();
            });
        }
        public static async Task LoadContentAsync(BlockingCollection<string> input,BlockingCollection<string> output)
        {
            foreach (string filename in input.GetConsumingEnumerable())
            {
                using (FileStream stream = File.OpenRead(filename))
                {
                    var reader = new StreamReader(stream);
                    string line = null;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        output.Add(line);
                        ConsoleHelper.WriteLine(string.Format("stage2:Added{0}", line));
                    }
                }
            }
            output.CompleteAdding();
        }

        public static Task ProcessContentAsync(BlockingCollection<string> input, ConcurrentDictionary<string, int> output)
        {
            return Task.Run(()=> {
                foreach (var  line in input.GetConsumingEnumerable())
                {
                    string[] words = line.Split(' ',';','\t','{','}','(',')',':',',','"');
                    foreach (var word in words.Where(w => !string.IsNullOrEmpty(w)))
                    {
                        output.AddOrIncrementValue(word);
                        ConsoleHelper.WriteLine(string.Format("stage3:Added{0}", word));
                    }
                }

            });
        }

        /// <summary>
        /// 从字典中获取值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static Task TransferConetntAsync(ConcurrentDictionary<string, int> input, BlockingCollection<Info> output)
        {
            return Task.Run(()=> {

                foreach (string word in  input.Keys)
                {
                    int value;
                    if (input.TryGetValue(word, out value))
                    {
                        var info = new Info { Word = word, Count = value };
                        output.Add(info);
                        ConsoleHelper.WriteLine(string.Format("starge4:Added{0}", info));

                    }
                }
                output.CompleteAdding();
            });
        }
        /// <summary>
        /// 管道进行阶段，根据Count属性的值设置Info类型的Color颜色
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static Task AddColorAsync(BlockingCollection<Info> input, BlockingCollection<Info> output)
        {
            return Task.Run(delegate() {
                foreach (var item in input.GetConsumingEnumerable())
                {
                    if (item.Count > 40)
                    {
                        item.Color = "Red";
                    }
                    else if (item.Count > 20)
                    {
                        item.Color = "Yellow";
                    }
                    else
                    {
                        item.Color = "Green";
                    }
                    output.Add(item);
                    ConsoleHelper.WriteLine(string.Format("stage5:color {0}to {1} ",item.Color,item));
                }
                output.CompleteAdding();
            });
        }
        /// <summary>
        /// 用指定的颜色在控制台输出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Task ShowContentAsync(BlockingCollection<Info> input)
        {
            return Task.Run(()=> {
                foreach (var item in input.GetConsumingEnumerable())
                {
                    ConsoleHelper.WriteLine(string.Format("stage6:{0}", item), item.Color);
                }
            });
        }
    }
}
