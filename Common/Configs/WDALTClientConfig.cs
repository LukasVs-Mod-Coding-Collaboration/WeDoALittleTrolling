using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WeDoALittleTrolling.Common.Configs
{
    public class WDALTClientConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;


        [Header("Movement")]
        [DefaultValue(false)]
        public bool DisableDoubleTapDashing;
    }
}
