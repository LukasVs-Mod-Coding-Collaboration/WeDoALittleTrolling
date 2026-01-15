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
using Terraria.Utilities;
using Terraria.ModLoader;

namespace WeDoALittleTrolling.Common.Utilities
{
    internal class WDALTPowerMaskItemUtil: GlobalItem
    {
        public override bool InstancePerEntity => true;
        public bool isPowerMask;
    }
    
    internal class WDALTPowerMaskUtil : ModPlayer
    {
        public Player player;
        public bool EternalMask;
        public bool FrenziedMask;
        public bool FuriousMask;
        public bool EnlightenedMask;
        public bool SwarmingMask;
        public bool PhasingMask;
        public bool HeroicMask;
        public bool FragmentedMask;
        public bool BalancedMask;
        public static UnifiedRandom random = new UnifiedRandom();
        
        public override void Initialize()
        {
            player = this.Player;
            EternalMask = false;
            FrenziedMask = false;
            FuriousMask = false;
            EnlightenedMask = false;
            SwarmingMask = false;
            PhasingMask = false;
            HeroicMask = false;
            FragmentedMask = false;
            BalancedMask = false;
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            EternalMask = false;
            FrenziedMask = false;
            FuriousMask = false;
            EnlightenedMask = false;
            SwarmingMask = false;
            PhasingMask = false;
            HeroicMask = false;
            FragmentedMask = false;
            BalancedMask = false;
        }

        public override void ResetEffects()
        {
            EternalMask = false;
            FrenziedMask = false;
            FuriousMask = false;
            EnlightenedMask = false;
            SwarmingMask = false;
            PhasingMask = false;
            HeroicMask = false;
            FragmentedMask = false;
            BalancedMask = false;
            base.ResetEffects();
        }

        public override void PostUpdateEquips()
        {
            if(EternalMask)
            {
                player.endurance = 0;
                player.statLifeMax2 += 250;
                if(player.lifeRegen > 0)
                {
                    player.lifeRegen = (int)Math.Round(player.lifeRegen * 1.5);
                }
            }
            if (BalancedMask)
            {
                player.statLifeMax2 += 20;
                player.GetDamage(DamageClass.Generic) += 0.1f;
            }
            base.PostUpdateEquips();
        }

        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            if(EternalMask)
            {
                healValue = (int)Math.Round(healValue * 1.5);
            }
            base.GetHealLife(item, quickHeal, ref healValue);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if(EternalMask)
            {
                modifiers.SourceDamage *= 0.5f;
            }
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}
