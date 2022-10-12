using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;

namespace WeDoALittleTrolling.Content.Projectiles
{
    internal class Beamlaser1 : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("D6A2 Death Laser");
        }


        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.damage = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.knockBack = 12f;
            Projectile.aiStyle = 0;
            Projectile.light = 1f;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.hostile = false;
            Projectile.friendly = true;
            Projectile.timeLeft = 2400;
        }

        public override void AI()
        {
            int dust1 = Dust.NewDust(Projectile.Center, 1, 1, DustID.CursedTorch, 0f, 0f, 1, Color.Green, 5f);
            Main.dust[dust1].noGravity = true;
            Main.dust[dust1].velocity *= 0.1f;

            int dust2 = Dust.NewDust(Projectile.Center, 1, 1, DustID.CursedTorch, 0f, 0f, 1, Color.Green, 5f);
            Main.dust[dust2].noGravity = true;
            Main.dust[dust2].velocity *= 0.1f;

            int dust3 = Dust.NewDust(Projectile.Center, 1, 1, DustID.CursedTorch, 0f, 0f, 1, Color.Green, 5f);
            Main.dust[dust3].noGravity = true;
            Main.dust[dust3].velocity *= 0.1f;

            int dust4 = Dust.NewDust(Projectile.Center, 1, 1, DustID.CursedTorch, 0f, 0f, 1, Color.Green, 5f);
            Main.dust[dust4].noGravity = true;
            Main.dust[dust4].velocity *= 0.1f;

            int dust5 = Dust.NewDust(Projectile.Center, 1, 1, DustID.CursedTorch, 0f, 0f, 1, Color.Green, 5f);
            Main.dust[dust5].noGravity = true;
            Main.dust[dust5].velocity *= 0.1f;

            int dust6 = Dust.NewDust(Projectile.Center, 1, 1, DustID.CursedTorch, 0f, 0f, 1, Color.Green, 5f);
            Main.dust[dust6].noGravity = true;
            Main.dust[dust6].velocity *= 0.1f;

            int dust7 = Dust.NewDust(Projectile.Center, 1, 1, DustID.CursedTorch, 0f, 0f, 1, Color.Green, 5f);
            Main.dust[dust7].noGravity = true;
            Main.dust[dust7].velocity *= 0.1f;



        }


    }
}