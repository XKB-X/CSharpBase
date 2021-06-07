using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10_Collection.CreatePipe
{
    public static  class ConcurrentDictionaryExtension
    {
        /// <summary>
        /// 把单词添加到字典中，如果字典中有该词，则在字典中递增一个值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        public static void AddOrIncrementValue(this ConcurrentDictionary<string, int> dict, string key)
        {
            bool success = false;
            while (!success)
            {
                int value;
                if (dict.TryGetValue(key, out value))
                {
                    if (dict.TryUpdate(key, value + 1, value))
                        success = true;
                }
                else
                {
                    if (dict.TryAdd(key, value))
                        success = true;
                }
            }
        }
    }
}
