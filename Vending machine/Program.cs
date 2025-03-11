using System;
using System.IO;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowMenu();
            int choice = GetUserChoice();
            VendItem(choice);
        }

        static void ShowMenu()
        {
            Console.WriteLine("Welcome to the Vending Machine!");
            Console.WriteLine("Please select an item:");
            Console.WriteLine("1. Soda");
            Console.WriteLine("2. Candy");
            Console.WriteLine("3. Gum");
            Console.WriteLine("4. Chips");
            Console.WriteLine("5. Twizzlers");
            Console.WriteLine("6. Mountain Dew");
        }

        static int GetUserChoice()
        {
            Console.Write("Enter the number of your choice: ");
            string? input = Console.ReadLine();
            int choice;

            while (!int.TryParse(input, out choice) || choice < 1 || choice > 6)
            {
                Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                input = Console.ReadLine();
            }
            return choice;
        }

        static void VendItem(int choice)
        {
            string itemName = "";

            switch (choice)
            {
                case 1:
                    itemName = "Soda";
                    break;
                case 2:
                    itemName = "Candy";
                    break;
                case 3:
                    itemName = "Gum";
                    break;
                case 4:
                    itemName = "Chips";
                    break;
                case 5:
                    itemName = "Twizzlers";
                    break;
                case 6:
                    itemName = "Mountain Dew";
                    break;
                default:
                    Console.WriteLine("Invalid selection.");
                    return;
            }

            Console.WriteLine($"Vending {itemName}...");
            LogVendingActivity(itemName);
        }

        static void LogVendingActivity(string item)
        {
            string logEntry = $"{DateTime.Now}: {item} was vended.";
            File.AppendAllText("log.txt", logEntry + Environment.NewLine);
        }
    }
}


