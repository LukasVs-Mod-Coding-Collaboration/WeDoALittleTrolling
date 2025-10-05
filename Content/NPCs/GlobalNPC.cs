/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2025 LukasV-Coding

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Material;
using Microsoft.Xna.Framework;
using Terraria.Utilities;
using WeDoALittleTrolling.Common.ModSystems;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Content.Projectiles.Minions;
using WeDoALittleTrolling.Content.Items.Weapons;
using WeDoALittleTrolling.Content.Tiles;
using WeDoALittleTrolling.Common.Configs;

namespace WeDoALittleTrolling.Content.NPCs
{
    internal class GlobalNPCs : GlobalNPC
    {
        public override bool InstancePerEntity => false;
        public static UnifiedRandom random = new UnifiedRandom();

        public static readonly int[] ResistGloriousDemise50PercentGroup =
        {
            NPCID.TheDestroyer,
            NPCID.TheDestroyerBody,
            NPCID.TheDestroyerTail,
            NPCID.EaterofWorldsHead,
            NPCID.EaterofWorldsBody,
            NPCID.EaterofWorldsTail
        };

        public override void SetDefaults(NPC npc)
        {
            if (npc.type == NPCID.Bee || npc.type == NPCID.BeeSmall)
            {
                Main.npcCatchable[npc.type] = true;
                npc.catchItem = ModContent.ItemType<Consumablebee>();
                //revenge. REVENGE!!
            }
            if (npc.type == NPCID.BoundTownSlimeOld)
            {
                Main.npcCatchable[npc.type] = true;
                npc.catchItem = ItemID.Gel;
            }
        }

        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.WitchDoctor)
            {
                Condition LPCT1 = new Condition("At least 5 finished fishing quests", WDALTConditionFunctions.HasTier1FishingQuests);
                Condition LPCT2 = new Condition("At least 10 finished fishing quests", WDALTConditionFunctions.HasTier2FishingQuests);
                Condition LPCT3 = new Condition("At least 15 finished fishing quests", WDALTConditionFunctions.HasTier3FishingQuests);
                NPCShop.Entry[] entry = new NPCShop.Entry[3];
                entry[0] = new NPCShop.Entry(ItemID.FishingPotion, LPCT1);
                entry[1] = new NPCShop.Entry(ItemID.CratePotion, LPCT2);
                entry[2] = new NPCShop.Entry(ItemID.SonarPotion, LPCT3);
                shop.Add(entry);
            }
            base.ModifyShop(shop);
        }

        public override bool? CanCollideWithPlayerMeleeAttack(NPC npc, Player player, Item item, Rectangle meleeAttackHitbox)
        {
            if (npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                return false;
            }
            return base.CanCollideWithPlayerMeleeAttack(npc, player, item, meleeAttackHitbox);
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff(ModContent.BuffType<OnyxBlaze>()))
            {
                modifiers.SourceDamage *= OnyxBlaze.dmgTakenMult;
            }
        }

        public override bool PreAI(NPC npc)
        {
            if (npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                if (npc.GetGlobalNPC<WDALTNPCUtil>().VileSpitTimeLeft <= 0)
                {
                    npc.active = false;
                }
                if (Collision.SolidCollision(npc.position, npc.width + (int)npc.velocity.Length() + 2, npc.height + (int)npc.velocity.Length() + 2) && npc.velocity.Length() > 0f)
                {
                    npc.ai[0] += 1f;
                    if (npc.ai[0] > 3f)
                    {
                        npc.ai[0] = 3f;
                    }
                    npc.position += npc.netOffset;
                    npc.rotation += 0.4f * (float)npc.direction;
                    npc.position -= npc.netOffset;
                    return false;
                }
            }
            if (npc.type == NPCID.CultistBoss)
            {
                WDALTBossAIUtil.BossAI_LunaticCultistExtras(npc);
            }
            if (npc.type == NPCID.Golem)
            {
                WDALTBossAIUtil.BossAI_GolemExtras(npc);
            }
            return base.PreAI(npc);
        }

        public override void AI(NPC npc)
        {
            if (npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                npc.GetGlobalNPC<WDALTNPCUtil>().VileSpitTimeLeft--;
            }
            if
            (
                npc.type == NPCID.EaterofWorldsHead ||
                npc.type == NPCID.EaterofWorldsBody ||
                npc.type == NPCID.EaterofWorldsTail
            )
            {
                WDALTBossAIUtil.BossAI_EaterofWorldsExtras(npc, ref random);
            }
            if (npc.type == NPCID.TheDestroyerBody)
            {
                WDALTBossAIUtil.BossAI_TheDestroyerExtras(npc, ref random);
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                WDALTBossAIUtil.BossAI_SkeletronPrimeExtras(npc);
            }
            if (npc.type == NPCID.Plantera)
            {
                WDALTBossAIUtil.BossAI_PlanteraShotgun(npc, ref random);
            }
            if (npc.type == NPCID.UndeadMiner)
            {
                GlobalNPCs.UndeadMinerThrowGrenade(npc, ref random);
            }
            base.AI(npc);
        }

        public static void UndeadMinerThrowGrenade(NPC npc, ref UnifiedRandom random)
        {
            if 
            (
                npc.GetGlobalNPC<WDALTNPCUtil>().ticksAlive % 120 == 0 &&
                Main.netMode != NetmodeID.MultiplayerClient &&
                Main.player[npc.target] != null &&
                Main.player[npc.target].active &&
                !Main.player[npc.target].dead
            )
            {
                if
                (
                    Collision.CanHitLine
                    (
                        npc.position,
                        npc.width,
                        npc.height,
                        Main.player[npc.target].position,
                        Main.player[npc.target].width,
                        Main.player[npc.target].height
                    )
                )
                {
                    Vector2 vectorToTarget = Main.player[npc.target].Center - npc.Center;
                    vectorToTarget = vectorToTarget.SafeNormalize(Vector2.Zero);
                    vectorToTarget *= 6.0f;
                    vectorToTarget = vectorToTarget.RotatedBy((MathHelper.ToRadians(30) * (double)npc.direction) * (-1.0));
                    Projectile proj = Projectile.NewProjectileDirect(npc.GetSource_FromAI(), npc.Center, vectorToTarget, ProjectileID.Grenade, npc.damage, 8f);
                    proj.friendly = false;
                    proj.hostile = true;
                    proj.GetGlobalProjectile<WDALTProjectileUtil>().undeadMinerGrenade = true;
                    proj.netUpdate = true;
                }
            }
        }

        public override void PostAI(NPC npc)
        {
            if (Main.getGoodWorld)
            {
                if (npc.type == NPCID.BurningSphere && npc.dontTakeDamage && !ModContent.GetInstance<WDALTServerConfig>().DisableBurningSphereNerf)
                {
                    npc.dontTakeDamage = false;
                }
            }
            base.PostAI(npc);
        }

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (npc.type == NPCID.VileSpitEaterOfWorlds)
            {
                npc.GetGlobalNPC<WDALTNPCUtil>().VileSpitTimeLeft = 300;
                npc.netUpdate = true;
            }
        }

        public static void OnSpawnProjectile(NPC npc, Projectile projectile)
        {
            if (npc.HasBuff(ModContent.BuffType<SearingInferno>()))
            {
                projectile.damage = (int)Math.Round(projectile.damage * (1.0f - SearingInferno.damageNerfMultiplier));
                projectile.netUpdate = true;
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.type == ModContent.ProjectileType<GloriousDemiseProjectile>() && ResistGloriousDemise50PercentGroup.Contains(npc.type))
            {
                modifiers.FinalDamage *= 0.5f;
            }
            if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
            {
                if
                (
                    (
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_SFF) ||
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_AET) ||
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_OLD) ||
                        npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_DE)
                    ) &&
                    (
                        projectile.type == ModContent.ProjectileType<PhantomStaffProjectile>() ||
                        projectile.type == ModContent.ProjectileType<PhantomStaffProjectileBullet>()
                    )
                )
                {
                    modifiers.FinalDamage *= 0.75f; //The Primordials only take 75% damage from the Luminte Phantom Staff.
                }
            }
            base.ModifyHitByProjectile(npc, projectile, ref modifiers);
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (WDALTModSystem.isThoriumModPresent && WDALTModSystem.MCIDIntegrity)
            {
                if
                (
                    npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_SFF) ||
                    npc.type == WDALTModContentID.GetThoriumBossNPCID(WDALTModContentID.ThoriumBoss_DE)
                )
                {
                    npc.buffImmune[ModContent.BuffType<SearingInferno>()] = true;
                    npc.buffImmune[ModContent.BuffType<OnyxBlaze>()] = true;
                }
                else
                {
                    npc.buffImmune[ModContent.BuffType<SearingInferno>()] = false;
                    npc.buffImmune[ModContent.BuffType<OnyxBlaze>()] = false;
                }
            }
            else
            {
                npc.buffImmune[ModContent.BuffType<SearingInferno>()] = false;
                npc.buffImmune[ModContent.BuffType<OnyxBlaze>()] = false;
            }
            base.UpdateLifeRegen(npc, ref damage);
        }

        public override void ResetEffects(NPC npc)
        {
            base.ResetEffects(npc);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(ModContent.BuffType<SearingInferno>()))
            {
                drawColor.R = 255;
                drawColor.G = 191;
                drawColor.B = 0;
                int xOffset = random.Next(-(npc.width / 2), (npc.width / 2));
                int yOffset = random.Next(-(npc.height / 2), (npc.height / 2));
                Vector2 dustPosition = npc.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                int dustType = random.Next(0, 2);
                switch (dustType)
                {
                    case 0:
                        Dust newDust1 = Dust.NewDustPerfect(dustPosition, DustID.SolarFlare);
                        newDust1.noGravity = true;
                        break;
                    case 1:
                        Dust newDust2 = Dust.NewDustPerfect(dustPosition, DustID.Ash);
                        newDust2.noGravity = true;
                        break;
                    default:
                        break;
                }

            }

            if (npc.HasBuff(ModContent.BuffType<OnyxBlaze>()))
            {
                int xOffset = random.Next(-(npc.width / 2), (npc.width / 2));
                int yOffset = random.Next(-(npc.height / 2), (npc.height / 2));
                Vector2 dustPosition = npc.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                int dustType = random.Next(0, 2);
                switch (dustType)
                {
                    case 0:
                        Dust newDust1 = Dust.NewDustPerfect(dustPosition, DustID.Asphalt);
                        newDust1.noGravity = true;
                        break;
                    case 1:
                        Dust newDust2 = Dust.NewDustPerfect(dustPosition, DustID.DemonTorch);
                        newDust2.noGravity = true;
                        break;
                    default:
                        break;
                }

            }

            base.DrawEffects(npc, ref drawColor);
        }

        public static void ModifyHitPlayerWithProjectile(NPC npc, Player target, Projectile projectile, ref Player.HurtModifiers modifiers)
        {
            if (target.GetModPlayer<WDALTPlayer>().soulPoweredShield)
            {
                float distanceToTarget = Vector2.Distance(npc.Center, target.Center);
                if (distanceToTarget > 512f)
                {
                    distanceToTarget = 512f;
                }
                if (distanceToTarget < 48f)
                {
                    distanceToTarget = 48f;
                }
                float rangeOffsetFactor = ((512f - 48f) / (1f - 0.75f));
                float modifierSPS = Math.Abs(((distanceToTarget - 48f) / rangeOffsetFactor) + 0.75f);
                if (modifierSPS < 0.75f)
                {
                    modifierSPS = 0.75f;
                }
                modifiers.FinalDamage *= modifierSPS;
            }
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (npc.HasBuff(ModContent.BuffType<SearingInferno>()))
            {
                modifiers.SourceDamage *= (1.0f - SearingInferno.damageNerfMultiplier);
            }
            if (target.GetModPlayer<WDALTPlayer>().soulPoweredShield)
            {
                float distanceToTarget = Vector2.Distance(npc.Center, target.Center);
                if (distanceToTarget > 512f)
                {
                    distanceToTarget = 512f;
                }
                float rangeOffsetFactor = ((512f - 48f) / (1f - 0.83f)); //3093.333f
                float modifierSPS = Math.Abs(((distanceToTarget - 48f) / rangeOffsetFactor) + 0.83f);
                if (modifierSPS < 0.83f)
                {
                    modifierSPS = 0.83f;
                }
                modifiers.FinalDamage *= modifierSPS;
            }
            base.ModifyHitPlayer(npc, target, ref modifiers);
        }

        public static void OnHitPlayerWithProjectile(NPC npc, Player target, Projectile projectile, Player.HurtInfo info)
        {
            if (target.GetModPlayer<WDALTPlayer>().searingSetBonus)
            {
                npc.AddBuff(ModContent.BuffType<SearingInferno>(), 600, false);
            }
        }

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if (target.GetModPlayer<WDALTPlayer>().searingSetBonus)
            {
                npc.AddBuff(ModContent.BuffType<SearingInferno>(), 600, false);
            }
            if (npc.type == NPCID.Gnome)
            {
                target.GetModPlayer<WDALTPlayer>().gnomedDebuffTicksLeft = 18000;
                target.GetModPlayer<WDALTPlayer>().gnomedDebuff = true;
                target.AddBuff(ModContent.BuffType<Gnomed>(), 18000, true);
            }
            if (npc.type == NPCID.Deerclops)
            {
                target.ClearBuff(BuffID.Frozen);
                target.ClearBuff(BuffID.Slow);
                target.buffImmune[BuffID.Frozen] = true;
                target.buffImmune[BuffID.Slow] = true;
            }
            /*
            if (npc.type == NPCID.PrimeVice)
            {
                if (!ModContent.GetInstance<WDALTServerConfig>().DisableSkeletronPrimeItemStealing)
                {
                    if (!target.HeldItem.IsAir)
                    {
                        Item itemToDrop = target.HeldItem;
                        target.TryDroppingSingleItem(target.GetSource_FromThis(), itemToDrop);
                        SoundEngine.PlaySound(SoundID.Item71, target.position);
                    }
                    else
                    {
                        int j = -1;
                        if (!target.armor[0].IsAir)
                        {
                            j = 0;
                        }
                        else if (!target.armor[1].IsAir)
                        {
                            j = 1;
                        }
                        else if (!target.armor[2].IsAir)
                        {
                            j = 2;
                        }
                        if (j >= 0)
                        {
                            Item itemToDrop = target.armor[j];
                            target.TryDroppingSingleItem(target.GetSource_FromThis(), itemToDrop);
                            SoundEngine.PlaySound(SoundID.Item71, target.position);
                        }
                    }
                    SoundEngine.PlaySound(SoundID.Item71, target.position);
                }
            }
            */
            if (npc.type == NPCID.PrimeSaw)
            {
                SoundEngine.PlaySound(SoundID.Item22, target.position);
            }
        }

        public override bool PreKill(NPC npc)
        {
            if (npc.type == NPCID.QueenSlimeBoss && !NPC.downedQueenSlime)
            {
                ModContent.GetInstance<EmberfluxOreSystem>().BlessWorldWithEmberfluxOre();
            }
            return base.PreKill(npc);
        }

        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {
            foreach (IItemDropRule rule in globalLoot.Get())
            {
                if (rule is ItemDropWithConditionRule conditionRule)
                {
                    if (conditionRule.condition is Conditions.SoulOfLight)
                    {
                        conditionRule.chanceNumerator = 2;
                        conditionRule.chanceDenominator = 5;
                    }
                    if (conditionRule.condition is Conditions.SoulOfNight)
                    {
                        conditionRule.chanceNumerator = 2;
                        conditionRule.chanceDenominator = 5;
                    }
                }
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.GiantTortoise)
            {
                foreach (IItemDropRule rule in npcLoot.Get())
                {
                    if (rule is CommonDrop drop && drop.itemId == ItemID.TurtleShell)
                    {
                        drop.chanceNumerator = 1;
                        drop.chanceDenominator = 5;
                    }
                }
            }
            if (npc.type == NPCID.IceTortoise)
            {
                foreach (IItemDropRule rule in npcLoot.Get())
                {
                    if (rule is CommonDrop drop && drop.itemId == ItemID.FrozenTurtleShell)
                    {
                        drop.chanceNumerator = 1;
                        drop.chanceDenominator = 20;
                    }
                }
            }
            if (npc.type == NPCID.ChaosElemental)
            {
                int chanceNumerator = 1; // 4% chance
                int chanceDenominator = 100;
                TryModifyRodOfDiscordDropChance(npcLoot, chanceNumerator, chanceDenominator);
            }
            if (npc.type == NPCID.CultistBoss)
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 1;
                int chanceDenominator = 1;
                int itemID = ModContent.ItemType<ThrowOfTheLunatic>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            /*
            if
            (
                npc.type == NPCID.Bee ||
                npc.type == NPCID.BeeSmall
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 1; // 1 in
                int chanceDenominator = 3; // 3 chance
                int itemID = ModContent.ItemType<Consumablebee>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }
            */
            /*
            if
            (
                npc.type == NPCID.RockGolem ||
                npc.type == NPCID.UndeadMiner
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 1;
                int chanceNumerator = 1;
                int chanceDenominator = 24;
                if (npc.type == NPCID.UndeadMiner)
                {
                    chanceDenominator = 12;
                }
                int itemID = ModContent.ItemType<YellowCrystal>();
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
            }

            if (WDALTModSystem.isConsolariaModPresent && WDALTModSystem.MCIDIntegrity)
            {
                if (npc.type == WDALTModContentID.GetConsolariaNPCID(WDALTModContentID.ConsolariaNPC_VM))
                {
                    int dropAmountMin = 1;
                    int dropAmountMax = 1;
                    int chanceNumerator = 1;
                    int chanceDenominator = 12;
                    int itemID = ModContent.ItemType<YellowCrystal>();
                    CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                    npcLoot.Add(drop);
                }
            }
            */
 
            if
            (
                npc.type == NPCID.WallCreeper ||
                npc.type == NPCID.WallCreeperWall
            )
            {
                int dropAmountMin = 1;
                int dropAmountMax = 3;
                int chanceNumeratorNormal = 1;
                int chanceDenominatorNormal = 2;
                int chanceNumeratorExpert = 9;
                int chanceDenominatorExpert = 10;
                int itemID = ModContent.ItemType<WallCreeperFang>();
                DropBasedOnExpertMode drop = new DropBasedOnExpertMode
                (
                    new CommonDrop(itemID, chanceDenominatorNormal, dropAmountMin, dropAmountMax, chanceNumeratorNormal),
                    new CommonDrop(itemID, chanceDenominatorExpert, dropAmountMin, dropAmountMax, chanceNumeratorExpert)
                );
                npcLoot.Add(drop);
            }
            if
            (
                npc.type == NPCID.WallCreeper ||
                npc.type == NPCID.WallCreeperWall ||
                npc.type == NPCID.BlackRecluse ||
                npc.type == NPCID.BlackRecluseWall
            )
            {
                int dropAmountMin = 5;
                int dropAmountMax = 15;
                int chanceNumeratorNormal = 1;
                int chanceDenominatorNormal = 2;
                int chanceNumeratorExpert = 9;
                int chanceDenominatorExpert = 10;
                int itemID = ItemID.Cobweb;
                DropBasedOnExpertMode drop = new DropBasedOnExpertMode
                (
                    new CommonDrop(itemID, chanceDenominatorNormal, dropAmountMin, dropAmountMax, chanceNumeratorNormal),
                    new CommonDrop(itemID, chanceDenominatorExpert, dropAmountMin, dropAmountMax, chanceNumeratorExpert)
                );
                npcLoot.Add(drop);
            }
            if (npc.type == NPCID.RockGolem)
            {
                int dropAmountMin = 1;
                int dropAmountMax = 3;
                int chanceNumerator = 1; // 100% chance
                int chanceDenominator = 1;
                int itemID = ItemID.Geode;
                CommonDrop drop = new CommonDrop(itemID, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator);
                npcLoot.Add(drop);
                /*
                CommonDrop[] dropList =
                {
                    new CommonDrop(ItemID.Ruby, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                    new CommonDrop(ItemID.Amber, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                    new CommonDrop(ItemID.Topaz, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                    new CommonDrop(ItemID.Emerald, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                    new CommonDrop(ItemID.Diamond, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                    new CommonDrop(ItemID.Sapphire, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                    new CommonDrop(ItemID.Amethyst, chanceDenominator, dropAmountMin, dropAmountMax, chanceNumerator),
                };
                foreach(CommonDrop drop in dropList)
                {
                    npcLoot.Add(drop);
                }
                */
            }
            base.ModifyNPCLoot(npc, npcLoot);
        }

        //Modify Rod of Discord drop chance. Are you kidding me, Re-Logic???!!!

        public static void TryModifyRodOfDiscordDropChance(NPCLoot npcLoot, int newChanceNumerator, int newChanceDenominator)
        {
            try
            {
                foreach (IItemDropRule rule in npcLoot.Get())
                {
                    if (rule is LeadingConditionRule leadingConditionRule)
                    {
                        try
                        {
                            foreach (IItemDropRuleChainAttempt chainedRuleAttempt in leadingConditionRule.ChainedRules)
                            {
                                try
                                {
                                    IItemDropRule chainedRule = chainedRuleAttempt.RuleToChain;
                                    if (chainedRule is CommonDrop drop)
                                    {
                                        if (drop.itemId == ItemID.RodofDiscord)
                                        {
                                            drop.chanceNumerator = newChanceNumerator;
                                            drop.chanceDenominator = newChanceDenominator;
                                        }
                                    }
                                    if (chainedRule is DropBasedOnExpertMode expertDropRule)
                                    {
                                        if (expertDropRule.ruleForNormalMode is CommonDrop normalDrop)
                                        {
                                            if (normalDrop.itemId == ItemID.RodofDiscord)
                                            {
                                                normalDrop.chanceNumerator = newChanceNumerator;
                                                normalDrop.chanceDenominator = newChanceDenominator;
                                            }
                                        }
                                        if (expertDropRule.ruleForExpertMode is CommonDrop expertDrop)
                                        {
                                            if (expertDrop.itemId == ItemID.RodofDiscord)
                                            {
                                                expertDrop.chanceNumerator = newChanceNumerator;
                                                expertDrop.chanceDenominator = newChanceDenominator;
                                            }
                                        }
                                    }
                                }
                                catch { }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }
    }
}
