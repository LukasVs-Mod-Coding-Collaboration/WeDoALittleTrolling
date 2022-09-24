using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace WeDoALittleTrolling.Content.Items
{
    internal class GlobalItemList : GlobalItem
    {

        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.Phantasm) 
            {
                item.damage = 70;
            }
            if (item.type == ItemID.FetidBaghnakhs) 
            {
                item.damage = 70;
                item.useTime = 7;
                item.useAnimation = 7;
            }
            if (item.type == ItemID.DaedalusStormbow) 
            {
                item.damage = 48;
            }
            if (item.type == ItemID.CactusBreastplate) 
            {
                item.defense = 2;
            }
            if (item.type == ItemID.CactusSword) 
            {
                item.damage = 9;
                item.useTime = 25;
                item.useAnimation = 25;
                item.knockBack = 5f;
            }
            if (item.type == ItemID.EnchantedSword) 
            {
                item.damage = 24;
                item.useTime = 18;
                item.useAnimation = 18;
                item.knockBack = 5.25f;
            }
            if (item.type == ItemID.FalconBlade) 
            {
                item.damage = 30;
                item.useTime = 15;
                item.useAnimation = 15;
            }
            if (item.type == ItemID.BeeKeeper) 
            {
                item.damage = 26;
                item.useTime = 20;
                item.useAnimation = 20;
                item.knockBack = 5.3f;
                item.autoReuse = true;
            }
            if (item.type == ItemID.BeamSword) 
            {
                item.useTime = 15;
                item.useAnimation = 15;
            }
            if (item.type == ItemID.BeesKnees) 
            {
                item.damage = 26;
                item.useTime = 23;
                item.useAnimation = 23;
            }
            if (item.type == ItemID.ClockworkAssaultRifle) 
            {
                item.damage = 19;
            }
            if (item.type == ItemID.Marrow) 
            {
                item.damage = 70;
                item.useTime = 17;
                item.useAnimation = 17;
                item.crit = 7;
                item.autoReuse = true;
            }

        }

    }
}
