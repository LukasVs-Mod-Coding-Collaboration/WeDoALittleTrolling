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

namespace WeDoALittleTrolling.Content.NPCs
{
    public class TestNPC : ModNPC
    {

        private Player target;
        //private int targetAggro;
        //private bool hasTarget;
        //Usable later but brick the NPC apparently

        public override void SetDefaults()
        {
            NPC.width = 42;
            NPC.height = 18;
            NPC.damage = 10;
            NPC.defense = 10;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit53;
            NPC.DeathSound = SoundID.NPCDeath56;
            NPC.value = 10000000f;
            NPC.knockBackResist = 1.0f;
            NPC.aiStyle = -1;
            NPC.noTileCollide = true;
            NPC.noGravity = true;

        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
        }

        public override void AI()
        {
            TestNPCAI();
        }

        private void TestNPCAI()
        {
            for (int i = 0 ; i < Main.player.Length ; i++)
            {
                float TargetDistanceControl = Vector2.Distance(NPC.Center, Main.player[i].Center);
                TargetDistanceControl = Math.Abs(TargetDistanceControl);
                if (

                    Main.player[i] != null &&
                    !Main.player[i].dead &&
                    Main.player[i].active
                    )
                {
                    target = Main.player[i];
                }
            }

            if (target != null && !target.dead && target.active)
            {
                    NPC.velocity = new Vector2(target.Center.X - NPC.Center.X, target.Center.Y - NPC.Center.Y);
                    NPC.velocity.Normalize();
                    NPC.velocity *= 4f;
            }
            else
            {
                NPC.EncourageDespawn(10);
            }
        }
    }
}

