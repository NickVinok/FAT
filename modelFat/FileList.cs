using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace modelFat
{
    class FileList<T> :IEnumerable<T>
    {
        Node<T> head; // головной/первый элемент
        Node<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке
        Dictionary<int, int> FAT;
        public FileList(Dictionary<int, int> FAT)
        {
            this.FAT = FAT;
        }
        public void Add(T data, bool EOF)
        {
            int index = -1;
            int indexNext = -1;

            for (int i = 0; i < FAT.Count(); i++)
            {
                if(FAT[i] == 0 && index == -1){
                    index = i;                    
                }                
            }
            for (int j = 0; j < FAT.Count(); j++)
            {
                if (FAT[j] == 0 && j != index && indexNext == -1)
                {
                    indexNext = j;
                    if (EOF)
                        indexNext = -3;
                }
            }
            FAT[index] = indexNext;
            Node<T> file = new Node<T>(data, index, indexNext);

            if (head == null)
                head = file;
            else
                tail.Next = file;
            tail = file;
            count++;
        }
        public bool Remove(int index)
        {
            Node<T> current = head;
            Node<T> previous = null;

            while (current != null)
            {
                if (current.Index.Equals(index))
                {
                    FAT[current.Index] = 0;
                    // Если узел в середине или в конце
                    if (previous != null)
                    {
                        // убираем узел current, теперь previous ссылается не на current, а на current.Next
                        previous.Next = current.Next;
                        if (previous.Next != null)
                        {
                            if (previous.Next.IndexNext == -3)
                            {
                                Remove(previous.Index);
                                break;
                            }                            
                            
                        }
                        // Если current.Next не установлен, значит узел последний,
                        // изменяем переменную tail
                        if (current.Next == null)
                            tail = previous;
                    }
                    else
                    {
                        // если удаляется первый элемент
                        // переустанавливаем значение head
                        head = head.Next;

                        // если после удаления список пуст, сбрасываем tail
                        if (head == null)
                        {
                            tail = null;
                        }else{
                            if(head.IndexNext == -3)
                            {
                                Remove(head.Index);
                                break;
                            }
                            
                        }
                            
                    }
                    count--;                    
                    return true;
                }
                /*
                 * Разобраться с удалением. Добавить проверки если файл кончился, а то если удалить 1 файл удаляются все блоки :С
                 * */
                previous = current;
                current = current.Next;
            }
            return false;
        }
        

        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }
        // очистка списка
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
        // содержит ли список элемент
        public bool Contains(T data)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }
        public Dictionary<int, T> GetPartFileByInx(int startInx) {            
            Node<T> current = head;
            while (current != null)
            {
                if (current.Index == startInx)
                {
                    return new Dictionary<int, T>() {
                        { current.IndexNext, current.Data }
                    };
                }
                current = current.Next;
            }
            return new Dictionary<int, T>();
        }

        public int GetLastIndex()
        {           
            return tail.GetIndexNext();
        }

        public int GetFirstIndex()
        {
            return head.GetIndex();
        }
        public int GetFirstEmptyBlock()
        {
            if(tail != null)
                return Count == 0 ? 0 : tail.Index + 1;
            else
            {
                return 0;
            }
        }
        // добвление в начало
        //public void AppendFirst(T data)
        //{
        //    Node<T> node = new Node<T>(data);
        //    node.Next = head;
        //    head = node;
        //    if (count == 0)
        //        tail = head;
        //    count++;
        //}

        // реализация интерфейса IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}
