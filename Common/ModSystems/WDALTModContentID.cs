/*
    WeDoALittleTrolling is a Terraria Mod made with tModLoader.
    Copyright (C) 2022-2023 LukasV-Coding

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

namespace WeDoALittleTrolling.Common.ModSystems
{
    internal class WDALTModContentID
    {
        public int ThoriumBoss_GTB;
        public int ThoriumBoss_QJ;
        public int ThoriumBoss_VC;
        public int ThoriumBoss_GES;
        public int ThoriumBoss_BC;
        public int ThoriumBoss_SCS;
        public int ThoriumBoss_BS_V1;
        public int ThoriumBoss_BS_V2;
        public int ThoriumBoss_FB_V1;
        public int ThoriumBoss_FB_V2;
        public int ThoriumBoss_LI_V1;
        public int ThoriumBoss_LI_V2;
        public int ThoriumBoss_FO_V1;
        public int ThoriumBoss_FO_V2;
        public int ThoriumBoss_FO_V3;
        public int ThoriumBoss_AET;
        public int ThoriumBoss_OLD;
        public int ThoriumBoss_SFF;
        public int ThoriumBoss_DE;
        public int[] InflictWreckedResistance1in1Group_ThoriumBoss = new int[(10+1)];
        public int[] InflictDevastated1in1Group_ThoriumBoss = new int[(1+1)];

        public bool SetContentIDs()
        {
            bool integrity = true;
            if(WDALTModSystem.TryGetThoriumMod(out Mod thoriumMod))
            {
                if(thoriumMod.TryFind("TheGrandThunderBird", out ModNPC GTB))
                {
                    ThoriumBoss_GTB = GTB.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("QueenJellyfish", out ModNPC QJ))
                {
                    ThoriumBoss_QJ = QJ.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("Viscount", out ModNPC VC))
                {
                    ThoriumBoss_VC = VC.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("GraniteEnergyStorm", out ModNPC GES))
                {
                    ThoriumBoss_GES = GES.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("BuriedChampion", out ModNPC BC))
                {
                    ThoriumBoss_BC = BC.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("StarScouter", out ModNPC SCS))
                {
                    ThoriumBoss_SCS = SCS.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("BoreanStrider", out ModNPC BS_V1))
                {
                    ThoriumBoss_BS_V1 = BS_V1.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("BoreanStriderPopped", out ModNPC BS_V2))
                {
                    ThoriumBoss_BS_V2 = BS_V2.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("FallenBeholder", out ModNPC FB_V1))
                {
                    ThoriumBoss_FB_V1 = FB_V1.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("FallenBeholder2", out ModNPC FB_V2))
                {
                    ThoriumBoss_FB_V2 = FB_V2.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("Lich", out ModNPC LI_V1))
                {
                    ThoriumBoss_LI_V1 = LI_V1.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("LichHeadless", out ModNPC LI_V2))
                {
                    ThoriumBoss_LI_V2 = LI_V2.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("ForgottenOne", out ModNPC FO_V1))
                {
                    ThoriumBoss_FO_V1 = FO_V1.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("ForgottenOneCracked", out ModNPC FO_V2))
                {
                    ThoriumBoss_FO_V2 = FO_V2.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("ForgottenOneReleased", out ModNPC FO_V3))
                {
                    ThoriumBoss_FO_V3 = FO_V3.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("Aquaius", out ModNPC AET))
                {
                    ThoriumBoss_AET = AET.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("Omnicide", out ModNPC OLD))
                {
                    ThoriumBoss_OLD = OLD.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("SlagFury", out ModNPC SFF))
                {
                    ThoriumBoss_SFF = SFF.Type;
                }
                else
                {
                    integrity = false;
                }
                if(thoriumMod.TryFind("DreamEater", out ModNPC DE))
                {
                    ThoriumBoss_DE = DE.Type;
                }
                else
                {
                    integrity = false;
                }
            }
            if(integrity)
            {
                InflictWreckedResistance1in1Group_ThoriumBoss[0] = ThoriumBoss_BS_V1;
                InflictWreckedResistance1in1Group_ThoriumBoss[1] = ThoriumBoss_BS_V2;
                InflictWreckedResistance1in1Group_ThoriumBoss[2] = ThoriumBoss_FB_V1;
                InflictWreckedResistance1in1Group_ThoriumBoss[3] = ThoriumBoss_FB_V2;
                InflictWreckedResistance1in1Group_ThoriumBoss[4] = ThoriumBoss_LI_V1;
                InflictWreckedResistance1in1Group_ThoriumBoss[5] = ThoriumBoss_FO_V1;
                InflictWreckedResistance1in1Group_ThoriumBoss[6] = ThoriumBoss_FO_V2;
                InflictWreckedResistance1in1Group_ThoriumBoss[7] = ThoriumBoss_FO_V3;
                InflictWreckedResistance1in1Group_ThoriumBoss[8] = ThoriumBoss_AET;
                InflictWreckedResistance1in1Group_ThoriumBoss[9] = ThoriumBoss_SFF;
                InflictWreckedResistance1in1Group_ThoriumBoss[10] = ThoriumBoss_DE;
                InflictDevastated1in1Group_ThoriumBoss[0] = ThoriumBoss_OLD;
                InflictDevastated1in1Group_ThoriumBoss[1] = ThoriumBoss_LI_V2;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
