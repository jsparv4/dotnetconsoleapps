using System;

class Program
{
    static void Main()
    {
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        bool firstIteration = true;

        while (true)
        {
            // Display the available options on the first iteration.
            if (firstIteration)
            {
                Console.WriteLine("Available operations:");
                Console.WriteLine("\t+ : Add");
                Console.WriteLine("\t- : Subtract");
                Console.WriteLine("\t* : Multiply");
                Console.WriteLine("\t/ : Divide");
                Console.WriteLine("\t^ : Raise to Power");
                Console.WriteLine("\t% : Modulus");
                Console.WriteLine("\t! : Factorial (unary, e.g., up to 20!)");
                firstIteration = false;
            }

            Console.WriteLine("Enter your calculation (e.g., 2+3 or 5!), without spaces (or 'q' to quit): ");

            // Read user input.
            string? input = Console.ReadLine();

            // Check for null input.
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Error: Input cannot be null or empty.");
                continue;
            }

            // Check for quit condition.
            if (input.ToLower() == "q")
            {
                break;
            }

            // Check for spaces in the input.
            if (input.Contains(" "))
            {
                Console.WriteLine("Error: Please do not enter spaces in your calculation.");
                continue;
            }

            // Perform the calculation.
            double result = PerformCalculation(input);

            // Validate the result.
            if (double.IsInfinity(result))
            {
                Console.WriteLine("Result is infinity or negative infinity. Please enter a valid calculation.");
            }
            else
            {
                Console.WriteLine($"Your result: {result}");
            }

            Console.WriteLine();
        }
    }

    static double PerformCalculation(string input)
    {
        try
        {
            // Convert all integers to doubles in the input
            input = ConvertIntegersToDoubles(input);

            // Handle factorial separately
            if (input.Contains("!"))
            {
                int num = int.Parse(input.Replace("!", "").Trim());
                if (num < 0)
                {
                    Console.WriteLine("Factorial is only defined for non-negative integers.");
                    return double.NaN;
                }
                if (num > 20)
                {
                    Console.WriteLine("Factorial is too large to calculate.");
                    return double.NaN;
                }
                return Factorial(num);
            }

            // Handle exponentiation separately
            while (input.Contains("^"))
            {
                input = HandleExponentiation(input);
            }

            var dataTable = new System.Data.DataTable();
            return Convert.ToDouble(dataTable.Compute(input, ""));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return double.NaN;
        }
    }

    static string ConvertIntegersToDoubles(string input)
    {
        string result = "";
        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsDigit(input[i]) && (i == 0 || !char.IsDigit(input[i - 1]) && input[i - 1] != '.'))
            {
                result += input[i] + ".0";
            }
            else
            {
                result += input[i];
            }
        }
        return result;
    }

    static string HandleExponentiation(string input)
    {
        int index = input.IndexOf("^");
        int leftIndex = index - 1;
        int rightIndex = index + 1;

        while (leftIndex >= 0 && (char.IsDigit(input[leftIndex]) || input[leftIndex] == '.'))
            leftIndex--;
        while (rightIndex < input.Length && (char.IsDigit(input[rightIndex]) || input[rightIndex] == '.'))
            rightIndex++;

        double baseNum = double.Parse(input.Substring(leftIndex + 1, index - leftIndex - 1));
        double exponent = double.Parse(input.Substring(index + 1, rightIndex - index - 1));
        double expResult = Math.Pow(baseNum, exponent);

        return input.Substring(0, leftIndex + 1) + expResult.ToString() + input.Substring(rightIndex);
    }

    static long Factorial(int n)
    {
        if (n == 0) return 1;
        long result = 1;
        for (int i = 1; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }
}