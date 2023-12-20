using CalamityFly.Config;
using CalamityMod;
using Terraria;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

public class RemoveCharge : GlobalItem {
    public override void SetDefaults(Item item) {
        if (UnNerfsConfig.Instance.removeCharge) {
            var cal = item.Calamity();
            cal.UsesCharge = false;
        }
    }
}