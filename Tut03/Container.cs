namespace Tut03;

public abstract class Container
{
    private static int _idCounter = 1; 
    public string SerialNumber { get; private set; } 
    public int Capacity { get; private set; } 
    public int TareWeight { get; private set; } 
    public int CurrentLoad { get; private set; } = 0; 

    // Constructor
    public Container(string type, int capacity, int tareWeight)
    {
        if (capacity <= 0 || tareWeight <= 0)
            throw new ArgumentException("Capacity and tare weight must be positive.");

        SerialNumber = $"KON-{type}-{_idCounter++}";
        Capacity = capacity;
        TareWeight = tareWeight;
    }
    
    public void SetCurrentLoad(int load)
    {
        if (load < 0)
            throw new ArgumentException("The amount of charge cannot be negative.");
    
        CurrentLoad = load;
    }
    
    public virtual void LoadCargo(int mass)
    {
        if (mass <= 0)
            throw new ArgumentException("The amount of charge must be positive.");

        if (CurrentLoad + mass > Capacity)
            throw new OverfillException($"Container capacity exceeded! (Maks: {Capacity} kg)");

        CurrentLoad += mass;
        Console.WriteLine($"{SerialNumber}: {mass} kg loaded. current load: {CurrentLoad} kg.");
    }
    
    public virtual void UnloadCargo()
    {
        Console.WriteLine($"{SerialNumber}: {CurrentLoad} kg unloaded.");
        CurrentLoad = 0;
    }

    public override string ToString()
    {
        return $"{SerialNumber} - load: {CurrentLoad}/{Capacity} kg, Tare wight: {TareWeight} kg";
    }
}

// Özel hata sınıfı
public class OverfillException : Exception
{
    public OverfillException(string message) : base(message) { }
}