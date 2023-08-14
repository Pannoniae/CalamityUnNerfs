using CalamityFly.Config;
using CalamityFly.On;
using CalamityMod.CalPlayer;

namespace CalamityFly.UnNerfs;

public class DefenseDamage : BaseUnNerf
{
	public override bool Active(UnNerfsConfig config) => config.DefenseDamage;

	public override void Apply()
	{
		base.Apply();
		OnCalPlayer.DealDefenseDamage += CalamityPlayer_DealDefenseDamage;
		
	}

	public override void Revert()
	{
		//OnCalPlayer.DealDefenseDamage -= CalamityPlayer_DealDefenseDamage;
	}

	private void CalamityPlayer_DealDefenseDamage(OnCalPlayer.orig_DealDefenseDamage orig, CalamityPlayer self, int damage, double realDamage)
	{
	}
}
