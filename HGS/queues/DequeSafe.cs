using System;
using System.Collections.Generic;

namespace Queues
{
    public class DequeSafe<T> : Deque<T>
    {
        #region Fields

        private object root = new Object();

        #endregion

        #region API

        public override void Push(T item)
        {
            lock(root)
            {
                base.Push(item);
            }
        }

        public override void Push(IEnumerable<T> range)
        {
            lock(root)
            {
                base.Push(range);
            }
        }

        public override void Prepend(T item)
        {
            lock(root)
            {
                base.Prepend(item);
            }
        }

        public override void Prepend(IEnumerable<T> range)
        {
            lock(root)
            {
                base.Prepend(range);
            }
        }

        #endregion
    }
}