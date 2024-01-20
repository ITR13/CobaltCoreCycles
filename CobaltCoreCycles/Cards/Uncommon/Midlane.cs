namespace CobaltCoreCycles.Cards.Uncommon;

[CardMeta(rarity = Rarity.uncommon, upgradesTo = [Upgrade.A, Upgrade.B])]
public class Midlane : BaseCycle
{
    public override string Name() => "Midlane";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = upgrade == Upgrade.A ? 0 : 1,
        exhaust = upgrade == Upgrade.A,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return
        [
            new AStatus
            {
                status = Status.droneShift,
                statusAmount = upgrade == Upgrade.None ? 1 : 2,
                targetPlayer = true,
            },

        ];
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return
        [
            new ABubbleField
            {
            },

        ];
    }
}