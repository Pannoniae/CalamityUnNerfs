using CalamityFly.Config;
using CalamityMod.ILEditing;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria.Utilities;

namespace CalamityFly.UnNerfs;

internal class DodgeItems : BaseUnNerf
{
	protected override bool Active(UnNerfsConfig config) => config.DodgeItems;

	protected override void Apply()
	{
		global::IL.Terraria.Player.Hurt += Player_Hurt;
	}

	private void Player_Hurt(ILContext il)
	{
		var c = new ILCursor(il);
		// ninja gear
		if (!c.TryGotoNext(MoveType.Before, i => i.MatchCallvirt<UnifiedRandom>("Next")))
		{
			Logger.Warn("unable to edit Player.Hurt (error:1)");
			return;
		}
		c.Index--;
		if (c.Next.OpCode != OpCodes.Ldc_I4_1)
		{
			Logger.Warn("unable to edit Player.Hurt (error:2)");
			return;
		}
		c.Remove();
		c.Emit(OpCodes.Ldc_I4, 10);
		c.Index++;

		c.RemoveRange(5);

		if (!c.TryGotoNext(MoveType.After, (Instruction i) => i.MatchCall<Terraria.Player>("NinjaDodge")))
		{
			Logger.Warn("unable to edit Player.Hurt (error:4)");
			return;
		}
		c.RemoveRange(4);

		// brain of confusion

		if (!c.TryGotoNext(MoveType.Before, i => i.MatchCallvirt<UnifiedRandom>("Next")))
		{
			Logger.Warn("unable to edit Player.Hurt (error:5)");
			return;
		}
		c.Index--;
		if (c.Next.OpCode != OpCodes.Ldc_I4_1)
		{
			Logger.Warn("unable to edit Player.Hurt (error:6)");
			return;
		}
		c.Remove();
		c.Emit(OpCodes.Ldc_I4, 6);
		c.Index++;

		c.RemoveRange(5);

		if (!c.TryGotoNext(MoveType.After, (Instruction i) => i.MatchCall<Terraria.Player>("BrainOfConfusionDodge")))
		{
			Logger.Warn("unable to edit Player.Hurt (error:7)");
			return;
		}
		c.RemoveRange(4);
	}

	protected override void Revert()
	{
		global::IL.Terraria.Player.Hurt -= Player_Hurt;
	}
}
