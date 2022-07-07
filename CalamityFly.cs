using CalamityFly.Config;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using On.CalamityMod.CalPlayer;
using System;
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
		if(config.RodOfDiscord)
		{
			Player.ItemCheck_CheckCanUse += UnNerfRodOfDiscord;
		}
		if(config.DefenseDamage)
		{
			CalamityPlayer.DealDefenseDamage += CalamityPlayer_DealDefenseDamage;
		}
	}

	private void CalamityPlayer_DealDefenseDamage(CalamityPlayer.orig_DealDefenseDamage orig, CalamityMod.CalPlayer.CalamityPlayer self, int damage)
	{
	}

	private void UnNerfRodOfDiscord(ILContext il)
	{
		var cursor = new ILCursor(il)
		{
			Index = 0
		};
		var label = cursor.DefineLabel();
		cursor.Emit(OpCodes.Ldarg_1);
		cursor.EmitDelegate<Func<Item, bool>>(i => i.type == ItemID.RodofDiscord);
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
	private void LogCursor(ILCursor cursor, int limit = int.MaxValue)
	{
		cursor.Index = 0;
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
		if (config.DefenseDamage)
		{
			CalamityPlayer.DealDefenseDamage -= CalamityPlayer_DealDefenseDamage;
		}
		if (config.RodOfDiscord)
		{
			Player.ItemCheck_CheckCanUse += UnNerfRodOfDiscord;
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