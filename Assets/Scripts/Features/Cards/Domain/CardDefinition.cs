namespace Arcana.Cards.Domain
{
    public class CardDefinition
    {
        public int ID{ get ; }
        public int Cost{ get ; }
        public float Damage{ get ; }
        public Element Element{ get ; }
        public RangeType RangeType{ get ; }
        public TargetType TargetType{ get ; }

        public CardDefinition(int id, int cost, float damage, Element element, RangeType rangeType, TargetType targetType)
        {
            if (id < 0)
            {
                throw new System.ArgumentException("Card ID must be a non-negative integer.");
            }
            
            ID = id;
            Cost = cost;
            Damage = damage;
            Element = element;
            RangeType = rangeType;
            TargetType = targetType;
        }
    }
}