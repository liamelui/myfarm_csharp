using System;

namespace myfarm
{
    public class Bed
    {
        private Plant plantedItem;
        private DateTime? ripeningTime;
        private int id;

        public DateTime? RipeningTime { get => ripeningTime; set => ripeningTime = value; }
        public Plant PlantedItem { get => plantedItem; set => plantedItem = value; }
        public int Id { get => id; set => id = value; }

        public string TimeLeftBeforeRipening()
        {
            if (ripeningTime == null)
            {
                return null;
            }
            TimeSpan span = ripeningTime.Value.Subtract(DateTime.Now);
            if (span.CompareTo(TimeSpan.Zero) <= 0)
            {
                return null;
            }
            else
            {
                return span.Minutes + "m " + span.Seconds + "s";
            }
        }

        public void PlantItem(Plant item)
        {
            plantedItem = item;
            EconomicsManager.SubtractFromCurrentAmountOfMoney(item.SeedPrice);
            ripeningTime = DateTime.Now.AddSeconds(item.TimeToRipen);
        }

        public void SellRipedItem()
        {
            EconomicsManager.AddToCurrentAmountOfMoney(plantedItem.SellingPrice);
            plantedItem = null;
            ripeningTime = null;
        }
    }
}
