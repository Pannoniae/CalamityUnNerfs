using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

internal class EndurancePotion : GlobalBuff
{

	public override bool IsLoadingEnabled(Mod mod) => CalamityFly.config.EndurancePotion;

	public override void Update(int type, Player player, ref int buffIndex)
	{
		if(type == BuffID.Endurance)
		{
			player.endurance += 0.05f;
		}
	}

}
