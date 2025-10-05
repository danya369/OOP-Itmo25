using System;
using VendingMachineApp;

class Program
{
    static void Main(string[] args)
    {
        var vendingMachine = new VendingMachine();

        while (true)
        {
            Console.WriteLine("\n========== Вендинговый автомат ==========");
            Console.WriteLine("1. Показать товары");
            Console.WriteLine("2. Вставить монету");
            Console.WriteLine("3. Купить товар");
            Console.WriteLine("4. Отменить и вернуть монеты");
            Console.WriteLine("5. Войти как администратор");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");

            string? input = Console.ReadLine();
            if (input == null) continue;

            switch (input)
            {
                case "1":
                    vendingMachine.ShowProducts();
                    break;

                case "2":
                    Console.Write("Введите номинал монеты (1, 2, 5, 10): ");
                    string? coinInput = Console.ReadLine();
                    if (coinInput != null && int.TryParse(coinInput, out int coin))
                    {
                        vendingMachine.InsertCoin(coin);
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод.");
                    }
                    break;

                case "3":
                    Console.Write("Введите ID товара: ");
                    string? productInput = Console.ReadLine();
                    if (productInput != null && int.TryParse(productInput, out int productId))
                    {
                        vendingMachine.TryBuyProduct(productId);
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод.");
                    }
                    break;

                case "4":
                    vendingMachine.ReturnCoins();
                    break;

                case "5":
                    Console.Write("Введите пароль: ");
                    string? password = Console.ReadLine();
                    if (password != null)
                    {
                        vendingMachine.EnterAdminMode(password);
                    }
                    else
                    {
                        Console.WriteLine("Пароль не может быть пустым.");
                    }
                    break;

                case "0":
                    Console.WriteLine("До свидания!");
                    return;

                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }
        }
    }
}