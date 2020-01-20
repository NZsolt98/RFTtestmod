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
	public class TrueNorthBlade : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Exploding ice shards");
		}

		public override void SetDefaults() {
			item.damage = 57;
			item.melee = true;
			item.width = 82;
			item.height = 82;
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = 1;
			item.knockBack = 7;
			item.value = Item.buyPrice(gold: 8);
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("IceBolt");
			item.shootSpeed = 22f;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Frostbrand, 1);
			recipe.AddIngredient(ItemID.FrostCore, 10);
			recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		
		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit) {
			target.AddBuff(BuffID.Frostburn, 600);
		}
	}
}
