using CalamityFly.Config;
using CalamityMod.Projectiles;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;
using Terraria.Social.Steam;

namespace CalamityFly.UnNerfs;

internal class YoyoBag : BaseUnNerf
{
	public override bool Active(UnNerfsConfig config) => config.YoyoBag;

	public override void Apply() {
		base.Apply();
		var type = typeof(CalamityGlobalProjectile);
		var method = type.GetMethod("PreAI");
		MonoModHooks.Modify(method, unnerfYoyos);
	}

	private void unnerfYoyos(ILContext il) {
		var ilCursor = new ILCursor(il);
		// IL_00dc: ldfld        bool [tModLoader]Terraria.Player::yoyoGlove
		// IL_00e1: brfalse      IL_0182
		if (ilCursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Player>("yoyoGlove"))) {
			// just jump
			ilCursor.EmitPop();
			ilCursor.Emit(OpCodes.Ldc_I4_0);
		}
	}
}
