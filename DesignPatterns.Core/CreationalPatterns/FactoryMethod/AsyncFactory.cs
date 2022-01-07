using System.Threading.Tasks;

namespace DesignPatterns.Core.CreationalPatterns.FactoryMethod
{
    public interface IAsyncInit<T>
    {
        Task<T> InitAsync();
    }

    public class Foo : IAsyncInit<Foo>
    {
        /*public*/
        public Foo()
        {
            // await Task.Delay(1000);
        }

        // ↓↓
        public async Task<Foo> InitAsync()
        {
            // some work here
            await Task.Delay(1000);
            return this; // fluent
        }

        public static Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return result.InitAsync();
        }
    }

    public static class AsyncFactory
    {
        public static async Task<T> Create<T>()
            where T : IAsyncInit<T>, new()
        {
            var result = await new T().InitAsync();
            return result;
        }
    }

    class AsyncFactoryDemo
    {
        public static async void Main(string[] args)
        {
            // var foo = new Foo();
            // await foo.InitAsync();

            // instead
            var foo = await Foo.CreateAsync();

            // or
            var foo2 = AsyncFactory.Create<Foo>();
        }
    }
}
