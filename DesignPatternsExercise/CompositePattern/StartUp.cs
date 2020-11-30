namespace CompositePattern
{
    using System;

    public class StartUp
    {
        public static void Main()
        {
            SingleGift phone = new SingleGift("Phone", 256);
            phone.CalculateTotalPrice();
            Console.WriteLine();

            CompositeGift rootBox 
                = new CompositeGift("Rootbox", 0);
            SingleGift truckToy 
                = new SingleGift("TruckToy", 289);
            SingleGift planeToy 
                = new SingleGift("PlaneToy", 587);
            rootBox.Add(truckToy);
            rootBox.Add(planeToy);

            CompositeGift childBox 
                = new CompositeGift("ChildBox", 0);
            SingleGift soldier 
                = new SingleGift("SoldierToy", 200);
            childBox.Add(soldier);
            rootBox.Add(childBox);

            Console.WriteLine($"Total price of this composite present is: {rootBox.CalculateTotalPrice()}");
        }
    }
}
