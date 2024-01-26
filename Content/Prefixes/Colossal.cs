/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2024 LukasV-Coding

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

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using WeDoALittleTrolling.Content.Projectiles;
using WeDoALittleTrolling.Content.Items.Weapons;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using System.Linq;

namespace WeDoALittleTrolling.Content.Prefixes
{
    public class Colossal : ModPrefix
    {
        public const double scalingFactor = 2.0;
        public static readonly double sqrt = Math.Sqrt(scalingFactor);
        public static readonly int[] CompatibleItemIDs =
        {
            //Ore/Wood Swords
            ItemID.WoodenSword,
            ItemID.BorealWoodSword,
            ItemID.PalmWoodSword,
            ItemID.EbonwoodSword,
            ItemID.ShadewoodSword,
            ItemID.AshWoodSword,
            ItemID.PearlwoodSword,
            ItemID.RichMahoganySword,
            ItemID.CactusSword,
            ItemID.CopperShortsword,
            ItemID.TinShortsword,
            ItemID.IronShortsword,
            ItemID.LeadShortsword,
            ItemID.SilverShortsword,
            ItemID.TungstenShortsword,
            ItemID.GoldShortsword,
            ItemID.PlatinumShortsword,
            ItemID.CopperBroadsword,
            ItemID.TinBroadsword,
            ItemID.IronBroadsword,
            ItemID.LeadBroadsword,
            ItemID.SilverBroadsword,
            ItemID.TungstenBroadsword,
            ItemID.GoldBroadsword,
            ItemID.PlatinumBroadsword,
            ItemID.CobaltSword,
            ItemID.PalladiumSword,
            ItemID.MythrilSword,
            ItemID.OrichalcumSword,
            ItemID.AdamantiteSword,
            ItemID.TitaniumSword,
            //Unique Weapons
            ItemID.ZombieArm,
            ItemID.BladedGlove,
            ItemID.Flymeal,
            ItemID.AntlionClaw, //Mandible Blade
            ItemID.StylistKilLaKillScissorsIWish, //Stylish Scissors
            ItemID.Gladius,
            ItemID.BoneSword,
            ItemID.BatBat,
            ItemID.TentacleSpike,
            ItemID.Katana,
            ItemID.LightsBane,
            ItemID.BloodButcherer,
            ItemID.Muramasa,
            ItemID.Starfury,
            ItemID.DyeTradersScimitar, //Exotic Scimitar
            ItemID.BluePhaseblade,
            ItemID.RedPhaseblade,
            ItemID.GreenPhaseblade,
            ItemID.PurplePhaseblade,
            ItemID.WhitePhaseblade,
            ItemID.YellowPhaseblade,
            ItemID.OrangePhaseblade,
            ItemID.BluePhasesaber,
            ItemID.RedPhasesaber,
            ItemID.GreenPhasesaber,
            ItemID.PurplePhasesaber,
            ItemID.WhitePhasesaber,
            ItemID.YellowPhasesaber,
            ItemID.OrangePhasesaber,
            ItemID.CandyCaneSword,
            ItemID.PurpleClubberfish,
            ItemID.BeeKeeper,
            ItemID.FalconBlade,
            ItemID.BladeofGrass,
            ItemID.FieryGreatsword,
            ItemID.NightsEdge,
            ItemID.EnchantedSword,
            ItemID.BeamSword,
            ItemID.IceSickle,
            ItemID.DeathSickle,
            ItemID.ChlorophyteClaymore,
            ItemID.ChainGuillotines,
            ItemID.TrueExcalibur,
            ItemID.TrueNightsEdge,
            ItemID.TerraBlade,
            ItemID.TaxCollectorsStickOfDoom,
            ItemID.SlapHand,
            ItemID.DD2SquireDemonSword,
            ItemID.BreakerBlade,
            ItemID.Cutlass,
            ItemID.FetidBaghnakhs,
            ItemID.HamBat,
            ItemID.Excalibur,
            ItemID.WaffleIron,
            ItemID.PsychoKnife,
            ItemID.Keybrand,
            ItemID.TheHorsemansBlade,
            ItemID.ChristmasTreeSword,
            ItemID.Seedler,
            ItemID.InfluxWaver,
            ItemID.Meowmere,
            ItemID.DD2SquireBetsySword,
            ItemID.StarWrath,
            ItemID.PiercingStarlight,
            ItemID.DayBreak,
            ItemID.SolarEruption,
            ItemID.FlyingKnife,
            ItemID.PossessedHatchet,
            ItemID.PaladinsHammer,
            ItemID.Terragrim,
            ItemID.Arkhalis,
            ItemID.VampireKnives,
            ItemID.ScourgeoftheCorruptor,
            ModContent.ItemType<HallowedDisintegrationBlade>(),
            ModContent.ItemType<LittleBlue>()
        };
        public static readonly int[] CompatibleProjectileIDs =
        {
            ProjectileID.EnchantedBeam,
            ProjectileID.SwordBeam,
            ProjectileID.ChlorophyteOrb,
            ProjectileID.IceSickle,
            ProjectileID.DeathSickle,
            ProjectileID.ChainGuillotine,
            ProjectileID.TentacleSpike,
            ProjectileID.LightsBane,
            ProjectileID.BloodButcherer,
            ProjectileID.Muramasa,
            ProjectileID.Starfury,
            ProjectileID.Bee,
            ProjectileID.BladeOfGrass,
            ProjectileID.Waffle,
            ProjectileID.FlamingJack,
            ProjectileID.OrnamentFriendly,
            ProjectileID.OrnamentStar,
            ProjectileID.SeedlerNut,
            ProjectileID.SeedlerThorn,
            ProjectileID.InfluxWaver,
            ProjectileID.Meowmere,
            ProjectileID.DD2SquireSonicBoom,
            ProjectileID.StarWrath,
            ProjectileID.PiercingStarlight,
            ProjectileID.Daybreak,
            ProjectileID.SolarWhipSword,
            ProjectileID.FinalFractal,
            ProjectileID.FlyingKnife,
            ProjectileID.PossessedHatchet,
            ProjectileID.PaladinsHammerFriendly,
            ProjectileID.Terragrim,
            ProjectileID.Arkhalis,
            ProjectileID.VampireKnife,
            ProjectileID.VampireHeal,
            ProjectileID.EatersBite,
            ProjectileID.TinyEater,
            ProjectileID.CrystalStorm,
            ProjectileID.Typhoon,
            ProjectileID.Electrosphere
        };
        public static readonly int[] ShortswordCompatibleProjectileIDs =
        {
            ProjectileID.CopperShortswordStab,
            ProjectileID.TinShortswordStab,
            ProjectileID.IronShortswordStab,
            ProjectileID.LeadShortswordStab,
            ProjectileID.SilverShortswordStab,
            ProjectileID.TungstenShortswordStab,
            ProjectileID.GoldShortswordStab,
            ProjectileID.PlatinumShortswordStab,
            ProjectileID.GladiusStab
        };
        public static readonly int[] SpeedupProjectileIDs =
        {
            ProjectileID.EnchantedBeam,
            ProjectileID.SwordBeam,
            ProjectileID.IceSickle,
            ProjectileID.DeathSickle,
            ProjectileID.ChlorophyteOrb,
            ProjectileID.TrueNightsEdge,
            ProjectileID.Waffle,
            ProjectileID.OrnamentFriendly,
            ProjectileID.OrnamentStar,
            ProjectileID.SeedlerNut,
            ProjectileID.SeedlerThorn,
            ProjectileID.InfluxWaver,
            ProjectileID.Meowmere,
            ProjectileID.DD2SquireSonicBoom,
            ProjectileID.StarWrath,
            ProjectileID.Starfury,
            ProjectileID.Daybreak,
            ProjectileID.PossessedHatchet,
            ProjectileID.PaladinsHammerFriendly,
            ProjectileID.VampireKnife,
            ProjectileID.VampireHeal,
            ProjectileID.EatersBite,
            ProjectileID.TinyEater,
            ProjectileID.CrystalStorm
        };

        public override PrefixCategory Category => PrefixCategory.AnyWeapon;
        public override float RollChance(Item item)
        {
            return 1.0f;
        }

        public override bool CanRoll(Item item)
        {
            if(CompatibleItemIDs.Contains(item.type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            scaleMult *= 2.0f;
            useTimeMult *= 1.25f;
            damageMult *= 1.40f;
            knockbackMult *= 1.25f;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1.0f;
        }

        public override void Apply(Item item)
        {
            
        }
    }
}