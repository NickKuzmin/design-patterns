using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatterns.Core.BehavioralPatterns.Command
{
    public class BankAccount
    {
        private int _balance;
        private int overdraftLimit = -500;

        public void Deposit(int amount)
        {
            _balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance is now {_balance}");
        }

        public bool Withdraw(int amount)
        {
            if (_balance - amount >= overdraftLimit)
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
        private bool _succeeded;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            _account = account;
            _action = action;
            _amount = amount;
        }

        public void Call()
        {
            switch (_action)
            {
                case Action.Deposit:
                    _account.Deposit(_amount);
                    _succeeded = true;
                    break;
                case Action.Withdraw:
                    _succeeded = _account.Withdraw(_amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Undo()
        {
            if (!_succeeded) return;
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
    }

    class CompositeBankAccountCommand : List<BankAccountCommand>, ICommand
    {
        public void Call()
        {
            ForEach(cmd => cmd.Call());
        }

        public void Undo()
        {
            foreach (var cmd in
              ((IEnumerable<BankAccountCommand>)this).Reverse())
            {
                cmd.Undo();
            }
        }
    }

    class Demo
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount();
            var cmd = new BankAccountCommand(ba,
              BankAccountCommand.Action.Deposit, 100);
            cmd.Call();
            Console.WriteLine(ba);

            var commands = new List<BankAccountCommand>
            {
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 1000)
            };

            Console.WriteLine(ba);

            foreach (var c in commands)
                c.Call();

            Console.WriteLine(ba);

            foreach (var c in Enumerable.Reverse(commands))
                c.Undo();

            Console.WriteLine(ba);
        }
    }
}
