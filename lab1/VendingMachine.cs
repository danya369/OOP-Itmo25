using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineApp
{
    public class VendingMachine
    {
        private List<Product> _products;
        private Dictionary<int, int> _insertedCoins;
        private int _balance;
        private int _collectedMoney;

        private readonly int[] _validCoins = { 1, 2, 5, 10 };

        public VendingMachine()
        {
            _products = new List<Product>
            {
                new Product(1, "Вензель с малиной", 109, 5),
                new Product(2, "Булочка с корицей", 79, 3),
                new Product(3, "Вода", 69, 10),
                new Product(4, "Баунти", 89, 2)
            };

            _insertedCoins = new Dictionary<int, int>();
            _balance = 0;
            _collectedMoney = 0;
        }

        public void ShowProducts()
        {
            Console.WriteLine("\nДоступные товары:");
            foreach (var product in _products)
            {
                Console.WriteLine(product);
            }
        }

        public void InsertCoin(int coin)
        {
            if (!_validCoins.Contains(coin))
            {
                Console.WriteLine("Неверный номинал монеты!");
                return;
            }

            if (!_insertedCoins.ContainsKey(coin))
                _insertedCoins[coin] = 0;

            _insertedCoins[coin]++;
            _balance += coin;

            Console.WriteLine($"Монета {coin} руб. принята. Баланс: {_balance} руб.");
        }

        public void ReturnCoins()
        {
            Console.WriteLine("\nВозврат монет:");
            foreach (var kv in _insertedCoins)
            {
                Console.WriteLine($"Монет {kv.Key} руб.: {kv.Value} шт.");
            }

            _insertedCoins.Clear();
            _balance = 0;
        }

        public void TryBuyProduct(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                Console.WriteLine("Товар не найден!");
                return;
            }

            if (product.Quantity == 0)
            {
                Console.WriteLine("Товар закончился.");
                return;
            }

            if (_balance < product.Price)
            {
                Console.WriteLine($"Недостаточно средств. Не хватает {product.Price - _balance} руб.");
                return;
            }

            product.Quantity--;
            _balance -= product.Price;
            _collectedMoney += product.Price;

            Console.WriteLine($"\nВы получили: {product.Name}.");
            if (_balance > 0)
            {
                Console.WriteLine($"Ваша сдача: {_balance} руб.");
                _balance = 0;
                _insertedCoins.Clear(); // сдача возвращается одной суммой
            }
        }

        public void EnterAdminMode(string password)
        {
            if (password != "admin123")
            {
                Console.WriteLine("Неверный пароль!");
                return;
            }

            Console.WriteLine("\n[Админ режим]");
            Console.WriteLine("1. Пополнить товары");
            Console.WriteLine("2. Забрать деньги");
            Console.Write("Выбор: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RestockProducts();
                    break;
                case "2":
                    Console.WriteLine($"Собрано {_collectedMoney} руб.");
                    _collectedMoney = 0;
                    break;
                default:
                    Console.WriteLine("Неизвестная команда.");
                    break;
            }
        }

        private void RestockProducts()
        {
            Console.WriteLine("\nВведите ID товара для пополнения:");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ввод.");
                return;
            }

            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                Console.WriteLine("Такого товара нет.");
                return;
            }

            Console.WriteLine("Введите количество для добавления:");
            if (!int.TryParse(Console.ReadLine(), out int qty))
            {
                Console.WriteLine("Неверный ввод.");
                return;
            }

            product.Quantity += qty;
            Console.WriteLine($"Товар {product.Name} пополнен. Теперь: {product.Quantity} шт.");
        }
    }
}