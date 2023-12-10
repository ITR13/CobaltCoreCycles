namespace CobaltCoreCycles.Cards.Uncommon;

[CardMeta(rarity = Rarity.uncommon, upgradesTo = new[] { Upgrade.A, Upgrade.B, (Upgrade)3 })]
public class Magic : BaseCycle
{
    public override string Name() => "Magic";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = 0,
        exhaust = upgrade != Upgrade.None,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.None => new List<CardAction>
            {
                new AStatus
                {
                    status = Status.shard,
                    statusAmount = 1,
                    targetPlayer = true,
                },
                new ADrawCard
                {
                    count = 1,
                },
            },
            Upgrade.A => new List<CardAction>
            {
                new AStatus
                {
                    status = Status.shard,
                    statusAmount = 3,
                    targetPlayer = true,
                },
            },
            Upgrade.B => new List<CardAction>
            {
                new AStatus
                {
                    status = Status.shard,
                    statusAmount = 3,
                    targetPlayer = true,
                },
            },
            (Upgrade)3 => new List<CardAction>
            {
                new AStatus
                {
                    status = Status.shard,
                    statusAmount = 3,
                    targetPlayer = true,
                },
                new ADrawCard
                {
                    count = 1,
                },
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.None => new List<CardAction>
            {
                new AAttack
                {
                    damage = 1,
                },
                new AAttack
                {
                    shardcost = 1,
                    damage = 2,
                },
            },
            Upgrade.A => new List<CardAction>
            {
                new AAttack
                {
                    shardcost = 1,
                    damage = 2,
                },
                new AAttack
                {
                    shardcost = 1,
                    damage = 2,
                },
                new AAttack
                {
                    shardcost = 1,
                    damage = 2,
                },
            },
            Upgrade.B => new List<CardAction>
            {
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 1,
                    shardcost = 1,
                },
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 1,
                    shardcost = 2,
                },
            },
            (Upgrade)3 => new List<CardAction>
            {
                new AStatus
                {
                    status = Status.shard,
                    statusAmount = 1,
                    targetPlayer = true,
                },
                new ADrawCard
                {
                    count = 3,
                },
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}