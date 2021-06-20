namespace myfarm
{
    class EconomicsManager
    {
        private static int amountOfMoney;

        public static int AmountOfMoney { get => amountOfMoney; set => amountOfMoney = value; }

        static EconomicsManager()
        {
            amountOfMoney = 12;
        }

        public static void AddToCurrentAmountOfMoney(int amount)
        {
            amountOfMoney = amountOfMoney + amount;
        }

        public static void SubtractFromCurrentAmountOfMoney(int amount)
        {
            amountOfMoney = amountOfMoney - amount;
        }

        public static bool IsPurchasePossible(int price)
        {
            return amountOfMoney - price >= 0;
        }

        public static int GetNewBedPrice()
        {
            return 10 * BedsManager.BedsArray.Count;
        }
    }
}
