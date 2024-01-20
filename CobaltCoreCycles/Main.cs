using System.Reflection;
using CobaltCoreCycles.Cards.Common;
using Microsoft.Extensions.Logging;
using Nickel;

namespace CobaltCoreCycles;

public class Main : Mod
{
    private const string Cycles = "cycles";
    private const string Cycles2 = "cycles2";
    private readonly ILogger _logger;

    public static Deck CyclesDeck { get; private set; }
    public static Deck Cycles2Deck { get; private set; }

    public Main(
        ILogger logger,
        Dictionary<string, IDeckEntry> decks, 
        Func<string, List<Type>, List<ICardEntry>> registerCards,
        Func<string, List<Type>, List<IArtifactEntry>> registerArtifacts,
        Func<string, List<Type>, List<Type>, bool, ICharacterEntry> registerCharacter
    )
    {
        _logger = logger;
        CyclesDeck = decks[Cycles].Deck;
        Cycles2Deck = decks[Cycles2].Deck;
        
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
        _ = registerCards(
            Cycles2,
            Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Card)) && !type.IsAbstract)
                .ToList()
        );
        _ = registerCharacter(
            Cycles,
            [typeof(OneTwoShot), typeof(NoShot)],
            [],
            false
        );
        _ = registerCharacter(
            Cycles2,
            [typeof(OneTwoShot), typeof(NoShot)],
            [],
            true
        );
    }
}