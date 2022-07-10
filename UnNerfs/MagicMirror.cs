using CalamityFly.Config;
using CalamityFly.Helpers;
using System.Collections.Generic;
using Terraria.ID;

namespace CalamityFly.UnNerfs;

internal class MagicMirror : BaseUnNerf
{
	static readonly List<int> mirrors = new () {
		ItemID.MagicMirror,
		ItemID.IceMirror,
		ItemID.CellPhone,
		ItemID.RecallPotion,
		ItemID.PotionOfReturn
	};

	protected override bool Active(UnNerfsConfig config) => config.MagicMirror;

	protected override void Apply()
	{
		UseItemHelper.Init();
		mirrors.ForEach(UseItemHelper.UsableItem);
	}

	protected override void Revert()
	{
		mirrors.ForEach(UseItemHelper.UnusableItem);
		UseItemHelper.UnInit();
	}
}
