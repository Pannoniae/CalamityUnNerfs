using System.Reflection;
using CalamityFly.Config;
using CalamityMod.CalPlayer;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

internal class DamageReductionCap : BaseUnNerf {
    public override bool Active(UnNerfsConfig config) {
        return config.DamageReductionCap;
    }

    public override void Apply() {
        base.Apply();
        var type = typeof(CalamityPlayer);
        var method = type.GetMethod("Limits", BindingFlags.Instance | BindingFlags.NonPublic);
        MonoModHooks.Modify(method, removeDamageScaling);
    }

    private void removeDamageScaling(ILContext il) {
        var ilCursor = new ILCursor(il);
        // IL_00c0: stfld        float32 [tModLoader]Terraria.Player::endurance
        if (ilCursor.TryGotoNext(MoveType.Before, i => i.MatchStfld<Player>("endurance"))) {
            ilCursor.EmitPop();
            ilCursor.EmitPop();
            ilCursor.Remove();
        }
    }
}