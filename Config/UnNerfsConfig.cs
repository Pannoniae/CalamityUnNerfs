using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CalamityFly.Config;

public class UnNerfsConfig : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Header("reverts")]
	[DefaultValue(true)]
	public bool sellAdditionalItems;

	[Header("unnerfs")]
	[ReloadRequired]
	[DefaultValue(true)]
	public bool SoaringInsigniaFlight;

	[ReloadRequired]
	[DefaultValue(false)]
	public bool SoaringInsigniaMovement;

	[ReloadRequired]
	[DefaultValue(true)]
	public bool Magiluminescence;

	[ReloadRequired]
	[DefaultValue(false)]
	public bool RodOfDiscord;

	/*[Label("Magic Mirror")]
	[ReloadRequired]
	[DefaultValue(true)]
	[Tooltip("True to allow use of magic mirror/recall potion while fighting a boss")]
	public bool MagicMirror;

	[ReloadRequired]
	[DefaultValue(true)]
	[Tooltip("True to allow use of teleporters while fighting a boss")]
	public bool Teleporters;*/

	[ReloadRequired]
	[DefaultValue(true)]
	public bool YoyoBag;

	//Damage/Defense/Endurance Un-Nerfs
	[Header("defenseDamage")]
	[ReloadRequired]
	[DefaultValue(false)]
	public bool DefenseDamage;

	[ReloadRequired]
	[DefaultValue(false)]
	public bool SummonDamage;

	[ReloadRequired]
	[DefaultValue(false)]
	public bool DamageReductionCap;

	[ReloadRequired]
	[DefaultValue(false)]
	public bool WormScarf;

	[ReloadRequired]
	[DefaultValue(false)]
	public bool EndurancePotion;

	[ReloadRequired]
	[DefaultValue(false)]
	public bool MagicPowerPotion;

	[ReloadRequired]
	[DefaultValue(false)]
	public bool FrozenTurtleShell;

	[ReloadRequired]
	[DefaultValue(false)]
	public bool DodgeItems;

	[Header("armorSetUnnerfs")]
	[ReloadRequired]
	[DefaultValue(false)]
	public bool MeteorArmorSet;

	[Header("crafts")]
	[ReloadRequired]
	[DefaultValue(false)]
	public bool VanillaZenithCraft;
}
