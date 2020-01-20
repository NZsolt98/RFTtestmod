using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Test.Projectiles
{
	public class HydraTalonsArrow : ModProjectile 
	{
		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 38;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.ranged = true;
			aiType = ProjectileID.WoodenArrowFriendly;
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
		}
		
		public override void AI()
		{
			int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 4, projectile.height + 4, 15, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 15, default(Color), 0.75f); //Spawns dust
			Main.dust[DustID].noGravity = true;

		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) //When you hit an NPC
		{
			Player player = Main.player[projectile.owner];
			float projectilepositionY = Main.rand.Next(40);
			float speedX = -projectile.velocity.X * Main.rand.NextFloat(0.15f, 0.3f);
			float speedY = 0f;
			float projectileknockBack = 4f;
			float projectilecenter = projectile.position.X+projectile.position.Y;
			int projectiledamage = 96;

			if (projectilecenter >= -projectile.owner)
			{
				float projectilepositionX = -projectile.velocity.X*Main.rand.Next(0,15);
				int Proj = Projectile.NewProjectile(target.position.X-projectilepositionX, target.position.Y+projectilepositionY, speedX, speedY, mod.ProjectileType<HydraTalons>(), projectiledamage, projectileknockBack, projectile.owner, 0f, 0f);
			}

			if(projectilecenter <= +projectile.owner)
			{
				float projectilepositionX = -projectile.velocity.X*Main.rand.Next(-15,0);
				int Proj = Projectile.NewProjectile(target.position.X+projectilepositionX, target.position.Y+projectilepositionY, speedX, speedY, mod.ProjectileType<HydraTalons>(), projectiledamage, projectileknockBack, projectile.owner, 0f, 0f);
			}
		}
	}
}