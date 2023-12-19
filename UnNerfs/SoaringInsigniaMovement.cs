using CalamityFly.Config;
using MonoMod.Cil;
using Terraria;

namespace CalamityFly.UnNerfs;

internal class SoaringInsigniaMovement : BaseUnNerf
{
	public override bool Active(UnNerfsConfig config) => config.SoaringInsigniaMovement;

	public override void Apply()
	{
		base.Apply();
		IL_Player.Update += UnNerfSoaringAcceleration;
		IL_Player.UpdateJumpHeight += UnNerfSoaringJump;
	}

	public override void Revert()
	{
		IL_Player.Update -= UnNerfSoaringAcceleration;
		IL_Player.UpdateJumpHeight -= UnNerfSoaringJump;
	}

	private void UnNerfSoaringJump(ILContext il)
	{
		var cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Player>("empressBrooch")))
		{
			CalamityFly.Instance.Logger.Warn("unable to edit Player_WingMovement (error:8)");
			return;
		}
		if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(0.5f)))
		{
			CalamityFly.Instance.Logger.Warn("unable to edit Player_Update (error:9)");
			return;
		}
		cursor.Next.Operand = 2.4f;

	}

	private void UnNerfSoaringAcceleration(ILContext il)
	{
		var cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Player>("empressBrooch")))
		{
			CalamityFly.Instance.Logger.Warn("unable to edit Player_Update (error:6)");
			return;
		}
		if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)))
		{
			CalamityFly.Instance.Logger.Warn("unable to edit Player_Update (error:7)");
			return;
		}
		cursor.Next.Operand = 2f;
	}
}
