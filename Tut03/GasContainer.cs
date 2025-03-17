namespace Tut03;
using System;

public class GasContainer : Container, IHazardNotifier
{
    public int Pressure { get; private set; } 
    
    public GasContainer(int capacity, int tareWeight, int pressure) 
        : base("G", capacity, tareWeight)
    {
        if (pressure <= 0)
            throw new ArgumentException("Pressure must be positive.");

        Pressure = pressure;
    }
    
    public override void UnloadCargo()
    {
        int remainingGas = (int)(CurrentLoad * 0.05); // %5 gaz bırak
        Console.WriteLine($"{SerialNumber}: The gas was drained, the remaining amount is: {remainingGas} kg.");
        SetCurrentLoad(remainingGas); 
    }

    // Tehlike bildirimi
    public void NotifyHazard(string message)
    {
        Console.WriteLine($"[Warning] {message}");
    }

    public override string ToString()
    {
        return base.ToString() + $" | Pressure: {Pressure} atm";
    }
}