using CalamityFly.Config;
using CalamityFly.IL;
using CalamityFly.On;
using CalamityMod;
using CalamityMod.CalPlayer;
using CalamityMod.ILEditing;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Player = IL.Terraria.Player;

namespace CalamityFly;

public class CalamityFly : Mod
{
	public static CalamityFly Instance;
	internal static UnNerfsConfig config = ModContent.GetInstance<UnNerfsConfig>();

	public override void Load()
	{
		Instance = this;
		if (config.SoaringInsigniaFlight)
		{
			Player.WingMovement += UnNerfSoarinfInfiniteWings;
			Player.Update += UnNerfSoaringInfiniteRocket;
		}
		if(config.SoaringInsigniaMovement)
		{
			Player.Update += UnNerfSoaringAcceleration;
			Player.UpdateJumpHeight += UnNerfSoaringJump;
		}
		if (config.Magiluminescence)
		{
			Player.Update += UnNerfMagiluminescence;
		}
		if(config.RodOfDiscord || config.MagicMirror)
		{
			Player.ItemCheck_CheckCanUse += UnNerfUsableItems;
		}
		if(config.DefenseDamage)
		{
			OnCalPlayer.DealDefenseDamage += CalamityPlayer_DealDefenseDamage;
		}
		if(config.SummonDamage)
		{
			IlCalPlayer.ModifyHitNPCWithProj += IlCalPlayer_ModifyHitNPCWithProj;
		}
		if(config.Teleporters)
		{
			var origT = typeof(Wiring);
			var origM = origT.GetMethod("Teleport", BindingFlags.Static | BindingFlags.NonPublic);
			var modT = typeof(ILChanges);
			var modM = modT.GetMethod("DisableTeleporters", BindingFlags.Static | BindingFlags.NonPublic);
			Delegate del = modM.CreateDelegate(typeof(global::On.Terraria.Wiring.hook_Teleport));
			HookEndpointManager.Remove(origM, del);
		}
	}

	private void IlCalPlayer_ModifyHitNPCWithProj(ILContext il)
	{
		var cursor = new ILCursor(il);

		// rogue max stealh fix
		var rogueType = typeof(RogueDamageClass);
		var ProjType = typeof(Projectile);
		var MethodBase = ProjType.GetMethod("CountsAsClass", 1, Type.EmptyTypes);
		MethodBase = MethodBase.MakeGenericMethod(rogueType);
		while(cursor.TryGotoNext(MoveType.Before, i=>i.MatchCallvirt(MethodBase)))
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
		cursor.Index-=2;
		if (cursor.Prev.OpCode != OpCodes.Ldc_R8)
		{
			Logger.Warn("unable to edit Player_ModifyHitNPCWithProj (error:17)");
			return;
		}
		cursor.Prev.Operand = 1d;
	}

	private static bool CountAsRogueClass(Projectile p) => p.CountsAsClass<RogueDamageClass>();

	private void CalamityPlayer_DealDefenseDamage(OnCalPlayer.orig_DealDefenseDamage orig, CalamityPlayer self, int damage)
	{
	}

	private void UnNerfUsableItems(ILContext il)
	{
		var cursor = new ILCursor(il)
		{
			Index = 0
		};
		var label = cursor.DefineLabel();
		cursor.Emit(OpCodes.Ldarg_1);
		cursor.EmitDelegate<Func<Item, bool>>(i =>
		{
			if(config.RodOfDiscord && i.type == ItemID.RodofDiscord)
				return true;
			if(config.MagicMirror)
				return i.type is ItemID.MagicMirror or ItemID.IceMirror or ItemID.CellPhone or ItemID.RecallPotion;
			return false;
		});
		cursor.Emit(OpCodes.Brfalse_S, label);
		cursor.Emit(OpCodes.Ldc_I4_1);
		cursor.Emit(OpCodes.Ret);
		cursor.MarkLabel(label);
	}

	public override void AddRecipes()
	{
		if(config.VanillaZenithCraft)
		{
			Recipe.Create(ItemID.Zenith)
			.AddIngredient(ItemID.TerraBlade)
			.AddIngredient(ItemID.Meowmere)
			.AddIngredient(ItemID.StarWrath)
			.AddIngredient(ItemID.InfluxWaver)
			.AddIngredient(ItemID.TheHorsemansBlade)
			.AddIngredient(ItemID.Seedler)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.BeeKeeper)
			.AddIngredient(ItemID.EnchantedSword)
			.AddIngredient(ItemID.CopperShortsword)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
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

	private void UnNerfSoaringJump(ILContext il)
	{
		var cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Terraria.Player>("empressBrooch")))
		{
			Logger.Warn("unable to edit Player_WingMovement (error:8)");
			return;
		}
		if(!cursor.TryGotoNext(MoveType.Before, i=> i.MatchLdcR4(0.5f))) 
		{
			Logger.Warn("unable to edit Player_Update (error:9)");
			return;
		}
		cursor.Next.Operand = 2.4f;

	}

	private void UnNerfSoaringAcceleration(ILContext il)
	{
		var cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(MoveType.After, i => i.MatchLdfld<Terraria.Player>("empressBrooch")))
		{
			Logger.Warn("unable to edit Player_Update (error:6)");
			return;
		}
		if (!cursor.TryGotoNext(MoveType.Before, i => i.MatchLdcR4(1.1f)))
		{
			Logger.Warn("unable to edit Player_Update (error:7)");
			return;
		}
		cursor.Next.Operand = 2f;
	}

	private void UnNerfSoaringInfiniteRocket(ILContext il)
	{
		var cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(MoveType.After,i => i.MatchLdfld<Terraria.Player>("empressBrooch")))
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
#if DEBUG
	private void LogCursor(ILCursor cursor, int limit = int.MaxValue, bool resetIndex = true)
	{
		if(resetIndex) cursor.Index = 0;
		int c = 0;
		do
		{
			Logger.Info($"{cursor.Next?.OpCode} {cursor.Next?.Operand}");
			c++;
		} while (cursor.TryGotoNext() && c < limit);
	}
#endif
	public override void Unload()
	{
		if (config.SummonDamage)
		{
			IlCalPlayer.ModifyHitNPCWithProj -= IlCalPlayer_ModifyHitNPCWithProj;
		} 
		if (config.DefenseDamage)
		{
			OnCalPlayer.DealDefenseDamage -= CalamityPlayer_DealDefenseDamage;
		}
		if (config.RodOfDiscord || config.MagicMirror)
		{
			Player.ItemCheck_CheckCanUse -= UnNerfUsableItems;
		}
		if (config.Magiluminescence)
		{
			Player.Update -= UnNerfMagiluminescence;
		}
		if (config.SoaringInsigniaMovement)
		{
			Player.Update -= UnNerfSoaringAcceleration;
			Player.UpdateJumpHeight -= UnNerfSoaringJump;
		}
		if (config.SoaringInsigniaFlight)
		{
			Player.WingMovement -= UnNerfSoarinfInfiniteWings;
			Player.Update -= UnNerfSoaringInfiniteRocket;
		}
		Instance = null;
	}
}