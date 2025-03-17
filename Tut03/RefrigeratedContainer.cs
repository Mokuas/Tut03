namespace Tut03;
using System;

public class RefrigeratedContainer : Container
{
    public string ProductType { get; private set; } 
    public double RequiredTemperature { get; private set; } 
    
    public RefrigeratedContainer(int capacity, int tareWeight, double requiredTemperature) 
        : base("C", capacity, tareWeight)
    {
        RequiredTemperature = requiredTemperature;
    }
    
    public void SetProduct(string productType, double productTemperature)
    {
        if (string.IsNullOrWhiteSpace(productType))
            throw new ArgumentException("Product type cannot be empty.");

        if (productTemperature > RequiredTemperature)
            throw new ArgumentException($"Warning: {productType} product {productTemperature} C It cannot be stored at temperature! (Require: {RequiredTemperature} C)");

        ProductType = productType;
        Console.WriteLine($"{SerialNumber}: {ProductType} product added successfully.");
    }

    public override string ToString()
    {
        return base.ToString() + $" | Product: {ProductType ?? "Boş"} | Required C: {RequiredTemperature} C";
    }
}