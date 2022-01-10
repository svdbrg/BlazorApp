using Google.Cloud.Firestore;

namespace BlazorApp.Features.Mortgager.Data;

[FirestoreData]
public class MortgageItemDto
{
    [FirestoreProperty]
    public int PurchasePrice { get; set; }
    [FirestoreProperty]
    public int DownPayment { get; set; }
    [FirestoreProperty]
    public int TotalSalary { get; set; }
    [FirestoreProperty]
    public double InterestRate { get; set; }
    [FirestoreProperty]
    public bool IsHouse { get; set; }
    [FirestoreProperty]
    public IEnumerable<ExpenseDto> Expenses { get; set; } = new List<ExpenseDto>();
}

[FirestoreData]
public class ExpenseDto
{
    [FirestoreProperty]
    public string? Name { get; set; }
    [FirestoreProperty]
    public int Cost { get; set; }
}