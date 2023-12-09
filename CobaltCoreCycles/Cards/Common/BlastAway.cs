namespace CobaltCoreCycles.Cards.Common;

[CardMeta(rarity = Rarity.common, upgradesTo = new[] { Upgrade.A, Upgrade.B })]
public class BlastAway : BaseCycle
{
    public override string Name() => "Blast Away";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = 2,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.None => new List<CardAction> { new AAttack { damage = 2, } },
            Upgrade.A => new List<CardAction>
            {
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 3,
                    targetPlayer = true,
                },
            },
            Upgrade.B => new List<CardAction>
            {
                new AStatus
                {
                    status = Status.shield,
                    statusAmount = 5,
                    targetPlayer = true,
                },
            },
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        var damage = 3;
        var list = new List<CardAction>
        {
            new AAttack
            {
                damage = damage,
            },
            new AAttack
            {
                damage = damage,
            },
        };

        return list;
    }
}