using System.Collections.Generic;

namespace csharp
{
  public class GildedRose
  {
    private const string NameSulfuras = "Sulfuras";
    private const string NameBrie = "Aged Brie";
    private const string NameBackstagePass = "Backstage passes";
    private const string NameConjured = "Conjured";

    private const int MaxItemQuality = 50;

    IList<Item> Items;
    public GildedRose(IList<Item> Items)
    {
      this.Items = Items;
    }

    /// <summary>
    /// “Aged Brie” actually increases in Quality the older it gets
    /// </summary>
    /// <param name="item"></param>
    private void UpdateBrie(Item item)
    {
      var amountToImprove = item.SellIn > 0 ? 1 : 2;
      item.Quality = int.Min(item.Quality + amountToImprove, MaxItemQuality);
    }

    /// <summary>
    /// “Backstage passes”, like aged brie, increases in Quality as its SellIn value approaches
    /// Quality increases by 2 when there are 10 days or less
    /// Quality increases by 3 when there are 5 days or less
    /// Quality drops to 0 after the concert
    /// </summary>
    /// <param name="item"></param>
    private void UpdateBackstagePass(Item item)
    {
      if (item.SellIn > 10)
        item.Quality = int.Min(item.Quality + 1, 50);
      else if (item.SellIn > 5)
        item.Quality = int.Min(item.Quality + 2, 50);
      else if (item.SellIn > 0)
        item.Quality = int.Min(item.Quality + 3, 50);
      else
        item.Quality = 0;
    }

    /// <summary>
    /// Once the sell by date has passed, Quality degrades twice as fast
    /// The Quality of an item is never negative
    /// </summary>
    /// <param name="item"></param>
    private void UpdateStandardItem(Item item)
    {
      var amountToDegrade = item.SellIn > 0 ? 1 : 2;
      item.Quality = int.Max(item.Quality - amountToDegrade, 0);
    }


    public void UpdateQuality()
    {
      for (var i = 0; i < Items.Count; i++)
      {
        var item = Items[i];
        UpdateQualitySingleItem(item);
      }
    }

    private void UpdateQualitySingleItem(Item item)
    {
      /// “Sulfuras”, being a legendary item, never has to be sold or decreases in Quality
      /// It is a legendary item and as such its Quality is 80 and it never alters.
      if (item.Name.Contains(NameSulfuras))
        return;

      item.SellIn--;

      QualityStep(item);

      // if the item is conjured, it's quality changes twice as fast!
      if (item.Name.StartsWith(NameConjured))
        QualityStep(item);
    }

    private void QualityStep(Item item)
    {
      if (item.Name.Contains(NameBrie)) { UpdateBrie(item); }
      else if (item.Name.Contains(NameBackstagePass)) { UpdateBackstagePass(item); }
      else { UpdateStandardItem(item); }
    }
  }
}
