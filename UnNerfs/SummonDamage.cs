using CalamityFly.Config;
using CalamityFly.IL;
using CalamityMod;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Terraria;

namespace CalamityFly.UnNerfs;

internal class SummonDamage : BaseUnNerf
{
	protected override bool Active(UnNerfsConfig config) => config.SummonDamage;

	protected override void Apply()
	{
		IlCalPlayer.ModifyHitNPCWithProj += IlCalPlayer_ModifyHitNPCWithProj;
	}

	protected override void Revert()
	{
		IlCalPlayer.ModifyHitNPCWithProj -= IlCalPlayer_ModifyHitNPCWithProj;
	}

	public static bool CountAsRogueClass(Projectile p) => p.CountsAsClass<RogueDamageClass>();

	private void IlCalPlayer_ModifyHitNPCWithProj(ILContext il)
	{
		var cursor = new ILCursor(il);

		// rogue max stealh fix
		var rogueType = typeof(RogueDamageClass);
		var ProjType = typeof(Projectile);
		var MethodBase = ProjType.GetMethod("CountsAsClass", 1, Type.EmptyTypes);
		MethodBase = MethodBase.MakeGenericMethod(rogueType);
		while (cursor.TryGotoNext(MoveType.Before, i => i.MatchCallvirt(MethodBase)))
		{
			cursor.Remove();
			cursor.EmitDelegate(CountAsRogueClass);
		}

		// summoner damage

		cursor.Index = 0;

		if (!cursor.TryGotoNext(MoveType.Before,
			i => i.MatchStloc(5)
			))
		{
			Logger.Warn("unable to edit Player_ModifyHitNPCWithProj (error:15)");
			return;
		}

		if (!cursor.TryGotoNext(MoveType.After,
			i => i.MatchLdcR8(0.5)
			))
		{
			Logger.Warn("unable to edit Player_ModifyHitNPCWithProj (error:16)");
			return;
		}
		cursor.Prev.Operand = 1d;

		if (!cursor.TryGotoNext(MoveType.After,
			i => i.MatchLdcR8(0.75)
			))
		{
			Logger.Warn("unable to edit Player_ModifyHitNPCWithProj (error:17)");
			return;
		}
		cursor.Prev.Operand = 1d;
	}
}
