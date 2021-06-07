using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10_Collection.Collection
{
    public class Document
    {
        private string title;
        private string content;
        public Document(string title, string content)
        {
            this.title = title;
            this.content = content;
        }
        public string Title
        {
            get => title;
            private set => title = value;
        }
        public string Content
        {
            get => this.content;
            private set => this.content = value;
        }
    }
}
