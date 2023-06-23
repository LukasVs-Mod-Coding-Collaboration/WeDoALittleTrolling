using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace WeDoALittleTrolling.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class SearingBreastplate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 16;
        }

        public override void UpdateEquip(Player player)
        {
        }
    }
}
