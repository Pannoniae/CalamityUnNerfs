using CalamityFly.Config;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityFly;

public class CalamityFly : Mod
{
	public static CalamityFly Instance;
	internal static UnNerfsConfig config = ModContent.GetInstance<UnNerfsConfig>();

	public override void Load()
	{
		Instance = this;
	}

	public override void AddRecipes()
	{
		if(config.VanillaZenithCraft)
		{
			Recipe.Create(ItemID.Zenith)
			.AddIngredient(ItemID.TerraBlade)
			.AddIngredient(ItemID.Meowmere)
			.AddIngredient(ItemID.StarWrath)
			.AddIngredient(ItemID.InfluxWaver)
			.AddIngredient(ItemID.TheHorsemansBlade)
			.AddIngredient(ItemID.Seedler)
			.AddIngredient(ItemID.Starfury)
			.AddIngredient(ItemID.BeeKeeper)
			.AddIngredient(ItemID.EnchantedSword)
			.AddIngredient(ItemID.CopperShortsword)
			.AddTile(TileID.MythrilAnvil)
			.Register();
		}
	}

#if DEBUG
	public static void LogCursor(ILCursor cursor, int limit = int.MaxValue, bool resetIndex = true)
	{
		if(resetIndex) cursor.Index = 0;
		int c = 0;
		do
		{
			Instance.Logger.Info($"{cursor.Next?.OpCode} {cursor.Next?.Operand}");
			c++;
		} while (cursor.TryGotoNext() && c < limit);
	}
#endif

	public override void Unload()
	{
		Instance = null;
	}
}