using System.Reflection;
using Terraria;
using Terraria.ModLoader;
using OrigCalPlayer = CalamityMod.CalPlayer.CalamityPlayer;

namespace CalamityFly.On;

public static class OnCalPlayer
{
	public delegate void hook_DealDefenseDamage(orig_DealDefenseDamage orig, OrigCalPlayer self, Player.HurtInfo hurtInfo,
		int customIncomingDamage,
		bool absolute);

	public delegate void orig_DealDefenseDamage(OrigCalPlayer self, Player.HurtInfo hurtInfo, int customIncomingDamage,
		bool absolute);

	public delegate void hook_ForceVariousEffects(orig_DealDefenseDamage orig, OrigCalPlayer self);

	public delegate void orig_ForceVariousEffects(OrigCalPlayer self);

	public static event hook_DealDefenseDamage DealDefenseDamage {
		add
		{
			var t = typeof(OrigCalPlayer);
			var m = t.GetMethod("DealDefenseDamage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

			MonoModHooks.Add(m, value);
		}
		remove {
			
		}
	}

	public delegate void hook_ModifyHitNPCWithProj(orig_ModifyHitNPCWithProj orig, OrigCalPlayer self, Terraria.Projectile proj, Terraria.NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection);

	public delegate void orig_ModifyHitNPCWithProj(OrigCalPlayer self, Terraria.Projectile proj, Terraria.NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection);

}
