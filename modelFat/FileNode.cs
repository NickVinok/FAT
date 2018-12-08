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
        public int index { get; set; }
        public int indexNext { get; set; }
        public Node(T data, int index, int indexNext)
        {
            Data = data;
            this.index = index;
            this.indexNext = indexNext;
        }
        public int GetIndex() { return this.index; }
        public int GetIndexNext() { return this.indexNext; }
    }
}
