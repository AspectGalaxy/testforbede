 public class Deposit
{
    private decimal currentBalance;
    private decimal stakeAmount;
    public  Deposit(decimal amount)
    {
        this.currentBalance = amount;
    }
    public decimal getBalance()
    {
        return currentBalance;
    }
    public decimal getStake()
    {
        return stakeAmount;
    }
    public void stakeFurtherFunds(decimal amount)
    {
            currentBalance = currentBalance - amount;
            stakeAmount = amount;
    }
    public void addReward(decimal amount)
    {
        currentBalance = currentBalance + amount;
    }
}