namespace csharp
{
    public class Item
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }

        public override string ToString() => $"{Name}, {SellIn}, {Quality}";
    }
}
