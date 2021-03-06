using BlazorApp.Features.Shared;

namespace BlazorApp.Features.Mortgager.Data;

public class MortgageItem
{
    public string Name { get; set; } = string.Empty;
    public double PurchasePrice { get; set; }
    public double DownPayment { get; set; }
    public double TotalSalary { get; set; }
    public double InterestRate { get; set; }
    public double TitleDeed { get => (this.PurchasePrice * 0.015) + 825; }
    public double MortgageDeed { get => (this.Loan * 0.02) + 375; }
    public double PriceIncludingDeeds { get => (this.PurchasePrice + this.TitleDeed + this.MortgageDeed); }
    public List<Expense> Expenses { get; set; } = new();
    public bool IsHouse { get; set; }

    public string PurchasePriceString
    {
        get => this.PurchasePrice.ToString("N0");
        set
        {
            if (int.TryParse(value.RemoveWhitespace(), out var number))
            {
                this.PurchasePrice = number;
            }
        }
    }

    public string DownPaymentString
    {
        get => this.DownPayment.ToString("N0");
        set
        {
            if (int.TryParse(value.RemoveWhitespace(), out var number))
            {
                this.DownPayment = number;
            }
        }
    }

    public string TotalSalaryString
    {
        get => this.TotalSalary.ToString("N0");
        set
        {
            if (int.TryParse(value.RemoveWhitespace(), out var number))
            {
                this.TotalSalary = number;
            }
        }
    }

    public double TotalSalaryToCompareWith { get => (this.TotalSalary * 12) * 4.5; }

    public int Loan { get => (int)this.PurchasePrice - (int)this.DownPayment; }

    public double MonthlyLoanCost
    {
        get
        {
            return
            (Loan * (this.InterestRate / 100) / 12)
            + ((double)Loan * ((double)this.InstallmentRate / 100) / 12);
        }
    }

    public int DebtRatio
    {
        get
        {
            if (this.Loan > 0 && this.TotalSalary > 0)
            {
                return (int)Math.Round((decimal)this.Loan / (decimal)(this.TotalSalary * 12) * 100, 0);
            }
            return 0;
        }
    }

    public int InstallmentRate
    {
        get
        {
            if (Loan == 0 || this.PurchasePrice == 0)
            {
                return 0;
            }

            var loanPercentageOfTotalCost = (double)Loan / (double)this.PurchasePrice;
            var percentage = 0;

            if (Loan > this.DownPayment)
            {
                if (loanPercentageOfTotalCost > 0.5 && loanPercentageOfTotalCost <= 0.7)
                {
                    percentage++;
                }

                if (loanPercentageOfTotalCost > 0.7)
                {
                    percentage = percentage + 2;
                }
            }
            if (Loan > TotalSalaryToCompareWith)
            {
                percentage++;
            }

            return percentage;
        }
    }
}

public class Expense
{
    public string Name { get; set; } = string.Empty;
    public int Cost { get; set; }
}