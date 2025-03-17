// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using Tut03;

class Program
{
    static void Main()
    {
        List<ContainerShip> ships = new List<ContainerShip>(); 
        List<Container> containers = new List<Container>(); 
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Container Loading Management System ===");
            Console.WriteLine("1. Add Container Ship");
            Console.WriteLine("2. Add Container");
            Console.WriteLine("3. Load Container onto Ship");
            Console.WriteLine("4. Remove the Container from the Ship");
            Console.WriteLine("5. Print Ship and Cargo Information");
            Console.WriteLine("6. Exit");
            Console.Write("Make your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddContainerShip(ships);
                    break;
                case "2":
                    AddContainer(containers);
                    break;
                case "3":
                    LoadContainerOntoShip(ships, containers);
                    break;
                case "4":
                    RemoveContainerFromShip(ships);
                    break;
                case "5":
                    PrintShipInfo(ships);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid selection, try again...");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
    
    static void AddContainerShip(List<ContainerShip> ships)
    {
        Console.Write("Ship Name: ");
        string name = Console.ReadLine();
        
        Console.Write("Maximum speed (knots): ");
        int maxSpeed = int.Parse(Console.ReadLine());

        Console.Write("Maximum number of containers: ");
        int maxContainers = int.Parse(Console.ReadLine());

        Console.Write("Maximum carrying capacity (tons): ");
        int maxWeight = int.Parse(Console.ReadLine());

        ships.Add(new ContainerShip(name, maxSpeed, maxContainers, maxWeight));
        Console.WriteLine($"{name} The ship named has been added!");
    }
    
    static void AddContainer(List<Container> containers)
    {
        Console.Write("Container Type (L = Liquid, G = Gas, C = refrigerated): ");
        string type = Console.ReadLine().ToUpper();

        Console.Write("capasity (kg): ");
        int capacity = int.Parse(Console.ReadLine());

        Console.Write("Tare wight (kg): ");
        int tareWeight = int.Parse(Console.ReadLine());

        Container container;

        switch (type)
        {
            case "L":
                container = new LiquidContainer(capacity, tareWeight);
                break;
            case "G":
                Console.Write("Pressure (atm): ");
                int pressure = int.Parse(Console.ReadLine());
                container = new GasContainer(capacity, tareWeight, pressure);
                break;
            case "C":
                Console.Write("Required temperature (C): ");
                double temperature = double.Parse(Console.ReadLine());
                container = new RefrigeratedContainer(capacity, tareWeight, temperature);
                break;
            default:
                Console.WriteLine("Invalid container type!");
                return;
        }

        containers.Add(container);
        Console.WriteLine($"Container added: {container.SerialNumber}");
    }
    
    static void LoadContainerOntoShip(List<ContainerShip> ships, List<Container> containers)
    {
        if (ships.Count == 0 || containers.Count == 0)
        {
            Console.WriteLine("No ship or container found!");
            return;
        }

        Console.Write("Enter ship name: ");
        string shipName = Console.ReadLine();
        ContainerShip ship = ships.Find(s => s.Name == shipName);

        if (ship == null)
        {
            Console.WriteLine("The ship was not found!");
            return;
        }

        Console.Write("Enter container serial number: ");
        string serial = Console.ReadLine();
        Container container = containers.Find(c => c.SerialNumber == serial);

        if (container == null)
        {
            Console.WriteLine("Container not found!");
            return;
        }

        ship.AddContainer(container);
        containers.Remove(container); 
        Console.WriteLine($"{serial} numbered container {ship.Name} loaded onto ship.");
    }
    
    static void RemoveContainerFromShip(List<ContainerShip> ships)
    {
        Console.Write("Enter ship name: ");
        string shipName = Console.ReadLine();
        ContainerShip ship = ships.Find(s => s.Name == shipName);

        if (ship == null || ship.Containers.Count == 0)
        {
            Console.WriteLine("No ship found or no container!");
            return;
        }

        Console.Write("Enter the serial number of the container you want to remove: ");
        string serial = Console.ReadLine();
        Container container = ship.Containers.Find(c => c.SerialNumber == serial);

        if (container == null)
        {
            Console.WriteLine("Container not found!");
            return;
        }

        ship.RemoveContainer(container);
        Console.WriteLine($"{serial} \nContainer numbered was removed from the ship.");
    }
    
    static void PrintShipInfo(List<ContainerShip> ships)
    {
        if (ships.Count == 0)
        {
            Console.WriteLine("No registered ships!");
            return;
        }

        foreach (var ship in ships)
        {
            Console.WriteLine($"Ship: {ship.Name} (speed: {ship.MaxSpeed} knots)");
            Console.WriteLine($"Carrying Capacity: {ship.MaxWeight} ton, Container Capacity: {ship.MaxContainers}");
            Console.WriteLine("Loaded Containers:");
            foreach (var container in ship.Containers)
            {
                Console.WriteLine($"  - {container.SerialNumber} ({container.GetType().Name})");
            }
        }
    }
}