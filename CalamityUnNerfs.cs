using System.Collections.Generic;
using CalamityFly.Config;
using CalamityFly.UnNerfs;
using MonoMod.Cil;
using Terraria.ModLoader;

namespace CalamityFly;

public class CalamityFly : Mod {

    private static List<BaseUnNerf> unnerfs = new();

    public static CalamityFly Instance;
    internal static UnNerfsConfig config = ModContent.GetInstance<UnNerfsConfig>();

    public override void Load() {
        Instance = this;
    }

    public override void PostSetupContent() {
        foreach (var unnerf in unnerfs) {
            unnerf.Apply();
        }
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

    public override void Unload() {
        Instance = null;
    }

    /// <summary>
    /// Register an un-nerf for PostSetupContent.
    /// </summary>
    /// <param name="baseUnNerf"></param>
    public void registerForPostSetup(BaseUnNerf unnerf) {
        unnerfs.Add(unnerf);
    }
}