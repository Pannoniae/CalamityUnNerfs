using CalamityFly.Config;
using IL.Terraria;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace CalamityFly.UnNerfs;

public class SoaringInsigniaFlight : BaseUnNerf
{
	protected override void Apply()
	{
		Player.WingMovement += UnNerfSoarinfInfiniteWings;
		Player.Update += UnNerfSoaringInfiniteRocket;
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

	protected override void Revert()
	{
		Player.WingMovement -= UnNerfSoarinfInfiniteWings;
		Player.Update -= UnNerfSoaringInfiniteRocket;
	}

	protected override bool Active(UnNerfsConfig config) => config.SoaringInsigniaFlight;
}
