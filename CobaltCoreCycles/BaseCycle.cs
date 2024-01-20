using CobaltCoreCycles.Artifacts;

namespace CobaltCoreCycles;

[Serializable]
public abstract class BaseCycle : Card
{
    public int Cycle;
    public bool IsDoubled;
    protected abstract int MaxCycles { get; }
    protected Spr[]? Sprites;

    protected abstract CardData CardData { get; }

    public override void AfterWasPlayed(State state, Combat c)
    {
        var artifacts = new HashSet<Type>(state.artifacts.Select(artifact => artifact.GetType()));
        if (artifacts.Contains(typeof(DoubleCycle)) && !IsDoubled)
        {
            IsDoubled = true;
            return;
        }

        Cycle = (Cycle + 1) % MaxCycles;
        IsDoubled = false;
    }

    protected virtual List<CardAction> ActionsA(State s, Combat c)
    {
        return new List<CardAction>();
    }

    protected virtual List<CardAction> ActionsB(State s, Combat c)
    {
        return new List<CardAction>();
    }

    protected virtual List<CardAction> ActionsC(State s, Combat c)
    {
        return new List<CardAction>();
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        var list = new List<CardAction>();
        var actionGetters = new[]
        {
            ActionsA,
            ActionsB,
            ActionsC,
        };
        for (var i = 0; i < MaxCycles; i++)
        {
            var actions = actionGetters[i](s, c);
            for (var j = 0; j < actions.Count; j++)
            {
                if (actions[j] is AAttack attack) attack.damage = GetDmg(s, attack.damage);
                actions[j].disabled = Cycle != i;
            }

            list.AddRange(actions);
        }

        return list;
    }

    public override CardData GetData(State state)
    {
        var data = CardData;
        data.art = Sprites?[Cycle];
        return data;
    }
}