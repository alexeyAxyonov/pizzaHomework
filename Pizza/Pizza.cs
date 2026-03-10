using MyApp.ClassFoundations;
using MyApp.Utility;

namespace MyApp.PizzaClasses
{
    public class Pizza : PizzaPart
    {

        private PizzaBase _base;
        private List<Ingredient> _ingredients = new List<Ingredient> (16);
        private Edge _edge;
        private int _size = 0;

        public override int Price
        {
            get => _price;

            set {}
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

        public int Size
        {
            get => _size;
            set
            {
                _size = value;
            }
        }
        public void AddIngredient(Ingredient ingredient, bool do_remove_duplicates = true)
        {
            if (_ingredients.Count == 16)
            {
                Console.Write("            Невозможно добавить больше 16 ингредиентов");
            }
            else{
                _ingredients.Add(ingredient);
                if (do_remove_duplicates){
                    UtilityClass.RemoveDuplicatesFromIngredientList(ref _ingredients);
                }
            }
        }

        public List<Ingredient> GetIngredients()
        {
            return _ingredients;
        }

        public override string Display()
        {
            string additional_string = "";
            switch (_size)
            {
                case 6:
                    additional_string = "Маленькая";
                    break;
                case 8:
                    additional_string = "Средняя";
                    break;
                case 12:
                    additional_string = "Большая";
                    break;
                case 0:
                    break;
            }
            string display_string = @$"{_name}: {_price} {additional_string}₽
                Основа:
                    {_base.Name}: {_base.Price}₽
                Ингредиенты:" + "\n";
            display_string += DisplayIngredients();
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

        public Ingredient ReturnIngredient(int index)
        {
            return _ingredients[index];
        }
        /*
        public void FillIngredients(List<Ingredient> ingredients)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                _ingredients.Add(ingredient);
            }
            UpdatePrice();
        }
        */
    }
}