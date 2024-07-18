using System;
using System.Globalization;
using System.Text.RegularExpressions;

internal partial class Program
{
    private const string Pattern = @"^\d+(\.\d{1,2})?$"; // Ensure only positive numbers are allowed

    private static void Main(string[] args)
    {
        Console.WriteLine("\nWelcome to Prestige Bank ATM.");
        Console.WriteLine("Please select a desired action by entering the corresponding number.\n");

        Random random = new Random();
        double balance = random.NextDouble() * 50000;
        bool live = true;

        while (live)
        {
            DisplayMenu();
            Console.Write("Enter your choice (1-4): ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 4)
            {
                switch (choice)
                {
                    case 1:
                        DisplayBalance(balance);
                        break;
                    case 2:
                        balance = HandleTransaction("deposit", balance);
                        DisplayBalance(balance);
                        break;
                    case 3:
                        balance = HandleTransaction("withdraw", balance);
                        DisplayBalance(balance);
                        break;
                    case 4:
                        live = false;
                        Console.WriteLine("\nThank you for using the ATM. Goodbye!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input. Please enter a number between 1 and 4.\n");
            }
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine("Available Actions:");
        Console.WriteLine("1. Check Current Balance");
        Console.WriteLine("2. Make a Deposit");
        Console.WriteLine("3. Withdraw Money");
        Console.WriteLine("4. Exit\n");
    }

    private static double HandleTransaction(string action, double balance)
    {
        while (true)
        {
            Console.Write($"\nHow much would you like to {action}? Enter a valid amount or type 'exit' to cancel: ");
            string? input = Console.ReadLine();

            if (input?.ToLower() == "exit")
            {
                Console.WriteLine("\nTransaction cancelled.\n");
                break;
            }

            if (input != null && MyRegex().IsMatch(input) && double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double amount) && amount > 0)
            {
                if (action == "withdraw" && amount > balance)
                {
                    Console.WriteLine("\nInsufficient funds. Please enter a valid amount.\n");
                }
                else
                {
                    balance += action == "deposit" ? amount : -amount;
                    Console.WriteLine($"\nTransaction successful. You have {action}ed {amount.ToString("C", CultureInfo.CurrentCulture)}.\n");
                    break; // Exit the loop after a successful transaction
                }
            }
            else
            {
                Console.WriteLine("\nInvalid input. Please enter a positive number with up to two decimal places.\n");
            }
        }

        return balance;
    }

    private static void DisplayBalance(double balance)
    {
        Console.WriteLine($"\nCurrent balance: {balance.ToString("C", CultureInfo.CurrentCulture)}\n");
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex MyRegex();
}