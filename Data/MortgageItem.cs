using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorApp.Data;

public class MortgageItem
{
    public int PurchasePrice { get; set; }
    public int DownPayment { get; set; }
    public int TotalSalary { get; set; }
    public double InterestRate { get; set; }
    public List<Expense> Expenses { get; set; } = new();


    [JsonIgnore]
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

    [JsonIgnore]
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

    [JsonIgnore]
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

    [JsonIgnore]
    public double TotalSalaryToCompareWith { get => (this.TotalSalary * 12) * 4.5; }

    [JsonIgnore]
    public int Loan { get => this.PurchasePrice - this.DownPayment; }

    [JsonIgnore]
    public double MonthlyLoanCost
    {
        get
        {
            return
            (Loan * (this.InterestRate / 100) / 12)
            + ((double)Loan * ((double)this.InstallmentRate / 100) / 12);
        }
    }

    [JsonIgnore]
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

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }
}

public class Expense
{
    public string Name { get; set; } = string.Empty;
    public int Cost { get; set; }
}