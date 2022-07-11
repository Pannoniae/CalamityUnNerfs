using Terraria;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

internal class WormScarf : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => CalamityFly.config.WormScarf;


	public override void UpdateAccessory(Item item, Player player, bool hideVisual)
	{
		if (item.type == 3224)
		{
			player.endurance += 0.07f;
		}
	}
}
