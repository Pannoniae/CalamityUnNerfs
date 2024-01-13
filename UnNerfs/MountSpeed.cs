using System.Reflection;
using CalamityFly.Config;
using CalamityMod.CalPlayer;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

public class MountSpeed : BaseUnNerf {
    public override bool Active(UnNerfsConfig config) => config.mountSpeed;

    public override void Apply() {
        base.Apply();
        var calPlayer = typeof(CalamityPlayer);
        var method = calPlayer.GetMethod("MiscEffects",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        MonoModHooks.Modify(method, mountSpeedPatch);
    }

    public void mountSpeedPatch(ILContext il) {
        // IL_10ba: br.s         IL_1117

        // // [8587 12 - 8587 44]
        // IL_10bc: ldarg.0      // this
        // IL_10bd: call         instance class [tModLoader]Terraria.Player [tModLoader]Terraria.ModLoader.ModPlayer::get_Player()
        // IL_10c2: ldfld        class [tModLoader]Terraria.Mount [tModLoader]Terraria.Player::mount
        // IL_10c7: callvirt     instance int32 [tModLoader]Terraria.Mount::get_Type()
        // IL_10cc: ldc.i4.3
        // IL_10cd: bne.un.s     IL_10ea
        var ilCursor = new ILCursor(il);
        ILLabel label = null!;
        if (!ilCursor.TryGotoNext(MoveType.Before, i => i.MatchBr(out label!),
                i => i.MatchLdarg0(),
                i => i.MatchCall<ModPlayer>("get_Player"),
                i => i.MatchLdfld<Player>("mount"),
                i => i.MatchCallOrCallvirt<Mount>("get_Type"),
                i => i.MatchLdcI4(3))) {
            CalamityFly.Instance.Logger.Warn("Couldn't match mount checks in CalamityPlayer.MiscEffects!");
            return;
        }
        // point to ldarg.0
        ilCursor.Index++;
        // emit the unconditional jump in the block itself too
        ilCursor.MoveAfterLabels();
        ilCursor.EmitBr(label);
    }
}