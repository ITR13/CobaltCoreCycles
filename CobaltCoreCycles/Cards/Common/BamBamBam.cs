namespace CobaltCoreCycles.Cards.Common;

[CardMeta(rarity = Rarity.common, upgradesTo = [Upgrade.A, Upgrade.B])]
public class BamBamBam : BaseCycle
{
    public override string Name() => "Bam! Bam! Bam!";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = 1,
        exhaust = upgrade == Upgrade.B,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return
        [
            new AAttack
            {
                damage = 1,
            },

        ];
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        var damage = upgrade == Upgrade.B ? 2 : 1;
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
            new AAttack
            {
                damage = damage,
            },
        };

        if (upgrade == Upgrade.A)
        {
            list.Add(
                new AAttack
                {
                    damage = 1,
                }
            );
        }

        return list;
    }
}