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


namespace Test.Projectiles
{
    class IceBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 11;
			projectile.timeLeft = 37;
			projectile.penetrate = 4;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
        }
		
		
        public override void AI()
        {
            Vector2 dustPosition = projectile.Center + new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5));
			Dust dust = Dust.NewDustPerfect(dustPosition,  15, null, 100, Color.White, 2.5f);
			dust.velocity *= 1f;
			dust.noGravity = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 600); 
		}

		public override bool PreKill(int timeLeft)
		{
			projectile.damage = 78;
			int explosionArea = 200;
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
          	return true;
		}

		public override void Kill(int timeLeft) {
			Main.PlaySound(SoundID.Item10, projectile.position);
			for (int i = 0; i < 200; i++) 
			{
				Dust dust = Dust.NewDustDirect(projectile.position - projectile.velocity, projectile.width, projectile.height, 15, 0, 0, 100, default(Color), 2);
				dust.noGravity = true;
				dust.velocity *= 5f;
			}
		}
    }
}