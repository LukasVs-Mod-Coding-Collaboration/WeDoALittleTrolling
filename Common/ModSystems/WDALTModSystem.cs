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

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using WeDoALittleTrolling.Common.SkillTree;
using WeDoALittleTrolling.Content.Items;
using WeDoALittleTrolling.Content.Items.Accessories;
using WeDoALittleTrolling.Content.NPCs;
using WeDoALittleTrolling.Content.Prefixes;
using WeDoALittleTrolling.Content.Tiles;
using WeDoALittleTrolling.Content.Recipes;

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal class WDALTModSystem : ModSystem
    {

        public static bool isCalamityModPresent = false;
        public static bool isThoriumModPresent = false;
        public static bool isConsolariaModPresent = false;
        public static readonly string calamityModName = "CalamityMod";
        public static readonly string thoriumModName = "ThoriumMod";
        public static readonly string consolariaModName = "Consolaria";
        public static bool MCIDIntegrity;
        
        public override void OnModLoad()
        {
            if(ModLoader.HasMod(consolariaModName))
            {
                isConsolariaModPresent = true;
                WeDoALittleTrolling.logger.Info("Consolaria Mod detected.");
                WeDoALittleTrolling.logger.Info("Vampire Miners can now drop Yellow Crystals.");
            }
            if(ModLoader.HasMod(thoriumModName))
            {
                isThoriumModPresent = true;
                WeDoALittleTrolling.logger.Info("Thorium Mod detected.");
                WeDoALittleTrolling.logger.Info("WeDoALittleTrolling has advanced compatibility features for Thorium Mod.");
                WeDoALittleTrolling.logger.Info("For a list of changes this Mod does to Thorium Mod, please see this Mod's description.");
                WeDoALittleTrolling.logger.Info("If you report any bugs to the Thorium Mod developers, make sure this Mod is disabled first.");
            }
            if(ModLoader.HasMod(calamityModName))
            {
                isCalamityModPresent = true;
                WeDoALittleTrolling.logger.Warn("Calamity Mod detected.");
                WeDoALittleTrolling.logger.Warn("WeDoALittleTrolling is not intended to be used together with Calamity Mod.");
                WeDoALittleTrolling.logger.Warn("Most rebalancing features of WeDoALittleTrolling are not compatible with Calamity Mod.");
                WeDoALittleTrolling.logger.Warn("Disabling most rebalancing features of WeDoALittleTrolling to ensure the game stays playable...");
            }
            MCIDIntegrity = WDALTModContentID.SetContentIDs();
            WDALTIntermediateLanguageEditing.RegisterILHooks();
            RegisterHooks();
            if (!Main.dedServ && Main.netMode != NetmodeID.Server)
            {
                WDALTSkillTreeSystem.UIInit();
            }
            base.OnModLoad();
        }

        public override void OnModUnload()
        {
            WDALTIntermediateLanguageEditing.UnregisterILHooks();
            UnregisterHooks();
            if (!Main.dedServ && Main.netMode != NetmodeID.Server)
            {
                WDALTSkillTreeSystem.UIDestroy();
            }
            base.OnModUnload();
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (!Main.dedServ && Main.netMode != NetmodeID.Server)
            {
                WDALTSkillTreeSystem.UpdateUI(gameTime);
            }
            base.UpdateUI(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (!Main.dedServ && Main.netMode != NetmodeID.Server)
            {
                WDALTSkillTreeSystem.ModifyInterfaceLayers(layers);
            }
            base.ModifyInterfaceLayers(layers);
        }

        public static void RegisterHooks()
        {
            WDALTSceneMetrics.RegisterHooks();
            UnionMirror.RegisterHooks();
            WDALTSeedSystem.RegisterHooks();
            NightmarePhantom.RegisterHooks();
            GlobalTiles.RegisterHooks();
            Accelerated.RegisterHooks();
            GlobalItemList.RegisterHooks();
            WDALTImmunitySystem.RegisterHooks();
        }

        public static void UnregisterHooks()
        {
            WDALTSceneMetrics.UnregisterHooks();
            UnionMirror.UnregisterHooks();
            WDALTSeedSystem.UnregisterHooks();
            NightmarePhantom.UnregisterHooks();
            GlobalTiles.UnregisterHooks();
            Accelerated.UnregisterHooks();
            GlobalItemList.UnregisterHooks();
            WDALTImmunitySystem.UnregisterHooks();
        }

        public override void PreUpdatePlayers()
        {
            Main.anglerWhoFinishedToday.Clear();
            Main.anglerQuestFinished = false;
        }

        public static bool TryGetCalamityMod(out Mod calamityMod)
        {
            if(ModLoader.TryGetMod(calamityModName, out Mod mod))
            {
                calamityMod = mod;
                return true;
            }
            else
            {
                calamityMod = null;
                return false;
            }
        }

        public static bool TryGetThoriumMod(out Mod thoriumMod)
        {
            if(ModLoader.TryGetMod(thoriumModName, out Mod mod))
            {
                thoriumMod = mod;
                return true;
            }
            else
            {
                thoriumMod = null;
                return false;
            }
        }

        public static bool TryGetConsolariaMod(out Mod consolariaMod)
        {
            if(ModLoader.TryGetMod(consolariaModName, out Mod mod))
            {
                consolariaMod = mod;
                return true;
            }
            else
            {
                consolariaMod = null;
                return false;
            }
        }

        public override void OnWorldLoad()
        {
            WDALTSeedSystem.InitWorldVariables();
            base.OnWorldLoad();
        }

        public override void OnWorldUnload()
        {
            WDALTSeedSystem.ResetWorldVariables();
            base.OnWorldUnload();
        }

        public override void AddRecipes()
        {
            ProgressionRecipes.AddRecipes();
            base.AddRecipes();
        }

        public override void PostAddRecipes()
        {
            ProgressionRecipes.PostAddRecipes();
            base.PostAddRecipes();
        }
    }
}

