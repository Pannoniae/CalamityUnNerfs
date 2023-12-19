using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

internal class MagicPowerPotion : GlobalBuff {
    public override bool IsLoadingEnabled(Mod mod) => CalamityFly.config.MagicPowerPotion;

    public override void Update(int type, Player player, ref int buffIndex) {
        if (type == BuffID.MagicPower) {
            player.GetDamage<MagicDamageClass>() += 0.1f;
        }
    }

    public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare) {
        if (type == BuffID.MagicPower) {
            tip = tip.Replace("10", "20");
        }
    }
}