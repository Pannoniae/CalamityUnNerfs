using CalamityFly.Config;
using IL.Terraria;
using MonoMod.Cil;

namespace CalamityFly.UnNerfs;

internal class Magiluminescence : BaseUnNerf
{
	protected override bool Active(UnNerfsConfig config) => config.Magiluminescence;

	protected override void Apply()
	{
		Player.Update += UnNerfMagiluminescence;
	}

	protected override void Revert()
	{
		Player.Update -= UnNerfMagiluminescence;
	}

	private void UnNerfMagiluminescence(ILContext il)
	{
		var cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Terraria.Player>("hasMagiluminescence")))
		{
			Logger.Warn("unable to edit Player_Update (error:10)");
			return;
		}
		if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.25f)))
		{
			Logger.Warn("unable to edit Player_Update (error:11)");
			return;
		}
		cursor.Next.Operand = 2f;
		if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1f)))
		{
			Logger.Warn("unable to edit Player_Update (error:12)");
			return;
		}
		cursor.Next.Operand = 1.2f;
		if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1f)))
		{
			Logger.Warn("unable to edit Player_Update (error:13)");
			return;
		}
		cursor.Next.Operand = 1.2f;
		if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.25f)))
		{
			Logger.Warn("unable to edit Player_Update (error:14)");
			return;
		}
		cursor.Next.Operand = 2f;
	}
}
