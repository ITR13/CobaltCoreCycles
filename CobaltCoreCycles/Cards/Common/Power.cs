namespace CobaltCoreCycles.Cards.Common;

[CardMeta(rarity = Rarity.common, upgradesTo = [Upgrade.A, Upgrade.B])]
public class Power : BaseCycle
{
    public override string Name() => "Powah";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = upgrade == Upgrade.None ? 2 : 1,
        exhaust = upgrade == Upgrade.A,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.None => [new AAttack { damage = 1, }],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = Status.overdrive, statusAmount = 1,
                    targetPlayer = true,
                },

            ],
            Upgrade.B => [new AAttack { damage = 1, }, new AAttack { damage = 1, }],
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return
        [
            new AStatus
            {
                status = Status.overdrive,
                statusAmount = upgrade switch
                {
                    Upgrade.None => 3,
                    Upgrade.A => 3,
                    Upgrade.B => 2,
                    _ => throw new ArgumentOutOfRangeException()
                },
                targetPlayer = true,
            },

        ];
    }
}