using CalamityFly.Config;
using MonoMod.Cil;
using Terraria;

namespace CalamityFly.UnNerfs;

internal class SoaringInsigniaMovement : BaseUnNerf
{
	protected override bool Active(UnNerfsConfig config) => config.SoaringInsigniaMovement;

	protected override void Apply()
	{
		IL_Player.Update += UnNerfSoaringAcceleration;
		IL_Player.UpdateJumpHeight += UnNerfSoaringJump;
	}

	protected override void Revert()
	{
		IL_Player.Update -= UnNerfSoaringAcceleration;
		IL_Player.UpdateJumpHeight -= UnNerfSoaringJump;
	}

	private void UnNerfSoaringJump(ILContext il)
	{
		var cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Terraria.Player>("empressBrooch")))
		{
			Logger.Warn("unable to edit Player_WingMovement (error:8)");
			return;
		}
		if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.5f)))
		{
			Logger.Warn("unable to edit Player_Update (error:9)");
			return;
		}
		cursor.Next.Operand = 2.4f;

	}

	private void UnNerfSoaringAcceleration(ILContext il)
	{
		var cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Terraria.Player>("empressBrooch")))
		{
			Logger.Warn("unable to edit Player_Update (error:6)");
			return;
		}
		if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)))
		{
			Logger.Warn("unable to edit Player_Update (error:7)");
			return;
		}
		cursor.Next.Operand = 2f;
	}
}
