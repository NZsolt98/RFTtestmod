using System; 
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace Test.Items.Magic
{
	class TomeOfIceball : ModItem
	{
		public override void SetStaticDefaults() {
			//Tooltip.SetDefault("");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults() {
			item.damage = 88;
			item.magic = true;
			item.mana = 30;
			item.width = 25;
			item.height = 30;
			item.useTime = 40;
			item.useAnimation = 30;
			item.useStyle = 4;
			item.noMelee = true;
			item.knockBack = 7;
			item.value = Item.buyPrice(gold: 300);
			item.rare = 9;
			item.UseSound = SoundID.Item30;
			item.autoReuse = true;
		 	item.shoot = mod.ProjectileType("IceBall");
			item.shootSpeed = 50f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SpellTome, 2);
			recipe.AddIngredient(ItemID.IceBlock, 12);
			recipe.AddIngredient(ItemID.SoulofFright, 8);
			recipe.AddIngredient(ItemID.SoulofSight, 8);
			recipe.AddIngredient(ItemID.SoulofMight, 8);
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
	
	
    public class IceBall : ModProjectile
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
			Dust dust = Dust.NewDustPerfect(dustPosition,  15, null, 100, Color.White, 1.5f);
			dust.velocity *= 1f;
			dust.noGravity = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 150); 
		}

    }
}