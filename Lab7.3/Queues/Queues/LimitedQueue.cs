﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queues
{
    class LimitedQueue<T>:IEnumerable<T> 
    {
        Queue<T> _queue;
        Semaphore _lock;
        public LimitedQueue(int maxSizeQueue)
        {
            _queue = new Queue<T>(maxSizeQueue);
            _lock = new Semaphore(maxSizeQueue, maxSizeQueue);
        }
        public T Dequeue()
        {
            T value = _queue.Dequeue();
            _lock.Release();
            return value;
        }
        public void Enqueue(T item)
        {
            _lock.WaitOne();
            _queue.Enqueue(item);
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
