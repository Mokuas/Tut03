namespace Tut03;
using System;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; private set; } // Tehlikeli madde taşıyor mu?
    
    public LiquidContainer(int capacity, int tareWeight, bool isHazardous = false) 
        : base("L", capacity, tareWeight)
    {
        IsHazardous = isHazardous;
    }
    
    public override void LoadCargo(int mass)
    {
        if (mass <= 0)
            throw new ArgumentException("The amount of charge must be positive.");

        int maxAllowed = IsHazardous ? Capacity / 2 : (int)(Capacity * 0.9);

        if (CurrentLoad + mass > maxAllowed)
        {
            NotifyHazard($"Dangerous installation error: {SerialNumber} ({mass} kg Attempted to load )");
            throw new OverfillException($"Dangerous load limit exceeded! (Maksimum: {maxAllowed} kg)");
        }

        base.LoadCargo(mass);
    }

    // Tehlike bildirimi
    public void NotifyHazard(string message)
    {
        Console.WriteLine($"[WARNING] {message}");
    }

    public override string ToString()
    {
        return base.ToString() + $" | Tehlikeli: {(IsHazardous ? "yes" : "no")}";
    }
}

// Tehlike bildirme arayüzü
public interface IHazardNotifier
{
    void NotifyHazard(string message);
}