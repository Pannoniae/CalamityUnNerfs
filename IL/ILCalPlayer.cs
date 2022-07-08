using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;

using static CalamityFly.On.OnCalPlayer;
using OrigCalPlayer = CalamityMod.CalPlayer.CalamityPlayer;

namespace CalamityFly.IL;

public static class IlCalPlayer
{
	public static event ILContext.Manipulator ModifyHitNPCWithProj
	{
		add
		{
			var t = typeof(OrigCalPlayer);
			var n = t.GetMethod("ModifyHitNPCWithProj");
			HookEndpointManager.Modify<hook_ModifyHitNPCWithProj>(n, value);
		}
		remove
		{
			var t = typeof(OrigCalPlayer);
			var n = t.GetMethod("ModifyHitNPCWithProj");
			HookEndpointManager.Unmodify<hook_ModifyHitNPCWithProj>(n, value);
		}
	}


}
