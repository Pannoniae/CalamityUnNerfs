using MonoMod.Cil;
using System.Reflection;
using Terraria.ModLoader;
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
