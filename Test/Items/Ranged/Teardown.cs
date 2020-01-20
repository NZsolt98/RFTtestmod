using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Test.Items.Ranged
{
	public class Teardown : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Talons of Second Head, what tear enemies apart");
		}

		public override void SetDefaults()
		{
		item.damage = 48;
		item.ranged = true;
		item.width = 54;
		item.height = 96;
		item.maxStack = 1;
		item.useTime = 25;
		item.useAnimation = 25;
		item.useStyle = 5;
		item.knockBack = 2;
		item.value = Item.buyPrice(silver: 81);
		item.rare = 9;
		item.UseSound = SoundID.Item5;
		item.noMelee = true;
		item.shoot = 1;
		item.useAmmo = AmmoID.Arrow;
		item.shootSpeed = 9f;
		item.autoReuse = true;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IceBlock, 30);
			recipe.AddIngredient(ItemID.Phantasm, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int i = 0; i < 4; i++) 
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType<Projectiles.HydraTalonsArrow>(), damage, knockBack, player.whoAmI);
			}
			
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-20, 0);
		}
	}
}