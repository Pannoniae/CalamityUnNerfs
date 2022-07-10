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
			i => i.MatchStloc(7)
			))
		{
			Logger.Warn("unable to edit Player_ModifyHitNPCWithProj (error:15)");
			return;
		}
		if (cursor.Prev.OpCode != OpCodes.Ldc_R8)
		{
			Logger.Warn("unable to edit Player_ModifyHitNPCWithProj (error:16)");
			return;
		}
		cursor.Prev.Operand = 1d;
		cursor.Index -= 2;
		if (cursor.Prev.OpCode != OpCodes.Ldc_R8)
		{
			Logger.Warn("unable to edit Player_ModifyHitNPCWithProj (error:17)");
			return;
		}
		cursor.Prev.Operand = 1d;
	}
}
