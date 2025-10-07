using vendingmachine.Models;

namespace vendingmachine.Services
{
    public class VendingMachine
    {
        private List<Product> productList;
        private int insertedMoney;
        private int totalRevenue;
        private string adminPassword;

        public VendingMachine()
        {
            productList = new List<Product>
            {
                new Product("Coca Cola", 120, 10),
                new Product("Chips", 80, 8),
                new Product("Ice Tea", 100, 12),
                new Product("Oreo", 70, 15),
                new Product("Instant Noodles", 90, 6)
            };

            insertedMoney = 0;
            totalRevenue = 0;
            adminPassword = "admin123";
        }

        public void Run()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("         WELCOME TO VENDING MACHINE");
            Console.WriteLine("=======================================");

            bool running = true;
            while (running)
            {
                ShowMainMenu();
                string? choice = Console.ReadLine();

                if (choice == "1") CustomerMode();
                else if (choice == "2") AdminMode();
                else if (choice == "3")
                {
                    Console.WriteLine("Thank you for using the vending machine!");
                    running = false;
                }
                else Console.WriteLine("Invalid choice! Try again.");
            }
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("\n=== MAIN MENU ===");
            Console.WriteLine("1. Customer Mode");
            Console.WriteLine("2. Admin Mode");
            Console.WriteLine("3. Exit");
            Console.Write("Select menu (1-3): ");
        }

        private void CustomerMode()
        {
            insertedMoney = 0;

            bool finished = false;
            while (!finished)
            {
                Console.WriteLine("\n=== CUSTOMER MODE ===");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Insert Money");
                Console.WriteLine("3. Buy Product");
                Console.WriteLine("4. View My Money");
                Console.WriteLine("5. Cancel & Get Refund");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Select menu (1-6): ");

                string? choice = Console.ReadLine();

                if (choice == "1") ShowProducts();
                else if (choice == "2") InsertMoney();
                else if (choice == "3") BuyProduct();
                else if (choice == "4") Console.WriteLine($"Money inserted: ₽ {insertedMoney:N0}");
                else if (choice == "5")
                {
                    CancelTransaction();
                    finished = true;
                }
                else if (choice == "6")
                {
                    if (insertedMoney > 0) Console.WriteLine($"Returning ₽ {insertedMoney:N0}");
                    finished = true;
                }
                else Console.WriteLine("Invalid choice!");
            }
        }

        private void ShowProducts()
        {
            Console.WriteLine("\n=== PRODUCT LIST ===");
            for (int i = 0; i < productList.Count; i++) productList[i].DisplayInfo(i + 1);
        }

        private void InsertMoney()
        {
            Console.WriteLine("\n=== INSERT MONEY ===");
            Console.WriteLine("Choose denomination:");
            Console.WriteLine("1. ₽ 10");
            Console.WriteLine("2. ₽ 20");
            Console.WriteLine("3. ₽ 50");
            Console.WriteLine("4. ₽ 100");
            Console.WriteLine("5. ₽ 200");
            Console.Write("Select (1-5): ");

            string? choice = Console.ReadLine();
            int nominal = 0;

            if (choice == "1") nominal = 10;
            else if (choice == "2") nominal = 20;
            else if (choice == "3") nominal = 50;
            else if (choice == "4") nominal = 100;
            else if (choice == "5") nominal = 200;
            else
            {
                Console.WriteLine("Invalid choice!");
                return;
            }

            Console.Write("How many coins/bills? ");
            if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
            {
                int totalInsert = nominal * amount;
                insertedMoney += totalInsert;
                Console.WriteLine($"Inserted ₽ {totalInsert:N0}");
                Console.WriteLine($"Total money now: ₽ {insertedMoney:N0}");
            }
            else Console.WriteLine("Invalid input!");
        }

        private void BuyProduct()
        {
            ShowProducts();
            Console.Write($"\nWhich product number? (1-{productList.Count}): ");

            if (int.TryParse(Console.ReadLine(), out int number) && number >= 1 && number <= productList.Count)
            {
                Product selectedProduct = productList[number - 1];

                if (!selectedProduct.IsAvailable())
                {
                    Console.WriteLine("Sorry, this product is sold out!");
                    return;
                }

                if (insertedMoney < selectedProduct.Price)
                {
                    int shortage = selectedProduct.Price - insertedMoney;
                    Console.WriteLine($"Not enough money! You need ₽ {shortage:N0} more");
                    return;
                }

                ProcessTransaction(selectedProduct);
            }
            else Console.WriteLine("Invalid product number!");
        }

        private void ProcessTransaction(Product product)
        {
            Console.WriteLine("\n*** PURCHASE SUCCESSFUL ***");
            Console.WriteLine($"Product: {product.Name}");
            Console.WriteLine($"Price: ₽ {product.Price:N0}");

            insertedMoney -= product.Price;
            product.DecreaseStock();
            totalRevenue += product.Price;

            if (insertedMoney > 0)
            {
                Console.WriteLine($"Your change: ₽ {insertedMoney:N0}");
                insertedMoney = 0;
            }

            Console.WriteLine("Thank you for shopping!");
        }

        private void CancelTransaction()
        {
            if (insertedMoney > 0)
            {
                Console.WriteLine($"Returning ₽ {insertedMoney:N0}");
                insertedMoney = 0;
            }
            else Console.WriteLine("No money to return.");
        }

        private void AdminMode()
        {
            Console.Write("Enter admin password: ");
            string? input = Console.ReadLine();

            if (input != adminPassword)
            {
                Console.WriteLine("Wrong password!");
                return;
            }

            bool finished = false;
            while (!finished)
            {
                Console.WriteLine("\n=== ADMIN MODE ===");
                Console.WriteLine("1. View All Stock");
                Console.WriteLine("2. Restock");
                Console.WriteLine("3. View Total Revenue");
                Console.WriteLine("4. Collect Revenue");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Select menu (1-5): ");

                string? choice = Console.ReadLine();

                if (choice == "1") ViewStock();
                else if (choice == "2") Restock();
                else if (choice == "3") Console.WriteLine($"Total Revenue: ₽ {totalRevenue:N0}");
                else if (choice == "4") CollectRevenue();
                else if (choice == "5") finished = true;
                else Console.WriteLine("Invalid choice!");
            }
        }

        private void ViewStock()
        {
            Console.WriteLine("\n=== ALL PRODUCT STOCK ===");
            for (int i = 0; i < productList.Count; i++)
                Console.WriteLine($"{i + 1}. {productList[i].Name} - Stock: {productList[i].Stock} units");
        }

        private void Restock()
        {
            ViewStock();
            Console.Write($"Which product to restock? (1-{productList.Count}): ");

            if (int.TryParse(Console.ReadLine(), out int number) && number >= 1 && number <= productList.Count)
            {
                Product product = productList[number - 1];
                Console.Write($"How many units to add for {product.Name}? ");

                if (int.TryParse(Console.ReadLine(), out int amount) && amount >= 0)
                {
                    product.AddStock(amount);
                    Console.WriteLine($"Added {amount} units.");
                    Console.WriteLine($"Stock of {product.Name}: {product.Stock} units");
                }
                else Console.WriteLine("Invalid input!");
            }
            else Console.WriteLine("Invalid product number!");
        }

        private void CollectRevenue()
        {
            Console.WriteLine($"Collecting revenue: ₽ {totalRevenue:N0}");
            totalRevenue = 0;
            Console.WriteLine("Revenue has been collected and reset to ₽ 0");
        }
    }
}

