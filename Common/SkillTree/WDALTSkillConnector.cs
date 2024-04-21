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

using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace WeDoALittleTrolling.Common.SkillTree
{
    public class WDALTSkillConnector
    {
        public int type;
        public int[] dependencies;
        public Asset<Texture2D> enabledTexture;
        public Asset<Texture2D> disabledTexture;
        public int textureWidth;
        public int textureHeight;

        public WDALTSkillConnector(int type, int[] dependencies, Asset<Texture2D> enabledTexture, Asset<Texture2D> disabledTexture, int textureWidth, int textureHeight)
        {
            this.type = type;
            this.dependencies = dependencies;
            this.enabledTexture = enabledTexture;
            this.disabledTexture = disabledTexture;
            this.textureWidth = textureWidth;
            this.textureHeight = textureHeight;
        }
    }
}
