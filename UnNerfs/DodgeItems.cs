using System.Reflection;
using CalamityFly.Config;
using CalamityMod.ILEditing;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

internal class DodgeItems : BaseUnNerf {
    public override bool Active(UnNerfsConfig config) => config.DodgeItems;

    public override void EarlyApply() {
        base.EarlyApply();
        // I'm sorry
        var t = typeof(ILChanges);
        var n = t.GetMethod("DodgeMechanicAdjustments", BindingFlags.Static | BindingFlags.NonPublic);
        MonoModHooks.Modify(n, nullifyDodgeHook);
        //Player
    }

    private void nullifyDodgeHook(ILContext il) {
        var c = new ILCursor(il);
        c.Index = 0;
        //MonoModHooks.DumpIL(ModContent.GetInstance<CalamityUnNerfs>(), il);
        // go to calamity edits in head
        c.Emit(OpCodes.Ret); // fuck it we are canceling the method
    }
}