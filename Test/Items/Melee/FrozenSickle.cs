using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Test.Items.Melee
{
	public class FrozenSickle : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Frozen sickles.");
		}

		public override void SetDefaults() {
			item.damage = 54;
			item.melee = true;
			item.width = 48;
			item.height = 48; 
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = Item.buyPrice(gold: 5);
			item.rare = 7;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		 	item.shoot = ProjectileID.IceSickle;
			item.shootSpeed = 40f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IceSickle, 1);
			recipe.AddIngredient(ItemID.Frostbrand, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage * 1, knockBack, player.whoAmI);
            }
			return false;
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(BuffID.Frostburn, 300);
		}
	}
}
