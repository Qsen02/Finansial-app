namespace Data.Models
{
    public enum CategoryType { Food, Technologies, Clothes, Travels, Education }

    public enum TypeEnum
    {
        Income,
        Expenses
    }
    public class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public CategoryType Category { get; set; }
        public TypeEnum Type { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateOnly Created_at { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }
}
