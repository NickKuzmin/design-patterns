﻿using System;
using System.Dynamic;

namespace DesignPatterns.Core.BehavioralPatterns.NullObject.NullObjectDynamic
{
    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }

    class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
            Console.WriteLine(msg);
        }

        public void Warn(string msg)
        {
            Console.WriteLine("WARNING: " + msg);
        }
    }

    class OptionalLog : ILog
    {
        private ILog impl;
        public static ILog NoLogging = null;

        public OptionalLog(ILog impl)
        {
            this.impl = impl;
        }

        public void Info(string msg)
        {
            impl?.Info(msg);
        }

        public void Warn(string msg)
        {
            throw new NotImplementedException();
        }
    }

    public class BankAccount
    {
        private ILog log;
        private int balance;

        public BankAccount(ILog log)
        {
            this.log = new OptionalLog(log);
        }

        public void Deposit(int amount)
        {
            balance += amount;
            // check for null everywhere
            log?.Info($"Deposited ${amount}, balance is now {balance}");
        }

        public void Withdraw(int amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                log?.Info($"Withdrew ${amount}, we have ${balance} left");
            }
            else
            {
                log?.Warn($"Could not withdraw ${amount} because " +
                          $"balance is only ${balance}");
            }
        }
    }

    public class Null<T> : DynamicObject where T : class
    {
        /*
        public static T Instance
        {
            get
            {
                if (!typeof(T).IsInterface)
                    throw new ArgumentException("I must be an interface type");

                return new Null<T>().ActLike<T>();
            }
        }
        */
        public override bool TryInvokeMember(InvokeMemberBinder binder,
          object[] args, out object result)
        {
            var name = binder.Name;
            result = Activator.CreateInstance(binder.ReturnType);
            return true;
        }
    }

    public class Demo
    {
        static void Main()
        {
            /*
            var log = Null<ILog>.Instance;
            var ba = new BankAccount(log);
            ba.Deposit(100);
            ba.Withdraw(200);
            */
        }
    }
}
