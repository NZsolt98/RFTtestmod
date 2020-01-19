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

namespace Test.Items.Melee
{
	public class IceSword : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Basic sword from ice");  
		}

		public override void SetDefaults() {
			item.damage = 15;           
			item.melee = true;         
			item.width = 34;            
			item.height = 34;           
			item.useTime = 30;          
			item.useAnimation = 30;         
			item.useStyle = 1;          
			item.knockBack = 4;        
			item.value = Item.buyPrice(silver: 30);           
			item.rare = 1;              
			item.UseSound = SoundID.Item1;      
			item.autoReuse = false;          
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SnowBlock, 20);
			recipe.AddIngredient(ItemID.IceBlock, 10);
			recipe.AddIngredient(ItemID.BorealWoodSword, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("IceSword"), 1);
			recipe.AddIngredient(ItemID.IceBlock, 5);
			recipe.AddIngredient(ItemID.Snowball, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(ItemID.IceBlade);
			recipe.AddRecipe();
		}
		
		
		public override void MeleeEffects(Player player, Rectangle hitbox) {
			if (Main.rand.NextBool(3)) {
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 15);
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(BuffID.Frostburn, 60);
		}
		
	}
}
