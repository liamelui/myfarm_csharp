namespace myfarm
{
    public class Plant
    {
        private readonly string _name;
        private readonly long _timeToRipen;
        private readonly int _seedPrice;
        private readonly int _sellingPrice;

        public string Name => _name;

        public long TimeToRipen => _timeToRipen;

        public int SeedPrice => _seedPrice;

        public int SellingPrice => _sellingPrice;

        public Plant(string name, int timeToRipen, int seedPrice, int sellingPrice)
        {
            _name = name;
            _timeToRipen = timeToRipen;
            _seedPrice = seedPrice;
            _sellingPrice = sellingPrice;
        }

    }
}
