using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Subscription<Tmessage> : IDisposable
    {
        public readonly MethodInfo MethodInfo;
        private readonly EventAggregator EventAggregator;
        public readonly WeakReference TargetObjet;
        public readonly bool IsStatic;

        private bool isDisposed;
        public Subscription(Action<Tmessage> action, EventAggregator eventAggregator)
        {
            MethodInfo = action.Method;
            if (action.Target == null)
                IsStatic = true;
            TargetObjet = new WeakReference(action.Target);
            EventAggregator = eventAggregator;
        }

        ~Subscription()
        {
            if (!isDisposed)
                Dispose();
        }

        public void Dispose()
        {
            EventAggregator.UnSbscribe(this);
            isDisposed = true;
        }

        public Action<Tmessage> CreatAction()
        {
            if (TargetObjet.Target != null && TargetObjet.IsAlive)
                return (Action<Tmessage>)Delegate.CreateDelegate(typeof(Action<Tmessage>), TargetObjet.Target, MethodInfo);
            if (this.IsStatic)
                return (Action<Tmessage>)Delegate.CreateDelegate(typeof(Action<Tmessage>), MethodInfo);

            return null;
        }
    }

    public interface IEventAggregator
    {
        Task Publish<TMessageType>(TMessageType message);
        Subscription<T> Subscribe<T>(Action<T> action);
        void UnSbscribe<TMessageType>(Subscription<TMessageType> subscription);
    }

    public class EventAggregator : IEventAggregator
    {
        private readonly object lockObj = new object();
        private Dictionary<Type, IList> subscriber = new Dictionary<Type, IList>();

        public EventAggregator()
        {
            subscriber = new Dictionary<Type, IList>();
        }

        public async Task Publish<TMessageType>(TMessageType message)
        {
            Type t = typeof(TMessageType);
            IList sublst;
            await Task.Run(() =>
            {
                if (subscriber.ContainsKey(t))
                {
                    lock (lockObj)
                    {
                        sublst = new List<Subscription<TMessageType>>(subscriber[t].Cast<Subscription<TMessageType>>());
                    }

                    foreach (Subscription<TMessageType> sub in sublst)
                    {
                        var action = sub.CreatAction();
                        if (action != null)
                            action(message);
                    }
                }
            });
        }


        public Subscription<TMessageType> Subscribe<TMessageType>(Action<TMessageType> action)
        {
            Type t = typeof(TMessageType);
            IList actionlst;
            var actiondetail = new Subscription<TMessageType>(action, this);

            lock (lockObj)
            {
                if (!subscriber.TryGetValue(t, out actionlst))
                {
                    actionlst = new List<Subscription<TMessageType>>();
                    actionlst.Add(actiondetail);
                    subscriber.Add(t, actionlst);
                }
                else
                {
                    actionlst.Add(actiondetail);
                }
            }

            return actiondetail;
        }

        public void UnSbscribe<TMessageType>(Subscription<TMessageType> subscription)
        {
            Type t = typeof(TMessageType);
            if (subscriber.ContainsKey(t))
            {
                lock (lockObj)
                {
                    subscriber[t].Remove(subscription);
                }
                subscription = null;
            }
        }

    }

    public class Publisher
    {
        IEventAggregator EventAggregator;
        public Publisher(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
        }

        public Task PublishMessage<T>(T obj)
        {
            return EventAggregator.Publish(obj);
        }
    }

    public class Subscriber
    {
        IEventAggregator eventAggregator;

        public Subscriber(IEventAggregator eve)
        {
            eventAggregator = eve;
        }
        
        public void Subscribe<T>(Action<T> action)
        {
            eventAggregator.Subscribe<T>(action);
        }

        public void UnSubscribe<T>(Subscription<T> obj)
        {
            eventAggregator.UnSbscribe(obj);
        }

    }
}
