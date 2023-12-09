namespace CobaltCoreCycles.Cards.Common;

[CardMeta(rarity = Rarity.common, upgradesTo = new[] { Upgrade.A, Upgrade.B })]
public class Stunning : BaseCycle
{
    public override string Name() => "Stunning!";

    protected override int MaxCycles => upgrade == Upgrade.A ? 3 : 2;

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
                damage = 0,
                stunEnemy = true,
            },
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AAttack
            {
                damage = 3,
                stunEnemy = upgrade == Upgrade.B,
            },
        };
    }

    protected override List<CardAction> ActionsC(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AAttack
            {
                damage = 2,
                stunEnemy = true,
            },
        };
    }
}