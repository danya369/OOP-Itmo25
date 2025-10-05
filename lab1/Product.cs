namespace VendingMachineApp
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public int Price { get; }
        public int Quantity { get; set; }

        public Product(int id, string name, int price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Id}. {Name} - {Price} руб. (Осталось: {Quantity})";
        }
    }
}