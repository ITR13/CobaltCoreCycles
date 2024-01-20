namespace CobaltCoreCycles;

public class AReplaceDeck : CardAction
{
    public Deck FromDeck, ToDeck;

    public override void Begin(G g, State s, Combat c)
    {
        var cards = s.deck.Where(card => card.GetMeta().deck == FromDeck).ToList();
        foreach (var card in cards)
        {
            s.RemoveCardFromWhereverItIs(card.uuid);
            s.GetCurrentQueue()
                .QueueImmediate(
                    new ACardOffering()
                    {
                        limitDeck = ToDeck,
                        rarityOverride = card.GetMeta().rarity,
                        overrideUpgradeChances = card.upgrade != Upgrade.None,
                    }
                );
        }
    }
}