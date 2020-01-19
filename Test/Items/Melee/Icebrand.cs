using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Test.Items.Melee
{
	public class Icebrand : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Second attack to release Icebolts.");
		}

		public override void SetDefaults() {
			item.damage = 34;
			item.melee = true;
			item.width = 46;
			item.height = 56;
			item.useTime = 21;
			item.useAnimation = 21;
			item.useStyle = 1;
			item.knockBack = 4.25f;
			item.value = Item.buyPrice(silver: 92);
			item.rare = 3;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IceBlade, 1);
			recipe.AddIngredient(ItemID.IceBlock, 30);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(BuffID.Frostburn, 60);
		}
		
		public override bool AltFunctionUse(Player player) {
			return true;
		}
		
		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2) {
				item.useStyle = 1;
				item.useTime = 50;
				item.useAnimation = 50;
				item.damage = 51;
				item.shoot = ProjectileID.IceBolt;
				item.shootSpeed = 33f;
				item.noMelee = true;
			}
			else {
				item.useStyle = 1;
				item.useTime = 21;
				item.useAnimation = 21;
				item.damage = 34;
				item.shoot = 0;
				item.noMelee = false;
			}
			return base.CanUseItem(player);
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
			return false;
		}
	}
}
