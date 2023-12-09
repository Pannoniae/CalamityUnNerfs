using System.Reflection;
using CalamityFly.Config;
using CalamityMod.CalPlayer;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

public class RespawnTimer : BaseUnNerf {
    public override bool Active(UnNerfsConfig config) {
        return config.RespawnTimer;
    }

    public override void Apply() {
        var type = typeof(CalamityPlayer);
        var method = type.GetMethod("UpdateDead", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        var method2 = type.GetMethod("KillPlayer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        MonoModHooks.Modify(method, removeRespawnTimer);
        MonoModHooks.Modify(method2, removeRespawnTimer2);
    }

    //IL_0b6d: ldc.i4       900 // 0x00000384
    public void removeRespawnTimer(ILContext il) {
        var ilCursor = new ILCursor(il);
        // this is a simple one, match 900 then modify to maxvalue, hehe
        if (ilCursor.TryGotoNext(MoveType.AfterLabel, i => i.MatchLdcI4(900))) {
            ilCursor.Emit(OpCodes.Ldc_I4, int.MaxValue);
            ilCursor.Remove();
        }
        else {
            CalamityFly.Instance.Logger.Warn("Couldn't modify respawnTime constant! (900)");
        }
    }

    public void removeRespawnTimer2(ILContext il) {
        var ilCursor = new ILCursor(il);
        // this is a simple one, match 600 then modify to maxvalue, hehe
        if (ilCursor.TryGotoNext(MoveType.AfterLabel, i => i.MatchLdcI4(600))) {
            ilCursor.Emit(OpCodes.Ldc_I4, int.MaxValue);
            ilCursor.Remove();
        }
        else {
            CalamityFly.Instance.Logger.Warn("Couldn't modify respawnTime constant! (600)");
        }
    }
}