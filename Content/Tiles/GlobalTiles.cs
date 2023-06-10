using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeDoALittleTrolling.Content.Tiles
{
    internal class GlobalTiles : GlobalTile
    {
        public override void RightClick(int i, int j, int type)
        {
            if(type == TileID.Moondial)
            {
                Main.moondialCooldown = 0;
            }
            else if(type == TileID.Sundial)
            {
                Main.sundialCooldown = 0;
            }
            base.RightClick(i, j, type);
        }
    }
}

