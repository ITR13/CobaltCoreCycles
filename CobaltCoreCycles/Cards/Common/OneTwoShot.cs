namespace CobaltCoreCycles.Cards.Common;

[CardMeta(rarity = Rarity.common, upgradesTo = new[] { Upgrade.A, Upgrade.B })]
public class OneTwoShot : BaseCycle
{
    public override string Name() => "One-Two";

    protected override int MaxCycles => upgrade == Upgrade.A ? 3 : 2;

    protected override CardData CardData => new()
    {
        cost = 1,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AAttack
            {
                damage = 0,
                piercing = upgrade == Upgrade.B,
            },
            new AAttack
            {
                damage = 1,
                piercing = upgrade == Upgrade.B,
            },
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AAttack
            {
                damage = 1,
                piercing = upgrade == Upgrade.B,
            },
            new AAttack
            {
                damage = 2,
                piercing = upgrade == Upgrade.B,
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
            },
            new AAttack
            {
                damage = 3,
            },
        };
    }
}