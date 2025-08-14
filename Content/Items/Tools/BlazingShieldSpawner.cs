using Terraria;
using Terraria.Utilities;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Content.NPCs;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Common.ModSystems;

namespace WeDoALittleTrolling.Content.Items.Tools
{
    internal class BlazingShieldSpawner : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;

            Item.consumable = false;

            Item.value = Item.sellPrice(platinum: 1);
            Item.maxStack = 1;

            Item.autoReuse = false;
            Item.useTime = 32;
            Item.useAnimation = 32;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTurn = false;
            Item.UseSound = SoundID.Item1;

            Item.rare = ItemRarityID.Red;
            Item.scale = 1.0f;

            //Item.shoot = ModContent.ProjectileType<BlazingShield>();
        }

        /*public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<BlazingShield>(), damage, knockback, player.whoAmI);
        }*/

        public override void UseAnimation(Player player)
        {
            if (Main.netMode == NetmodeID.SinglePlayer && player.whoAmI == Main.myPlayer)
            {
                NPC shield = NPC.NewNPCDirect(player.GetSource_FromThis(), (int)Math.Round((double)player.Center.X), (int)Math.Round((double)player.Center.Y), ModContent.NPCType<BlazingShieldNPC>());
                shield.GetGlobalNPC<WDALTNPCUtil>().BlazingShieldOwnerIndex = Main.myPlayer;
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
            {
                ModPacket spawnBlazingShieldPacket = Mod.GetPacket();
                spawnBlazingShieldPacket.Write(WDALTPacketTypeID.spawnBlazingShield);
                spawnBlazingShieldPacket.Write((int)Main.myPlayer);
                spawnBlazingShieldPacket.Send();
            }
            base.UseAnimation(player);
        }

        public override bool AltFunctionUse(Player player)
        {
            if (Main.netMode == NetmodeID.SinglePlayer && player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].GetGlobalNPC<WDALTNPCUtil>().BlazingShieldOwnerIndex == Main.myPlayer)
                    {
                        Main.npc[i].StrikeInstantKill();
                    }
                }
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
            {
                ModPacket clearBlazingShieldPacket = Mod.GetPacket();
                clearBlazingShieldPacket.Write(WDALTPacketTypeID.clearBlazingShield);
                clearBlazingShieldPacket.Write((int)Main.myPlayer);
                clearBlazingShieldPacket.Send();
            }
            return base.AltFunctionUse(player);
        }
    }
}