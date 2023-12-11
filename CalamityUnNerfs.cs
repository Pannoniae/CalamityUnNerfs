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