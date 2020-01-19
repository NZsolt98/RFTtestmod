using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Test.Projectiles
{
	public class IceCrystalBullet : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Example Bullet");     //The English name of the projectile
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 28;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults() {
			projectile.CloneDefaults(ProjectileID.CrystalBullet);
			aiType = ProjectileID.Bullet;
		}
		
		public override bool PreKill(int timeLeft) {
			projectile.type = ProjectileID.CrystalBullet;
			projectile.damage = 13;
			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(BuffID.Frostburn, 60);
		}
	}
}
