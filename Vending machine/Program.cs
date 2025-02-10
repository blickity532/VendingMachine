using System;

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
        }

        static int GetUserChoice()
        {
            Console.Write("Enter the number of your choice: ");
            string? input = Console.ReadLine();
            int choice;
            while (!int.TryParse(input, out choice) || choice < 1 || choice > 2)
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                input = Console.ReadLine();
            }
            return choice;
        }

        static void VendItem(int choice)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Vending Soda...");
                    break;
                case 2:
                    Console.WriteLine("Vending Candy...");
                    break;
            }
        }
    }
}