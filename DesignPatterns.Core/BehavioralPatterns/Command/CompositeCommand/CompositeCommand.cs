﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Core.BehavioralPatterns.Command.CompositeCommand
{
    public class BankAccount
    {
        private int _balance;
        private readonly int _overdraftLimit = -500;

        public BankAccount(int balance = 0)
        {
            _balance = balance;
        }

        public void Deposit(int amount)
        {
            _balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance is now {_balance}");
        }

        public bool Withdraw(int amount)
        {
            if (_balance - amount >= _overdraftLimit)
            {
                _balance -= amount;
                Console.WriteLine($"Withdrew ${amount}, balance is now {_balance}");
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{nameof(_balance)}: {_balance}";
        }
    }

    public interface ICommand
    {
        void Call();

        void Undo();

        bool Success { get; set; }
    }

    public class BankAccountCommand : ICommand
    {
        private readonly BankAccount _account;

        public enum Action
        {
            Deposit, Withdraw
        }

        private readonly Action _action;
        private readonly int _amount;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            this._account = account;
            this._action = action;
            this._amount = amount;
        }

        public void Call()
        {
            switch (_action)
            {
                case Action.Deposit:
                    _account.Deposit(_amount);
                    Success = true;
                    break;
                case Action.Withdraw:
                    Success = _account.Withdraw(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Undo()
        {
            if (!Success) return;
            switch (_action)
            {
                case Action.Deposit:
                    _account.Withdraw(_amount);
                    break;
                case Action.Withdraw:
                    _account.Deposit(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool Success { get; set; }
    }

    public class CompositeBankAccountCommand
      : List<BankAccountCommand>, ICommand
    {
        public CompositeBankAccountCommand()
        {
        }

        public CompositeBankAccountCommand(IEnumerable<BankAccountCommand> collection) : base(collection)
        {
        }

        public virtual void Call()
        {
            Success = true;
            ForEach(cmd =>
            {
                cmd.Call();
                Success &= cmd.Success;
            });
        }

        public virtual void Undo()
        {
            foreach (var cmd in
              ((IEnumerable<BankAccountCommand>)this).Reverse())
            {
                cmd.Undo();
            }
        }

        public bool Success { get; set; }
    }

    public class MoneyTransferCommand : CompositeBankAccountCommand
    {
        public MoneyTransferCommand(BankAccount from,
          BankAccount to, int amount)
        {
            AddRange(new[]
            {
                new BankAccountCommand(from, BankAccountCommand.Action.Withdraw, amount),
                new BankAccountCommand(to, BankAccountCommand.Action.Deposit, amount),
            });
        }

        public override void Call()
        {
            BankAccountCommand last = null;
            foreach (var cmd in this)
            {
                if (last == null || last.Success)
                {
                    cmd.Call();
                    last = cmd;
                }
                else
                {
                    cmd.Undo();
                    break;
                }
            }
        }
    }

    class CompositeCommandDemo
    {
        static void Main(string[] args)
        {
            // composite
            var ba = new BankAccount();
            var cmdDeposit = new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100);
            var cmdWithdraw = new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 1000);
            var composite = new CompositeBankAccountCommand(new[]{ cmdDeposit, cmdWithdraw });

            composite.Call();
            Console.WriteLine(ba);

            composite.Undo();
            Console.WriteLine(ba);


            // money transfer
            var from = new BankAccount();
            from.Deposit(100);
            var to = new BankAccount();

            var mtc = new MoneyTransferCommand(from, to, 1000);
            mtc.Call();

            Console.WriteLine(from);
            Console.WriteLine(to);

            mtc.Undo();

            Console.WriteLine(from);
            Console.WriteLine(to);
        }
    }
}
