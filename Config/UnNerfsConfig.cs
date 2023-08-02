using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CalamityFly.Config;

[Label("Un-Nerfs Config")]
public class UnNerfsConfig : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Header("unnerfs")]
	[Label("Soaring Insignia Flight")]
	[ReloadRequired]
	[DefaultValue(true)]
	[Tooltip("true to remove the infinite flight calamity nerf to the soaring insignia, false to keep the nerf")]
	public bool SoaringInsigniaFlight;

	[Label("Soaring Insignia Movement")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("True to remove the movement calamity nerf to the soaring insignia, false to keep the nerf")]
	public bool SoaringInsigniaMovement;

	[Label("Magiluminescence Stats")]
	[ReloadRequired]
	[DefaultValue(true)]
	[Tooltip("True to remove the stats calamity nerf to the magiluminescence, false to keep the nerf")]
	public bool Magiluminescence;

	[Label("Rod Of Discord")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("True to allow the rod of discord to be used while chaos state is active, also use vanilla bebuff duration")]
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
	
	[Label("Yoyo Bag And Glove")]
	[ReloadRequired]
	[DefaultValue(true)]
	[Tooltip("True to remove damage reduction of Yoyo Bag and Yoyo Glove")]
	public bool YoyoBag;
	
	//Damage/Defense/Endurance Un-Nerfs
	[Header("defenseDamage")]
	[Label("Defense Damage")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("True to remove defense damage from calamity")]
	public bool DefenseDamage;

	[Label("Summon Damage")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("True to remove damage reduction to minions while holding a weapon of another class")]
	public bool SummonDamage;

	[Label("Damage Reduction Cap")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("True to remove damage reduction cap of calamity")]
	public bool DamageReductionCap;

	[Label("Worm Scarf")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("True to make the Worm Scarf grants 17% damage reduction again")]
	public bool WormScarf;

	[Label("Endurance Potion")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("True to make the Endurance Potion grants 10% damage reduction again")]
	public bool EndurancePotion;

	[Label("Magic Power Potion")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("True to make the Magic Power Potion grants 20% magic damage again")]
	public bool MagicPowerPotion;

	[Label("Frozen Turtle Shell")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("True to make the Frozen Turtle Shell grants 25% damage reduction again")]
	public bool FrozenTurtleShell;

	[Label("Dodge Items")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("True to make the Brain of Confusion and Master Ninja Gear not use calamity global cd instead restore vanilla behavior")]
	public bool DodgeItems;

	[Header("armorSetUnnerfs")]
	[Label("Meteor Armor Set")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("True to make the Meteor Armor Set remove the space gun's mana consumption instead of halving it")]
	public bool MeteorArmorSet;

	[Header("crafts")]
	[Label("Zenith Vanilla Craft")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("Set to true to add an alternate vanilla craft to Zenith but without auric bar, the calamity craft will stay, only not use it")]
	public bool VanillaZenithCraft;
}
