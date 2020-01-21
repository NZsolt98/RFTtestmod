using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Test.Items.MagicMissiles
{
	public class RottenFrostMissile : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Apply ichor/cursed inferno buff.");
		}

		public override void SetDefaults() {
			item.damage = 67;
			item.magic = true;
			item.mana = 23;
			item.width = 26;
			item.height = 26;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.noMelee = true;
			item.channel = true;
			item.knockBack = 6;
			item.value = Item.sellPrice(silver : 50);
			item.rare = 3;
			item.UseSound = SoundID.Item9;
			item.shoot = mod.ProjectileType<RottenFrostMissileHead>();
			item.shootSpeed = 10f;
		}
		
		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("CorrupticFrsotMissile"), 1);
			recipe.AddIngredient(mod.ItemType("CrimonicFrsotMissile"), 1);
			recipe.AddIngredient(ItemID.CursedFlame, 5);
			recipe.AddIngredient(ItemID.Ichor, 5);
			recipe.AddIngredient(ItemID.SoulofFright, 5);
			recipe.AddIngredient(ItemID.SoulofMight, 5);
			recipe.AddIngredient(ItemID.SoulofSight, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}


	public class RottenFrostMissileHead : ModProjectile
	{
		public override void SetDefaults() {
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.light = 0.8f;
			projectile.magic = true;
			drawOriginOffsetY = -6;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(BuffID.Frostburn, 240);

			if (Main.dayTime == false)
			{
				target.AddBuff(BuffID.CursedInferno, 240);
				target.AddBuff(BuffID.Ichor, 240);
			}
		}

		public override void AI() {
			if (projectile.soundDelay == 0 && Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y) > 2f) 
			{
				projectile.soundDelay = 10;
				Main.PlaySound(SoundID.Item9, projectile.position);
			}

			Vector2 dustPosition = projectile.Center + new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5));
			Dust dust = Dust.NewDustPerfect(dustPosition,  63, null, 100, Color.White, 3f);
			dust.velocity *= 1f;
			dust.noGravity = true;

			if (Main.myPlayer == projectile.owner && projectile.ai[0] == 0f) 
			{
				Player player = Main.player[projectile.owner];

				if (player.channel) 
				{
					float maxDistance = 18f; 
					Vector2 vectorToCursor = Main.MouseWorld - projectile.Center;
					float distanceToCursor = vectorToCursor.Length();

					if (distanceToCursor > maxDistance) 
					{
						distanceToCursor = maxDistance / distanceToCursor;
						vectorToCursor *= distanceToCursor;
					}

					int velocityXBy1000 = (int)(vectorToCursor.X * 1000f);
					int oldVelocityXBy1000 = (int)(projectile.velocity.X * 1000f);
					int velocityYBy1000 = (int)(vectorToCursor.Y * 1000f);
					int oldVelocityYBy1000 = (int)(projectile.velocity.Y * 1000f);

					if (velocityXBy1000 != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000) 
					{
						projectile.netUpdate = true;
					}

					projectile.velocity = vectorToCursor;

				}
				else if (projectile.ai[0] == 0f) 
				{
					projectile.netUpdate = true;
					float maxDistance = 14f; 
					Vector2 vectorToCursor = Main.MouseWorld - projectile.Center;
					float distanceToCursor = vectorToCursor.Length();

					if (distanceToCursor == 0f) 
					{
						vectorToCursor = projectile.Center - player.Center;
						distanceToCursor = vectorToCursor.Length();
					}

					distanceToCursor = maxDistance / distanceToCursor;
					vectorToCursor *= distanceToCursor;
					projectile.velocity = vectorToCursor;

					if (projectile.velocity == Vector2.Zero) 
					{
						projectile.Kill();
					}

					projectile.ai[0] = 1f;
				}
			}
			
			if (projectile.velocity != Vector2.Zero) 
			{
				projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver4;
			}
		}
		public override void Kill(int timeLeft) {
			if (projectile.penetrate == 1) {
				projectile.maxPenetrate = -1;
				projectile.penetrate = -1;

				int explosionArea = 120;
				Vector2 oldSize = projectile.Size;

				projectile.position = projectile.Center;
				projectile.Size += new Vector2(explosionArea);
				projectile.Center = projectile.position;

				projectile.tileCollide = false;
				projectile.velocity *= 0.01f;

				projectile.Damage();
				projectile.scale = 0.01f;

				projectile.position = projectile.Center;
				projectile.Size = new Vector2(10);
				projectile.Center = projectile.position;
			}

			Main.PlaySound(SoundID.Item10, projectile.position);
			for (int i = 0; i < 10; i++) 
			{
				Dust dust = Dust.NewDustDirect(projectile.position - projectile.velocity, projectile.width, projectile.height, 63, 0, 0, 100, Color.White, 2);
				dust.noGravity = true;
				dust.velocity *= 2f;
				dust = Dust.NewDustDirect(projectile.position - projectile.velocity, projectile.width, projectile.height, 63, 0f, 0f, 100, Color.White, 2);
			}
		}
	}
}