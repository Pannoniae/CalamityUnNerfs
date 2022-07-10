using Terraria;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using Player = IL.Terraria.Player;
using System.Collections.Generic;

namespace CalamityFly.Helpers;

internal static class UseItemHelper
{
	private static int initcount = 0;

	private static readonly HashSet<int> items = new();

	internal static void Init()
	{
		if(initcount++ == 0)
		{
			Player.ItemCheck_CheckCanUse += UnNerfUsableItems;
		}
	}

	internal static void UnInit()
	{
		if (--initcount == 0)
		{
			Player.ItemCheck_CheckCanUse -= UnNerfUsableItems;
		}
	}

	private static void UnNerfUsableItems(ILContext il)
	{
		var cursor = new ILCursor(il)
		{
			Index = 0
		};
		var label = cursor.DefineLabel();
		cursor.Emit(OpCodes.Ldarg_1);
		cursor.EmitDelegate<Func<Item, bool>>(i => items.Contains(i.type));
		cursor.Emit(OpCodes.Brfalse_S, label);
		cursor.Emit(OpCodes.Ldc_I4_1);
		cursor.Emit(OpCodes.Ret);
		cursor.MarkLabel(label);
	}

	internal static void UnusableItem(int rodofDiscord)
	{
		items.Remove(rodofDiscord);
	}

	internal static void UsableItem(int rodofDiscord)
	{
		items.Add(rodofDiscord);
	}
}
