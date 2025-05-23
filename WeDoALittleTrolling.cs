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

using System.IO;
using log4net;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.ModSystems;

namespace WeDoALittleTrolling
{
    public class WeDoALittleTrolling : Mod
    {
        public const string ASSET_PATH = "WeDoALittleTrolling/Assets/";
        internal static WDALTNetworkingSystem networkingSystem = new WDALTNetworkingSystem();
        public static ILog logger;
        public static Mod instance;
        
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            networkingSystem.HandlePacket(reader, whoAmI, this);
            base.HandlePacket(reader, whoAmI);
        }

        public override void Load()
        {
            logger = this.Logger;
            instance = this;
            base.Load();
        }
    }
}
