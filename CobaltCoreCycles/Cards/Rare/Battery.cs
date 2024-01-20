namespace CobaltCoreCycles.Cards.Rare;

[CardMeta(rarity = Rarity.rare, upgradesTo = [Upgrade.A, Upgrade.B])]
public class Battery : BaseCycle
{
    public override string Name() => "Battery";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = upgrade == Upgrade.B ? 0 : 2,
        exhaust = true,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        if (upgrade != Upgrade.None)
        {
            return
            [
                new ADrawCard
                {
                    count = 1,
                },
            ];
        }

        return [];
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        var list = new List<CardAction>
        {
            new AEnergy
            {
                changeAmount = upgrade == Upgrade.B ? 5 : 8,
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