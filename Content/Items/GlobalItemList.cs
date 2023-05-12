using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using WeDoALittleTrolling.Content.Prefixes;
using System;

namespace WeDoALittleTrolling.Content.Items
{
    internal class GlobalItemList : GlobalItem
    {
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if
            (
                item.prefix == ModContent.PrefixType<Leeching>() &&
                !target.friendly && 
                !target.CountsAsACritter && 
                !target.isLikeATownNPC && 
                target.type != NPCID.TargetDummy
            )
            {
                // 1 Base Heal + 5% of damage done
                int healingAmount = 1 + (int)Math.Round(damageDone * 0.05);
                if(hit.Crit)
                {
                    // Stop Sacling at ~480 Damage for Critical Hits
                    if(healingAmount > 24)
                    {
                        healingAmount = 24;
                    }
                }
                else
                {
                    // Stop Sacling at ~160 Damage for Normal Hits
                    if(healingAmount > 8)
                    {
                        healingAmount = 8;
                    }
                }
                // Having Moon Bite means the effect still works, hoever,
                // it will be 75% less effective
                // 1 Base Heal is still guaranteed
                if(player.HasBuff(BuffID.MoonLeech))
                {
                    healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.25);
                }
                player.Heal(healingAmount);
            }

            base.OnHitNPC(item, player, target, hit, damageDone);
        }
        /*
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            int healingAmount = (int)Math.Round(damageDone * 0.05);
            player.chatOverhead.NewMessage("healing amount: " + healingAmount, 60);
            if(item.prefix == ModContent.PrefixType<Leeching>())
            {
                player.chatOverhead.NewMessage("Leeching Modifier Detected!", 60);
            }
            base.OnHitNPC(item, player, target, hit, damageDone);
        }
        */


        public override bool CanUseItem(Item item, Player player)
        {
            // Anti-Poo-Block-Mechanism
            if(item.type == ItemID.PoopBlock)
            {
                PlayerDeathReason reason = new PlayerDeathReason();
                reason.SourceCustomReason = player.name + " tried to uglify the world.";
                player.KillMe(reason, 99999999999999, 0, false);
                return false;
            }
            // Anti-Landmine-Mechanism
            else if(item.type == ItemID.LandMine)
            {
                PlayerDeathReason reason = new PlayerDeathReason();
                reason.SourceCustomReason = player.name + " tried to teamtroll and had it backfire.";
                player.KillMe(reason, 99999999999999, 0, false);
                return false;
            }
            return base.CanUseItem(item, player);
        }

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
            if (item.type == ItemID.SniperRifle)
            {
                item.autoReuse = true;
            }
            if (item.type == ItemID.StarWrath)
            {
                item.damage = 250;
            }


        }

    }
}
