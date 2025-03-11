using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml;

namespace VendingMachine
{
    class Program
    {
        static string stockFile = "stock.json";
        static string logFile = "log.txt";
        static Dictionary<int, (string Name, decimal Price, int Stock)> items;

        static void Main(string[] args)
        {
            LoadStock();

            while (true)
            {
                ShowMenu();
                int choice = GetUserChoice();
                if (items[choice].Stock <= 0)
                {
                    Console.WriteLine("Item is out of stock. Please select another item.");
                    continue;
                }

                decimal payment = GetPayment(items[choice].Price);
                VendItem(choice, payment);

                Console.Write("\nWould you like to buy another item? (yes/no): ");
                if (!Console.ReadLine().Trim().ToLower().StartsWith("y"))
                {
                    Console.WriteLine("Thank you for using the vending machine!");
                    break;
                }
            }
        }

        static void LoadStock()
        {
            if (File.Exists(stockFile))
            {
                string json = File.ReadAllText(stockFile);
                items = JsonConvert.DeserializeObject<Dictionary<int, (string, decimal, int)>>(json);
            }
            else
            {
                items = new Dictionary<int, (string, decimal, int)>
                {
                    { 1, ("Soda", 1.50m, 10) },
                    { 2, ("Candy", 1.00m, 10) },
                    { 3, ("Gum", 0.75m, 10) },
                    { 4, ("Chips", 1.25m, 10) },
                    { 5, ("Twizzlers", 1.75m, 10) },
                    { 6, ("Mountain Dew", 2.00m, 10) }
                };
                SaveStock();
            }
        }

        static void SaveStock()
        {
            string json = JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(stockFile, json);
        }

        static void ShowMenu()
        {
            static void ShowMenu()
            {
                Console.Clear();
                Console.WriteLine("VENDING MACHINE MENU");
                Console.WriteLine("----------------------");
                Console.WriteLine($"Current Date and Time: {DateTime.Now}");
                Console.WriteLine("----------------------");
                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Key}. {item.Value.Name} - ${item.Value.Price} (Stock: {item.Value.Stock})");
                }
                Console.WriteLine();
            }

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Key}. {item.Value.Name} - ${item.Value.Price} (Stock: {item.Value.Stock})");
            }
            Console.WriteLine();
        }

        static int GetUserChoice()
        {
            int choice;
            while (true)
            {
                Console.Write("Enter item number: ");
                if (int.TryParse(Console.ReadLine(), out choice) && items.ContainsKey(choice))
                {
                    return choice;
                }
                Console.WriteLine("Invalid choice. Please select a valid item.");
            }
        }

        static decimal GetPayment(decimal itemPrice)
        {
            decimal totalInserted = 0;
            Console.WriteLine($"\nThe item costs ${itemPrice}");
            while (totalInserted < itemPrice)
            {
                Console.Write($"Insert money (remaining: ${itemPrice - totalInserted}): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal money) && money > 0)
                {
                    totalInserted += money;
                    Console.WriteLine($"Current balance: ${totalInserted}");
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please insert a valid amount.");
                }
            }
            return totalInserted;
        }

        static void VendItem(int choice, decimal payment)
        {
            string itemName = items[choice].Name;
            decimal itemPrice = items[choice].Price;
            decimal change = payment - itemPrice;

            items[choice] = (itemName, itemPrice, items[choice].Stock - 1);
            SaveStock();

            Console.WriteLine($"\nDispensing {itemName}. Enjoy!");
            if (change > 0)
            {
                Console.WriteLine($"Change returned: ${change}");
            }

            LogVendingActivity(itemName, itemPrice, payment, change);

            // Display the log file contents
            Console.WriteLine("\nLog Entries:");
            Console.WriteLine(File.ReadAllText(logFile));
        }

        static void LogVendingActivity(string item, decimal price, decimal payment, decimal change)
        {
            string logEntry = $"{DateTime.Now}: {item} vended for ${price}. Paid: ${payment}, Change: ${change}";
            File.AppendAllText(logFile, logEntry + Environment.NewLine);
        }
    }
}