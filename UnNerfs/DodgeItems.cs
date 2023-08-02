using System.Reflection;
using CalamityFly.Config;
using CalamityMod.ILEditing;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

internal class DodgeItems : BaseUnNerf
{
	protected override bool Active(UnNerfsConfig config) => config.DodgeItems;

	protected override void Apply() {
		// I'm sorry
		var t = typeof(ILChanges);
		var n = t.GetMethod("RemoveRNGFromDodges", BindingFlags.Static | BindingFlags.NonPublic);
		MonoModHooks.Modify(n, Player_Hurt);
		//Player
	}

	private void Player_Hurt(ILContext il)
	{
		var c = new ILCursor(il);
		//MonoModHooks.DumpIL(ModContent.GetInstance<CalamityFly>(), il);
		// go to calamity edits in head
		if (!c.TryGotoNext(MoveType.Before, i => i.MatchLdfld<Entity>("whoAmI")))
		{
			Logger.Warn("unable to edit Player.Hurt (error:1)");
			return;
		}

		c.Index--;
		c.Remove();
		c.Remove();
		c.Emit(OpCodes.Ldc_I4_0);
		
		if (!c.TryGotoNext(MoveType.Before, i => i.MatchLdsfld<Main>("myPlayer")))
		{
			Logger.Warn("unable to edit Player.Hurt (error:2)");
			return;
		}
		
		c.Remove();
		c.Emit(OpCodes.Ldc_I4_1);
	}

	protected override void Revert()
	{
		
	}
}
