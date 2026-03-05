using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;
using MyApp.Interfaces;

namespace MyApp.PizzaClasses
{
    public class PizzaBase : PizzaPart
    {
        private int _price;
        private string? _name;
        private int _classic_base_price;
        public int Price
        {
            get => _price;

            set
            {
                Console.WriteLine("DEBUG: " + value);
                Console.WriteLine("DEBUG: " + _classic_base_price);
                Console.WriteLine("DEBUG: " + (value <= _classic_base_price*1.2? "true" : "false"));
                if (value > 0 && value <= _classic_base_price*1.2)
                {
                    _price = value;
                }
            }
        }

        public string Name
        {
            get => _name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Название не может быть пустой строкой: ", nameof(value));
                }
                _name = value;
            }
        }

        public int ClassicBasePrice
        {
            get => _classic_base_price;

            set
            {
                if (value > 0)
                {
                    _classic_base_price = value;
                }
            }
        }

        public string Display()
        {
            return $"{_name}: {_price}₽";
        }
    }
}