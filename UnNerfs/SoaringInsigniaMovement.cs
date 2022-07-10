using CalamityFly.Config;
using IL.Terraria;
using MonoMod.Cil;

namespace CalamityFly.UnNerfs;

internal class SoaringInsigniaMovement : BaseUnNerf
{
	protected override bool Active(UnNerfsConfig config) => config.SoaringInsigniaMovement;

	protected override void Apply()
	{
		Player.Update += UnNerfSoaringAcceleration;
		Player.UpdateJumpHeight += UnNerfSoaringJump;
	}

	protected override void Revert()
	{
		Player.Update -= UnNerfSoaringAcceleration;
		Player.UpdateJumpHeight -= UnNerfSoaringJump;
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
