namespace CobaltCoreCycles.Artifacts;

[ArtifactMeta(pools = [ArtifactPool.Boss])]
public class Assimilation : Artifact
{
    public override void OnReceiveArtifact(State state)
    {
        var characters = state
            .characters
            .Where(ch => ch.deckType != Main.CyclesDeck && ch.deckType != null)
            .ToList();
        
        if (characters.Count == 0) return;

        var ch = characters[0];
        List<CardAction> actions =
        [
            new ARemoveCharacter
            {
                deck = ch.deckType!.Value,
            },
            new AAddCharacter()
            {
                deck = Main.Cycles2Deck,
            },
            new AReplaceDeck
            {
                FromDeck = ch.deckType.Value,
                ToDeck = Main.Cycles2Deck,
            },
        ];
        actions.Reverse();
        foreach (var action in actions)
        {
            state.GetCurrentQueue().QueueImmediate(action);
        }
    }
}