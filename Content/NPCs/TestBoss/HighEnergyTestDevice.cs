using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Utilities;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Content.Buffs;
using Terraria.Audio;
using WeDoALittleTrolling.Common.Utilities;
using Terraria.DataStructures;

namespace WeDoALittleTrolling.Content.NPCs.TestBoss
{
    public class HighEnergyTestDevice : ModNPC
    {

        private int bossPhase = 0;
        public override void SetDefaults()
        {
            NPC.width = 246;
            NPC.height = 246;
            NPC.damage = 14;
            NPC.defense = 20;
            NPC.lifeMax = 250000;
            NPC.HitSound = SoundID.NPCHit53;
            NPC.DeathSound = SoundID.NPCDeath56;
            NPC.value = 10000000f;
            NPC.knockBackResist = 0.0f;
            NPC.aiStyle = -1;
            NPC.noTileCollide = true;
            Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/TestBossTheme");
            NPC.noGravity = true;

        }

        public override void OnSpawn(IEntitySource source)
        {
            bossPhase = 0;
            base.OnSpawn(source);
        }

        public override void AI()
        {
            AI_TestBoss_High_Energy_Test_Device();
        }

        private void AI_TestBoss_High_Energy_Test_Device()
        {
            NPC.TargetClosest();
            Player target = Main.player[NPC.target];
            if(target != null)
            {
                if (bossPhase == 0)
                {
                    NPC.velocity = new Vector2(target.Center.X - NPC.Center.X, target.Center.Y - NPC.Center.Y);
                    NPC.velocity.Normalize();
                    NPC.velocity *= 4f;
                } 
            }
        }
    }
}
