using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modelFat
{
    class Node<T>
    {
        public T Data;
        public Node<T> Next { get; set; }
        public int Index { get; set; }
        public int IndexNext { get; set; }
        public Node(T data, int index, int indexNext)
        {
            Data = data;
            this.Index = index;
            this.IndexNext = indexNext;
        }
        public int GetIndex() { return this.Index; }
        public int GetIndexNext() { return this.IndexNext; }
    }
}
