using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WeDoALittleTrolling.Common.Configs
{
    public class WDALTServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;


        [Header("Items")]

        [DefaultValue(false)]
        [ReloadRequired]
        public bool NoWingsChallenge;

        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableFishronSkipNerf;
    }
}
