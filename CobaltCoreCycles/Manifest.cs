using System.Reflection;
using System.Text;
using CobaltCoreCycles.Cards.Common;
using CobaltCoreModding.Definitions;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using Microsoft.Extensions.Logging;

namespace CobaltCoreCycles;

public class Manifest
    : ICharacterManifest, IDeckManifest, ICardManifest, IArtifactManifest, ISpriteManifest, IAnimationManifest
{
    public ILogger? Logger { get; set; }
    public DirectoryInfo? ModRootFolder { get; set; }
    public IEnumerable<DependencyEntry> Dependencies => Array.Empty<DependencyEntry>();
    public DirectoryInfo? GameRootFolder { get; set; }
    public string Name => "ITR's Cycles";

    private readonly Dictionary<string, ExternalSprite> _sprites = new Dictionary<string, ExternalSprite>();
    private List<ExternalCard> _cards = new List<ExternalCard>();
    private ExternalSprite CardArtDefault => _sprites["cards.cardframe.png"];
    private ExternalSprite BorderSprite => _sprites["cards.cardframe.png"];
    private ExternalSprite CharPanel => _sprites["cards.panel.png"];

    private ExternalAnimation? _neutralAnimation, _miniAnimation;
    private ExternalDeck? _deck;

    public void LoadManifest(ISpriteRegistry artRegistry)
    {
        var spriteFolder = Path.Combine(ModRootFolder!.FullName, "Sprites");

        if (!Directory.Exists(spriteFolder))
        {
            Logger?.LogError($"Sprite folder not found: {spriteFolder}");
            return;
        }

        var pngFiles = Directory.GetFiles(spriteFolder, "*.png", SearchOption.AllDirectories);

        var sb = new StringBuilder();
        foreach (var spriteFile in pngFiles)
        {
            var fileInfo = new FileInfo(spriteFile);
            var name = $"{fileInfo.Directory?.Name}.{fileInfo.Name}".ToLower();

            var externalSprite = new ExternalSprite($"itr.cycles.{name}", fileInfo);
            _sprites.Add(name, externalSprite);
            artRegistry.RegisterArt(externalSprite);
            sb.AppendLine($"Registered sprite {name}");
        }

        Logger?.LogInformation(sb.ToString());
    }

    public void LoadManifest(IAnimationRegistry registry)
    {
        if (_deck == null) return;
        var neutralSprites = new List<ExternalSprite>();
        var miniSprites = new List<ExternalSprite>();
        for (var i = 0; _sprites.TryGetValue($"char.neutral{i:D2}.png", out var sprite); i++)
        {
            neutralSprites.Add(sprite);
        }

        for (var i = 0; _sprites.TryGetValue($"char.mini{i:D2}.png", out var sprite); i++)
        {
            miniSprites.Add(sprite);
        }

        Logger.LogError($"Neutral: {neutralSprites.Count}, Mini: {miniSprites.Count}");

        _neutralAnimation = new ExternalAnimation(
            $"itr.cycles.cycles.neutral",
            _deck,
            "neutral",
            false,
            neutralSprites
        );
        _miniAnimation = new ExternalAnimation(
            $"itr.cycles.cycles.mini",
            _deck,
            "mini",
            false,
            miniSprites
        );
        registry.RegisterAnimation(_neutralAnimation);
        registry.RegisterAnimation(_miniAnimation);
    }

    public void LoadManifest(IArtifactRegistry registry)
    {
    }

    public void LoadManifest(IDeckRegistry registry)
    {
        _deck = new ExternalDeck(
            "itr.cycles",
            System.Drawing.Color.Khaki,
            System.Drawing.Color.Black,
            CardArtDefault!,
            BorderSprite!,
            null
        );
        registry.RegisterDeck(_deck);
    }

    public void LoadManifest(ICardRegistry registry)
    {
        if (_deck == null) return;

        var sb = new StringBuilder();
        foreach (var cardType in Assembly.GetExecutingAssembly()
                     .GetTypes()
                     .Where(type => type.IsSubclassOf(typeof(Card)) && !type.IsAbstract))
        {
            var externalCard = new ExternalCard(
                $"itr.cycles.{cardType.Name}",
                cardType,
                _sprites["cards.temp.png"],
                _deck
            );

            _cards.Add(externalCard);
            registry.RegisterCard(externalCard);
            sb.AppendLine($"Registered card {cardType.Name}");
        }

        Logger?.LogInformation(sb.ToString());
    }


    public void LoadManifest(ICharacterRegistry registry)
    {
        if (_deck == null || _neutralAnimation == null || _miniAnimation == null)
        {
            return;
        }

        var cycles = new ExternalCharacter(
            "itr.cycles",
            _deck,
            CharPanel,
            new[] { typeof(OneTwoShot), typeof(NoShot) },
            Array.Empty<Type>(),
            _neutralAnimation,
            _miniAnimation
        );

        registry.RegisterCharacter(
            cycles
        );
    }
}