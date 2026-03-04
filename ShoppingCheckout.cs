using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// ShoppingCheckout class
public class ShoppingCheckout
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public ShoppingCheckout(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public decimal GetTotalPrice()
    {
        return Product.GetPrice() * Quantity;
    }

    public decimal GetTotalTax()
    {
        return Product.GetTax() * Quantity;
    }
}

// ShoppingCart class
public class ShoppingCart
{
    private List<ShoppingCheckout> items = new List<ShoppingCheckout>();

    public void AddProduct(Product product, int quantity)
    {
        ShoppingCheckout? existingItem = null;

        foreach (ShoppingCheckout item in items)
        {
            if (item.Product.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))
            {
                existingItem = item;
                break;
            }
        }

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            items.Add(new ShoppingCheckout(product, quantity));
        }
        Console.WriteLine($"{quantity} x {product.Name} added to the cart.");
    }

    public void RemoveProduct(string productName)
    {
        ShoppingCheckout? itemToRemove = null;
        foreach (var item in items)
        {
            if (item.Product.Name.Equals(productName, StringComparison.OrdinalIgnoreCase))
            {
                itemToRemove = item;
                break;
            }
        }

        if (itemToRemove != null)
        {
            items.Remove(itemToRemove);
            Console.WriteLine($"{itemToRemove.Product.Name} removed from the cart.");
        }
        else
        {
            Console.WriteLine($"Product '{productName}' not found in the cart.");
        }
    }

    public void DisplayCart()
    {
        if (!items.Any())
        {
            Console.WriteLine("\nYour cart is empty.");
            return;
        }

        Console.WriteLine("\nShopping Cart:");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Quantity} x {item.Product.Name}: ${item.GetTotalPrice():0.00}");
        }
    }

    public void DisplayInvoice()
    {
        Console.WriteLine("\n--- Invoice ---");
        Console.WriteLine("Product\tQty\tPrice\tTax\tTotal");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Product.Name}\t{item.Quantity}\t${item.Product.Price:0.00}\t${item.GetTotalTax():0.00}\t${item.GetTotalPrice():0.00}");
        }

        decimal totalTax = 0;
        decimal totalPrice = 0;

        foreach (var item in items)
        {
            totalTax += item.GetTotalTax();
            totalPrice += item.GetTotalPrice();
        }

        decimal subtotal = 0;

        foreach (var item in items)
        {
            subtotal += item.Product.Price * item.Quantity;
        }

        Console.WriteLine("\nSubtotal:\t\t\t\t${0:0.00}", subtotal);
        Console.WriteLine("Total Tax:\t\t\t\t${0:0.00}", totalTax);
        Console.WriteLine("Total Price:\t\t\t\t${0:0.00}", totalPrice);
    }

    public void Checkout()
    {
        if (!items.Any())
        {
            Console.WriteLine("\nYour cart is empty. Add some items before checking out.");
            return;
        }

        DisplayInvoice();
        Console.WriteLine("\nThank you for shopping with us!");
        items.Clear();
    }

}
