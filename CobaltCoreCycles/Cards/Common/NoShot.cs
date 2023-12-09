namespace CobaltCoreCycles.Cards.Common;

[CardMeta(rarity = Rarity.common, upgradesTo = new[] { Upgrade.A, Upgrade.B })]
public class NoShot : BaseCycle
{
    public override string Name() => "No Shot!";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = 1,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AStatus
            {
                status = Status.tempShield,
                statusAmount = upgrade == Upgrade.A ? 5 : 2,
                targetPlayer = true,
            },
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AStatus()
            {
                status = Status.evade,
                statusAmount = upgrade == Upgrade.B ? 3 : 2,
                targetPlayer = true,
            },
        };
    }
}