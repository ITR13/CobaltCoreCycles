namespace CobaltCoreCycles.Cards.Uncommon;

[CardMeta(rarity = Rarity.uncommon, upgradesTo = new[] { Upgrade.A, Upgrade.B })]
public class Cat : BaseCycle
{
    public override string Name() => "Cat";

    protected override int MaxCycles => 3;

    protected override CardData CardData => new()
    {
        cost = 2,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AStatus
            {
                status = upgrade == Upgrade.A ? Status.perfectShield : Status.shield,
                statusAmount = upgrade == Upgrade.A ? 1 : 3,
            },
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AStatus
            {
                status = upgrade == Upgrade.B ? Status.timeStop : Status.evade,
                statusAmount = 3,
            },
        };
    }

    protected override List<CardAction> ActionsC(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AAttack
            {
                damage = 3,
            },
        };
    }
}