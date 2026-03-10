
namespace MyApp.ClassFoundations{
    public abstract class PizzaPart
    {
        protected int _price;
        protected string _name;

        public virtual int Price
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
        public virtual string Name
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

        public virtual string Display()
        {
            return $"{_name}: {_price}₽";
        }
    }
}