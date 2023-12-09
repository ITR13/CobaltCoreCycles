namespace CobaltCoreCycles.Cards.Uncommon;

[CardMeta(rarity = Rarity.uncommon, upgradesTo = new[] { Upgrade.A, Upgrade.B })]
public class Mill : BaseCycle
{
    public override string Name() => "Mill";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = upgrade == Upgrade.B ? 1 : 0,
        infinite = true,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        return new List<CardAction>
        {
            new ADiscard
            {
                count = upgrade == Upgrade.A ? 2 : 1,
            },
        };
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        var list = new List<CardAction>
        {
            new ACardSelect
            {
                browseAction = upgrade != Upgrade.B
                    ? new PutDiscardedCardOnTopOfDrawPile()
                    : new PutDiscardedCardInYourHand(),
                browseSource = CardBrowse.Source.DiscardPile,
            },
        };

        if (upgrade != Upgrade.None)
        {
            list.Add(
                new ACardSelect
                {
                    browseAction = upgrade != Upgrade.B
                        ? new PutDiscardedCardOnTopOfDrawPile()
                        : new PutDiscardedCardInYourHand(),
                    browseSource = CardBrowse.Source.DiscardPile,
                }
            );
        }

        return list;
    }

    protected override List<CardAction> ActionsC(State s, Combat c)
    {
        return new List<CardAction>
        {
            new ADrawCard
            {
                count = 1,
            },
        };
    }
}