using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Test.Items.Magic
{
	class SnowRod : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Not working yet");  //The (English) text shown below your weapon's name*/
		}

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.CrimsonRod);
			item.color = Color.LightBlue; // This just tweaks the weapon sprite in inventory, just so we know which ShadowBeam Staff is ours and not vanillas
			item.damage = 100; // Down from 53
			item.width = 35;
			item.height = 34;
			
		 	item.shoot = ProjectileID.IceSickle;
		}
	}
}