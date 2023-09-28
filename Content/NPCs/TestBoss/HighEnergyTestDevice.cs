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
            NPC.EncourageDespawn(10);

        }

        public override void OnSpawn(IEntitySource source)
        {
            bossPhase = 1;
            base.OnSpawn(source);
        }

        public override void AI()
        {
            AI_TestBoss_High_Energy_Test_Device();
        }

        private void AI_TestBoss_High_Energy_Test_Device()
        {
            if (bossPhase == 0)
            {
                NPC.velocity = new Vector2(0f, 0.4f);
            }
        }





    }
}
