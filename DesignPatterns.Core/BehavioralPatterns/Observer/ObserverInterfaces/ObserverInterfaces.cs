﻿using System;
using System.Collections.Generic;

namespace DesignPatterns.Core.BehavioralPatterns.Observer.ObserverInterfaces
{
    public class Event
    {
        // something that can happen
    }

    public class FallsIllEvent : Event
    {
        public string Address;
    }

    public class Person : IObservable<Event>
    {
        private readonly HashSet<Subscription> _subscriptions
            = new HashSet<Subscription>();

        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var subscription = new Subscription(this, observer);
            _subscriptions.Add(subscription);
            return subscription;
        }

        public void CatchACold()
        {
            foreach (var sub in _subscriptions)
                sub.Observer.OnNext(new FallsIllEvent { Address = "123 London Road" });
        }

        private class Subscription : IDisposable
        {
            private Person person;
            public IObserver<Event> Observer;

            public Subscription(Person person, IObserver<Event> observer)
            {
                this.person = person;
                Observer = observer;
            }

            public void Dispose()
            {
                person._subscriptions.Remove(this);
            }
        }
    }

    public class Demo : IObserver<Event>
    {
        static void Main(string[] args)
        {
            new Demo();
        }

        public Demo()
        {
            var person = new Person();
            var sub = person.Subscribe(this);

            /*person.OfType<FallsIllEvent>()
                .Subscribe(args => WriteLine($"A doctor has been called to {args.Address}"));*/
        }

        public void OnNext(Event value)
        {
            if (value is FallsIllEvent args)
                Console.WriteLine($"A doctor has been called to {args.Address}");
        }

        public void OnError(Exception error) { }
        public void OnCompleted() { }
    }
}
