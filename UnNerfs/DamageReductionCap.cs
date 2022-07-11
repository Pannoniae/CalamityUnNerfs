using System;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

internal class DamageReductionCap : ModPlayer
{
	public override bool IsLoadingEnabled(Mod mod)
	{
		return CalamityFly.config.DamageReductionCap;
	}

	public override void PostUpdateMiscEffects()
	{
		if(Player.endurance > 0)
		{
			Player.endurance = -(Player.endurance / (Player.endurance - 1));

			Player.endurance = MathF.Round(Player.endurance, 6);
		}
	}
}
