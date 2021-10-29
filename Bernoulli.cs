using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;


/// @author Antiikdev https://github.com/antiikdev/Bernoulli
/// @version 16 Oct 2021; 22 Oct 2021
/// <summary>
/// Petersburg Wager Payoff Profile
/// (Spitznagel, M., 2021. Safe Haven;
/// based on Bernoulli, D. 1738, 1954.
/// Exposition of a New Theory on the Measurement of Risk)
/// </summary>
// TODO: refactor
public class Bernoulli
{
    // Number of possible futures, or sides in the dice
    private static int diceSideNumber = 6;

    /// <summary>
    /// Main
    /// </summary>
    public static void Main()
    {
        bool menu = true;
        while (menu)
        {
            menu = MenuSelect();
        }
    }


    /// <summary>
    /// Menu for user to select function
    /// </summary>
    /// <returns>true to select function, false if exit the program</returns>
    private static bool MenuSelect()
    {
        Console.Clear();
        Console.WriteLine("Menu options:");
        Console.WriteLine("1) New geometric avg. dice game");
        Console.WriteLine("2) Print example");
        Console.WriteLine("3) Exit");
        Console.Write("Write option number and press Enter > ");

        switch (Console.ReadLine())
        {
            case "1":
                StartNewGame();
                return true;
            case "2":
                PrintExample();
                return true;
            case "3":
                return false;
            default:
                return true;
        }
    }


    /// <summary>
    /// User input
    /// </summary>
    /// <returns>User's input in string</returns>
    private static int UserIntegerInput()
    {
        Console.Write(" > ");
        string input = Console.ReadLine();
        int integer = 0;
        try
        {
            integer = int.Parse(input);
        }
        catch (FormatException)
        {
            Console.WriteLine($"Unable to parse '{input}'. Enter integer.");
        }
        return integer;
    }


    /// <summary>
    /// Collect user input selection
    /// </summary>
    /// <returns></returns>
    private static int[] UserPayoffInput()
    {
        // 1) a) ask number, b) convert from string to int -> table, c) ask next i
        int[] table = new int[diceSideNumber];
        int i = 0;

        while (i < diceSideNumber)
        {
            Console.Write("Enter payoff number " + (i+1) + ". > ");
            string input = Console.ReadLine();
            try
            {
                int payoff = int.Parse(input);
                table[i] = payoff;
                i++;
            }
            catch (FormatException)
            {
                Console.WriteLine($"Unable to parse '{input}'. Enter integer.");
            }
        }
        return table;
    }


    /// <summary>
    /// Starts a new dice game
    /// </summary>
    public static void StartNewGame()
    {
        // 1) Enter payoffs for the dice (default: six)
        int[] dicePayoff = UserPayoffInput();

        // 2) Print six payoffs
        Console.Write("Payoff for each six side dice roll: \n");
        PrintDicePayoff(dicePayoff);

        // 3) Enter your wealth
        Console.Write("Enter your wealth");
        int wealth = UserIntegerInput();

        // 4) Enter your wager (bet)
        Console.Write("Enter your wager (bet)");
        int wager = UserIntegerInput();
        Console.WriteLine();

        // 5) Print ending wealths for the six dice rolls
        Console.Write("Ending wealth (payoff) for each six side dice roll: \n");
        int[] endingWealth = new int[diceSideNumber];
        endingWealth = CalculateEndingWealth(dicePayoff, wealth, wager);
        PrintDicePayoff(endingWealth);

        // 6) Expected value of ending wealth
        //      - Print artihmetic avg.
        //      - Print geometric avg.
        Console.WriteLine();
        Console.WriteLine("Expected value of ending wealth");
        Console.WriteLine("-------------------------------");
        double avg = CalculateArithmeticAvg(endingWealth);
        Console.WriteLine("Arithmetic average is {0:#,###,###} $", avg);
        // Geometric average
        double geometricAvg = CalculateGeometricAvg(endingWealth);
        Console.WriteLine("Geometric average is {0:#,###,###} $ ", geometricAvg);
        Console.WriteLine();

        // TODO: 7) The limit for the "ideal" bet would be... calculate and print
        //          Inform the user if the bed was good or bad

        // 8) Select: new game or exit
        Console.WriteLine();
        Console.WriteLine("Press any key to get back to menu...");
        Console.ReadKey();
        return;
    }


    /// <summary>
    /// Prints example of geometric average
    /// </summary>
    public static void PrintExample()
    {
        Console.Clear();
        Console.WriteLine();

        // Six sided dice payoffs [1-6]
        int[] dicePayoff = { 1, 2, 6, 22, 200, 1000000 };
        Console.Write("Payoff for each six side dice roll: \n");
        PrintDicePayoff(dicePayoff);

        // Arithmetic average of the payoff
        double avg = CalculateArithmeticAvg(dicePayoff);
        Console.WriteLine("Arithmetic average {0:#,###,###} $.", avg);
        Console.WriteLine();

        // The Petersburg Wager Ending Wealth
        Console.WriteLine("** The Petersburg Wager Ending Wealth: ***");

        // Your wealth
        int wealth = 100000;
        Console.WriteLine("Your current wealth is {0:#,###,###} $", wealth);

        // Wager (bet size)
        int wager = 50000;
        Console.WriteLine("Your wager is {0:#,###,###} $", wager);

        //Ending wealth
        Console.Write("Ending wealth (payoff) for each six side dice roll: \n");
        int[] endingWealth = new int[diceSideNumber];
        endingWealth = CalculateEndingWealth(dicePayoff, wealth, wager);
        PrintDicePayoff(endingWealth);

        Console.WriteLine();
        Console.WriteLine("Expected value of ending wealth");
        Console.WriteLine("-------------------------------");

        avg = CalculateArithmeticAvg(endingWealth);
        Console.WriteLine("Arithmetic average is {0:#,###,###} $ " +
            "(same as previous avg + 100k - 50k).", avg);

        // Geometric average
        double geometricAvg = CalculateGeometricAvg(endingWealth);
        Console.WriteLine("Geometric average is {0:#,###,###} $ ", geometricAvg);
        Console.WriteLine();

        // Press any key to exit
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        return;
    }


    /// <summary>
    /// Print's payoff of dice roll from a table
    /// </summary>
    /// <param name="table">int[] of payoffs</param>
    public static void PrintDicePayoff(int[] table)
    {
        for (int i = 0; i < table.Length; i++)
        {
            Console.Write("[" + (i + 1) + "]: ");
            Console.Write("{0:#,###,###}",table[i]);
            Console.Write("$ ");
        }
        Console.WriteLine();
    }


    /// <summary>
    /// Calculates arithmetic average
    /// </summary>
    /// <param name="table">double numbers</param>
    /// <returns>arithmetic average</returns>
    public static double CalculateArithmeticAvg(int[] table)
    {
        double result = 0;
        for (int i = 0; i < table.Length; i++)
        {
            result += table[i];
        }
        return result / table.Length + 1;
    }


    /// <summary>
    /// Calculates six side dice payoffs
    /// based on wealth and wager
    /// </summary>
    /// <param name="table">Payoff from rolling the six sided dice</param>
    /// <param name="wealth">Your current wealth</param>
    /// <param name="wager">Your Wager or bet size</param>
    /// <returns>table of your ending wealth</returns>
    public static int[] CalculateEndingWealth(int[] table, int wealth, int wager)
    {
        int[] endWealth = new int[6];

        for (int i = 0; i < table.Length; i++)
        {
            endWealth[i] = (wealth - wager) + table[i];
        }
        return endWealth;
    }

    
    /// <summary>
    /// Calculates dice (six) geometric average
    /// </summary>
    /// <param name="table">six int numbers</param>
    /// <returns>Geometric average</returns>
    public static double CalculateGeometricAvg(int[] table)
    {
        double sum = table[0];

        // Multiply ending wealth values
        for (int i = 1; i < table.Length; i++)
        {
            double number = table[i];
            sum *= number;
        }

        // Take exponent (1/6) of the geometric avg results
        // TODO: if the diceSideNumber (sides in a dice) changes,
        //      then this need to be changed.
        double exponent = 0.16666666666666666666666666666667;
        double bev = Math.Pow(sum, exponent);

        return bev;
    }
 
}
