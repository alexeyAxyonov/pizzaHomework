using System.Globalization;
using System.IO.Pipelines;
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

        public static DateTime ValidateDateTimeInput(string value, string message)
        {
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (DateTime.TryParseExact(
                        value.Trim(), "dd/MM/yyyy HH/mm/ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                    {
                        return result;
                    }
                    else
                    {
                        value = "";
                    }
                }
                else
                {
                    Console.Write(message);
                    value = Console.ReadLine();
                }
            }
        }

        public static int ValidatePizzaSize(int value, string message)
        {
            string var = "";
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(var))
                {
                    value = int.Parse(var);
                    if (value == 6 || value == 10 || value == 12)
                    {
                        return value;
                    }
                    else
                    {
                        var = "";
                    }
                }
                else
                {
                    Console.Write(message);
                    var = Console.ReadLine();
                }
            }
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

        public static void RemoveDuplicatesFromIngredientArray(ref Ingredient[] arr_var)
        {
            arr_var = [.. arr_var.Distinct()];
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