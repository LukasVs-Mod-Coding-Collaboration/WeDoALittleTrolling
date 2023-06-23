using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WeDoALittleTrolling.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class SearingHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 16;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SearingBreastplate>() && legs.type == ModContent.ItemType<SearingLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Attackers deal 25% reduced damage and slowly loose life";
        }
    }
}
