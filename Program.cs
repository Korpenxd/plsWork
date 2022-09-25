class Program
{
    static void Main(string[] args)
    {
        Betting game = new Betting();
        game.rules();
        Console.WriteLine("");
        
        
        while (game.playRound())
        {
            string answer = "";
            Console.WriteLine("");
            while (true)
            {
                Console.Write("Would you like to play again?(y or n) ");
                answer = Console.ReadLine();
                if (!answer.ToLower().Equals("y") && !answer.ToLower().Equals("n"))
                {
                    Console.WriteLine("Please enter a valid answer.");
                    Console.WriteLine("");
                    continue;
                }
                else
                    break;
            }
            if (answer.ToLower().Equals("n"))
                break;
        }
        if (game.playerAmount < 50)
        {
            Console.WriteLine("");
            Console.WriteLine("You lose homie");
        }
    }
}

class Betting
{
    private long cashAmount;
    public long playerAmount;
    private long betAmount;
    private long betNumber;


    public Betting()
    {
        cashAmount = 500;
        playerAmount = cashAmount;
        betAmount = 0;
        betNumber = 0;
    }
    public void rules()
    {
        Console.WriteLine("How the game works!");
        Console.WriteLine("");
        Console.WriteLine("1st: Place a bet! (Min 50 Pix)");
        Console.WriteLine("2nd: Select a number between 1-6.");
        Console.WriteLine("3rd: Roll the 3 dice and hope one of them lands on your number!");
        Console.WriteLine("");
        Console.WriteLine("Lastly, it will print the results, the new bank balance, and ask if you want to play another round!");
        Console.WriteLine("");
        Console.WriteLine("Balance: " + playerAmount + " Pix");
    }

    public bool playRound()
    {
        // ask how much the player wants to bet and betting number
        while (true)
        {
            Console.WriteLine("");
            Console.Write("Player bet amount: ");
            string answer = Console.ReadLine();
            if (Int64.TryParse(answer, out betAmount))
            {
                if (betAmount < 50 || betAmount > playerAmount)
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }
                else if (betAmount <= playerAmount)
                {
                    playerAmount = playerAmount - betAmount;
                    break;
                }
                    
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Very funny.");
                continue;
            }
        }
        while (true)
        {
            Console.WriteLine("");
            Console.WriteLine("Betting number: ");
            string betN = Console.ReadLine();
            if (Int64.TryParse(betN, out betNumber))
            {
                if (betNumber < 1 || betNumber > 6)
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }
            } else
            {
                Console.WriteLine("");
                Console.WriteLine("Not funnier the second time.");
                continue;
            }
            break;
        }
        int[] diceNumbers = rollDice();
        playerAmount = playerAmount + calcProfit(diceNumbers);
        Console.WriteLine("");
        Console.WriteLine("New Balance: " + playerAmount + " Pix");
        return playerAmount >= 50;
    }

    public long calcProfit(int[] diceNumbers)
    {
        int matches = 0;

        for (int i = 0; i < diceNumbers.Length; i++)
        {
            //Om en tärning visar lyckotalet får man dubbla insatsen
            //Om två tärningar visar får man tre gånger insatsen
            //Om alla tärningarna visar lyckonumret så får man fyra gånger insatsen.
            if (diceNumbers[i] == betNumber)
            {
                matches++;
            }
        }

        if (matches == 0)
            return 0;
       
        else return (matches + 1) * betAmount;
    }
    

    public int[] rollDice()

    {
        int[] diceNumbers = new int[3];
        Random rand = new Random();
        Console.WriteLine("Press any key to roll each dice.");

        for (int i = 0; i < 3; i++)
        {
            Console.ReadKey();

            diceNumbers[i] = rand.Next(1, 7);
            Console.WriteLine("You rolled a " + diceNumbers[i]);
        }
        Console.ReadKey();
        return diceNumbers;
    }
}