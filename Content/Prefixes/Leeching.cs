using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;

namespace WeDoALittleTrolling.Content.Prefixes
{
    public class Leeching : ModPrefix
    {
        public override PrefixCategory Category => PrefixCategory.Melee;
        public override float RollChance(Item item)
        {
            return 1f;
        }

        public override bool CanRoll(Item item)
        {
            return true;
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult *= 0.8f;
        }

        
        public override void ModifyValue(ref float valueMult)
        {
            valueMult *= 1f + 0.05f;
        }

        public override void Apply(Item item)
        {

        }
    }

}
