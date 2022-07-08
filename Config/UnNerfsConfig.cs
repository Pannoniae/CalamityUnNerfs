using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace CalamityFly.Config;

[Label("Un-Nerfs Config")]
public class UnNerfsConfig : ModConfig
{
	public override ConfigScope Mode => ConfigScope.ServerSide;

	[Header("Un-Nerfs")]
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

	[Label("Defense Damage")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("True to remove defense damage from calamity")]
	public bool DefenseDamage;

	[Label("Summon Damage")]
	[ReloadRequired]
	[DefaultValue(true)]
	[Tooltip("True to remove damage reduction to minions while holding a weapon of another class")]
	public bool SummonDamage;

	[Label("Magic Mirror")]
	[ReloadRequired]
	[DefaultValue(true)]
	[Tooltip("True to allow use of magic mirror/recall potion while fighting a boss")]
	public bool MagicMirror;

	[ReloadRequired]
	[DefaultValue(true)]
	[Tooltip("True to allow use of teleporters while fighting a boss")]
	public bool Teleporters;

	[Header("Crafts")]
	[Label("Zenith Vanilla Craft")]
	[ReloadRequired]
	[DefaultValue(false)]
	[Tooltip("Set to true to add an alternate vanilla craft to Zenith but without auric bar, the calamity craft will stay, only not use it")]
	public bool VanillaZenithCraft;

}
