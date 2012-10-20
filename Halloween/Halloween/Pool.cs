using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halloween
{
    public static class Pool
    {
        internal static Dictionary<Type, InternalPool> Pools = new Dictionary<Type, InternalPool>();

        public static T Acquire<T>() where T : IRecyclable
        {
            var type = typeof(T);
            if (!Pools.ContainsKey(type))
                Pools.Add(type, new InternalPool(type));
            return (T)Pools[type].Acquire();
        }

        public static void Release<T>(T obj) where T : IRecyclable
        {
            var type = obj.GetType();
            if (!Pools.ContainsKey(type))
                return;
            Pools[type].Release(obj);
        }

    }

    internal sealed class InternalPool
    {
        readonly Type _type;
        readonly Stack<IRecyclable> _items = new Stack<IRecyclable>();

        internal InternalPool(Type type)
        {
            _type = type;
        }

        internal IRecyclable Acquire()
        {
            if (_items.Count == 0)
                _items.Push((IRecyclable)Activator.CreateInstance(_type, true));
            return _items.Pop();
        }

        internal void Release(IRecyclable obj)
        {
            obj.Recycle();
            _items.Push(obj);
        }
    }
}
