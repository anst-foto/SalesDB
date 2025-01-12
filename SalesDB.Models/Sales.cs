namespace SalesDB.Models;

public class Sales : IEquatable<Sales>
{
    public long Id { get; init; }
    public DateTime Date { get; init; }
    public string ProductName { get; init; }
    public int Amount { get; init; }
    public decimal Price { get; init; }

    #region IEquatable<Sales>

    public bool Equals(Sales? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Date.Equals(other.Date) && ProductName == other.ProductName &&
               Amount == other.Amount && Price == other.Price;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Sales)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Date, ProductName, Amount, Price);
    }

    #endregion
}
