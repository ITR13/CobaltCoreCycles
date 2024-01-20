namespace CobaltCoreCycles.Cards.Uncommon;

[CardMeta(rarity = Rarity.uncommon, upgradesTo = [Upgrade.A, Upgrade.B])]
public class Shuffled : BaseCycle
{
    public override string Name() => "Shuffled";

    protected override int MaxCycles => 2;

    protected override CardData CardData => new()
    {
        cost = 0,
        exhaust = upgrade == Upgrade.A,
        retain = upgrade == Upgrade.B,
    };

    protected override List<CardAction> ActionsA(State s, Combat c)
    {
        var list = new List<CardAction>
        {
            new AShuffleHand
            {
            },
        };

        if (upgrade == Upgrade.A)
        {
            list.Add(
                new ACardSelect
                {
                    browseAction = new PutDiscardedCardInYourHand(),
                    browseSource = CardBrowse.Source.DrawOrDiscardPile,
                }
            );
        }

        return list;
    }

    protected override List<CardAction> ActionsB(State s, Combat c)
    {
        return
        [
            new AReverseHand
            {
            },

        ];
    }
}