using System;
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
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Accessories;

namespace WeDoALittleTrolling.Content.Items
{
    internal class GlobalItemList : GlobalItem
    {
        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if
            (
                modifiers.DamageType == DamageClass.Magic
            )
            {
                if (item.prefix == ModContent.PrefixType<Supercritical>())
                {
                    modifiers.CritDamage *= 2.0f;
                }
            }
            if
            (
                modifiers.DamageType == DamageClass.Summon ||
                modifiers.DamageType == DamageClass.SummonMeleeSpeed ||
                modifiers.DamageType == DamageClass.MagicSummonHybrid
            )
            {
                if(WDALTUtil.hasPlayerAcessoryEquipped(player, ModContent.ItemType<SpookyEmblem>()))
                {
                    modifiers.ArmorPenetration += (3 * player.maxMinions);
                    Random random = new Random();
                    if(random.Next(0, 100) < (3 * player.maxMinions)) //(3 x <Player Minion Slots>)% Chance
                    {
                        modifiers.SetCrit();
                    }
                }
            }
            base.ModifyHitNPC(item, player, target, ref modifiers);
        }
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if
            (
                (
                    item.prefix == ModContent.PrefixType<Leeching>() ||
                    item.prefix == ModContent.PrefixType<Siphoning>()
                ) &&
                (
                    hit.DamageType == DamageClass.Melee ||
                    hit.DamageType == DamageClass.MeleeNoSpeed ||
                    hit.DamageType == DamageClass.SummonMeleeSpeed ||
                    hit.DamageType == DamageClass.Magic
                ) &&
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
                    // Stop Sacling at ~320 Damage for Critical Hits
                    if(healingAmount > 16)
                    {
                        healingAmount = 16;
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
                // Having Moon Bite means the effect still works, however,
                // it will be 90% less effective
                // 1 Base Heal is still guaranteed
                if(player.HasBuff(BuffID.MoonLeech) && item.prefix == ModContent.PrefixType<Leeching>())
                {
                    healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.1);
                }
                // Siphoning should always be
                // 75% less effective than Leeching
                // 1 Base Heal is still guaranteed
                if(item.prefix == ModContent.PrefixType<Siphoning>())
                {
                    healingAmount = 1 + (int)Math.Round((healingAmount - 1) * 0.25);
                }
                if(item.prefix == ModContent.PrefixType<Leeching>())
                {
                    player.Heal(healingAmount);
                }
                else if(item.prefix == ModContent.PrefixType<Siphoning>())
                {
                    if(player.statMana <= (player.statManaMax2 - healingAmount))
                    {
                        player.statMana += healingAmount;
                    }
                    player.ManaEffect(healingAmount);
                }
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
            if (item.type == ItemID.ChlorophyteShotbow)
            {
                item.damage = 40;
            }
            if (item.type == ItemID.DD2PhoenixBow)
            {
                item.damage = 40;
            }
            if (item.type == ItemID.CactusBreastplate) 
            {
                item.defense += 2;
            }
            if (item.type == ItemID.CactusSword) 
            {
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
            if (item.type == ItemID.BeamSword) 
            {
                item.useTime = 15;
                item.useAnimation = 15;
            }
            if (item.type == ItemID.BeesKnees) 
            {
                item.damage = 26;
            }
            if (item.type == ItemID.ClockworkAssaultRifle) 
            {
                item.damage = 19;
            }
            if (item.type == ItemID.Marrow) 
            {
                item.damage = 64;
                item.useTime = 16;
                item.useAnimation = 16;
                item.crit = 12;
                item.ArmorPenetration = 16;
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
                item.useTime = 37;
                item.useAnimation = 37;
            }
            if (item.type == ItemID.ZapinatorGray)
            {
                item.damage = 36;
                item.useTime = 37;
                item.useAnimation = 37;
            }
            if (item.type == ItemID.LastPrism)
            {
                item.mana = 8;
            }
            if (item.type == ItemID.ClingerStaff)
            {
                item.damage = 50;
            }
            if (item.type == ItemID.RazorbladeTyphoon)
            {
                item.damage = 60;
                item.useTime = 14;
                item.useAnimation = 28;
                item.reuseDelay = 12;
                item.consumeAmmoOnFirstShotOnly = true;
            }
            if (item.type == ItemID.OpticStaff)
            {
                item.damage = 32;
            }
            if(item.type == ItemID.ScytheWhip) //Dark Harvest
            {
                item.damage = 125;
                item.useTime = 25;
                item.useAnimation = 25;
            }
            if(item.type == ItemID.MaceWhip) //Morning Star
            {
                item.damage = 180;
            }
            if (item.type == ItemID.RainbowCrystalStaff)
            {
                item.damage = 150;
            }
            if (item.type == ItemID.XenoStaff)
            {
                item.damage = 44;
            }
            if (item.type == ItemID.StardustCellStaff)
            {
                item.damage = 70;
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
                item.shopCustomPrice = 300000;
            }
            if (item.type == ItemID.TikiShirt)
            {
                item.shopCustomPrice = 300000;
            }
            if (item.type == ItemID.TikiPants)
            {
                item.shopCustomPrice = 300000;
            }
            if (item.type == ItemID.SpookyHelmet)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.SpookyBreastplate)
            {
                item.defense += 4;
            }
            if (item.type == ItemID.SpookyLeggings)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.SpectreRobe)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.BeeHeadgear)
            {
                item.defense += 1;
            }
            if (item.type == ItemID.BeeBreastplate)
            {
                item.defense += 1;
            }
            if (item.type == ItemID.BeeGreaves)
            {
                item.defense += 1;
            }
            if (item.type == ItemID.FossilHelm)
            {
                item.defense += 1;
            }
            if (item.type == ItemID.FossilShirt)
            {
                item.defense += 1;
            }
            if (item.type == ItemID.FossilPants)
            {
                item.defense += 1;
            }
            if (item.type == ItemID.ObsidianHelm)
            {
                item.defense += 1;
            }
            if (item.type == ItemID.ObsidianShirt)
            {
                item.defense += 1;
            }
            if (item.type == ItemID.ObsidianPants)
            {
                item.defense += 1;
            }
            if (item.type == ItemID.SpiderMask)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.SpiderBreastplate)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.SpiderGreaves)
            {
                item.defense += 2;
            }
            if (item.type == ItemID.AncientBattleArmorHat) //Forbidden Mask
            {
                item.defense += 2;
            }
            if (item.type == ItemID.AncientBattleArmorShirt) //Forbidden Robes
            {
                item.defense += 2;
            }
            if (item.type == ItemID.AncientBattleArmorPants) //Forbidden Treads
            {
                item.defense += 2;
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
                item.damage = 220;
            }
            if (item.type == ItemID.DayBreak)
            {
                item.useTime = 12;
                item.useAnimation = 12;
            }
            if (item.type == ItemID.IllegalGunParts)
            {
                item.shopCustomPrice = 80000;
            }
            if (item.type == ItemID.AntlionClaw) //Mandible Blade
            {
                item.damage = 20;
            }
            if (item.type == ItemID.ThunderSpear)
            {
                item.damage = 20;
            }
            if (item.type == ItemID.ThunderStaff)
            {
                item.damage = 24;
            }
            if (item.type == ItemID.JackOLanternMask)
            {
                item.material = true;
            }
            if (item.type == ItemID.SWATHelmet)
            {
                item.material = true;
            }

        }

    }
}
