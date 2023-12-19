using System.Collections.Generic;
using CalamityFly.Config;
using CalamityFly.UnNerfs;
using Terraria.ModLoader;
using VanillaQoL;

namespace CalamityFly;

public class CalamityFly : Mod {

    private static List<BaseUnNerf> unnerfs = new();

    public static CalamityFly Instance = null!;
    internal static UnNerfsConfig config => UnNerfsConfig.Instance;

    public CalamityFly() {
        Instance = this;
        unnerfs.Add(new DefenseDamage());
        unnerfs.Add(new DodgeItems());
        unnerfs.Add(new RodOfDiscord());
        unnerfs.Add(new SoaringInsigniaFlight());
        unnerfs.Add(new SoaringInsigniaMovement());
        unnerfs.Add(new SummonDamage());

        foreach (var unnerf in unnerfs) {
            unnerf.VeryEarlyApply();
        }

    }

    public override void Load() {
        foreach (var unnerf in unnerfs) {
            unnerf.Load(this);
        }
    }

    public override void PostSetupContent() {
        foreach (var unnerf in unnerfs) {
            unnerf.OnLateLoad(this);
        }
    }

    public override void Unload() {
        Instance = null!;

        // unload all the IL edits
        Utils.completelyWipeClass(typeof(DamageReductionCap));
    }
}