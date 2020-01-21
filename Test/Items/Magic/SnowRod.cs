using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Test.Items.Magic
{
	class SnowRod : ModItem
	{
		public override void SetStaticDefaults() {
			//Tooltip.SetDefault("");  The (English) text shown below your weapon's name
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.CrimsonRod);
			item.color = Color.LightBlue; // This just tweaks the weapon sprite in inventory, just so we know which ShadowBeam Staff is ours and not vanillas
			item.damage = 100; // Down from 53
			item.magic = true;
			item.mana = 10;
			item.width = 35;
			item.height = 34;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 4;
			item.noMelee = true;
			item.knockBack = 2;
			item.value = Item.buyPrice(gold: 60);
			item.rare = 20;
			item.UseSound = SoundID.Item30;
			item.autoReuse = true;
			item.shoot = ProjectileID.IceSickle;
			item.shootSpeed = 35f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpellTome, 1);
			recipe.AddIngredient(ItemID.IceBlock, 10);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddTile(TileID.Bookcases);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 4; // 3 shots
			float rotation = MathHelper.ToRadians(13);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
	}

	public class SnowRod : ModProjectile
	{

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 11;
			projectile.alpha = 50;
			projectile.timeLeft = 500;
			projectile.penetrate = -1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
		}


		public override void AI()
		{
			Vector2 dustPosition = projectile.Center + new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5));
			Dust dust = Dust.NewDustPerfect(dustPosition, 15, null, 100, Color.White, 1.5f);
			dust.velocity *= 1f;
			dust.noGravity = true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Frostburn, 120);
		}

	}
}
}