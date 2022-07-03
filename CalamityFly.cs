using IL.Terraria;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.ModLoader;

namespace CalamityFly;

public class CalamityFly : Mod
{
	public static CalamityFly Instance;

	public override void Load()
	{
		Instance = this;
		Player.WingMovement += Player_WingMovement;
	}

	private void Player_WingMovement(ILContext il)
	{
		var cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(MoveType.After, i => ILPatternMatchingExt.MatchLdfld<Terraria.Player>(i, "empressBrooch")))
		{
			Logger.Warn("unable to edit Player_WingMovement");
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
			Logger.Warn("unable to edit Player_WingMovement");
		}
	}

	public override void Unload()
	{
		Player.WingMovement -= Player_WingMovement;
		Instance = null;
	}
}