namespace CobaltCoreCycles.Cards.Uncommon;

[CardMeta(rarity = Rarity.uncommon, upgradesTo = [Upgrade.A, Upgrade.B])]
public class Shielded : BaseCycle
{
    public override string Name() => "Shielded";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = 1,
        exhaust = true,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return
        [
            new AStatus
            {
                status = upgrade == Upgrade.A ? Status.shield : Status.maxShield,
                statusAmount = -3,
                targetPlayer = true,
            },

        ];
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return upgrade != Upgrade.B
            ?
            [
                new AStatus
                {
                    status = Status.maxShield,
                    statusAmount = 3,
                    targetPlayer = true,
                },

            ]
            :
            [
                new AStatus
                {
                    status = Status.maxShield,
                    statusAmount = 2,
                    targetPlayer = true,
                },

                new AStatus
                {
                    status = Status.shield,
                    statusAmount = 2,
                    targetPlayer = true,
                },

            ];
    }
}