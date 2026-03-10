
using System.Linq.Expressions;
using System.Threading.Tasks.Dataflow;
using MyApp.PizzaClasses;
using MyApp.Utility;

namespace MyApp.UIStuff
{
    public class UI
    {
        private int _amount_of_orders = 0;
        private List<Ingredient> _ingredients = [
            new Ingredient {Name = "Моцарелла", Price = 125},
            new Ingredient {Name = "Сыры чеддер и пармезан", Price = 89},
            new Ingredient {Name = "Пепперони", Price = 99},
            new Ingredient {Name = "Курица", Price = 99},
            new Ingredient {Name = "Ветчина", Price = 99},
            new Ingredient {Name = "Бекон", Price = 99},
            new Ingredient {Name = "Маринованые огурцы", Price = 79},
            new Ingredient {Name = "Томаты", Price = 79},
            new Ingredient {Name = "Красный лук", Price = 79},
            new Ingredient {Name = "Сочные ананасы", Price = 79},
            new Ingredient {Name = "Сладкий перец", Price = 59},
            new Ingredient {Name = "Чеснок", Price = 29},
            new Ingredient {Name = "Итальянские травы", Price = 59}
        ];
        private List<PizzaBase> _pizza_bases = [
            new PizzaBase {Name = "Классическая", ClassicBasePrice = 100, Price = 100},
            new PizzaBase {Name = "Чёрная", ClassicBasePrice = 100, Price = 120},
            new PizzaBase {Name = "Толстая", ClassicBasePrice = 100, Price = 120}
        ];
        private List<Pizza> _pizzas = [];
        private List<Edge> _edges = [
            new Edge{Name = "Обычный"},
        ];
        private List<Order> _orders = [];

        private int _classic_base_price = 100;
        public void Run()
        {
            bool exit_required = false;
            string message = @"
            ===================================================================================================================================
            1. Создать ингридиент    | 5. Создать основу для пиццы     | 9. Создать пиццу       | 13. Создать бортик   | 17. Сделать заказ
            2. Удалить ингридиент    | 6. Удалить основу для пиццы     | 10. Удалить пиццу      | 14. Удалить бортик   | 18. Показать все заказы
            3. Изменить ингридиент   | 7. Изменить основу для пиццы    | 11. Изменить пиццу     | 15. Изменить бортик  |
            4. Показать ингридиенты  | 8. Показать основы для пицц     | 12. Показать все пиццы | 16. Показать все бортики 
            19. Компаратор по цене   | 20. Компаратор по совместимости | 21. Компаратор по ингридиентам                | 22. Компаратор по дате
            ===================================================================================================================================
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
                        WriteIngridients(_ingredients);
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
                        WritePizzaBases(_pizza_bases);
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
                        WritePizzas(_pizzas);
                        break;
                    case 13:
                        MakeEdge();
                        break;
                    case 14:
                        DeleteEdge();
                        break;
                    case 15:
                        ChangeEdge();
                        break;
                    case 16:
                        WriteEdges(_edges);
                        break;
                    case 17:
                        MakeOrder();
                        break;
                    case 18:
                        WriteOrders(_orders);
                        break;
                    case 19:
                        ComparePrice();
                        break;
                    case 20:
                        CompareAllowances();
                        break;
                    case 21:
                        CompareIngredients();
                        break;
                    case 22:
                        CompareDateOfOrder();
                        break;
                    default:
                        Console.Write("            Неопознанное действие \n");
                        continue;
                }
            }
        }
        private void ComparePrice()
        {
            string price_comp = "";
            List<Ingredient> compared_ingredients;
            List<PizzaBase> compared_bases;
            List<Pizza> compared_pizzas;
            List<Edge> compared_edges;
            List<Order> compared_orders;
            price_comp = UtilityClass.ValidateInput(price_comp, @"          Введите цену, по которой вы будете сравнивать: ");
            string mode = "";
            Console.WriteLine(@"
            1. Показать всё, что выше этой цены
            2. Показать всё, что ниже этой цены
            0. Выйти");
            mode = UtilityClass.ValidateInputForDeleteIngredient(2, mode, @"            Введите, как вы будете сравнивать: ");
            switch (mode)
            {
                case "1":
                    Console.WriteLine("Ингредиенты: ");
                    compared_ingredients = [.. _ingredients
                        .Where(p => p.Price > int.Parse(price_comp))
                        .OrderBy(p => p.Price)];
                    WriteIngridients(compared_ingredients);

                    Console.WriteLine("Основы: ");
                    compared_bases = [.. _pizza_bases
                        .Where(p => p.Price > int.Parse(price_comp))
                        .OrderBy(p => p.Price)];
                    WritePizzaBases(compared_bases);

                    Console.WriteLine("Пиццы: ");
                    compared_pizzas = [.. _pizzas
                        .Where(p => p.Price > int.Parse(price_comp))
                        .OrderBy(p => p.Price)];
                    WritePizzas(compared_pizzas);

                    Console.WriteLine("Бортики: ");
                    compared_edges = [.. _edges
                        .Where(p => p.Price > int.Parse(price_comp))
                        .OrderBy(p => p.Price)];
                    WriteEdges(compared_edges);

                    Console.WriteLine("Заказы: ");
                    compared_orders = [.. _orders
                        .Where(p => p.Price > int.Parse(price_comp))
                        .OrderBy(p => p.Price)];
                    WriteOrders(compared_orders);

                    break;
                case "2":
                    Console.Write("Ингредиенты: ");
                    compared_ingredients = [.. _ingredients
                        .Where(p => p.Price < int.Parse(price_comp))
                        .OrderBy(p => p.Price)];
                    WriteIngridients(compared_ingredients);

                    Console.WriteLine("Основы: ");
                    compared_bases = [.. _pizza_bases
                        .Where(p => p.Price < int.Parse(price_comp))
                        .OrderBy(p => p.Price)];
                    WritePizzaBases(compared_bases);

                    Console.WriteLine("Пиццы: ");
                    compared_pizzas = [.. _pizzas
                        .Where(p => p.Price < int.Parse(price_comp))
                        .OrderBy(p => p.Price)];
                    WritePizzas(compared_pizzas);

                    Console.WriteLine("Бортики: ");
                    compared_edges = [.. _edges
                        .Where(p => p.Price < int.Parse(price_comp))
                        .OrderBy(p => p.Price)];
                    WriteEdges(compared_edges);

                    Console.WriteLine("Заказы: ");
                    compared_orders = [.. _orders
                        .Where(p => p.Price < int.Parse(price_comp))
                        .OrderBy(p => p.Price)];
                    WriteOrders(compared_orders);
                    break;
            }
        }
        private void CompareAllowances()
        {
            List<Edge> compared_edges;
            Pizza compared_pizza;
            WritePizzas(_pizzas);
            Console.WriteLine(@"            0: Выход");
            string compatibility_var = "";
            compatibility_var = UtilityClass.ValidateInputForDeleteIngredient(_pizzas.Count(), compatibility_var, @"            Введите пиццу: ");

            string choice_var = "";
            Console.WriteLine(@"
            1. Показать все бортики, совместимые с этой основой
            2. Показать все бортики, не совместимые с этой основой
            0. Выйти");
            choice_var = UtilityClass.ValidateInputForDeleteIngredient(2, choice_var, @"            Введите, как будете сравнивать: ");
            switch(choice_var){
                case "1":
                    compared_pizza = _pizzas[int.Parse(compatibility_var) - 1];
                    compared_edges = [.._edges
                    .Where(p => p.AllowedPizzas.Contains(compared_pizza))
                    .OrderBy(p => p.Price)];
                    break;
                case "2":
                    compared_pizza = _pizzas[int.Parse(compatibility_var) - 1];
                    compared_edges = [.._edges
                    .Where(p => !p.AllowedPizzas.Contains(compared_pizza))
                    .OrderBy(p => p.Price)];
                    break;
                case "0":
                    break;
                }
        }
        private void CompareIngredients()
        {
            List<Pizza> compared_pizzas;
            WriteIngridients(_ingredients);
            Console.WriteLine(@"            0: Выход");
            string ingredient_var = "";
            ingredient_var = UtilityClass.ValidateInputForDeleteIngredient(_ingredients.Count(), ingredient_var, @"            Введите ингредиент: ");

            string choice_var = "";
            Console.WriteLine(@"
            1. Показать все пиццы с этим ингредиентом
            2. Показать все пиццы без этого ингредиента
            0. Выйти");
            choice_var = UtilityClass.ValidateInputForDeleteIngredient(2, choice_var, @"            Введите, как будете сравнивать: ");
            switch(choice_var){
                case "1":
                    compared_pizzas = [.._pizzas
                    .Where(p => p.GetIngredients().Contains(_ingredients[int.Parse(ingredient_var)]))];
                    WritePizzas(compared_pizzas);
                    break;
                case "2":
                    compared_pizzas = [.._pizzas
                    .Where(p => !p.GetIngredients().Contains(_ingredients[int.Parse(ingredient_var)]))];
                    WritePizzas(compared_pizzas);
                    break;
                case "0":
                    break;
                }
        }
        private void CompareDateOfOrder()
        {
            List<Order> compared_orders;
            DateTime time_var;
            string order_time = "";
            time_var = UtilityClass.ValidateDateTimeInput(order_time, @"            Введите время заказа(формат: dd/MM/yyyy HH/mm/ss): ");

            string choice_var = "";
            Console.WriteLine(@"
            1. Показать все заказы до этого времени
            2. Показать все заказы после этого времени
            0. Выйти");
            choice_var = UtilityClass.ValidateInputForDeleteIngredient(2, choice_var, @"            Введите, как будете сравнивать: ");

            switch (choice_var)
            {
                case "1":
                    compared_orders = [.._orders
                    .Where(p => p.TimeOfOrder < time_var)];
                    WriteOrders(compared_orders);
                    break;
                case "2":
                    compared_orders = [.._orders
                    .Where(p => p.TimeOfOrder >= time_var)];
                    WriteOrders(compared_orders);
                    break;
                case "0":
                    break;
            }
        }

        private void MakeIngridient()
        {
            Ingredient ingredient_var = new();

            string message = @"
            ===================================================================================================================================
            Введите название ингредиента: ";
            string ingredient_name = "";
            ingredient_name = UtilityClass.ValidateInput(ingredient_name, message);
            ingredient_var.Name = ingredient_name;

            message = @"
            ===================================================================================================================================
            Введите цену: ";
            string ingredient_price = "";
            ingredient_price = UtilityClass.ValidateInput(ingredient_price, message);
            int price_var = int.Parse(ingredient_price);
            /*
            while (!int.TryParse(ingredient_price, out price_var))
            {
                ingredient_price = "";
                ingredient_price = UtilityClass.ValidateInput(ingredient_price, message);
            }*/
            ingredient_var.Price = price_var;
            _ingredients.Add(ingredient_var);
            UtilityClass.RemoveDuplicatesFromIngredientList(ref _ingredients);
        }
        private void DeleteIngridient()
        {
            WriteIngridients(_ingredients);
            Console.Write(@"
            ===================================================================================================================================
            0: Выйти
            ");
            string message = @"
            ===================================================================================================================================
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
            WriteIngridients(_ingredients);
            Console.Write(@"
            ===================================================================================================================================
            0: Выйти
            ");
            string message = @"
            ===================================================================================================================================
            Выберите номер ингредиента, который вы хотите изменить:";
            string choice_str = "";

            choice_str = UtilityClass.ValidateInputForDeleteIngredient(_ingredients.Count, choice_str, message);
            int choice = int.Parse(choice_str);

            if (choice != 0)
            {
                Ingredient ingredient_for_change = _ingredients[choice-1];
                message = @"
            ===================================================================================================================================
            Введите новое название: ";
                choice_str = "";
                choice_str = UtilityClass.ValidateInput(choice_str, message);
                ingredient_for_change.Name = choice_str;

                message = @"
            ===================================================================================================================================
            Введите новую цену: ";
                choice_str = "";
                choice_str = UtilityClass.ValidateInput(choice_str, message);
                ingredient_for_change.Price = int.Parse(choice_str);
            }
        }
        private void WriteIngridients(List<Ingredient> ingredients)
        {
            Console.Write(@"
            ===================================================================================================================================
            Ингредиенты:" + "\n");
            int i = 1;
            foreach(Ingredient ingredient in ingredients)
            {
                Console.WriteLine(@"            " + i + ": " + ingredient.Display());
                i++;
            }
        }

        private void MakePizzaBase()
        {
            PizzaBase pizza_base_var = new();

            string message = @"
            ===================================================================================================================================
            Введите название основы: ";
            string base_name = "";
            base_name = UtilityClass.ValidateInput(base_name, message);
            pizza_base_var.Name = base_name;

            message = @"
            ===================================================================================================================================
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
            WritePizzaBases(_pizza_bases);
            Console.Write(@"
            ===================================================================================================================================
            0: Выйти
            ");
            string message = @"
            ===================================================================================================================================
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
            WritePizzaBases(_pizza_bases);
            Console.Write(@"
            ===================================================================================================================================
            0: Выйти
            ");
            string message = @"
            ===================================================================================================================================
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
            ===================================================================================================================================
            Введите новое название: ";
                    name_holder = UtilityClass.ValidateInput(name_holder, message);
                }

                message = @"
            ===================================================================================================================================
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

        private void WritePizzaBases(List<PizzaBase> pizza_bases)
        {
            Console.Write(@"
            ===================================================================================================================================
            Основы:" + "\n");
            int i = 1;
            foreach(PizzaBase pizza_base in pizza_bases)
            {
                Console.WriteLine(@"            " + i + ": " + pizza_base.Display());
                i++;
            }
        }
        private void MakePizza()
        {
            Pizza new_pizza = new();

            string message = @"
            ===================================================================================================================================
            Введите название: ";
            string name_var = "";
            name_var = UtilityClass.ValidateInput(name_var, message);
            new_pizza.Name = name_var;

            WritePizzaBases(_pizza_bases);
            message = @"
            ===================================================================================================================================
            Выберите основу: ";
            string base_var = "";
            base_var = UtilityClass.ValidateInputForDeleteIngredient(_pizza_bases.Count, base_var, message);
            int base_int = int.Parse(base_var);
            new_pizza.Base = _pizza_bases[base_int-1];

            WriteIngridients(_ingredients);
            Console.WriteLine("            0: Все ингредиенты добавлены");
            message = @"
            ===================================================================================================================================
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
            WritePizzas(_pizzas);

            Console.Write(@"
            ===================================================================================================================================
            0: Выйти
            ");
            string message = @"
            ===================================================================================================================================
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
            WritePizzas(_pizzas);
            Console.Write(@"
            ===================================================================================================================================
            0: Выйти
            ");
            string message = @"
            ===================================================================================================================================
            Выберите номер пиццы, который вы хотите изменить:";
            string choice_str = "";

            choice_str = UtilityClass.ValidateInputForDeleteIngredient(_pizzas.Count, choice_str, message);
            int choice = int.Parse(choice_str);

            Pizza pizza_for_change = _pizzas[choice-1];

            while (choice != 0)
            {
                Console.Write(@"
            ===================================================================================================================================
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
                        WritePizzaBases(_pizza_bases);
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
                        WriteIngridients(_ingredients);
                        Console.WriteLine(@"
            0: Все ингредиенты добавлены");
                        message = @"
            ===================================================================================================================================
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
        private void WritePizzas(List<Pizza> pizzas)
        {
            int i = 1;
            foreach (Pizza pizza in pizzas)
            {
                Console.Write(@"            " + i + ": " + pizza.Display());
            }
        }

        private void MakeEdge()
        {
            Edge new_edge = new();
            string message = @"
            ===================================================================================================================================
            Введите название: ";
            string name_var = "";
            name_var = UtilityClass.ValidateInput(name_var, message);
            new_edge.Name = name_var;

            WriteIngridients(_ingredients);
            Console.WriteLine("            0: Все ингредиенты добавлены");
            message = @"
            ===================================================================================================================================
            Выберите ингредиенты: ";
            string ingredient_var = "";

            while (ingredient_var != "0")
            {
                ingredient_var = UtilityClass.ValidateInputForDeleteIngredient(_ingredients.Count, ingredient_var, message);
                if (ingredient_var != "0")
                {
                    int ingredient_index = int.Parse(ingredient_var) - 1;
                    Ingredient added_ingredient = _ingredients[ingredient_index];
                    new_edge.AddIngredient(added_ingredient);
                    ingredient_var = "";
                }
            }

            message = @"
            ===================================================================================================================================
            Выберите пиццы, в которые можно добавить этот бортик: ";
            WritePizzas(_pizzas);

            string pizza_var = "";
            while (pizza_var != "0")
            {
                pizza_var = UtilityClass.ValidateInputForDeleteIngredient(_pizzas.Count, pizza_var, message);
                if (pizza_var != "0")
                {
                    int pizza_index = int.Parse(pizza_var) - 1;
                    Pizza added_pizza = _pizzas[pizza_index];
                    new_edge.AddAllowedPizza(added_pizza);
                    pizza_var = "";
                }
            }
            new_edge.UpdatePrice();
            _edges.Add(new_edge);
        }
        private void DeleteEdge()
        {
            WriteEdges(_edges);
            Console.Write(@"
            ===================================================================================================================================
            0: Выйти
            ");
            string message = @"
            ===================================================================================================================================
            Выберите номер бортика, который вы хотите удалить: ";
            string choice_str = "";

            choice_str = UtilityClass.ValidateInputForDeleteIngredient(_edges.Count, choice_str, message);
            int choice = int.Parse(choice_str);

            if (choice != 0)
            {
                _edges.RemoveAt(choice-1);
            }
        }
        private void ChangeEdge()
        {
            WriteEdges(_edges);
            Console.Write(@"
            ===================================================================================================================================
            0: Выйти
            ");
            string message = @"
            ===================================================================================================================================
            Выберите номер бортика, который вы хотите изменить:";
            string choice_str = "";

            choice_str = UtilityClass.ValidateInputForDeleteIngredient(_edges.Count, choice_str, message);
            int choice = int.Parse(choice_str);
            if (choice <= 0)
            {
                return;
            }
            Edge edge_for_change = _edges[choice-1];

            while (choice != 0)
            {
                Console.Write(@"
            ===================================================================================================================================
            Что вы хотите изменить?");
                message = @"
            0. Выйти
            1. Название
            2. Удалить ингредиенты
            3. Добавить ингредиенты
            4. Удалить совместимые пиццы
            5. Добавить совместимые пиццы
            ";
                choice_str = "";
                choice_str = UtilityClass.ValidateInputForDeleteIngredient(5, choice_str, message);

                choice = int.Parse(choice_str);
                switch(choice){
                    case 1:
                        string new_name = "";
                        new_name = UtilityClass.ValidateInput(new_name, "            Введите новое название: ");
                        edge_for_change.Name = new_name;
                        break;
                    case 2:
                        Console.Write(edge_for_change.DisplayIngredients());
                        Console.Write("            0: Выйти");
                        string ingredient_choice = "";
                        ingredient_choice = UtilityClass.ValidateInputForDeleteIngredient(edge_for_change.CountIngredients(), ingredient_choice, @"            Введите номер ингредиента, который вы хотите удалить: ");

                        if (ingredient_choice != "0")
                        {
                            edge_for_change.DeleteIngredient(int.Parse(ingredient_choice) - 1);
                        }
                        break;
                    case 3:
                        WriteIngridients(_ingredients);
                        Console.WriteLine(@"
            0: Все ингредиенты добавлены");
                        message = @"
            ===================================================================================================================================
            Выберите ингредиенты: ";
                        string ingredient_var = "";

                        while (ingredient_var != "0")
                        {
                            ingredient_var = UtilityClass.ValidateInputForDeleteIngredient(_ingredients.Count, ingredient_var, message);
                            if (ingredient_var != "0")
                            {
                                int ingredient_index = int.Parse(ingredient_var) - 1;
                                Ingredient added_ingredient = _ingredients[ingredient_index];
                                edge_for_change.AddIngredient(added_ingredient);
                                ingredient_var = "";
                            }
                        }
                        break;
                    case 4:
                        Console.Write(edge_for_change.DisplayAllowedPizzas());
                        Console.WriteLine(@"
            0: Все совместимые пиццы удалены");
                        message = @"
            ===================================================================================================================================
            Делайте выбор.";
                        choice_str = "";
                        while (choice_str != "0")
                        {
                            choice_str = UtilityClass.ValidateInputForDeleteIngredient(edge_for_change.CountAllowedPizzas(), choice_str, message);
                            if (choice_str != "0")
                            {
                                choice = int.Parse(choice_str);
                                edge_for_change.DeleteAllowedPizza(choice - 1);
                                choice_str = "";
                            }
                        }
                        break;
                    case 5:
                        WritePizzas(_pizzas);
                        Console.WriteLine(@"            0.Выйти");
                        message = @"
            ===================================================================================================================================
            Делайте выбор.";
                        choice_str = "";
                        while (choice_str != "0")
                            {
                                choice_str = UtilityClass.ValidateInputForDeleteIngredient(_pizzas.Count, choice_str, message);
                                if (choice_str != "0")
                                {
                                    choice = int.Parse(choice_str);
                                    edge_for_change.AddAllowedPizza(_pizzas[choice - 1]);
                                    choice_str = "";
                                }
                            }
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }
                edge_for_change.UpdatePrice();
            }
        }
        private void WriteEdges(List<Edge> edges)
        {
            Console.Write(@"
            ===================================================================================================================================
            Бортики:" + "\n");
            int i = 1;
            foreach(Edge edge in edges)
            {
                Console.WriteLine(@"            " + i + ": " + edge.Display());
                i++;
            }
        }
        private void MakeOrder()
        {
            int num_of_double_pizzas = 0;
            /* Заказ идёт так:
            1. Выбор типа заказа. Либо из имеющихся пицц, либо из составной самому, либо из комбинированной пиццы.
            2. Если из имеющихся пицц, то выбирать пока не нажмёт 0.
            2.1 Если из составной пиццы, то вызвать по типу создания своей пиццы.
            2.2 Если из комбинированной, то просто вызвать опять сообщение только теперь без 3 пункта. Но до этого нужно выбрать основу.
            В любом случае при выборе пиццы нужно указать размер и можно будет либо добавить ингридиент, либо изменить бортик, либо удвоить ингридиенты. 
            */
            string message = @"
            ===================================================================================================================================
            Что вы ходите заказать?
            1. Заказать из имеющихся пицц
            2. Составить свою пиццу
            3. Комбинированная пицца
            0. Выйти
            ";
            string choice_str = "";
            string choice = "";
            Order order = new();
            int size = 0;

            while (choice_str != "0")
            {
                message = @"
            ===================================================================================================================================
            Что вы ходите заказать?
            1. Заказать из имеющихся пицц
            2. Составить свою пиццу
            3. Комбинированная пицца
            0. Выйти
            ";
                size = 0;
                choice_str = "";
                choice_str = UtilityClass.ValidateInputForDeleteIngredient(3, choice_str, message);
                switch (choice_str)
                {
                    case "1":
                        WritePizzas(_pizzas);
                        Console.WriteLine("            0: Выход");
                        string choice_message = @"            Выберите пиццу: ";
                        choice = "";
                        while (choice != "0")
                            {
                                choice = UtilityClass.ValidateInputForDeleteIngredient(_pizzas.Count, choice, choice_message);
                                if (choice != "0")
                                {
                                    Pizza the_chosen_one = _pizzas[int.Parse(choice) - 1];
                                    Console.Write(the_chosen_one.DisplayIngredients());
                                    Console.WriteLine("            0: Выйти");
                                    string double_ingredients_choice_message = @"
            ===================================================================================================================================
            Выберите ингридиенты, количество которых вы хотите удвоить.
            ";
                                    string double_ingredients_choice = "";
                                    while (double_ingredients_choice != "0")
                                    {
                                        double_ingredients_choice = UtilityClass.ValidateInputForDeleteIngredient(the_chosen_one.CountIngredients(), double_ingredients_choice, double_ingredients_choice_message);
                                        if (double_ingredients_choice != "0"){
                                            the_chosen_one.AddIngredient(the_chosen_one.ReturnIngredient(int.Parse(double_ingredients_choice)-1), false);
                                            double_ingredients_choice = "";
                                        }
                                    }
                                    the_chosen_one.UpdatePrice();
                                    
                                    size = UtilityClass.ValidatePizzaSize(size, @"           Введите размер пиццы: ");
                                    the_chosen_one.Size = size;
                                    order.AddPizza(the_chosen_one);
                                    choice = "";
                                }
                                else
                                {
                                    break;
                                }
                            }
                        choice_str = "";
                        break;
                    case "2":
                        Pizza new_pizza = new();
                        string combined_message = @"
            ===================================================================================================================================
            Введите название: ";
                        string name_var = "";
                        name_var = UtilityClass.ValidateInput(name_var, combined_message);
                        new_pizza.Name = name_var;

                        WritePizzaBases(_pizza_bases);
                        combined_message = @"
            ===================================================================================================================================
            Выберите основу: ";
                        string base_var = "";
                        base_var = UtilityClass.ValidateInputForDeleteIngredient(_pizza_bases.Count, base_var, combined_message);
                        int base_int = int.Parse(base_var);
                        new_pizza.Base = _pizza_bases[base_int-1];

                        WriteIngridients(_ingredients);
                        Console.WriteLine("            0: Все ингредиенты добавлены");
                        combined_message = @"
            ===================================================================================================================================
            Выберите ингредиенты: ";
                        string ingredient_var = "";

                        while (ingredient_var != "0")
                        {
                            ingredient_var = UtilityClass.ValidateInputForDeleteIngredient(_ingredients.Count, ingredient_var, combined_message);
                            if (ingredient_var != "0")
                            {
                                int ingredient_index = int.Parse(ingredient_var) - 1;
                                Ingredient added_ingredient = _ingredients[ingredient_index];
                                new_pizza.AddIngredient(added_ingredient, false);
                                ingredient_var = "";
                            }
                        }
                        new_pizza.UpdatePrice();
                        size = UtilityClass.ValidatePizzaSize(size, @"           Введите размер пиццы: ");
                        new_pizza.Size = size;
                        order.AddPizza(new_pizza);
                        choice_str = "";
                        break;
                    case "3":
                        bool quit = false;
                        string half_message;
                        List<Pizza> double_pizza = [];
                        for (int i = 0; i < 2; i++){
                            if (i == 0)
                            {
                                half_message = @"
            ===================================================================================================================================
            1. Выбрать первую половину пиццы из списка
            2. Создать первую половину пиццы
            0. Выйти
            ";
                            }
                            else
                            {
                                half_message = @"
            ===================================================================================================================================
            1. Выбрать вторую половину пиццы из списка
            2. Создать вторую половину пиццы
            0. Выйти
            ";
                            }
                            string combined_pizza_choice = "";
                            combined_pizza_choice = UtilityClass.ValidateInputForDeleteIngredient(2, combined_pizza_choice, half_message);
                            switch (combined_pizza_choice)
                            {
                                case "1":
                                    WritePizzas(_pizzas);
                                    Console.WriteLine("            0: Выход");
                                    choice = UtilityClass.ValidateInputForDeleteIngredient(_pizzas.Count, choice, @"            Выберите пиццу: ");
                                    if (choice != "0")
                                    {
                                        double_pizza.Add(_pizzas[int.Parse(choice) - 1]);
                                    }
                                    combined_pizza_choice = "";
                                    break;
                                case "2":
                                    Pizza new_pizza_half = new();
                                    half_message = @"
            ===================================================================================================================================
            Введите название: ";
                                    string half_name = "";
                                    name_var = UtilityClass.ValidateInput(half_name, half_message);
                                    new_pizza_half.Name = name_var;

                                    new_pizza_half.Base = _pizza_bases[0]; //Placeholder

                                    WriteIngridients(_ingredients);
                                    Console.WriteLine("            0: Все ингредиенты добавлены");
                                    half_message = @"
            ===================================================================================================================================
            Выберите ингредиенты: ";
                                    string ingredients = "";

                                    while (ingredients != "0")
                                    {
                                        ingredients = UtilityClass.ValidateInputForDeleteIngredient(_ingredients.Count, ingredients, half_message);
                                        if (ingredients != "0")
                                        {
                                            int ingredient_index = int.Parse(ingredients) - 1;
                                            Ingredient added_ingredient = _ingredients[ingredient_index];
                                            new_pizza_half.AddIngredient(added_ingredient, false);
                                            ingredients = "";
                                        }
                                    }
                                    new_pizza_half.UpdatePrice();
                                    double_pizza.Add(new_pizza_half);
                                    break;

                                //case "3":
                                //    order.ClearPizzaHalves(num_of_double_pizzas);
                                //    num_of_double_pizzas--;
                                //    break;
                                case "0":
                                    i = 3;
                                    break;
                            }
                        }
                        if (quit)
                        {
                            break;
                        }
                        string base_choice = "";
                        WritePizzaBases(_pizza_bases);
                        Console.Write("            0: Выйти");
                        message = @"
            ===================================================================================================================================
            Выберите основу: ";
                        base_choice = UtilityClass.ValidateInputForDeleteIngredient(_pizza_bases.Count, base_choice, message);
                        
                        PizzaBase shared_base = _pizza_bases[int.Parse(base_choice)-1];
                        double_pizza[0].Base = shared_base;
                        double_pizza[1].Base = shared_base;

                        size = UtilityClass.ValidatePizzaSize(size, @"           Введите размер пиццы: ");
                        double_pizza[0].Size = size;
                        double_pizza[1].Size = size;
                        order.AddTwoHalves(double_pizza);
                        //order.AddPizzaHalf(double_pizza[0], num_of_double_pizzas);
                        //order.AddPizzaHalf(double_pizza[1], num_of_double_pizzas);
                        num_of_double_pizzas++;
                        break;
                }
            }
            order.UpdatePrice();
            string comment = "";
            Console.Write(@"            Добавьте комментарий: ");
            comment = Console.ReadLine();
            order.Comment = comment;

            Console.Write(@"            Сделать ли заказ отложенным? Да/Нет" + "\n" + @"            ");
            string do_delay = Console.ReadLine().ToLower();

            if (do_delay == "да")
            {
                DateTime time_var;
                message = @"            Введите время заказа(формат: dd/MM/yyyy HH/mm/ss): ";
                string order_time = "";
                time_var = UtilityClass.ValidateDateTimeInput(order_time, message);
                order.UpdateDeliveryTime(time_var);
            }
            else
            {
                order.UpdateDeliveryTime(order.TimeOfOrder);
            }
            _amount_of_orders += 1;
            order.FinishOrder(_amount_of_orders);
            _orders.Add(order);
        }
        private void WriteOrders(List<Order> orders)
        {
            foreach(Order order in orders)
            {
                Console.Write(order.Display());
            }
        }
    }
}