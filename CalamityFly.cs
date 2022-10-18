using CalamityFly.Config;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityFly;

public class CalamityFly : Mod
{
	public static CalamityFly Instance;
	internal static UnNerfsConfig config = ModContent.GetInstance<UnNerfsConfig>();

	public override void Load()
	{
		Instance = this;
	}

#if DEBUG
	public static void LogCursor(ILCursor cursor, int limit = int.MaxValue, bool resetIndex = true)
	{
		Instance ??= (CalamityFly)ModLoader.GetMod("CalamityFly");
		if(resetIndex) cursor.Index = 0;
		int c = 0;
		do
		{
			Instance.Logger.Info($"{cursor.Next?.OpCode} {cursor.Next?.Operand}");
			c++;
		} while (cursor.TryGotoNext() && c < limit);
	}
#endif

	public override void Unload()
	{
		Instance = null;
	}
}