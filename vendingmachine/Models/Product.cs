namespace vendingmachine.Models
{
    public class Product
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }

        public Product(string name, int price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void DisplayInfo(int number)
        {
            string status = Stock > 0 ? "Available" : "SOLD OUT";
            Console.WriteLine($"{number}. {Name} - ₽ {Price:N0} (Stock: {Stock}) [{status}]");
        }

        public bool IsAvailable()
        {
            return Stock > 0;
        }

        public void DecreaseStock()
        {
            if (Stock > 0) Stock--;
        }

        public void AddStock(int amount)
        {
            Stock += amount;
        }
    }
}

