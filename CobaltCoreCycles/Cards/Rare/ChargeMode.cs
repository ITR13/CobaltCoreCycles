namespace CobaltCoreCycles.Cards.Rare;

[Serializable]
[CardMeta(rarity = Rarity.rare, upgradesTo = [Upgrade.A, Upgrade.B])]
public class ChargeMode : BaseCycle
{
    public override string Name() => "Charge Mode";
    protected override int MaxCycles => 2;
    public int Charge;
    private int ChargeAmount => upgrade == Upgrade.B ? 4 : 2;

    public override void OnDraw(State s, Combat c)
    {
        if (Cycle == 0)
        {
            Charge += ChargeAmount;
        }
    }

    public override void AfterWasPlayed(State state, Combat c)
    {
        if (Cycle == 1)
        {
            Charge = 0;
        }
    }

    private const string Cycle0Desc =
        "<c=disabledText>Attack for {0}, Charge=0.</c>\nOn Draw: Charge+=<c=keyword>{1}</c>";

    private const string Cycle1Desc =
        "Attack for {0}, Charge=0.\n<c=disabledText>On Draw: Charge+=</c><c=keyword>{1}</c>";

    private const string Cycle0DescA =
        "<c=disabledText>Attack for {0}, Charge=0.</c>\nDraw <c=keyword>2</c>.\nOn Draw: Charge+=<c=keyword>{1}</c>";

    private const string Cycle1DescA =
        "Attack for {0}, Charge=0.\n<c=disabledText>Draw <c=keyword>2</c>.\nOn Draw: Charge+=</c><c=keyword>{1}</c>";

    protected override CardData CardData => new()
    {
        cost = upgrade == Upgrade.B ? 1 : 0,
        exhaust = upgrade == Upgrade.B,
        description = string.Format(
            upgrade == Upgrade.A ? (Cycle == 0 ? Cycle0DescA : Cycle1DescA) : (Cycle == 0 ? Cycle0Desc : Cycle1Desc),
            Charge,
            ChargeAmount
        ),
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        if (upgrade == Upgrade.A)
        {
            return
            [
                new ADrawCard
                {
                    count = 2,
                },

            ];
        }

        return [];
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return
        [
            new AAttack
            {
                damage = Charge,
            },

        ];
    }
}