using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using System.Collections.Generic;

namespace WeDoALittleTrolling.Content.Prefixes
{

    public class Supercritical : ModPrefix
    {

        public override PrefixCategory Category => PrefixCategory.Magic;
        public override float RollChance(Item item)
        {
            return 1.0f;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            useTimeMult *= 1.25f;
            manaMult *= 2.0f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.05f;
        }

        public override void Apply(Item item)
        {
            //
        }

        public LocalizedText AdditionalTooltip => this.GetLocalization(nameof(AdditionalTooltip));

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            yield return new TooltipLine(Mod, "PrefixWeaponLeechingDescription", AdditionalTooltip.Value) {
				IsModifier = true,
			};
        }

        public override void SetStaticDefaults() {
            _ = AdditionalTooltip;
        }
    }
}