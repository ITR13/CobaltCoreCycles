﻿namespace CobaltCoreCycles.Cards.Rare;

[Serializable]
[CardMeta(rarity = Rarity.rare, upgradesTo = new[] { Upgrade.A, Upgrade.B })]
public class ChargeMode : BaseCycle
{
    public override string Name() => "Charge Mode";
    protected override int MaxCycles => 2;
    public int Charge;
    private int ChargeAmount => upgrade == Upgrade.B ? 4 : 2;

    public override void OnDraw(State s, Combat c)
    {
        if (Cycle == 0 || upgrade == Upgrade.B)
        {
            Charge += ChargeAmount;
        }
    }

    private const string Cycle0Desc =
        "<c=disabledText>Attack for {0} and reset charge.</c>\nEach time this card is drawn, add <c=keyword>{1}</c>";

    private const string Cycle1Desc =
        "Attack for {0} and reset charge.<c=disabledText>\nEach time this card is drawn, add </c><c=keyword>{1}</c>";

    private const string Cycle0DescA =
        "<c=disabledText>Attack for {0} and reset charge.</c>Draw 2.\nEach time this card is drawn, add <c=keyword>{1}</c>";

    private const string Cycle1DescA =
        "Attack for {0} and reset charge.<c=disabledText>Draw 2.\nEach time this card is drawn, add </c><c=keyword>{1}</c>";

    protected override CardData CardData => new()
    {
        cost = upgrade == Upgrade.B ? 1 : 0,
        exhaust = upgrade == Upgrade.B,
        description = string.Format(Cycle == 0 ? Cycle0Desc : Cycle1Desc, Charge, ChargeAmount),
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        if (upgrade == Upgrade.A)
        {
            return new List<CardAction>
            {
                new ADrawCard
                {
                    count = 2,
                },
            };
        }

        return new List<CardAction>
        {
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return new List<CardAction>
        {
            new AAttack
            {
                damage = Charge,
            },
        };
    }
}