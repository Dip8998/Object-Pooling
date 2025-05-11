using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmicCuration.Utilities
{
    public class GenericObjectPool<T> where T : class
    {
        private List<PooledItem<T>> pooledItem = new List<PooledItem<T>>();

        protected T GetItem()
        {
            if(pooledItem.Count > 0)
            {
                PooledItem<T> item = pooledItem.Find(item => !item.isUsed);
                if(item != null)
                {
                    item.isUsed = true;
                    return item.Item;
                }
            }
            return CreatePooledItem();
        }

        private T CreatePooledItem()
        {
            PooledItem<T> newItem = new PooledItem<T>();

            newItem.Item = CreateItem();
            newItem.isUsed = true;
            pooledItem.Add(newItem);
            return newItem.Item;
        }

        protected virtual T CreateItem()
        {
            throw new NotImplementedException("Child class don't have implementation of CreateItem()");
        }

        public class PooledItem<T>
        {
            public T Item;
            public bool isUsed;
        }
    }
}
