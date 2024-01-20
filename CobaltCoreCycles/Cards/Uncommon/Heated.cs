namespace CobaltCoreCycles.Cards.Uncommon;

[CardMeta(rarity = Rarity.uncommon, upgradesTo = [Upgrade.A, Upgrade.B])]
public class Heated : BaseCycle
{
    public override string Name() => "Heated";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = 1,
        exhaust = upgrade == Upgrade.B,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return upgrade == Upgrade.B
            ?
            [
                new AStatus
                {
                    status = Status.heat,
                    statusAmount = -1,
                    targetPlayer = true,
                },

                new ADrawCard
                {
                    count = 1,
                },

            ]
            :
            [
                new AStatus
                {
                    status = Status.heat,
                    statusAmount = 2,
                    targetPlayer = true,
                },

            ];
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return upgrade == Upgrade.A
            ?
            [
                new AAttack
                {
                    damage = 2,
                    piercing = true,
                },

                new AAttack
                {
                    damage = 3,
                    piercing = true,
                },

            ]
            :
            [
                new AStatus
                {
                    status = Status.serenity,
                    statusAmount = 2,
                    targetPlayer = true,
                },

            ];
    }
}