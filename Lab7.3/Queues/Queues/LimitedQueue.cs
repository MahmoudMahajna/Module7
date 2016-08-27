using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queues
{
    //No IDisposable to release the semaphore?
    //the implementation of IEnumerable is a really nice idea.
    class LimitedQueue<T>:IEnumerable<T> 
    {
        Queue<T> _queue;
        Semaphore _lock;
        private object _addRemoveLock=new object();
        public LimitedQueue(int maxSizeQueue)
        {
            _queue = new Queue<T>(maxSizeQueue);
            _lock = new Semaphore(maxSizeQueue, maxSizeQueue);
        }
        public T Dequeue()
        {
            T value;
            lock (_addRemoveLock)
            {
                value = _queue.Dequeue();  
            }

            //You should insert it into a finaly clause when working with acquire-release pattern
            _lock.Release();
            return value;
        }
        public void Enqueue(T item)
        {
            _lock.WaitOne();
            lock (_addRemoveLock)
            {
                _queue.Enqueue(item);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
           foreach(var el in _queue)
            {
                yield return el;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();        }
    }
}

