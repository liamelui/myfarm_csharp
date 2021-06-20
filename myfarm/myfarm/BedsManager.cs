using System.Collections.Generic;

namespace myfarm
{
    class BedsManager
    {
        static private List<Bed> bedsArray;

        public static List<Bed> BedsArray { get => bedsArray; }

        static BedsManager()
        {
            bedsArray = new List<Bed>();
            Bed bed = new Bed();
            bed.PlantedItem = null;
            bed.RipeningTime = null;
            bed.Id = bedsArray.Count;
            bedsArray.Add(bed);
        }

        public static void AddNewBed()
        {
            Bed bed = new Bed();
            bed.PlantedItem = null;
            bed.RipeningTime = null;
            bed.Id = bedsArray.Count;
            bedsArray.Add(bed);
        }
    }
}
