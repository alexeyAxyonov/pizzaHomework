

namespace MyApp.PizzaClasses
{
    public class Order
    {
        private int _number_of_order;
        private List<Pizza> _pizzas = [];
        private List<List<Pizza>> _pizza_halves = [];
        private int _price;
        public Guid Id {get; private set;}
        public string Comment { get; set; }
        public DateTime TimeOfOrder { get; private set; }
        public DateTime TimeOfDelivery { get; private set; }

        public int NumberOfOrder
        {
            get => _number_of_order;
            set 
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Значение номера заказа не может быть отрицательным или равным нулю");
                }
                _number_of_order = value;
            }
        }
        public int Price
        {
            get => _price;
            set {}
        }

        public void UpdatePrice()
        {
            int new_price = 0;
            foreach (Pizza pizza in _pizzas)
            {
                new_price += pizza.Price;
            }
            foreach (List<Pizza> list_var in _pizza_halves)
            {
                foreach (Pizza pizza_half in list_var)
                {
                    new_price += pizza_half.Price/2;
                }
            }
            _price = new_price;
        }

        public void AddPizza(Pizza pizza)
        {
            _pizzas.Add(pizza);
        }

        public void RemovePizza(int index)
        {
            _pizzas.RemoveAt(index);
        }

        public void AddPizzaHalf(Pizza pizza, int index)
        {
            if (_pizza_halves.Count == index)
            {
                _pizza_halves[index] = [];
            }
            if (_pizza_halves[index].Count < 2)
            {
                _pizza_halves[index].Add(pizza);
            }
            else
            {
                Console.Write(@"            Пицца уже полная.");
            }
        }
        public void AddTwoHalves(List<Pizza> halves)
        {
            _pizza_halves.Add(halves);
        }
        public void RemovePizzaHalf(Pizza pizza, int index, int half_index)
        {
            _pizza_halves[index].RemoveAt(half_index);
        }
        public void ClearPizzaHalves(int index)
        {
            _pizza_halves.RemoveAt(index);
        }
        public string Display()
        {
            string display_string = @"
            Id: " + Id + "\n";
            display_string += $@"
            Время заказа: {TimeOfOrder:dd/MM/yyyy HH:mm:ss}" + "\n";
            if (TimeOfOrder != TimeOfDelivery)
            {
                display_string += @"
            Время, к которому должен быть готов заказ: " + TimeOfDelivery + "\n";
            }
            display_string += @"
            Цена: " + _price + "\n";
            display_string += @"
            Пиццы:
            ";
            int i = 1;
            foreach (Pizza pizza in _pizzas)
            {
                display_string += @$"            {i}: {pizza.Name} {pizza.Price}" + "\n";
            }
            if (_pizza_halves.Count > 0){
                i = 1;
                display_string += @"
                Двойные пиццы:
                ";
                foreach (List<Pizza> pizza_half in _pizza_halves)
                {
                    display_string += @$"            {i}.1: {pizza_half[0].Name} {pizza_half[0].Price/2}" + "\n";
                    display_string += @$"            {i}.2: {pizza_half[1].Name} {pizza_half[1].Price/2}" + "\n";
                }
            }
            return display_string;
        }

        public void UpdateDeliveryTime(DateTime time)
        {
            TimeOfDelivery = time;
        }
        public void FinishOrder(int num_of_order)
        {
            NumberOfOrder = num_of_order;
            TimeOfOrder = DateTime.Now;
            Id = Guid.NewGuid();
        }
    }
}