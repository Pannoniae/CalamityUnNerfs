using CalamityFly.Config;
using CalamityFly.Helpers;
using Terraria.ID;

namespace CalamityFly.UnNerfs;

internal class RodOfDiscord : BaseUnNerf {
    public override bool Active(UnNerfsConfig config) => config.RodOfDiscord;

    public override void Apply() {
        base.Apply();
        UseItemHelper.Init();
        UseItemHelper.UsableItem(ItemID.RodofDiscord);
    }

    public override void Revert() {
        UseItemHelper.UnusableItem(ItemID.RodofDiscord);
        UseItemHelper.UnInit();
    }
}