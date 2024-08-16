using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace WeDoALittleTrolling.Content.Tiles
{
    public class EmbersteelBar : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileShine[Type] = 512;
            Main.tileSolid[Type] = true;
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(235, 111, 0), name);

            DustType = DustID.Lava;
            HitSound = SoundID.Tink;
            MineResist = 4f;
            MinPick = 180;
        }
    }
}
