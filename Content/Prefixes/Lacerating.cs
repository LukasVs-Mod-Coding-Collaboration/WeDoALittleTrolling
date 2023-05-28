using Terraria;
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

namespace WeDoALittleTrolling.Content.Prefixes
{

    public class Lacerating : ModPrefix
    {

        public override PrefixCategory Category => PrefixCategory.Melee;
        public override float RollChance(Item item)
        {
            return 0.25f;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult *= 1.20f;
            scaleMult *= 0.8f;
            useTimeMult *= 0.65f;
            knockbackMult *= 0.0f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.05f;
        }

        public override void Apply(Item item)
        {
            //
        }
    }
}