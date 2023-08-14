using CalamityFly.Config;
using CalamityFly.IL;
using CalamityMod;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using CalamityMod.Balancing;
using CalamityMod.CalPlayer;
using Terraria;

namespace CalamityFly.UnNerfs;

internal class SummonDamage : BaseUnNerf
{
	public override bool Active(UnNerfsConfig config) => config.SummonDamage;

	public override void Apply()
	{
		base.Apply();
		IlCalPlayer.ModifyHitNPCWithProj += IlCalPlayer_ModifyHitNPCWithProj;
	}

	public override void Revert()
	{
		IlCalPlayer.ModifyHitNPCWithProj -= IlCalPlayer_ModifyHitNPCWithProj;
	}

	public static bool CountAsRogueClass(Projectile p) => p.CountsAsClass<RogueDamageClass>();

	private void IlCalPlayer_ModifyHitNPCWithProj(ILContext il)
	{
		var cursor = new ILCursor(il);

		// rogue max stealth fix
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
			    i => i.MatchLdsfld(typeof(BalancingConstants), "SummonerCrossClassNerf")
		    ))
		{
			Logger.Warn("unable to edit Player_ModifyHitNPCWithProj (error:17)");
			return;
		}
		//cursor.Prev.Operand = 1d;
		cursor.Remove();
		cursor.Emit(OpCodes.Ldc_R4, 1f);
	}
}
