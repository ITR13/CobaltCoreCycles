namespace CobaltCoreCycles.Cards.Rare;

[CardMeta(rarity = Rarity.rare, upgradesTo = new[] { Upgrade.A, Upgrade.B })]
public class Battery : BaseCycle
{
    public override string Name() => "Battery";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = upgrade == Upgrade.B ? 0 : 3,
        exhaust = true,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return new List<CardAction>
        {
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        var list = new List<CardAction>
        {
            new AEnergy
            {
                changeAmount = 6,
            },
        };
        if (upgrade == Upgrade.A)
        {
            list.Add(
                new ADrawCard
                {
                    count = 2,
                }
            );
        }

        return list;
    }
}