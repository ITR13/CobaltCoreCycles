namespace CobaltCoreCycles.Cards.Common;

[CardMeta(rarity = Rarity.common, upgradesTo = new[] { Upgrade.A, Upgrade.B })]
public class DrawShot : BaseCycle
{
    public override string Name() => "Draw Shot?";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = 1,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AAttack()
            {
                damage = 2,
            },
            new ADrawCard
            {
                count = upgrade == Upgrade.A ? 3 : 1,
            },
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AAttack()
            {
                damage = 1,
            },
            new ADrawCard
            {
                count = upgrade == Upgrade.B ? 4 : 2,
            },
        };
    }
}