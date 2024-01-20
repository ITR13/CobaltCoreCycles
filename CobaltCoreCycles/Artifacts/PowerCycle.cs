namespace CobaltCoreCycles.Artifacts;

[ArtifactMeta(pools = [ArtifactPool.Common])]
public class PowerCycle : Artifact
{
    public int Cycle;
    public override void OnCombatStart(State state, Combat combat)
    {
        var doubleCycle = state.artifacts.Any(a => a is DoubleCycle);

        Cycle += 1;
        if (Cycle > (doubleCycle ? 1 : 0))
        {
            state.GetCurrentQueue().QueueImmediate(new AStatus
            {
                status = Status.powerdrive,
                statusAmount = 1,
                targetPlayer = true,
            });
        }
        if (Cycle >= (doubleCycle ? 4 : 2))
        {
            Cycle = 0;
        }
    }
}