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
	public class HydraTalons : ModProjectile 
	{
		public override void SetDefaults()
		{
			projectile.width = 90;
			projectile.height = 44;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.ranged = true;
			projectile.timeLeft = 90;
			projectile.penetrate = -1;
			projectile.alpha = 65;
			aiType = ProjectileID.SwordBeam;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			if (projectile.timeLeft < 30)
			{
				projectile.alpha += 5;
			}

			projectile.spriteDirection = projectile.direction = (projectile.velocity.X > 0).ToDirectionInt();
			projectile.rotation = projectile.velocity.ToRotation() + (projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
			
			/*
			if(projectile.frameCounter < 30)
			{
				projectile.frame = 0;
			}
			else if(projectile.frameCounter >= 30 && projectile.frameCounter < 60)
			{
				projectile.frame = 1;
			}
			else if(projectile.frameCounter >= 60 && projectile.frameCounter < 90)
			{
				projectile.frame = 2;
			}*/
		}
	}
}