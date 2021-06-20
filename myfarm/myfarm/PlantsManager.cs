using System.Collections.Generic;

namespace myfarm
{
    class PlantsManager
    {
        static private readonly List<Plant> _plantsArray; 

        public static List<Plant> PlantsArray => _plantsArray;

        static PlantsManager()
        {
            _plantsArray = new List<Plant>();
            _plantsArray.Insert(0, new Plant("Carrot", 5, 1, 2));
            _plantsArray.Insert(1, new Plant("Tomato", 7, 2, 4));
            _plantsArray.Insert(2, new Plant("Wheat", 9, 4, 8));
        } 
        
    }
}
