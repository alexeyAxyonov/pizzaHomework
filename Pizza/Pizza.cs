using MyApp.Interfaces;

namespace MyApp.PizzaClasses
{
    public class Pizza : PizzaPart
    {
        private string _name;
        private int _price;
        private PizzaBase _base;
        private List<Ingredient> _ingredients = [];

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

        public int Price
        {
            get => _price;

            set
            {
                // - "Рванёт?"
                // - "Да вроде не должно..."
            }
        }

        public void UpdatePrice()
        {
            int new_price = 0;
            new_price += _base.Price;

            foreach(Ingredient ingredient in _ingredients)
            {
                new_price += ingredient.Price;
            }
            _price = new_price;
        }

        public PizzaBase Base
        {
            get => _base;

            set
            {
                if (value is PizzaBase)
                {
                    _base = value;
                }
            }
        }
        public void AddIngredient(Ingredient ingredient)
        {
            _ingredients.Add(ingredient); // Да, можно добавить 238 ветчины на тонкую пиццу, но не мне судить такое решение со стороны клиента.
        }

        public string Display()
        {
            string display_string = @$"{_name}: {_price}₽
                Основа:
                    {_base.Name}: {_base.Price}₽
                Ингредиенты:" + "\n";
            int i = 1;
            foreach(Ingredient ingredient in _ingredients)
            {
                display_string += "                    " + i + ": " + ingredient.Display() + "\n";
                i++;
            }
            return display_string;
        }

        public string DisplayIngredients(){
            string display_string = "";
            int i = 1;
            foreach(Ingredient ingredient in _ingredients)
            {
                display_string += "                " + i + ": " + ingredient.Display() + "\n";
                i++;
            }
            return display_string;
        }

        public int CountIngredients()
        {
            return _ingredients.Count();
        }

        public void DeleteIngredient(int index)
        {
            _ingredients.RemoveAt(index);
        }
    }
}