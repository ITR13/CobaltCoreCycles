namespace CobaltCoreCycles.Cards.Rare;

[CardMeta(rarity = Rarity.rare, upgradesTo = new[] { Upgrade.A, Upgrade.B })]
public class Investment : BaseCycle
{
    public override string Name() => "Investment";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = 0,
        exhaust = true,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AHurt()
            {
                hurtAmount = 1,
                targetPlayer = true,
            },
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return new List<CardAction>
        {
            upgrade != Upgrade.A
                ? new AAttack
                {
                    damage = 7,
                    piercing = upgrade == Upgrade.B,
                }
                : new AStatus
                {
                    status = Status.powerdrive,
                    statusAmount = 2,
                    targetPlayer = true,
                },
        };
    }
}