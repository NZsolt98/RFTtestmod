using Test.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Test.Items.Melee
{
	public class Yoyo : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoots out an example yoyo");
			ItemID.Sets.Yoyo[item.type] = true;
			ItemID.Sets.GamepadExtraRange[item.type] = 15;
			ItemID.Sets.GamepadSmartQuickReach[item.type] = true;
		}

		public override void SetDefaults() {
			item.useStyle = 5;
			item.width = 24;
			item.height = 24;
			item.useAnimation = 25;
			item.useTime = 25;
			item.shootSpeed = 16f;
			item.knockBack = 2.5f;
			item.damage = 9;
			item.rare = 0;

			item.melee = true;
			item.channel = true;
			item.noMelee = true;
			item.noUseGraphic = true;

			item.UseSound = SoundID.Item1;
			item.value = Item.sellPrice(silver: 1);
			item.shoot = mod.ProjectileType("Yoyo");
		}
	}
}
