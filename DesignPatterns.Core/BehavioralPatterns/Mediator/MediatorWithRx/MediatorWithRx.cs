using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Core.BehavioralPatterns.Mediator.MediatorWithRx
{
    /*[UsedImplicitly]*/
    public class EventBroker : IObservable<EventArgs>
    {
        private readonly List<Subscription> _subscribers = new List<Subscription>();

        public IDisposable Subscribe(IObserver<EventArgs> subscriber)
        {
            var sub = new Subscription(this, subscriber);
            if (_subscribers.All(s => s.Subscriber != subscriber))
                _subscribers.Add(sub);
            return sub;
        }

        private void Unsubscribe(IObserver<EventArgs> subscriber)
        {
            _subscribers.RemoveAll(s => s.Subscriber == subscriber);
        }

        public void Publish<T>(T args) where T : EventArgs
        {
            foreach (var s in _subscribers.ToArray())
                s.Subscriber.OnNext(args); // will call Unsubscribe() from here
        }

        private class Subscription : IDisposable
        {
            private readonly EventBroker broker;
            public IObserver<EventArgs> Subscriber { get; private set; }
            public Subscription(EventBroker broker, IObserver<EventArgs> subscriber)
            {
                this.broker = broker;
                Subscriber = subscriber;
            }
            public void Dispose()
            {
                broker.Unsubscribe(Subscriber);
            }
        }
    }

    class PlayerScoredEventArgs : EventArgs
    {
        public string PlayerName;
        public int GoalsScoredSoFar;

        public PlayerScoredEventArgs
          (string playerName, int goalsScoredSoFar)
        {
            PlayerName = playerName;
            GoalsScoredSoFar = goalsScoredSoFar;
        }
    }

    /*[UsedImplicitly]*/
    public class Player
    {
        public string Name;
        private int goalsScored;
        private EventBroker broker;

        public delegate Player Factory(string name);

        public Player(string name, EventBroker broker)
        {
            Name = name;
            this.broker = broker;
        }

        public void Score()
        {
            goalsScored++;
            var args = new PlayerScoredEventArgs(Name, goalsScored);
            broker.Publish(args);
        }
    }

    /*[UsedImplicitly]*/
    public class Coach
    {
        private IDisposable subscription;

        public Coach(EventBroker broker)
        {
            /*subscription = broker
              .OfType<PlayerScoredEventArgs>()
              .Skip(1)
              .Take(3)
              .Subscribe(args =>
                Console.WriteLine($"Well done, {args.PlayerName}! ({args.GoalsScoredSoFar} goals)"));*/
        }
    }

    static class Program
    {
        public static void Main(string[] args)
        {
            /*
            var cb = new ContainerBuilder();
            cb.RegisterType<EventBroker>().SingleInstance();
            cb.RegisterType<Player>();
            cb.RegisterType<Coach>();

            var container = cb.Build();

            var pf = container.Resolve<Player.Factory>();
            var player = pf("John");

            var coach = container.Resolve<Coach>();

            player.Score();
            player.Score(); //
            player.Score(); //
            player.Score(); //
            player.Score();
            */
        }
    }
}
