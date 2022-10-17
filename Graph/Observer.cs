using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace Graph
{
    public class Observer :  IObserver<No> 
    {
        public IDisposable _unsubscriber;

        public virtual void Subscribe(IObservable<No> provider)
        {
            if (provider != null)
            {
                _unsubscriber = provider.Subscribe(this);
            }
        }

        public virtual void OnCompleted()
        {
            _unsubscriber.Dispose();
        }

        public virtual void OnError(Exception ex)
        { }

        public virtual void OnNext(No stock)
        { }
    }
}
