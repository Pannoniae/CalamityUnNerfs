using MonoMod.Cil;
using MonoMod.RuntimeDetour.HookGen;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;
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
			MonoModHooks.Modify(n, value);
			//hook_ModifyHitNPCWithProj
		}
		remove
		{
			
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
			MonoModHooks.Modify(n, value);
			//hook_ForceVariousEffects
		}
		remove
		{
			
		}
	}

}
