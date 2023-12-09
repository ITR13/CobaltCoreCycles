namespace CobaltCoreCycles.Cards.Rare;

[CardMeta(rarity = Rarity.rare, upgradesTo = new[] { Upgrade.A, Upgrade.B })]
public class DelayedRepair : BaseCycle
{
    public override string Name() => "Delayed Repair";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = upgrade == Upgrade.A ? 1 : 2,
        exhaust = true,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AHurt()
            {
                hurtAmount = upgrade == Upgrade.B ? 3 : 2,
                targetPlayer = true,
            },
            new AStatus
            {
                status = Status.shield,
                statusAmount = 1,
                targetPlayer = true,
            },
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AHeal
            {
                healAmount = upgrade == Upgrade.B ? 6 : 4,
                targetPlayer = true,
            },
            new AStatus
            {
                status = Status.shield,
                statusAmount = 1,
                targetPlayer = true,
            },
        };
    }
}