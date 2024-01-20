using System.Reflection;
using CobaltCoreCycles.Cards.Common;
using Microsoft.Extensions.Logging;
using Nickel;

namespace CobaltCoreCycles;

public class Main : Mod
{
    private const string Cycles = "cycles";
    private readonly ILogger _logger;

    public Main(
        ILogger logger,
        Func<string, List<Type>, List<ICardEntry>> registerCards,
        Func<string, List<Type>, List<IArtifactEntry>> registerArtifacts,
        Func<string, List<Type>, List<Type>, ICharacterEntry> registerCharacter
    )
    {
        _logger = logger;
        _ = registerCards(
            Cycles,
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Card)) && !type.IsAbstract)
                .ToList()
        );
        _ = registerArtifacts(
            Cycles,
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Artifact)) && !type.IsAbstract)
                .ToList()
        );
        _ = registerCharacter(
            Cycles,
            [typeof(OneTwoShot), typeof(NoShot)],
            []
        );
    }
}