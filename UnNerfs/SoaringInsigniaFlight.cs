using CalamityFly.Config;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;

namespace CalamityFly.UnNerfs;

public class SoaringInsigniaFlight : BaseUnNerf
{
	public override void Apply()
	{
		base.Apply();
		IL_Player.WingMovement += UnNerfSoarinfInfiniteWings;
		IL_Player.Update += UnNerfSoaringInfiniteRocket;
	}

	private void UnNerfSoaringInfiniteRocket(ILContext il)
	{
		var cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Terraria.Player>("empressBrooch")))
		{
			Logger.Warn("unable to edit Player_Update (error:3)");
			return;
		}
		if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Terraria.Player>("empressBrooch")))
		{
			Logger.Warn("unable to edit Player_Update (error:4)");
			return;
		}
		cursor.Index++;
		if (cursor.Prev.OpCode == OpCodes.Ldc_I4_0 && cursor.Next.OpCode == OpCodes.And)
		{
			cursor.Index--;
			cursor.RemoveRange(2);
		}
		else
		{
			Logger.Warn("unable to edit Player_Update (error:5)");
		}
	}

	private void UnNerfSoarinfInfiniteWings(ILContext il)
	{
		var cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Terraria.Player>("empressBrooch")))
		{
			Logger.Warn("unable to edit Player_WingMovement (error:1)");
			return;
		}
		cursor.Index++;
		if (cursor.Prev.OpCode == OpCodes.Ldc_I4_0 && cursor.Next.OpCode == OpCodes.And)
		{
			cursor.Index--;
			cursor.RemoveRange(2);
		}
		else
		{
			Logger.Warn("unable to edit Player_WingMovement (error:2)");
		}
	}

	public override void Revert()
	{
		IL_Player.WingMovement -= UnNerfSoarinfInfiniteWings;
		IL_Player.Update -= UnNerfSoaringInfiniteRocket;
	}

	public override bool Active(UnNerfsConfig config) => config.SoaringInsigniaFlight;
}
