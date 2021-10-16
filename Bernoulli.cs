using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

/// @author Antiikdev
/// @version 16 Oct 2021
/// <summary>
/// Petersburg Wager Payoff Profile
/// (Spitznagel, M., 2021. Safe Haven)
/// </summary>
public class Bernoulli
{
    /// <summary>
    /// Main
    /// </summary>
    public static void Main()
    {
        // Six sided dice payoffs [1-6]
        int[] dicePayoff = { 1, 2, 6, 22, 200, 1000000 };
        Console.Write("Payoff for each six side dice roll: \n");
        printDicePayoff(dicePayoff);

        // Arithmetic average of the payoff
        double avg = arithmeticAvg(dicePayoff);
        Console.WriteLine("Arithmetic average {0:#,###,###} $.", avg);
        Console.WriteLine();

        // The Petersburg Wager Ending Wealth
        Console.WriteLine("The Petersburg Wager Ending Wealth:");

        // Your wealth
        int wealth = 100000;
        Console.WriteLine("Your current wealth is {0:#,###,###} $", wealth);

        // Wager (bet size)
        int wager = 50000;
        Console.WriteLine("Your wager is {0:#,###,###} $", wager);

        //Ending wealth
        Console.Write("Ending wealth (payoff) for each six side dice roll: \n");
        int[] endingWealth = new int[6];
        endingWealth = calculateEndingWealth(dicePayoff, wealth, wager);
        printDicePayoff(endingWealth);

        avg = arithmeticAvg(endingWealth);
        Console.WriteLine("Arithmetic average (expected value of ending wealth) {0:#,###,###} $ " +
            "(same as previous avg + 100k - 50k).", avg);

        // Bernoulli's EMOLUMENTUM MEDIUM (EM) (Bernoulli D., 1738, 1954),
        // i.e. mean utility or advantage, benefit or profit.
        // Geometric average
        // TODO: double geometricAvg = calculateGeometricAvg(endingWealth);
        // Console.WriteLine("Geometric average (expected value of ending wealth) {0:#,###,###} $ ", geometricAvg);
    }


    /// <summary>
    /// Print's payoff of dice roll from a table
    /// </summary>
    /// <param name="table">int[] of payoffs</param>
    public static void printDicePayoff(int[] table)
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
    public static double arithmeticAvg(int[] table)
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
    public static int[] calculateEndingWealth(int[] table, int wealth, int wager)
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
    /// <returns>Double geometric average</returns>
    // TODO: needs some work: use bigdouble, log or what?
    /*
    public static double calculateGeometricAvg(int[] table)
    {
        BigInteger sum = new BigInteger(Convert.ToUInt64(table[0]));
        BigInteger geometricAvg = new BigInteger();

        for (int i = 1; i < table.Length; i++)
        {
            ulong number = Convert.ToUInt64(table[i]);
            sum += number;
        }

        geometricAvg = sum / 6;
        double avg = Convert.ToDouble(geometricAvg);

        sum = Math.exp
        Convert.ToDouble(geometricAvg);
        return geometricAvg;
    }
    */
}
