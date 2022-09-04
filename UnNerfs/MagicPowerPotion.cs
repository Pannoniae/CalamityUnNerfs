using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

internal class MagicPowerPotion : GlobalBuff
{
	public override bool IsLoadingEnabled(Mod mod) => CalamityFly.config.MagicPowerPotion;

	public override void Update(int type, Player player, ref int buffIndex)
	{
		if(type == BuffID.MagicPower)
		{
			player.GetDamage<MagicDamageClass>() += 0.1f;
		}
	}

}
