namespace CobaltCoreCycles.Artifacts;

[ArtifactMeta(pools = [ArtifactPool.Common])]
public class PowerCycle : Artifact
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

        Cycle = (Cycle + (doubleCycle ? 2 : 1)) % 10;
        if (Cycle >= 8)
        {
            state.GetCurrentQueue()
                .QueueImmediate(
                    new AStatus
                    {
                        status = Status.powerdrive,
                        statusAmount = 1,
                        targetPlayer = true,
                    }
                );
        }
    }
}