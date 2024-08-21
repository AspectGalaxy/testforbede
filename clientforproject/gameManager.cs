
internal class gameManager
{
    Deposit linkedBank;
    SlotManager manager;
    bool isRunning;

    public gameManager()
    {
        //startup game first by taking inital player deposit
        
        Console.WriteLine("Please enter a deposit");
        decimal amount;
        Decimal.TryParse(Console.ReadLine(), out amount);
        linkedBank = new Deposit(amount);
        manager = new SlotManager(linkedBank);
        isRunning = true;
        runGame();
    }

    public void runGame()
    {
        while(isRunning)
        {
            bool completedCorrectly = stakeFurtherFunds();

            //restart loop to run stakeFurtherFunds() again if it has failed completion
            if(!completedCorrectly){continue;}
            
            //run game
            manager.spinTheSlots();

            //check to see if out of money
            checkToSeeIfGameIsOver();
        }
    }
    
    private bool stakeFurtherFunds()
    {
        Console.WriteLine($"Please enter a stake amount. Your current balance is {linkedBank.getBalance()}");
        string input = Console.ReadLine();
        decimal amount = convertToDecimal(input);

        //if amount is 0 or less return false to show there has been an error / and ensure the amount is not more than current balance allows
        if(amount > 0 && amount <= linkedBank.getBalance())
        {
            linkedBank.stakeFurtherFunds(amount); 
            return true;
        }
        else{return false;}        
    }
    private decimal convertToDecimal(string input)
    {
        decimal amount;
        if(! Decimal.TryParse(input, out amount))
        {
            Console.WriteLine("Error Staking Funds: Unrecognised stake input");
        }
        return amount;
    }
    private void checkToSeeIfGameIsOver()
    {
         if(linkedBank.getBalance() == 0)
          {
            isRunning = false;
            displayGoodbyeMessage();
          }
    }
    private void displayGoodbyeMessage()
    {
        Console.WriteLine("You have run out of funds. Goodbye!");
    }
}