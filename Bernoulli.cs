using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;


/// @author Antiikdev
/// @version 16 Oct 2021
/// <summary>
/// Petersburg Wager Payoff Profile
/// (Spitznagel, M., 2021. Safe Haven;
/// Bernoulli, D. 1738, 1954. Exposition of a New Theory on the Measurement of Risk)
/// </summary>
public class Bernoulli
{


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
        Console.Write("Enter menu number > ");

        switch (Console.ReadLine())
        {
            case "1":
                NewGame();
                return true;
            case "2":
                Example();
                return true;
            case "3":
                return false;
            default:
                return true;
        }
    }


    /// <summary>
    /// Collect user input selection
    /// </summary>
    /// <returns></returns>
    private static string UserInput()
    {
        Console.Write("Enter your selection > ");
        return Console.ReadLine();
    }


    /// <summary>
    /// Dice game
    /// </summary>
    public static void NewGame()
    {
        Console.Clear();
        Console.WriteLine("Not yet working. Enter 3 to exit.");
        while (true)
        {
            string input = UserInput();
            if (input == "3") break;
        }
        MenuSelect();
    }


    /// <summary>
    /// Prints example of geometric average
    /// </summary>
    public static void Example()
    {
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
        int[] endingWealth = new int[6];
        endingWealth = CalculateEndingWealth(dicePayoff, wealth, wager);
        PrintDicePayoff(endingWealth);

        avg = CalculateArithmeticAvg(endingWealth);
        Console.WriteLine("Arithmetic average (expected value of ending wealth) {0:#,###,###} $ " +
            "(same as previous avg + 100k - 50k).", avg);

        // Geometric average
        double geometricAvg = CalculateGeometricAvg(endingWealth);
        Console.WriteLine("Geometric average (expected value of ending wealth) {0:#,###,###} $ ", geometricAvg);
        Console.WriteLine();
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
        double exponent = 0.16666666666666666666666666666667;
        double bev = Math.Pow(sum, exponent);

        return bev;
    }
 
}
