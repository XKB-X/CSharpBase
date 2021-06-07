using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chapter10_Collection.Collection
{
    public class ProcessDocuments
    {
        private DocumentManager documentManager;
        public ProcessDocuments(DocumentManager dm)
        {
            this.documentManager = dm ?? throw new ArgumentNullException("dm");
        }
        /// <summary>
        /// 任务的启动方法
        /// </summary>
        protected void Run()
        {
            while (true)
            {
                if (documentManager.IsDocumentAvaliable())
                {
                    Document document = documentManager.GetDocument();
                    Console.WriteLine("Process Document {0}",document.Title);
                }
                Thread.Sleep(new Random().Next(20));
            }
        }
        public static void Start(DocumentManager dm)
        {
            Task.Factory.StartNew(new ProcessDocuments(dm).Run);
        }
    }
}
