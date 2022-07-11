using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityFly.UnNerfs;

internal class YoyoBag : GlobalItem
{
	public override bool IsLoadingEnabled(Mod mod) => CalamityFly.config.YoyoBag;

	public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
	{
		if (CalamityFly.config.YoyoBag && player.yoyoGlove && ItemID.Sets.Yoyo[item.type])
		{
			damage /= 0.66f;
		}
	}
}
