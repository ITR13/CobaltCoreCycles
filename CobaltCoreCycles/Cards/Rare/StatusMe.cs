namespace CobaltCoreCycles.Cards.Rare;

[Serializable]
[CardMeta(rarity = Rarity.rare, upgradesTo = new[] { Upgrade.A, Upgrade.B })]
public class StatusMe : BaseCycle
{
    public override string Name() => "Status";
    protected override int MaxCycles => 3;

    protected override CardData CardData => new()
    {
        cost = upgrade == Upgrade.A ? 3 : 2,
        exhaust = upgrade != Upgrade.A,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AStatus
            {
                status = Status.ace,
                statusAmount = 1,
                targetPlayer = true,
            },
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AStatus()
            {
                status = Status.corrode,
                statusAmount = upgrade == Upgrade.B ? 3 : 2,
                targetPlayer = false,
            },
        };
    }

    protected override List<CardAction> ActionsC(State s, Combat c)
    {
        return new List<CardAction>
        {
            new ADrawCard
            {
                count = 3,
            },
        };
    }
}