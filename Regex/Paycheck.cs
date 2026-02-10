using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Paycheck
{
    public int PayForDay { get; set; }
    public int AmountOfDays { get; set; }
    public int FinePerDayForPayDelay { get; set; }
    public int DaysDelayToPay { get; set; }
    public int SumToPayNoFine
    {
        get { return PayForDay * AmountOfDays; }
    }
    public int TotalFine
    {
        get {
            return FinePerDayForPayDelay * AmountOfDays;
        }
    }
    public int TotalSumToPay
    {
        get
        {
            return SumToPayNoFine + TotalFine;
        }
    }

}
