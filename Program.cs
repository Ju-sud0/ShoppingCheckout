using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

// Main program
class Program
{
    static void Main(string[] args)
    {
        // Available products
        var products = new List<Product>
        {
            new TaxableProduct("Camera", 600.00m),
            new NonTaxableProduct("Calculator", 25.00m),
            new DiscountedTaxableProduct("Xbox", 1000.00m, 0.2m)

        };

        ShoppingCart cart = new ShoppingCart();

        while (true)
        {
            Console.WriteLine("\n--- Shopping Cart Menu ---");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Add Product to Cart");
            Console.WriteLine("3. Remove Product from Cart");
            Console.WriteLine("4. View Cart");
            Console.WriteLine("5. Checkout");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\nAvailable Products:");
                    foreach (var product in products)
                    {
                        Console.WriteLine($"{product.Name}: ${product.Price:0.00} ({(product is TaxableProduct ? "Taxable" : "Non-Taxable")})");
                    }
                    break;

                case "2":
                    Console.Write("\nEnter the product name to add: ");
                    string productNameToAdd = Console.ReadLine();
                    var productToAdd = products.FirstOrDefault(p => p.Name.Equals(productNameToAdd, StringComparison.OrdinalIgnoreCase));

                    if (productToAdd != null)
                    {
                        Console.Write("Enter quantity: ");
                        if (int.TryParse(Console.ReadLine(), out int quantityToAdd) && quantityToAdd > 0)
                        {
                            cart.AddProduct(productToAdd, quantityToAdd);
                        }
                        else
                        {
                            Console.WriteLine("Invalid quantity. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Product not found.");
                    }
                    break;

                case "3":
                    Console.Write("\nEnter the product name to remove: ");
                    string productNameToRemove = Console.ReadLine();
                    cart.RemoveProduct(productNameToRemove);
                    break;

                case "4":
                    cart.DisplayCart();
                    break;

                case "5":
                    cart.Checkout();
                    break;

                case "6":
                    Console.WriteLine("Exiting... Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}