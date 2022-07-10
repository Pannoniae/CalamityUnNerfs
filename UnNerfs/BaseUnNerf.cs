using CalamityFly.Config;
using log4net;
using System;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

public abstract class BaseUnNerf : ILoadable
{
	protected ILog Logger;

	public void Load(Mod mod)
	{
		Logger = mod.Logger;
		if(Active(CalamityFly.config))
		{
			Logger.Info($"Applying {GetType().Name} unnerf");
			Apply();
		}
	}

	protected abstract void Apply();
	protected abstract bool Active(UnNerfsConfig config);
	protected abstract void Revert();

	public void Unload()
	{
		if (Active(CalamityFly.config))
		{
			Revert();
		}
		Logger = null;
	}
}
