namespace Tut03;
using System;
using System.Collections.Generic;

public class ContainerShip
{
    public string Name { get; private set; } 
    public int MaxSpeed { get; private set; } 
    public int MaxContainers { get; private set; }
    public int MaxWeight { get; private set; }
    public List<Container> Containers { get; private set; }

    // Constructor
    public ContainerShip(string name, int maxSpeed, int maxContainers, int maxWeight)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ship name cannot be blank.");
        if (maxSpeed <= 0 || maxContainers <= 0 || maxWeight <= 0)
            throw new ArgumentException("Speed, container capacity and weight limits must be positive.");

        Name = name;
        MaxSpeed = maxSpeed;
        MaxContainers = maxContainers;
        MaxWeight = maxWeight * 1000; // kg cinsine çevirdik
        Containers = new List<Container>();
    }
    
    public int GetTotalWeight()
    {
        int totalWeight = 0;
        foreach (var container in Containers)
        {
            totalWeight += container.TareWeight + container.CurrentLoad;
        }
        return totalWeight;
    }
    
    public void AddContainer(Container container)
    {
        if (Containers.Count >= MaxContainers)
            throw new InvalidOperationException("The ship is full! No more containers can be added.");

        if (GetTotalWeight() + container.TareWeight + container.CurrentLoad > MaxWeight)
            throw new InvalidOperationException("Weight limit exceeded! Cannot add container.");

        Containers.Add(container);
        Console.WriteLine($"{container.SerialNumber} numbered container {Name} added to ship.");
    }
    
    public void RemoveContainer(Container container)
    {
        if (!Containers.Contains(container))
            throw new InvalidOperationException("The container is not on board!");

        Containers.Remove(container);
        Console.WriteLine($"{container.SerialNumber} numbered container {Name} unloaded.");
    }
    
    public void ReplaceContainer(string oldSerial, Container newContainer)
    {
        Container oldContainer = Containers.Find(c => c.SerialNumber == oldSerial);
        if (oldContainer == null)
            throw new InvalidOperationException("Old container not found on ship!");

        RemoveContainer(oldContainer);
        AddContainer(newContainer);
        Console.WriteLine($"{oldSerial} numbered container, {newContainer.SerialNumber} replaced.");
    }

    // Konteyner taşıma (başka bir gemiye aktarma)
    public void TransferContainer(ContainerShip targetShip, string serialNumber)
    {
        Container container = Containers.Find(c => c.SerialNumber == serialNumber);
        if (container == null)
            throw new InvalidOperationException("Container not found on ship!");

        RemoveContainer(container);
        targetShip.AddContainer(container);
        Console.WriteLine($"{serialNumber} numbered container {Name}(Ship) {targetShip.Name} carried.");//gemiden gemiye tasima 
    }
    
    public override string ToString()
    {
        return $"Ship: {Name} | Speed: {MaxSpeed} knots | Maksimum weight: {MaxWeight / 1000} ton | Container number: {Containers.Count}/{MaxContainers}";
    }
}