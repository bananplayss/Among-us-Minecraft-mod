using bananplaysshu;
using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	internal class InventoryItemDatabase : MonoBehaviour {

		[HideFromIl2Cpp]
		public static InventoryItemDatabase Instance { get; private set; }

		[HideFromIl2Cpp]
		public List<InventoryItem> inventoryItems { get; set; }

		public InventoryItemDatabase(IntPtr ptr) : base(ptr) { }

		public enum InventoryItemsEnum {
			//refactor
			None,
			Wood,
			WoodenPlanks,
			Stone,
			Cobblestone,
			Stick,
			WoodenPickaxe,
			StonePickaxe,
			Bucket,
			BucketOfLava,
			Iron,
			Spyglass,
			Gunpowder,
			Trident,
			IronSword,
			EyeOfEnder,
			BlazeRod,
			EnderPearl,
			BlazePowder,
			TNT,
			Sand,
			Plushie,

		}

		private void Awake() {
			Instance = this;
			inventoryItems = new List<InventoryItem>();
		}

		public void AddInventoryItemToDatabase(InventoryItem item) {
			inventoryItems.Add(item);
		}

		public InventoryItem ReturnItemByEnumName(InventoryItemsEnum itemToReturn) {
			foreach (InventoryItem item in inventoryItems) {

				if (item.name.ToString().ToUpper() == itemToReturn.ToString().ToUpper()) {
					return item;
				}
			}
			return null;
		}


	}


	[HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
	public static class InventoryItemPatch {
		[HarmonyPriority(Priority.First)]
		public static void Postfix() {
			//refactor
			#region Initialize Database
			GameObject dbGo = new GameObject("InventoryItemDatabase");
			dbGo.AddComponent<InventoryItemDatabase>();
			#endregion

			#region Register inventory items
			//Wood
			Sprite woodSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.WoodItem.png", 150);
			woodSprite.name = woodSprite.ToString();
			InventoryItem wood = new InventoryItem("Wood", woodSprite, 64, InventoryItemTypes.BlockType);

			//Stone
			Sprite stoneSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Stone.png", 160);
			stoneSprite.name = stoneSprite.ToString();
			InventoryItem stone = new InventoryItem("Stone", stoneSprite, 64, InventoryItemTypes.BlockType);

			//Cobblestone
			Sprite cobblestoneSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Cobblestone.png", 160);
			cobblestoneSprite.name = cobblestoneSprite.ToString();
			InventoryItem cobblestone = new InventoryItem("Cobblestone", cobblestoneSprite, 64, InventoryItemTypes.BlockType);

			//Wooden Planks
			Sprite woodenPlankSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.WoodenPlanks.png", 160);
			InventoryItem woodenPlanks = new InventoryItem("WoodenPlanks", woodenPlankSprite, 64, InventoryItemTypes.BlockType);

			//Stick
			Sprite stickSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Stick.png", 160);
			InventoryItem stick = new InventoryItem("Stick", stickSprite, 64, InventoryItemTypes.ItemType);

			//Wooden Pickaxe
			Sprite woodenPickaxeSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.WoodenPickaxe.png", 160);
			InventoryItem woodenPickaxe = new InventoryItem("WoodenPickaxe", woodenPickaxeSprite, 1, InventoryItemTypes.ItemType);

			//Stone Pickaxe
			Sprite stonePickaxeSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.StonePickaxe.png", 160);
			stonePickaxeSprite.name = stonePickaxeSprite.ToString();
			InventoryItem stonePickaxe = new InventoryItem("StonePickaxe", stonePickaxeSprite, 1, InventoryItemTypes.ItemType);

			//Bucket
			Sprite bucketSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Bucket.png", 160);
			bucketSprite.name = bucketSprite.ToString();
			InventoryItem bucket = new InventoryItem("Bucket", bucketSprite, 10, InventoryItemTypes.ItemType);

			//Bucket Of Lava
			Sprite bucketOfLavaSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.BucketOfLava.png", 160);
			bucketOfLavaSprite.name = bucketOfLavaSprite.ToString();
			InventoryItem bucketOfLava = new InventoryItem("BucketOfLava", bucketOfLavaSprite, 10, InventoryItemTypes.ItemType);

			//Iron
			Sprite ironSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Iron.png", 160);
			ironSprite.name = ironSprite.ToString();
			InventoryItem iron = new InventoryItem("Iron", ironSprite, 64, InventoryItemTypes.BlockType);

			//Spyglass
			Sprite spyglassSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Spyglass.png", 160);
			spyglassSprite.name = spyglassSprite.ToString();
			InventoryItem spyglass = new InventoryItem("Spyglass", spyglassSprite, 1, InventoryItemTypes.ItemType);

			//Gunpowder
			Sprite gunpowderSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Gunpowder.png", 160);
			gunpowderSprite.name = gunpowderSprite.ToString();
			InventoryItem gunpowder = new InventoryItem("Gunpowder", gunpowderSprite, 64, InventoryItemTypes.BlockType);

			//Trident
			Sprite tridentSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Trident.png", 160);
			tridentSprite.name = tridentSprite.ToString();
			InventoryItem trident = new InventoryItem("Trident", tridentSprite, 1, InventoryItemTypes.ItemType);

			//Iron sword
			Sprite ironswordSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.IronSword.png", 160);
			ironswordSprite.name = ironswordSprite.ToString();
			InventoryItem ironSword = new InventoryItem("IronSword", ironswordSprite, 1, InventoryItemTypes.ItemType);

			//Eye of Ender
			Sprite eoeSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.EyeOfEnder.png", 160);
			eoeSprite.name = eoeSprite.ToString();
			InventoryItem eyeofender = new InventoryItem("EyeOfEnder", eoeSprite, 16, InventoryItemTypes.BlockType);

			//BlazeRod
			Sprite blazerodSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.BlazeRod.png", 160);
			blazerodSprite.name = blazerodSprite.ToString();
			InventoryItem blazeRod = new InventoryItem("BLazeRod", blazerodSprite, 64, InventoryItemTypes.BlockType);

			//EnderPearl
			Sprite epSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.EnderPearl.png", 160);
			epSprite.name = epSprite.ToString();
			InventoryItem enderPearl = new InventoryItem("EnderPearl", epSprite, 64, InventoryItemTypes.BlockType);

			//BlazePowder
			Sprite bpSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.BlazePowder.png", 160);
			bpSprite.name = bpSprite.ToString();
			InventoryItem blazePowder = new InventoryItem("BlazePowder", bpSprite, 64, InventoryItemTypes.BlockType);

			//TNT
			Sprite tntSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.TNT.png", 160);
			tntSprite.name = tntSprite.ToString();
			InventoryItem tnt = new InventoryItem("TNT", tntSprite, 64, InventoryItemTypes.BlockType);

			//Sand
			Sprite sandSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Sand.png", 160);
			sandSprite.name = sandSprite.ToString();
			InventoryItem sand = new InventoryItem("Sand", sandSprite, 64, InventoryItemTypes.BlockType);


			//Plushie
			Sprite plushieSprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Plushie.png", 900);
			plushieSprite.name = plushieSprite.ToString();
			InventoryItem plushie = new InventoryItem("Plushie", plushieSprite, 1, InventoryItemTypes.BlockType);


			#region Add items to database
			InventoryItemDatabase db = InventoryItemDatabase.Instance;
			db.AddInventoryItemToDatabase(wood);
			db.AddInventoryItemToDatabase(woodenPlanks);
			db.AddInventoryItemToDatabase(stone);
			db.AddInventoryItemToDatabase(cobblestone);
			db.AddInventoryItemToDatabase(stick);
			db.AddInventoryItemToDatabase(woodenPickaxe);
			db.AddInventoryItemToDatabase(stonePickaxe);
			db.AddInventoryItemToDatabase(bucket);
			db.AddInventoryItemToDatabase(bucketOfLava);
			db.AddInventoryItemToDatabase(iron);
			db.AddInventoryItemToDatabase(spyglass);
			db.AddInventoryItemToDatabase(gunpowder);
			db.AddInventoryItemToDatabase(trident);
			db.AddInventoryItemToDatabase(ironSword);
			db.AddInventoryItemToDatabase(eyeofender);
			db.AddInventoryItemToDatabase(blazeRod);
			db.AddInventoryItemToDatabase(enderPearl);
			db.AddInventoryItemToDatabase(blazePowder);
			db.AddInventoryItemToDatabase(tnt);
			db.AddInventoryItemToDatabase(sand);

			//Bonus content
			db.AddInventoryItemToDatabase(plushie);
			#endregion
			#endregion
		}
	}
}
