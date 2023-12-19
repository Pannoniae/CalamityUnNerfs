using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

internal class FrozenTurtleShell : GlobalBuff {
    public override bool IsLoadingEnabled(Mod mod) => CalamityFly.config.FrozenTurtleShell;

    public override void Update(int type, Player player, ref int buffIndex) {
        if (type == BuffID.IceBarrier) {
            player.endurance += 0.1f;
        }
    }


    public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare) {
        if (type == BuffID.IceBarrier) {
            tip = tip.Replace("15", "25");
        }
    }
}