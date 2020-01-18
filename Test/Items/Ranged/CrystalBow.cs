using System;
using System.Collections.Generic;
using System.Diagnostics;
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


namespace Test.Items.Ranged
{
	public class CrystalBow : ModItem
	{
		public override void SetDefaults()
		{
			item.damage = 26;
			item.ranged = true;
			item.width = 18;
			item.height = 36;
			item.maxStack = 1;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = 5;
			item.knockBack = 2;
			item.value = Item.buyPrice(silver: 81);
			item.rare = 4;
			item.UseSound = SoundID.Item5;
			item.noMelee = true;
			item.shoot = 1;
			item.useAmmo = AmmoID.Arrow;
			item.shootSpeed = 20f;
			item.autoReuse = true;
		}
	}
}