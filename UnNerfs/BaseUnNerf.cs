using System;
using CalamityFly.Config;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

public abstract class BaseUnNerf {

    // We load earlier than Calamity.
    public void Load(Mod mod) {
        if (Active(CalamityFly.config)) {
            Apply();
        }
    }

    public void OnLateLoad(Mod mod) {
        if (Active(CalamityFly.config)) {
            LateApply();
        }
    }

    public virtual void LateApply() {
        Console.WriteLine($"Applying {GetType().Name} unnerf");
    }

    public virtual void Apply() {
        Console.WriteLine($"Applying {GetType().Name} unnerf (early)");
    }

    public virtual void VeryEarlyApply() {
        Console.WriteLine($"Applying {GetType().Name} unnerf (very early)");
    }

    public abstract bool Active(UnNerfsConfig config);

    public virtual void Revert() {

    }

    public void Unload() {
        if (Active(CalamityFly.config)) {
            Revert();
        }
    }
}