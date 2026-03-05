using MyApp.PizzaClasses;

namespace MyApp.Utility
{
    public static class UtilityClass
    {
        public static string ValidateInput(string value, string message)
        {
            while (string.IsNullOrWhiteSpace(value))
            {
                Console.Write(message);
                value = Console.ReadLine();
            }
            return value;
        }

        public static string ValidateInputForDeleteIngredient(int length, string value, string message)
        {
            //Console.Write("DEBUG: entered ValidateInputForDeleteIngredient");
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(value) && (int.Parse(value) >= 0 ) && (int.Parse(value) <= length))
                {
                    //Console.Write("DEBUG: exited ValidateInputForDeleteIngredient");
                    break;
                }
                else{
                    Console.Write(message);
                    value = Console.ReadLine();
                }
            }
            return value;
        }

        public static string ValidateInputForChangePizzas(int length, string value, string message)
        {
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(value) && (int.Parse(value) >= 0 ) && (int.Parse(value) <= length))
                {
                    break;
                }
                else{
                    Console.Write(message);
                    value = Console.ReadLine();
                }
            }
            return value;
        }

        public static void RemoveDuplicatesFromIngredientList(ref List<Ingredient> list_var)
        {
            list_var = [.. list_var
                .GroupBy(p => p.Name)
                .Select(g => g.OrderByDescending(p => p.Price).First())];
        }
        public static void RemoveDuplicatesFromBaseList(ref List<PizzaBase> list_var)
        {
            list_var = [.. list_var
                .GroupBy(p => p.Name)
                .Select(g => g.OrderByDescending(p => p.Price).First())];
        }

        public static bool CheckBasesAfterChangingClassic(List<PizzaBase> pizza_bases, int new_classic_price)
        {
            foreach(PizzaBase base_var in pizza_bases)
            {
                if (base_var.Price > new_classic_price * 1.2)
                {
                    return false;
                }
            }
            return true;
        }

        public static void UpdateClassicBasePriceOfAllBases(List<PizzaBase> pizza_bases, int new_classic_price)
        {
            foreach(PizzaBase base_var in pizza_bases)
            {
                base_var.ClassicBasePrice = new_classic_price;
            }
        }
    }
}