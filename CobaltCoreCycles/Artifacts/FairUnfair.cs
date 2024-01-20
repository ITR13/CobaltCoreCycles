namespace CobaltCoreCycles.Artifacts;

[ArtifactMeta(pools = [ArtifactPool.Boss])]
public class FairUnfair : Artifact
{
    public int Cycle;

    public override void OnCombatStart(State state, Combat combat)
    {
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
                }
            );
    }
}