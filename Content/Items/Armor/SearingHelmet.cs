using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WeDoALittleTrolling.Common.Utilities;

namespace WeDoALittleTrolling.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class SearingHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;
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
            int enduranceBonus = 0;
            enduranceBonus += (1 * player.GetModPlayer<WDALTPlayerUtil>().GetAmountOfEquippedAccessoriesWithPrefixFromPlayer(PrefixID.Hard));
            enduranceBonus += (2 * player.GetModPlayer<WDALTPlayerUtil>().GetAmountOfEquippedAccessoriesWithPrefixFromPlayer(PrefixID.Guarding));
            enduranceBonus += (3 * player.GetModPlayer<WDALTPlayerUtil>().GetAmountOfEquippedAccessoriesWithPrefixFromPlayer(PrefixID.Armored));
            enduranceBonus += (4 * player.GetModPlayer<WDALTPlayerUtil>().GetAmountOfEquippedAccessoriesWithPrefixFromPlayer(PrefixID.Warding));
            float enduranceBonusFloat = ((enduranceBonus * 0.5f) * 0.01f);
            player.endurance += enduranceBonusFloat;
            player.GetDamage(DamageClass.Generic) += enduranceBonusFloat;
            player.setBonus = "Attackers deal 25% reduced damage and slowly loose life\nReduces damage taken by 0.5% per defense from accessory reforges\nIncreases damage dealt by 0.5% per defense from accessory reforges\nCurrent bonus: "+(enduranceBonus * 0.5f)+"%";
        }
    }
}
