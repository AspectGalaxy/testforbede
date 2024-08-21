
public class SlotManager
{
    private Apple apple;
    private Banana banana;
    private Pineapple pineapple;
    private Wildcard wildcard;
    private Random random;
    
    private List<Slot> saveSlotGeneration;

    private Deposit linkedBank;

    public bool spin = false;
    public SlotManager (Deposit deposit)
    {
        linkedBank = deposit;
        apple = new Apple();
        banana = new Banana();
        pineapple = new Pineapple();
        wildcard = new Wildcard();
        random = new Random();
        saveSlotGeneration = new List<Slot>();
        
    }

    public void spinTheSlots()
    {
        decimal reward = 0;
        
        int verticalCounter = @Const.verticalRows;

        while(verticalCounter >=1)
        {   
            int horrizontalCounter = @Const.horrizontalRows;
           
            //generate horrizontal rows
            while(horrizontalCounter >= 1)
            {
                //create random number to determine which slot we spawn
                int newRandomNumber = randomChance();
                Slot slot = selectSlot(newRandomNumber);
                saveSlotGeneration.Add(slot);
                slot.display();    
               
                horrizontalCounter --;
            }

            reward = reward + calculateReward();
            
            //empty saved slots ready for next row after reward calculated
            saveSlotGeneration.Clear();
           

             //input a new line after each cycle
            Console.WriteLine(" ");
            
            verticalCounter --;
        }

        //after completion update balance with reward amount
        linkedBank.addReward(reward);
        Console.WriteLine($"Your reward is: {reward}");

        //included for testing to ensure spinning is happening
        spin = true;
    }

    private decimal calculateReward()
    {
        decimal sumOfAllCoeficents = 0;
        int wildcardCount = 0;
        Slot nextSlot;

        for(int i = 0; i < saveSlotGeneration.Count  ; i++)
        {
            //make sure we are not on the last in index before peeking next
            if( i < saveSlotGeneration.Count - 1) {

                nextSlot = peekNextSlot(i);
                
                if(saveSlotGeneration[i] == nextSlot || saveSlotGeneration[i] == wildcard && nextSlot != wildcard|| nextSlot == wildcard)
                {
                   if(saveSlotGeneration[i] == wildcard) 
                    {
                        wildcardCount ++;
                        //check to ensure the slots prior to and infront of a wildcard are the same
                        if(i > 0){if(!checkLastAndNextSlots(i)){return 0;}}
                    }
                    //add all coeficents together.
                    sumOfAllCoeficents = sumOfAllCoeficents + saveSlotGeneration[i].getCoeffcient();

                }else
                {
                    //return 0 as no reward is present
                    return 0;
                    
                }
            }else
            {
                
                //last in sequence if we got this far it is a winning row so we just need to add coeeficent and workout reward
                Console.Write(" WIN!!");

                //ensure only one wildcard is in play
                if(saveSlotGeneration[i] == wildcard){wildcardCount ++;}
                if(wildcardCount >1){return 0;}

                //workout the reward by adding all coeficents together and multiplying that result by the stake
                sumOfAllCoeficents = sumOfAllCoeficents + saveSlotGeneration[i].getCoeffcient();
                decimal stake = linkedBank.getStake();
                decimal reward = sumOfAllCoeficents * stake;
                return reward;
            }
        }
        return 0;
    }
    private Slot peekLastSlot(int index)
    {
        return saveSlotGeneration[index -1];
    }
    private Slot peekNextSlot(int index)
    {
        return saveSlotGeneration[index+1];
    }
    private Slot selectSlot(int randomChance)
    {
       if(randomChance <= wildcard.probabilityToAppear)
       {return wildcard;}

       else if(randomChance <= pineapple.probabilityToAppear)
       {return pineapple;}

       else if(randomChance <= banana.probabilityToAppear)
       {return banana;}

       else{return apple;}
    }
   
    private bool checkLastAndNextSlots(int index)
    {
        if(peekLastSlot(index) == peekNextSlot(index))
        {return true;}
        else return false;
    }
    private int randomChance()
    {
       int generateRandom =  random.Next(1,100);
       return generateRandom;
    }
}