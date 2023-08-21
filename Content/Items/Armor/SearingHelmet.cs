using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Utilities;
using WeDoALittleTrolling.Content.Items.Material;

namespace WeDoALittleTrolling.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class SearingHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 4);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 20;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<SearingBreastplate>() && legs.type == ModContent.ItemType<SearingLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.GetModPlayer<WDALTPlayerUtil>().searingSetBonus = true;
            player.setBonus = "Attackers deal 30% reduced damage and loose life\nGrants immunity to Searing Inferno\nIncreases defense effectiveness and attack damage\nby 1% for every 4 defense you have\nCurrent bonus: "+player.GetModPlayer<WDALTPlayerUtil>().searingSetBonusValue+"%";
        }

        public override void AddRecipes()
        {
            CreateRecipe()
              .AddTile(TileID.MythrilAnvil)
              .AddIngredient(ItemID.TurtleHelmet, 1)
              .AddIngredient(ModContent.ItemType<HellishFossil>(), 8)
              .Register();
        }
    }
}
