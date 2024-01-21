namespace CobaltCoreCycles.Artifacts;

[ArtifactMeta(pools = [ArtifactPool.Common])]
public class Bonus : Artifact
{
    public bool Doubled;
    public int Cycle;

    public override int? GetDisplayNumber(State s)
    {
        return Cycle;
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        if (combat.turn != 1) return;
        
        var doubleCycle = state.artifacts.Any(a => a is DoubleCycle);

        if (Doubled || !doubleCycle)
        {
            Cycle = (Cycle + 1) % 3;
            Doubled = false;
        }
        else
        {
            Doubled = true;
        }

        switch (Cycle)
        {
            case 0:
                state.GetCurrentQueue()
                    .QueueImmediate(
                        new AStatus
                        {
                            status = Status.shield,
                            statusAmount = 3,
                            targetPlayer = true,
                            artifactPulse = Key(),
                        }
                    );
                state.GetCurrentQueue()
                    .QueueImmediate(
                        new AStatus
                        {
                            status = Status.maxShield,
                            statusAmount = 3,
                            targetPlayer = true,
                            artifactPulse = Key(),
                        }
                    );
                break;
            case 1:
                state.GetCurrentQueue()
                    .QueueImmediate(
                        new AStatus
                        {
                            status = Status.evade,
                            statusAmount = 3,
                            targetPlayer = true,
                            artifactPulse = Key(),
                        }
                    );
                break;
            default:
                state.GetCurrentQueue()
                    .QueueImmediate(
                        new ADrawCard
                        {
                            count = 3,
                            artifactPulse = Key(),
                        }
                    );
                break;
        }
    }
}