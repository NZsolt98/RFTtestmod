using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Test.Items.MagicMissiles
{
	public class LunaticMissile : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Lunatic powers in it");
		}

		public override void SetDefaults() {
			item.damage = 240;
			item.magic = true;
			item.mana = 65;
			item.width = 26;
			item.height = 26;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 1;
			item.noMelee = true;
			item.channel = true;
			item.knockBack = 8;
			item.value = Item.sellPrice(silver : 50);
			item.rare = 3;
			item.UseSound = SoundID.Item9;
			item.shoot = mod.ProjectileType<LunaticMissileHead>();
			item.shootSpeed = 10f;
		}

		public override void ModifyManaCost(Player player, ref float reduce, ref float mult) {
			double currentTime = Main.time;
			double maxTime = Main.dayTime ? Main.dayLength : Main.nightLength;
			int direction = Main.dayTime ? 1 : -1;
			float timeMult = (float)Math.Sin(currentTime / maxTime * Math.PI);
			timeMult = 1 + timeMult * direction * 0.5f;
			mult *= timeMult;
		}
		
		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MagicMissile, 1);
			recipe.AddIngredient(ItemID.IceBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}


	public class LunaticMissileHead : ModProjectile
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
			target.AddBuff(BuffID.Frostburn, 300);

			if (Main.dayTime == false)
			{
				target.AddBuff(BuffID.CursedInferno, 300);
				target.AddBuff(BuffID.Ichor, 300);
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
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType<Projectiles.FrostBlast>(), (int) (projectile.damage * 1.5), projectile.knockBack, Main.myPlayer);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType<Projectiles.FrostSoul>(), (int) (projectile.damage * 2), projectile.knockBack, Main.myPlayer);
		}
	}
}