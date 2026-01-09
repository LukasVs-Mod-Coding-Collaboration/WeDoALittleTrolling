using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WeDoALittleTrolling.Common.Configs
{
    public class WDALTServerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;


        [Header("Chat")]

        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableCustomDeathMessages;

        [Header("Items")]

        [DefaultValue(false)]
        [ReloadRequired]
        public bool NoWingsChallenge;

        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableFishronSkipNerf;

        [Header("Enemies")]

        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableBurningSphereNerf;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableTheDestroyerExtraAI;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableSkeletronPrimeExtraAI;
        /*
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableSkeletronPrimeItemStealing;
        */
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisablePlanteraExtraAI;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableGolemExtraAI;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableEaterOfWorldsExtraAI;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableLunaticCultistExtraAI;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableDukeFishronExtraAI;

        [Header("Combat")]

        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableSmartPvP;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableBossImmunityPatch;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableForTheWorthyDefenseEffectivenessIncrease;

        [Header("CustomSeeds")]

        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableCustomTheConstant;

        [Header("Balancing")]

        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableRebalancing;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableRodOfHarmonyCooldown;
        [DefaultValue(false)]
        [ReloadRequired]
        public bool DisableCalamityCompatibilityMode;
    }
}
