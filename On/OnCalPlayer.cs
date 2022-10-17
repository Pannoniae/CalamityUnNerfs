using MonoMod.RuntimeDetour.HookGen;
using System.Reflection;
using OrigCalPlayer = CalamityMod.CalPlayer.CalamityPlayer;

namespace CalamityFly.On;

public static class OnCalPlayer
{
	public delegate void hook_DealDefenseDamage(orig_DealDefenseDamage orig, OrigCalPlayer self, int damage, double realDamage);

	public delegate void orig_DealDefenseDamage(OrigCalPlayer self, int damage, double realDamage);

	public delegate void hook_ForceVariousEffects(orig_DealDefenseDamage orig, OrigCalPlayer self);

	public delegate void orig_ForceVariousEffects(OrigCalPlayer self);

	public static event hook_DealDefenseDamage DealDefenseDamage
	{
		add
		{
			var t = typeof(OrigCalPlayer);
			var m = t.GetMethod("DealDefenseDamage", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

			HookEndpointManager.Add<hook_DealDefenseDamage>(m, value);
		}
		remove
		{
			var t = typeof(OrigCalPlayer);
			var m = t.GetMethod("DealDefenseDamage", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

			HookEndpointManager.Remove<hook_DealDefenseDamage>(m, value);
		}
	}

	public delegate void hook_ModifyHitNPCWithProj(orig_ModifyHitNPCWithProj orig, OrigCalPlayer self, Terraria.Projectile proj, Terraria.NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection);

	public delegate void orig_ModifyHitNPCWithProj(OrigCalPlayer self, Terraria.Projectile proj, Terraria.NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection);

}
