using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityFly;

public class GItem : GlobalItem
{
	public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
	{
		if(item.type == ItemID.EmpressFlightBooster)
		{
			var old = tooltips.Find(t=>t.Text.Contains("50%"));
			if (old != null)
			{
				var i = tooltips.IndexOf(old);
				tooltips[i] = new TooltipLine(CalamityFly.Instance, old.Name, "Grants infinite wing and rocket boot flight");
			}


		}
		
	}
}
