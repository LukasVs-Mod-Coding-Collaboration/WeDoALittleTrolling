using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;

namespace WeDoALittleTrolling.Content.Prefixes
{
    public class Indecisive : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Accessory;
        public override float RollChance(Item item)
        {
            return 1.0f;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.05f;
        }

        public override void ApplyAccessoryEffects(Player player)
        {
            player.statDefense += 1;
            player.moveSpeed *= 1.01f;
            player.statManaMax2 += 10;
            player.GetDamage(DamageClass.Generic) += 0.01f;
            player.GetAttackSpeed(DamageClass.Generic) *= 1.01f;
            player.GetCritChance(DamageClass.Generic) += 1.0f;
        }
    }

}
