using MyApp.Interfaces;

namespace MyApp.PizzaClasses
{
    public class Ingredient : PizzaPart
    {
        private int _price;
        private string _name;

        public int Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Цена не может быть отрицательной или равной нулю: ", nameof(value));
                }
                _price = value;
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

        public string Display()
        {
            return $"{_name}: {_price}₽";
        }
    }
}