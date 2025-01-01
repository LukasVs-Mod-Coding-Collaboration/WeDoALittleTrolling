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
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.NPCs;
using WeDoALittleTrolling.Common.ModPlayers;


namespace WeDoALittleTrolling.Content.Buffs
{
    public class Haunted : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public static void AnimateHaunted(Player player)
        {
            for (int i = 0; i < 128; i++)
            {
                int rMax = (int)player.width;
                double r = rMax * Math.Sqrt(Main.rand.NextDouble());
                double angle = Main.rand.NextDouble() * 2 * Math.PI;
                int xOffset = (int)Math.Round(r * Math.Cos(angle));
                int yOffset = (int)Math.Round(r * Math.Sin(angle));
                Vector2 dustPosition = player.Center;
                dustPosition.X += xOffset;
                dustPosition.Y += yOffset;
                Vector2 dustVelocity = new Vector2((Main.rand.NextFloat() - 0.5f), (Main.rand.NextFloat() - 0.5f));
                dustVelocity = dustVelocity.SafeNormalize(Vector2.Zero);
                dustVelocity *= 16f;
                Dust newDust = Dust.NewDustPerfect(dustPosition, DustID.RedTorch, dustVelocity, 0, default);
                newDust.noGravity = true;
            }
            SoundEngine.PlaySound(SoundID.NPCHit55, player.Center);
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<WDALTPlayer>().hauntedDebuffTicksLeft > 0)
            {
                player.GetModPlayer<WDALTPlayer>().hauntedDebuffTicksLeft--;
            }
            else
            {
                player.GetModPlayer<WDALTPlayer>().hauntedDebuffTicksLeft = 0;
                player.GetModPlayer<WDALTPlayer>().hauntedDebuff = false;
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
