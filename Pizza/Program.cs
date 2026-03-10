
using MyApp.PizzaClasses;
using MyApp.UIStuff;

//TODO: при изменении ингридиента изменить также цену и название везде, где он используется.
namespace MyApp{
    class Program
    {
        private static void Main(string[] args)
        {
            UI app = new();
            app.Run();
        }
    }
}