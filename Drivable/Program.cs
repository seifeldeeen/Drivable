interface IDeliverable
{
    bool RequiresSpecialDocking { get; }
    void LoadCargo(int weight);
    void UnloadCargo(int weight);
}

class DeliveryTruck : IDeliverable
{
    private string truckName;
    private int currentLoadWeight;
    private int maxLoadCapacity;
    //constructor
    public DeliveryTruck(string name, int max)
    {
        truckName = name;
        maxLoadCapacity = max;
        currentLoadWeight = 0;
    }
    //Encapsulation
    public string TruckName { get { return truckName; } }
    public int CurrentLoadWeight
    {
        get { return currentLoadWeight; }
        set
        {
            if (value < 0) { Console.WriteLine($"Warning: Weight can't be less than zero."); currentLoadWeight = 0; return; }
            else if (value > maxLoadCapacity) { Console.WriteLine($"Warning: Over Capacity, the currentLoadWeight will be: {maxLoadCapacity}"); currentLoadWeight = maxLoadCapacity; return; }
            else currentLoadWeight = value; return;
        }
    }
    public int MaxLoadCapacity => maxLoadCapacity;
    //Polymorphism
    public virtual void StartEngine()
    {
        Console.WriteLine("DeliveryTruck engine started.");
    }
    //Interface Implementation
    public virtual bool RequiresSpecialDocking => false;
    public void LoadCargo(int weight)
    {
        if ((CurrentLoadWeight + weight) > MaxLoadCapacity)
        {
            Console.WriteLine($"Warning: Over Capacity, the currentLoadWeight will be: {MaxLoadCapacity}");
            CurrentLoadWeight = MaxLoadCapacity;
        }
        else
        {
            CurrentLoadWeight = CurrentLoadWeight + weight;
            Console.WriteLine($"{weight} weight loaded. Current load weight: {CurrentLoadWeight}");
        }
    }
    public void UnloadCargo(int weight)
    {
        if (weight > CurrentLoadWeight)
        {
            Console.WriteLine("Can't unload that weight, there's no Enough weight");
        }
        else
        {
            CurrentLoadWeight = CurrentLoadWeight - weight;
            Console.WriteLine($"{weight} weight unloaded. Current load weight: {CurrentLoadWeight}.");
        }
    }

}
class RefrigeratedTruck: DeliveryTruck
{

public RefrigeratedTruck(string name, int max) : base(name, max)
{

}


    public override void StartEngine()
    {
        Console.WriteLine("Cooling systems initialized. Welcome Back Sir.");
    }
}
class LuxuryCourierVan : DeliveryTruck 
{
    private readonly bool hasPremiumInterior;

    public LuxuryCourierVan(string name, int max, bool IsPremium) : base(name, max)
{
    hasPremiumInterior = IsPremium;
}
    public void ActivateClimateControl()
    {
        if (hasPremiumInterior)
        {
            Console.WriteLine("Premium Interior is installed");
        }
        else
        {
            Console.WriteLine("Premium Interior is not installed");
        }
    }
    public override void StartEngine()
    {
        Console.WriteLine("Luxury Van's engine started quietly.");
    }
    public override bool RequiresSpecialDocking => true;

}

class Program
{
    static void Main(string[] args)
    {
        //Inheritance
        DeliveryTruck Delivery = new("El Primo", 3);
        RefrigeratedTruck Ref = new("Cool", 5);
        LuxuryCourierVan Luxury = new("Luxury", 10, true);
        IDeliverable[] arrOfDeliverables = [Delivery, Ref, Luxury];
        //testing
        Console.WriteLine("POLYMORPHISM");
        Delivery.StartEngine();
        Ref.StartEngine();
        Luxury.StartEngine();
        Console.WriteLine("---------------------");
        Console.WriteLine("ENCAPSULATION AND VALIDATION");
        Delivery.LoadCargo(7);
        Delivery.UnloadCargo(7);
        Delivery.UnloadCargo(3);
        Console.WriteLine("---------------------");
        Console.WriteLine("INHERITANCE AND INTERFACE USAGE");
        for (int i = 0; i < arrOfDeliverables.Length; i++)
        {
            arrOfDeliverables[i].LoadCargo(3);
            Console.WriteLine($"Status of Special Docking for {arrOfDeliverables[i]}: { arrOfDeliverables[i].RequiresSpecialDocking}");
        }
        Console.WriteLine("---------------------");
    }
}
