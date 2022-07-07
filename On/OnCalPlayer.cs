using MonoMod.RuntimeDetour.HookGen;
using System.ComponentModel;
using System.Reflection;
using OrigCalPlayer = CalamityMod.CalPlayer.CalamityPlayer;

namespace On.CalamityMod.CalPlayer;

public static class CalamityPlayer
{
	public delegate void hook_DealDefenseDamage(orig_DealDefenseDamage orig, OrigCalPlayer self, int damage);

	public delegate void orig_DealDefenseDamage(OrigCalPlayer self, int damage);

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


}
