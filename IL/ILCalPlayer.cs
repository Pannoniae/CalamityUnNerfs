using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using System.Reflection;
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

	public static event ILContext.Manipulator ForceVariousEffects
	{
		add
		{
			var t = typeof(OrigCalPlayer);
			var n = t.GetMethod("ForceVariousEffects", BindingFlags.Instance | BindingFlags.NonPublic);
			if(n == null)
			{
				CalamityFly.Instance.Logger.Warn("Unable to Il Edit method OrigCalPlayer.ForceVariousEffects (error:1)");
				return;
			}
			HookEndpointManager.Modify<hook_ForceVariousEffects>(n, value);
		}
		remove
		{
			var t = typeof(OrigCalPlayer);
			var n = t.GetMethod("ForceVariousEffects", BindingFlags.Instance | BindingFlags.NonPublic);
			if (n == null)
			{
				CalamityFly.Instance.Logger.Warn("Unable to Il Edit method OrigCalPlayer.ForceVariousEffects (error:2)");
				return;
			}
			HookEndpointManager.Unmodify<hook_ForceVariousEffects>(n, value);
		}
	}

}
