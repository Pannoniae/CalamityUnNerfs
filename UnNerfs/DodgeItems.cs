using System.Reflection;
using CalamityFly.Config;
using CalamityMod.CalPlayer;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

internal class DodgeItems : BaseUnNerf {
    public override bool Active(UnNerfsConfig config) => config.DodgeItems;

    public override void EarlyApply() {
        base.EarlyApply();
        IL_Player.Hurt_PlayerDeathReason_int_int_refHurtInfo_bool_bool_int_bool_float_float_float +=
            revertDodgeItemsUselessness;
        // I'm sorry
        var t = typeof(CalamityPlayer);
        var dodgeMethod = t.GetMethod("ConsumableDodge",
            BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        MonoModHooks.Modify(dodgeMethod, removeCalamityDodge);

    }

    /*private void nullifyDodgeHook(ILContext il) {
        var c = new ILCursor(il);
        c.Index = 0;
        //MonoModHooks.DumpIL(ModContent.GetInstance<CalamityUnNerfs>(), il);
        // go to calamity edits in head
        c.Emit(OpCodes.Ret); // fuck it we are canceling the method
    }*/

    public void revertDodgeItemsUselessness(ILContext il) {
        var ilCursor = new ILCursor(il);
        if (!ilCursor.TryGotoNext(MoveType.After,
                i => i.MatchCall(typeof(Player.HurtModifiers), nameof(Player.HurtModifiers.ToHurtInfo)))) {
            CalamityFly.Instance.Logger.Warn("Failed to locate the call to HurtModifiers.ToHurtInfo in Player.Hurt!");
        }

        if (!ilCursor.TryGotoNext(MoveType.After,
                i => i.MatchLdarg(out var dodgeable))) {
            CalamityFly.Instance.Logger.Warn("Failed to locate the load to the dodgeable parameter in Player.Hurt!");
            return;
        }
        // we remove 6 instructions (calamity's patch + the delegate garbage injected)
        ilCursor.RemoveRange(6);

        // Skip ahead to Black Belt
        if (!ilCursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Player>("blackBelt")))
        {
            CalamityFly.Instance.Logger.Warn("Could not locate the Black Belt equipped boolean.");
            return;
        }
        ilCursor.RemoveRange(2);

        // Skip ahead to Brain of Confusion
        // Here the part we skip to is actually
        // brainOfConfusionItem != null
        // This is implemented as a brfalse, so just do the same thing as above.
        if (!ilCursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Player>("brainOfConfusionItem")))
        {
            CalamityFly.Instance.Logger.Warn("Could not locate the Brain of Confusion tracked equipped item.");
            return;
        }

        ilCursor.RemoveRange(2);

        MonoModHooks.DumpIL(VanillaQoL.VanillaQoL.instance, il);
    }

    public void removeCalamityDodge(ILContext il) {
        var ilCursor = new ILCursor(il);
        // IL_0010: brtrue       IL_00be
        if (ilCursor.TryGotoNext(MoveType.Before, i => i.MatchBrtrue(out var label))) {
            ilCursor.EmitPop();
            ilCursor.Emit(OpCodes.Ldc_I4_1);
            MonoModHooks.DumpIL(VanillaQoL.VanillaQoL.instance, il);
        }
        else {
            CalamityFly.Instance.Logger.Warn("Failed to match the first jump in CalamityPlayer.ConsumableDodge!");
        }
    }
}