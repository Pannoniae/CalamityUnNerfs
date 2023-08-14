using CalamityFly.Config;
using log4net;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

public abstract class BaseUnNerf : ILoadable {
    protected ILog Logger;

    // We load earlier than Calamity.
    public void Load(Mod mod) {
        Logger = mod.Logger;
        if (Active(CalamityFly.config)) {
            EarlyApply();
            ((CalamityFly)mod).registerForPostSetup(this);
        }
    }

    public virtual void Apply() {
        Logger.Info($"Applying {GetType().Name} unnerf");
    }

    public virtual void EarlyApply() {
        Logger.Info($"Applying {GetType().Name} unnerf (early)");
    }

    public abstract bool Active(UnNerfsConfig config);

    public virtual void Revert() {

    }

    public void Unload() {
        if (Active(CalamityFly.config)) {
            Revert();
        }

        Logger = null;
    }
}