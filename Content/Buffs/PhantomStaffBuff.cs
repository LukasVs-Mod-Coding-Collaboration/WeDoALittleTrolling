using System;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.NPCs;
using WeDoALittleTrolling.Common.Utilities;
using Terraria.GameContent;
using WeDoALittleTrolling.Content.Projectiles;

namespace WeDoALittleTrolling.Content.Buffs
{
    public class PhantomStaffBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if(player.ownedProjectileCounts[ModContent.ProjectileType<PhantomStaffProjectile>()] > 0)
            {
                player.buffTime[buffIndex] = 10;
            }
            else
            {
                player.DelBuff(buffIndex);
            }
        }
    }
}
