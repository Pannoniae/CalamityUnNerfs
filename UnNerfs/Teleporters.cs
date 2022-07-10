using CalamityFly.Config;
using CalamityMod.ILEditing;
using MonoMod.RuntimeDetour.HookGen;
using System.Reflection;
using System;
using Terraria;

namespace CalamityFly.UnNerfs;

internal class Teleporters : BaseUnNerf
{
	protected override bool Active(UnNerfsConfig config) => config.Teleporters;

	protected override void Apply()
	{
		var origT = typeof(Wiring);
		var origM = origT.GetMethod("Teleport", BindingFlags.Static | BindingFlags.NonPublic);
		var modT = typeof(ILChanges);
		var modM = modT.GetMethod("DisableTeleporters", BindingFlags.Static | BindingFlags.NonPublic);
		Delegate del = modM.CreateDelegate(typeof(global::On.Terraria.Wiring.hook_Teleport));
		HookEndpointManager.Remove(origM, del);
	}

	protected override void Revert()
	{}
}
