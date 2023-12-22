using System.Reflection;
using CalamityFly.Config;
using CalamityMod;
using CalamityMod.Items;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Projectiles.Ranged;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

public class RemoveCharge : GlobalItem {
    public override bool IsLoadingEnabled(Mod mod) {
        return UnNerfsConfig.Instance.removeCharge;
    }

    public override void Load() {
        var type = typeof(FreedomStarHoldout);
        var method = type.GetMethod("AI");
        var type2 = typeof(GatlingLaserProj);
        var method2 = type2.GetMethod("AI");
        var type3 = typeof(HydraulicVoltCrasherProjectile);
        var method3 = type3.GetMethod("AI");
        var type4 = typeof(PhaseslayerProjectile);
        var method4 = type4.GetMethod("ManipulatePlayer", BindingFlags.Instance | BindingFlags.NonPublic);
        var method5 = type4.GetMethod("HandleSwordBeams", BindingFlags.Instance | BindingFlags.NonPublic);
        var type5 = typeof(Phaseslayer);
        var method6 = type5.GetMethod("CanUseItem");
        var type6 = typeof(HydraulicVoltCrasher);
        var method7 = type5.GetMethod("CanUseItem");
        MonoModHooks.Modify(method, removeChargePatch);
        MonoModHooks.Modify(method2, removeChargePatch);
        MonoModHooks.Modify(method3, removeChargePatch);
        MonoModHooks.Modify(method4, removeChargePatchSeparate1);
        MonoModHooks.Modify(method5, removeChargePatchSeparate2);
        MonoModHooks.Modify(method6, removeChargePatchSeparate1);
        MonoModHooks.Modify(method7, removeChargePatchSeparate1);
    }

    private void removeChargePatch(ILContext il) {
        var ilCursor = new ILCursor(il);
        // IL_06bd: ldfld        float32 CalamityMod.Items.CalamityGlobalItem::Charge
        if (ilCursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<CalamityGlobalItem>("Charge"))) {
            ilCursor.EmitPop();
            ilCursor.EmitLdcR4(1f);
            if (ilCursor.TryGotoNext(MoveType.Before, i => i.MatchStfld<CalamityGlobalItem>("Charge"))) {
                ilCursor.EmitPop();
                ilCursor.EmitPop();
                ilCursor.Remove();
            }
        }
        else {
            CalamityFly.Instance.Logger.Warn("Couldn't match CalamityGlobalItem.Charge in weapon!");
        }
    }

    private void removeChargePatchSeparate1(ILContext il) {
        var ilCursor = new ILCursor(il);
        // IL_06bd: ldfld        float32 CalamityMod.Items.CalamityGlobalItem::Charge
        if (ilCursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<CalamityGlobalItem>("Charge"))) {
            ilCursor.EmitPop();
            ilCursor.EmitLdcR4(1f);
        }
        else {
            CalamityFly.Instance.Logger.Warn("Couldn't match CalamityGlobalItem.Charge in weapon!");
        }
    }

    private void removeChargePatchSeparate2(ILContext il) {
        var ilCursor = new ILCursor(il);
        if (ilCursor.TryGotoNext(MoveType.Before, i => i.MatchStfld<CalamityGlobalItem>("Charge"))) {
            ilCursor.EmitPop();
            ilCursor.EmitPop();
            ilCursor.Remove();
        }
        else {
            CalamityFly.Instance.Logger.Warn("Couldn't match CalamityGlobalItem.Charge in weapon!");
        }
    }

    public override void SetDefaults(Item item) {
        if (UnNerfsConfig.Instance.removeCharge) {
            var cal = item.Calamity();
            cal.UsesCharge = false;
        }
    }
}