namespace CobaltCoreCycles.Artifacts;

[ArtifactMeta(pools = [ArtifactPool.Boss])]
public class FairUnfair : Artifact
{
    public int Cycle;

    public override int? GetDisplayNumber(State s)
    {
        return Cycle / 2;
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        if (combat.turn != 1) return;

        var doubleCycle = state.artifacts.Any(a => a is DoubleCycle);

        Cycle += doubleCycle ? 1 : 2;
        state
            .GetCurrentQueue()
            .QueueImmediate(
                new AStatus
                {
                    status = Status.powerdrive,
                    statusAmount = 2,
                    targetPlayer = (Cycle & 0b10) == 0,
                    artifactPulse = Key(),
                }
            );
    }
}