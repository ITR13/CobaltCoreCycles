namespace CobaltCoreCycles.Cards.Common;

[CardMeta(rarity = Rarity.common, upgradesTo = [Upgrade.A, Upgrade.B])]
public class Infinite : BaseCycle
{
    public override string Name() => "More! More!";

    protected override int MaxCycles => upgrade == Upgrade.B ? 3 : 2;

    protected override CardData CardData => new()
    {
        cost = upgrade == Upgrade.A ? 0 : 1,
        exhaust = upgrade == Upgrade.A,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return
        [
            new ADrawCard
            {
                count = 1,
            },

        ];
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        var list = new List<CardAction>
        {
            new ADrawCard
            {
                count = 5,
            },
        };
        if (upgrade == Upgrade.B)
        {
            list.Insert(
                0,
                new AEnergy
                {
                    changeAmount = 2,
                }
            );
        }

        return list;
    }

    protected override List<CardAction> ActionsC(State s, Combat c)
    {
        return
        [
            new ADrawCard
            {
                count = 1,
            },

        ];
    }
}