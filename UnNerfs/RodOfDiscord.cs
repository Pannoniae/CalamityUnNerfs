using CalamityFly.Config;
using CalamityFly.Helpers;
using Terraria.ID;

namespace CalamityFly.UnNerfs;

internal class RodOfDiscord : BaseUnNerf
{
	protected override bool Active(UnNerfsConfig config)  => config.RodOfDiscord;

	protected override void Apply()
	{
		UseItemHelper.Init();
		UseItemHelper.UsableItem(ItemID.RodofDiscord);
	}

	protected override void Revert()
	{
		UseItemHelper.UnusableItem(ItemID.RodofDiscord);
		UseItemHelper.UnInit();
	}
}
