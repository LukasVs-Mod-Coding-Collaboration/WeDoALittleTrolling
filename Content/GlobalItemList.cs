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
            if (item.type == ItemID.IceBow) 
            {
                item.damage = 50;
            }
            if (item.type == ItemID.FlintlockPistol) 
            {
                item.damage = 15;
            }
            if (item.type == ItemID.FlareGun)
            {
                item.damage = 8;
                item.crit = 4;
            }
            if (item.type == ItemID.ChristmasTreeSword)
            {
                item.damage = 95;
            }
            if (item.type == ItemID.ZapinatorOrange)
            {
                item.damage = 80;
            }
            if (item.type == ItemID.ZapinatorGray)
            {
                item.damage = 36;
            }
            if (item.type == ItemID.LastPrism)
            {
                item.mana = 8;
                item.damage = 175;
            }
            if (item.type == ItemID.ClingerStaff)
            {
                item.damage = 60;
            }
            if (item.type == ItemID.RazorbladeTyphoon)
            {
                item.useTime = 32;
                item.useAnimation = 32;
            }
            if (item.type == ItemID.OpticStaff)
            {
                item.damage = 40;
            }
            if (item.type == ItemID.MoonlordTurretStaff)
            {
                item.damage = 100;
            }
            if (item.type == ItemID.RainbowCrystalStaff)
            {
                item.damage = 150;
            }
            if (item.type == ItemID.StardustHelmet)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.StardustBreastplate)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.StardustLeggings)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.NebulaHelmet)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.NebulaBreastplate)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.NebulaLeggings)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.TikiMask)
            {
                item.defense += 1;
            }
            if (item.type == ItemID.TikiShirt)
            {
                item.defense += 1;
            }
            if (item.type == ItemID.TikiPants)
            {
                item.defense += 1;
            }
            if (item.type == ItemID.ElectrosphereLauncher)
            {
                item.autoReuse = true;
            }


        }

    }
}
