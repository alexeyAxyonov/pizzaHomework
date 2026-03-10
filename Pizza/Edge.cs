using MyApp.ClassFoundations;
using MyApp.Utility;

namespace MyApp.PizzaClasses
{
    class Edge : PizzaPart
    {
        private List<Ingredient> _ingredients = new List<Ingredient> (3);
        private List<Pizza> _allowed_pizzas = [];

        public override int Price
        {
            get => _price;
            set {}
        }

        public List<Pizza> AllowedPizzas
        {
            get => _allowed_pizzas;
            set {}
        }

        public void UpdatePrice()
        {
            int new_price = 0;

            foreach(Ingredient ingredient in _ingredients)
            {
                new_price += ingredient.Price;
            }
            _price = new_price;
        }

        public void AddIngredient(Ingredient ingredient)
        {
            if (_ingredients.Count == 3)
            {
                Console.Write("            Невозможно добавить больше трёх ингредиентов");
            }
            else{
                _ingredients.Add(ingredient);
                UtilityClass.RemoveDuplicatesFromIngredientList(ref _ingredients);
            }
        }

        public int CountIngredients()
        {
            return _ingredients.Count();
        }

        public void DeleteIngredient(int index)
        {
            _ingredients.RemoveAt(index);
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

        public void AddAllowedPizza(Pizza pizza)
        {
            _allowed_pizzas.Add(pizza);
        }

        public int CountAllowedPizzas()
        {
            return _allowed_pizzas.Count;
        }

        public void DeleteAllowedPizza(int index)
        {
            _allowed_pizzas.RemoveAt(index);
        }

        public string DisplayAllowedPizzas()
        {
            string display_string = @"            Совместимые пиццы:" + "\n";
            int i = 1;
            foreach (Pizza pizza in _allowed_pizzas)
            {
                display_string += $@"            {i}: {pizza.Name}" + "\n";
            }
            return display_string;
        }

        public override string Display()
        {
            string display_string = @$"{_name}: {_price}₽
                Ингредиенты:" + "\n";
            display_string += DisplayIngredients();
            display_string += DisplayAllowedPizzas();
            return display_string;
        }
    }
}