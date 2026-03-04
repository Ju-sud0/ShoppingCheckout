public abstract class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public abstract decimal GetPrice();
    public abstract decimal GetTax();

}

public class TaxableProduct : Product {

    private const decimal taxRate = 0.08m;
    public TaxableProduct(string name, decimal price)
         : base(name, price) {}

    public override decimal GetTax()
    {
        return Price * taxRate;
    }

    public override decimal GetPrice()
    {
        return Price + GetTax();
    }
}

public class NonTaxableProduct : Product {

    public NonTaxableProduct(string name, decimal price) : base(name, price) {}

    public override decimal GetTax()
    {
        return 0;
    }
    public override decimal GetPrice()
    {
        return Price;
    }
}

public class DiscountedTaxableProduct : TaxableProduct {

    private decimal discountedPercentage;
    public DiscountedTaxableProduct(string name, decimal price, decimal discountedPercentage) 
            : base(name, price) 
    {
        this.discountedPercentage = discountedPercentage;
    }
    
    public void SetDiscount(decimal discountedPercentage)
    {
        this.discountedPercentage = discountedPercentage;
    }

    public override decimal GetTax()
    {
        decimal discountedPrice = Price * (1 - discountedPercentage);
        return discountedPrice * 0.06m;
    }

    public override decimal GetPrice()
    {
        decimal discountedPrice = Price * (1 - discountedPercentage);
        return discountedPrice + GetTax();
    }
}