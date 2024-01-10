using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
  public class GildedRoseTest
  {
    [Test]
    public void foo()
    {
      IList<Item> Items = new List<Item> { new() { Name = "foo", SellIn = 0, Quality = 0 } };
      GildedRose app = new(Items);
      app.UpdateQuality();
      Assert.AreEqual("foo", Items[0].Name);
    }
  }
}
