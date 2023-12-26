using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.ModPlayers;
using WeDoALittleTrolling.Content.Buffs;
using WeDoALittleTrolling.Content.Items.Material;

namespace WeDoALittleTrolling.Content.NPCs
{
    public class NightmarePhantom : ModNPC
    {
        public long ticksAlive = 0;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.Ghost];
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                PortraitScale = 1f,
                Scale = 1f,
                Velocity = 1f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            NPCID.Sets.ImmuneToAllBuffs[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Ghost);
            NPC.width = 22;
            NPC.height = 44;
            NPC.HitSound = SoundID.NPCHit36;
            NPC.DeathSound = SoundID.NPCDeath39;
            NPC.value = 1600f;
            NPC.knockBackResist = 0f;
            NPC.lifeMax = 240;
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.scale = 1.5f;
            if (Main.hardMode)
            {
                NPC.damage *= 2;
                NPC.lifeMax *= 2;
                NPC.defense *= 2;
            }
            if (NPC.downedPlantBoss)
            {
                NPC.damage *= 2;
                NPC.lifeMax *= 2;
                NPC.defense *= 2;
            }
            AIType = NPCID.Ghost;
            AnimationType = NPCID.Ghost;
            Banner = Item.NPCtoBanner(NPCID.Ghost);
            BannerItem = Item.BannerToItem(Banner);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            target.GetModPlayer<WDALTPlayer>().hauntedDebuffTicksLeft = 18000;
            target.GetModPlayer<WDALTPlayer>().hauntedDebuff = true;
            target.AddBuff(ModContent.BuffType<Haunted>(), 18000, true);
            Haunted.AnimateHaunted(target);
            base.OnHitPlayer(target, hurtInfo);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange
            (
                new IBestiaryInfoElement[]
                {
                    BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                    new FlavorTextBestiaryInfoElement("Even after death, the Nightmare Phantoms will still haunt their killers in the dark...")
                }
            );
        }

        public override void AI()
        {
            AI_013_NightmarePhantom();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if
            (
                Main.dontStarveWorld &&
                (
                    spawnInfo.Player.ZoneGraveyard ||
                    (
                        !Main.IsItDay() &&
                        !spawnInfo.PlayerInTown &&
                        spawnInfo.Player.ZoneOverworldHeight &&
                        Main.moonPhase != 0 &&
                        !Main.bloodMoon
                    )
                )
            )
            {
                return 0.125f;
            }
            return base.SpawnChance(spawnInfo);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int i = 0; i < 60; i++)
            {
                int rMax = (int)NPC.width;
                double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                double angle = Main.rand.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 dustPosition = NPC.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                Vector2 dustVelocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                dustVelocity.Normalize();
                dustVelocity *= 8f;
                Dust newDust = Dust.NewDustPerfect(dustPosition, 54, dustVelocity, 0, default);
                newDust.noGravity = true;
            }
            base.HitEffect(hit);
        }

        private void AI_013_NightmarePhantom()
        {
            ticksAlive++;
            Lighting.AddLight(NPC.Center, Color.Red.ToVector3() * 0.4f);
            if(ticksAlive % 150 == 0)
            {
                if
                (
                    Main.player[NPC.target] != null &&
                    Main.player[NPC.target].active &&
                    !Main.player[NPC.target].dead
                )
                {
                    Vector2 vectorToTarget = Main.player[NPC.target].Center - NPC.Center;
                    vectorToTarget.Normalize();
                    vectorToTarget *= 8f;
                    vectorToTarget.Y *= 1.5f;
                    NPC.velocity += vectorToTarget;
                    for (int i = 0; i < 60; i++)
                    {
                        int rMax = (int)NPC.width;
                        double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                        double angle = Main.rand.NextDouble() * 2 * Math.PI;
                        int xOffset = (int)Math.Round(r * Math.Cos(angle));
                        int yOffset = (int)Math.Round(r * Math.Sin(angle));
                        Vector2 dustPosition = NPC.Center;
                        dustPosition.X += xOffset;
                        dustPosition.Y += yOffset;
                        Vector2 dustVelocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                        dustVelocity.Normalize();
                        dustVelocity *= 8f;
                        Dust newDust = Dust.NewDustPerfect(dustPosition, 182, dustVelocity, 0, default);
                        newDust.noGravity = true;
                    }
                    SoundEngine.PlaySound(SoundID.Zombie53, NPC.Center);
                }
            }
        }
    }
}
