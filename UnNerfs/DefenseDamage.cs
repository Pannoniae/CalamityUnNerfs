using CalamityFly.Config;
using CalamityFly.On;
using CalamityMod.CalPlayer;

namespace CalamityFly.UnNerfs;

public class DefenseDamage : BaseUnNerf
{
	protected override bool Active(UnNerfsConfig config) => config.DefenseDamage;

	protected override void Apply()
	{
		OnCalPlayer.DealDefenseDamage += CalamityPlayer_DealDefenseDamage;
	}

	protected override void Revert()
	{
		OnCalPlayer.DealDefenseDamage -= CalamityPlayer_DealDefenseDamage;
	}

	private void CalamityPlayer_DealDefenseDamage(OnCalPlayer.orig_DealDefenseDamage orig, CalamityPlayer self, int damage)
	{
	}
}
