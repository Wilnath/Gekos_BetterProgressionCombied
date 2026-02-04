using SPTarkov.Server.Core.Models.Spt.Mod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gekos_server
{
    public record ModMetadata : AbstractModMetadata
    {
        public override string ModGuid { get; init; } = "Gekos_BetterProgression";
        public override string Name { get; init; } = "Gekos Better Progression";
        public override string Author { get; init; } = "DrunkGeko";
        public override List<string>? Contributors { get; init; } = new List<string>(["marbL-"]);
        public override SemanticVersioning.Version Version { get; init; } = new("1.0.0");
        public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.0");
        public override List<string>? Incompatibilities { get; init; }
        public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; }
        public override string? Url { get; init; } = "https://github.com/GionaCantarutti/Gekos_BetterProgressionCombied";
        public override bool? IsBundleMod { get; init; } = false;
        public override string? License { get; init; } = "MIT";
    }
}
