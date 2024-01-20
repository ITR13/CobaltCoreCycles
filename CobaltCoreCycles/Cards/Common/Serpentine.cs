namespace CobaltCoreCycles.Cards.Common;

[CardMeta(rarity = Rarity.common, upgradesTo = [Upgrade.A, Upgrade.B])]
public class Serpentine : BaseCycle
{
    public override string Name() => "Serpentine! Serpentine!";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = 1,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return
        [
            new AMove()
            {
                isRandom = upgrade == Upgrade.A,
                dir = 2,
                targetPlayer = true,
            },

            new AAttack
            {
                damage = upgrade == Upgrade.None ? 1 : 2,
            },

        ];
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        if (upgrade != Upgrade.A)
        {
            return
            [
                new AMove()
                {
                    dir = -2,
                    targetPlayer = true,
                },

                new AAttack
                {
                    damage = upgrade == Upgrade.None ? 1 : 2,
                },

            ];
        }
        return
        [
            new AAttack
            {
                damage = 2,
            },

            new AStatus
            {
                status = Status.evade,
                statusAmount = 2,
                targetPlayer = true,
            },

        ];
    }
}