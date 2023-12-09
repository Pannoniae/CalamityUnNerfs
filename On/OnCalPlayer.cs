using System.Reflection;
using Terraria;
using Terraria.ModLoader;
using OrigCalPlayer = CalamityMod.CalPlayer.CalamityPlayer;

namespace CalamityFly.On;

public static class OnCalPlayer
{
	public delegate void hook_ApplyDefenseDamageInternal(orig_ApplyDefenseDamageInternal orig, OrigCalPlayer self,
		int defenseDamage, bool showVisuals);

	public delegate void orig_ApplyDefenseDamageInternal(OrigCalPlayer self, int defenseDamage, bool showVisuals);

	public delegate void hook_ForceVariousEffects(orig_ApplyDefenseDamageInternal orig, OrigCalPlayer self);

	public delegate void orig_ForceVariousEffects(OrigCalPlayer self);

	public static event hook_ApplyDefenseDamageInternal ApplyDefenseDamageInternal {
		add
		{
			var t = typeof(OrigCalPlayer);
			var m = t.GetMethod("ApplyDefenseDamageInternal", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);

			MonoModHooks.Add(m, value);
		}
		remove {
			
		}
	}

	public delegate void hook_ModifyHitNPCWithProj(orig_ModifyHitNPCWithProj orig, OrigCalPlayer self, Terraria.Projectile proj, Terraria.NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection);

	public delegate void orig_ModifyHitNPCWithProj(OrigCalPlayer self, Terraria.Projectile proj, Terraria.NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection);

}
