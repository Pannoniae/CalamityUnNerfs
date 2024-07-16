using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CalamityFly.Config;

public class UnNerfsConfig : ModConfig {

	public static UnNerfsConfig Instance;
	public override ConfigScope Mode => ConfigScope.ServerSide;

	[DefaultValue(false)]
	[ReloadRequired]
	public bool murasamaUnnerf;

	[Header("unnerfs")]

	[ReloadRequired]
	[DefaultValue(true)]
	public bool SoaringInsigniaFlight;

	[ReloadRequired]
	[DefaultValue(true)]
	public bool SoaringInsigniaMovement;

	[ReloadRequired]
	[DefaultValue(true)]
	public bool RodOfDiscord;

	/*[Label("Magic Mirror")]
	[ReloadRequired]
	[DefaultValue(true)]
	[Tooltip("True to allow use of magic mirror/recall potion while fighting a boss.")]
	public bool MagicMirror;

	[ReloadRequired]
	[DefaultValue(true)]
	[Tooltip("True to allow use of teleporters while fighting a boss.")]
	public bool Teleporters;*/

	[ReloadRequired]
	[DefaultValue(true)]
	public bool YoyoBag;

	[ReloadRequired]
	[DefaultValue(false)]
	public bool removeCharge;

	[ReloadRequired]
	[DefaultValue(true)]
	public bool mountSpeed;

	//Damage/Defense/Endurance Un-Nerfs
	[Header("defenseDamage")]
	[ReloadRequired]
	[DefaultValue(true)]
	public bool DefenseDamage;

	[ReloadRequired]
	[DefaultValue(true)]
	public bool SummonDamage;

	[ReloadRequired]
	[DefaultValue(true)]
	public bool DamageReductionCap;

	[ReloadRequired]
	[DefaultValue(true)]
	public bool WormScarf;

	[ReloadRequired]
	[DefaultValue(true)]
	public bool MagicPowerPotion;

	[ReloadRequired]
	[DefaultValue(true)]
	public bool FrozenTurtleShell;

	[ReloadRequired]
	[DefaultValue(true)]
	public bool DodgeItems;

	[Header("armorSetUnnerfs")]
	[ReloadRequired]
	[DefaultValue(true)]
	public bool MeteorArmorSet;

	[Header("crafts")]
	[ReloadRequired]
	[DefaultValue(false)]
	public bool VanillaZenithCraft;
}
