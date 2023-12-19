using CalamityFly.Config;
using CalamityFly.On;
using CalamityMod.CalPlayer;

namespace CalamityFly.UnNerfs;

public class DefenseDamage : BaseUnNerf {
    public override bool Active(UnNerfsConfig config) => config.DefenseDamage;

    public override void Apply() {
        base.Apply();
        OnCalPlayer.ApplyDefenseDamageInternal += calamityPlayerApplyDefenseDamageInternal;
    }

    public override void Revert() {
        OnCalPlayer.ApplyDefenseDamageInternal -= calamityPlayerApplyDefenseDamageInternal;
    }

    private void calamityPlayerApplyDefenseDamageInternal(OnCalPlayer.orig_ApplyDefenseDamageInternal orig, CalamityPlayer self,
        int defenseDamage, bool showVisuals) {
    }
}