
using System.Linq.Expressions;
using MyApp.PizzaClasses;
using MyApp.Utility;

namespace MyApp.UIStuff
{
    public class UI
    {
        private List<Ingredient> _ingredients = [
            new Ingredient {Name = "Моцарелла", Price = 125},
            new Ingredient {Name = "Пепперони", Price = 99},
            new Ingredient {Name = "Курица", Price = 99},
            new Ingredient {Name = "Ветчина", Price = 99},
            new Ingredient {Name = "Маринованые огурцы", Price = 79},
            new Ingredient {Name = "Томаты", Price = 79},
            new Ingredient {Name = "Красный лук", Price = 79},
            new Ingredient {Name = "Сочные ананасы", Price = 79},
            new Ingredient {Name = "Сладкий перец", Price = 59}
        ];
        private List<PizzaBase> _pizza_bases = [
            new PizzaBase {Name = "Классическая", ClassicBasePrice = 100, Price = 100},
            new PizzaBase {Name = "Чёрная", ClassicBasePrice = 100, Price = 120},
            new PizzaBase {Name = "Толстая", ClassicBasePrice = 100, Price = 120}
        ];
        private List<Pizza> _pizzas = [];
        private int _classic_base_price = 100;
        public void Run()
        {
            bool exit_required = false;
            string message = @"
            =========================================================================================
            1. Создать ингридиент        | 5. Создать основу для пиццы     | 9. Создать пиццу
            2. Удалить ингридиент        | 6. Удалить основу для пиццы     | 10. Удалить пиццу
            3. Изменить ингридиент       | 7. Изменить основу для пиццы    | 11. Изменить пиццу
            4. Показать все ингридиенты  | 8. Показать все основы для пицц | 12. Показать все пиццы
            0. Выйти                     |                                 |
            =========================================================================================
            Выберите действие: ";
            while (!exit_required)
            {
                string input = "";
                input = UtilityClass.ValidateInput(input, message);

                int input_int = int.Parse(input);

                switch (input_int)
                {
                    case 0:
                        Console.WriteLine("            Хорошего вам дня!");
                        exit_required = true;
                        break;
                    case 1:
                        MakeIngridient();
                        break;
                    case 2:
                        DeleteIngridient();
                        break;
                    case 3:
                        ChangeIngridient();
                        break;
                    case 4:
                        WriteIngridients();
                        break;
                    case 5:
                        MakePizzaBase();
                        break;
                    case 6:
                        DeletePizzaBase();
                        break;
                    case 7:
                        ChangePizzaBase();
                        break;
                    case 8:
                        WritePizzaBases();
                        break;
                    case 9:
                        MakePizza();
                        break;
                    case 10:
                        DeletePizza();
                        break;
                    case 11:
                        ChangePizza();
                        break;
                    case 12:
                        WritePizzas();
                        break;
                    default:
                        Console.Write("            Неопознанное действие \n");
                        continue;
                }
            }
        }
        private void MakeIngridient()
        {
            Ingredient ingredient_var = new();

            string message = @"
            =========================================================================================
            Введите название ингредиента: ";
            string ingredient_name = "";
            ingredient_name = UtilityClass.ValidateInput(ingredient_name, message);
            ingredient_var.Name = ingredient_name;

            message = @"
            =========================================================================================
            Введите цену: ";
            string ingredient_price = "";
            ingredient_price = UtilityClass.ValidateInput(ingredient_price, message);
            ingredient_var.Price = int.Parse(ingredient_price);

            _ingredients.Add(ingredient_var);
            UtilityClass.RemoveDuplicatesFromIngredientList(ref _ingredients);
        }
        private void DeleteIngridient()
        {
            WriteIngridients();
            Console.Write(@"
            =========================================================================================
            0: Выйти
            ");
            string message = @"
            =========================================================================================
            Выберите номер ингредиента, который вы хотите удалить: ";
            string choice_str = "";

            choice_str = UtilityClass.ValidateInputForDeleteIngredient(_ingredients.Count, choice_str, message);
            int choice = int.Parse(choice_str);

            if (choice != 0)
            {
                _ingredients.RemoveAt(choice-1);
            }
        }
        private void ChangeIngridient()
        {
            WriteIngridients();
            Console.Write(@"
            =========================================================================================
            0: Выйти
            ");
            string message = @"
            =========================================================================================
            Выберите номер ингредиента, который вы хотите изменить:";
            string choice_str = "";

            choice_str = UtilityClass.ValidateInputForDeleteIngredient(_ingredients.Count, choice_str, message);
            int choice = int.Parse(choice_str);

            if (choice != 0)
            {
                Ingredient ingredient_for_change = _ingredients[choice-1];
                message = @"
            =========================================================================================
            Введите новое название: ";
                choice_str = "";
                choice_str = UtilityClass.ValidateInput(choice_str, message);
                ingredient_for_change.Name = choice_str;

                message = @"
            =========================================================================================
            Введите новую цену: ";
                choice_str = "";
                choice_str = UtilityClass.ValidateInput(choice_str, message);
                ingredient_for_change.Price = int.Parse(choice_str);
            }
        }
        private void WriteIngridients()
        {
            Console.Write(@"
            =========================================================================================
            Ингредиенты:" + "\n");
            int i = 1;
            foreach(Ingredient ingredient in _ingredients)
            {
                Console.WriteLine(@"            " + i + ": " + ingredient.Display());
                i++;
            }
        }

        private void MakePizzaBase()
        {
            PizzaBase pizza_base_var = new();

            string message = @"
            =========================================================================================
            Введите название основы: ";
            string base_name = "";
            base_name = UtilityClass.ValidateInput(base_name, message);
            pizza_base_var.Name = base_name;

            message = @"
            =========================================================================================
            Введите цену: ";
            string base_price = "";
            base_price = UtilityClass.ValidateInput(base_price, message);

            if (pizza_base_var.Name == "Классическая")
            {
                bool classic_can_be_changed = UtilityClass.CheckBasesAfterChangingClassic(_pizza_bases, int.Parse(base_price));
                if (classic_can_be_changed)
                {
                    pizza_base_var.ClassicBasePrice = int.Parse(base_price);
                    pizza_base_var.Price = int.Parse(base_price);
                    _pizza_bases.Add(pizza_base_var);
                    UtilityClass.RemoveDuplicatesFromBaseList(ref _pizza_bases);
                    UtilityClass.UpdateClassicBasePriceOfAllBases(_pizza_bases, pizza_base_var.Price);
                    _classic_base_price = pizza_base_var.Price;
                }
                else
                {
                    Console.WriteLine("\n" + "            Основа не может быть данной цены.");
                }
            }
            else
            {
                pizza_base_var.ClassicBasePrice = _classic_base_price;
                pizza_base_var.Price = int.Parse(base_price);
                if (pizza_base_var.Price != 0)
                {
                    _pizza_bases.Add(pizza_base_var);
                    UtilityClass.RemoveDuplicatesFromBaseList(ref _pizza_bases);
                }
                else
                {
                    Console.WriteLine("\n" + "            Основа не может быть данной цены.");
                }
            }
        }

        private void DeletePizzaBase()
        {
            WritePizzaBases();
            Console.Write(@"
            =========================================================================================
            0: Выйти
            ");
            string message = @"
            =========================================================================================
            Выберите номер основы, который вы хотите удалить: ";
            string choice_str = "";

            choice_str = UtilityClass.ValidateInputForDeleteIngredient(_pizza_bases.Count, choice_str, message);
            int choice = int.Parse(choice_str);

            if (choice != 0)
            {
                if(_pizza_bases[choice-1].Name == "Классическая")
                {
                    Console.WriteLine("            Невозможно удалить классическую основу");
                    
                }
                else{
                    _pizza_bases.RemoveAt(choice-1);
                }
            }
        }

        private void ChangePizzaBase()
        {
            WritePizzaBases();
            Console.Write(@"
            =========================================================================================
            0: Выйти
            ");
            string message = @"
            =========================================================================================
            Выберите номер основы, который вы хотите изменить:";
            string choice_str = "";

            choice_str = UtilityClass.ValidateInputForDeleteIngredient(_pizza_bases.Count, choice_str, message);
            int choice = int.Parse(choice_str);

            if (choice != 0)
            {
                PizzaBase base_for_change = _pizza_bases[choice-1];
                string name_holder = "";
                if(base_for_change.Name != "Классическая")
                {
                    message = @"
            =========================================================================================
            Введите новое название: ";
                    name_holder = UtilityClass.ValidateInput(name_holder, message);
                }

                message = @"
            =========================================================================================
            Введите новую цену: ";
                choice_str = "";
                choice_str = UtilityClass.ValidateInput(choice_str, message);
                int price_holder = base_for_change.Price;
                if (base_for_change.Name == "Классическая")
                {
                    bool classic_can_be_changed = UtilityClass.CheckBasesAfterChangingClassic(_pizza_bases, int.Parse(choice_str));
                    if (classic_can_be_changed)
                    {
                        base_for_change.ClassicBasePrice = int.Parse(choice_str);
                        base_for_change.Price = int.Parse(choice_str);
                        _pizza_bases.Add(base_for_change);
                        UtilityClass.RemoveDuplicatesFromBaseList(ref _pizza_bases);
                        UtilityClass.UpdateClassicBasePriceOfAllBases(_pizza_bases, base_for_change.Price);
                        _classic_base_price = base_for_change.Price;
                    }
                    else
                    {
                        Console.WriteLine("\n" + "            Основа не может быть данной цены.");
                    }
                }
                else{
                    base_for_change.Price = int.Parse(choice_str);
                    if (base_for_change.Price == int.Parse(choice_str))
                    {
                        Console.WriteLine("DEBUG: CHANGE_BASE_FLAG");
                        base_for_change.Name = name_holder;
                    }
                    else
                    {
                        Console.WriteLine("\n" + "            Основа не может быть данной цены.");
                    }
                }
            }
        }

        private void WritePizzaBases()
        {
            Console.Write(@"
            =========================================================================================
            Основы:" + "\n");
            int i = 1;
            foreach(PizzaBase pizza_base in _pizza_bases)
            {
                Console.WriteLine(@"            " + i + ": " + pizza_base.Display());
                i++;
            }
        }
        private void MakePizza()
        {
            Pizza new_pizza = new();

            string message = @"
            =========================================================================================
            Введите название: ";
            string name_var = "";
            name_var = UtilityClass.ValidateInput(name_var, message);
            new_pizza.Name = name_var;

            WritePizzaBases();
            message = @"
            =========================================================================================
            Выберите основу: ";
            string base_var = "";
            base_var = UtilityClass.ValidateInputForDeleteIngredient(_pizza_bases.Count, base_var, message);
            int base_int = int.Parse(base_var);
            new_pizza.Base = _pizza_bases[base_int-1];

            WriteIngridients();
            Console.WriteLine("            0: Все ингредиенты добавлены");
            message = @"
            =========================================================================================
            Выберите ингредиенты: ";
            string ingredient_var = "";

            while (ingredient_var != "0")
            {
                ingredient_var = UtilityClass.ValidateInputForDeleteIngredient(_ingredients.Count, ingredient_var, message);
                if (ingredient_var != "0")
                {
                    int ingredient_index = int.Parse(ingredient_var) - 1;
                    Ingredient added_ingredient = _ingredients[ingredient_index];
                    new_pizza.AddIngredient(added_ingredient);
                    ingredient_var = "";
                }
            }
            new_pizza.UpdatePrice();
            _pizzas.Add(new_pizza);
        }
        private void DeletePizza()
        {
            WritePizzas();
            Console.Write(@"
            =========================================================================================
            0: Выйти
            ");
            string message = @"
            =========================================================================================
            Выберите номер пиццы, который вы хотите удалить: ";
            string choice_str = "";

            choice_str = UtilityClass.ValidateInputForDeleteIngredient(_pizzas.Count, choice_str, message);
            int choice = int.Parse(choice_str);

            if (choice != 0)
            {
                _pizzas.RemoveAt(choice-1);
            }
        }
        private void ChangePizza()
        {
            WritePizzas();
            Console.Write(@"
            =========================================================================================
            0: Выйти
            ");
            string message = @"
            =========================================================================================
            Выберите номер пиццы, который вы хотите изменить:";
            string choice_str = "";

            choice_str = UtilityClass.ValidateInputForDeleteIngredient(_pizzas.Count, choice_str, message);
            int choice = int.Parse(choice_str);

            Pizza pizza_for_change = _pizzas[choice-1];

            while (choice != 0)
            {
                Console.Write(@"
            =========================================================================================
            Что вы хотите изменить?");
                message = @"
            0. Выйти
            1. Название
            2. Основу
            3. Удалить ингредиенты
            4. Добавить ингредиенты
            ";
                choice_str = "";
                choice_str = UtilityClass.ValidateInputForDeleteIngredient(4, choice_str, message);

                choice = int.Parse(choice_str);
                //Надо разбросать это по маленьким функциям, т.к. выше повторяется похожий код.
                switch(choice){
                    case 1:
                        string new_name = "";
                        new_name = UtilityClass.ValidateInput(new_name, "            Введите новое название: ");
                        pizza_for_change.Name = new_name;
                        break;
                    case 2:
                        WritePizzaBases();
                        Console.Write(@"            0: Выйти
                        ");
                        string base_choice = "";
                        base_choice = UtilityClass.ValidateInputForDeleteIngredient(_pizza_bases.Count(), base_choice, @"               Введите номер новой основы: ");
                        if (base_choice != "0"){
                            pizza_for_change.Base = _pizza_bases[int.Parse(base_choice) - 1];
                        }
                        break;
                    case 3:
                        Console.Write(pizza_for_change.DisplayIngredients());
                        Console.Write("            0: Выйти");
                        string ingredient_choice = "";
                        ingredient_choice = UtilityClass.ValidateInputForDeleteIngredient(pizza_for_change.CountIngredients(), ingredient_choice, @"            Введите номер ингредиента, который вы хотите удалить: ");

                        if (ingredient_choice != "0")
                        {
                            pizza_for_change.DeleteIngredient(int.Parse(ingredient_choice) - 1);
                        }
                        break;
                    case 4:
                        WriteIngridients();
                        Console.WriteLine(@"
            0: Все ингредиенты добавлены");
                        message = @"
            =========================================================================================
            Выберите ингредиенты: ";
                        string ingredient_var = "";

                        while (ingredient_var != "0")
                        {
                            ingredient_var = UtilityClass.ValidateInputForDeleteIngredient(_ingredients.Count, ingredient_var, message);
                            if (ingredient_var != "0")
                            {
                                int ingredient_index = int.Parse(ingredient_var) - 1;
                                Ingredient added_ingredient = _ingredients[ingredient_index];
                                pizza_for_change.AddIngredient(added_ingredient);
                                ingredient_var = "";
                            }
                        }
                        break;
                }
                pizza_for_change.UpdatePrice();
            }
        }
        private void WritePizzas()
        {
            int i = 1;
            foreach (Pizza pizza in _pizzas)
            {
                Console.Write(@"            " + i + ": " + pizza.Display());
            }
        }
    }
}