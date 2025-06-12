using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace bananplaysshu.Patches {
	[HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
	public class GameManagerPatch {
			public static void Postfix() {
			#region CraftingInventory
				GameObject craftinginventoryGO = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.craftingInventory);
				CraftingInventory craftingInventory = craftinginventoryGO.GetComponent<CraftingInventory>();

				craftinginventoryGO.transform.parent = HudManager.Instance.transform;
				craftinginventoryGO.transform.localScale *= .5f;

				craftinginventoryGO.layer = LayerMask.NameToLayer("UI");

				CraftingInventory.Instance.SetActive(false);
				#endregion

			#region CraftingTableManager
					Vector3[] wbPosArray = new Vector3[] { new Vector3(6.2f, -9.3f, 0f), new Vector3(16.9f, -5.8f),
					new Vector3(4.6f, 3.0f, 0.0f), new Vector3(-5.9f, 3.9f, 0.0f), new Vector3(6.2f, -3.2f, 0.0f) };


					for (int i = 0; i < 5 + 1; i++) {
						GameObject workbench = new GameObject("Workbench");
						SpriteRenderer sr = workbench.AddComponent<SpriteRenderer>();
						sr.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CraftingTable.png", 180);
						workbench.transform.position = wbPosArray[i];
						workbench.AddComponent<CraftingTableManager>();
					}
				#endregion

			#region EnderPearlAbility

				GameObject enderPearlMap = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.enderPearlMap);
				EnderPearlAbility ab = enderPearlMap.GetComponent<EnderPearlAbility>();

				enderPearlMap.transform.parent = HudManager.Instance.transform;
				enderPearlMap.transform.localScale *= .6f;
				enderPearlMap.transform.position += new Vector3(-1.5f, .2f, -100);

				enderPearlMap.layer = LayerMask.NameToLayer("UI");

				ab.Hide();
			#endregion

			#region GatherResource

			Vector3[] stonePackPosArray = new Vector3[] { new Vector3(9.2f, -12.9f, 1f), new Vector3(-13.4f, -7f, 0.0f) };
			Vector3[] ironPackPosArray = new Vector3[] { new Vector3(-22.1f, -8.1f, 1f), new Vector3(3f, -16f, 0.0f) };

			for (int i = 0; i < 2 + 1; i++) {
				//Stone packs
				GameObject stonePack = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.stonePack, stonePackPosArray[i], Quaternion.identity);
				InventoryItem cobblestone = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Cobblestone);
				stonePack.GetComponent<GatherResource>().SetGatherableResource(cobblestone);
				stonePack.transform.localScale *= .5f;
				stonePack.GetComponent<DynamicSorting>().SetOffset(.8f);

				//Iron packs
				GameObject ironPack = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.ironPack, new Vector3(-22.1f, -8.1f, 1f), Quaternion.identity);
				ironPack.GetComponent<GatherResource>().SetGatherableResource(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Iron));
				ironPack.transform.localScale *= .6f;
				ironPack.GetComponent<DynamicSorting>().SetOffset(.4f);

			}



			//Iron pack
			GameObject lavaPool = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.lavaPool, new Vector3(-1.5f, -16.7f, 1f), Quaternion.identity);
			lavaPool.GetComponent<GatherResource>().SetGatherableResource(InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.BucketOfLava));
			lavaPool.GetComponent<GatherResource>().SetRequiredItem(
				InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Bucket));
			lavaPool.GetComponent<DynamicSorting>().SetToLavaPool();
			#endregion

			#region Hand
			Hand.InitializeHandRpc(PlayerControl.LocalPlayer);
			#endregion

			#region Hotbar
			GameObject hotbarGO = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.hotbar);
			Hotbar hotbar = hotbarGO.GetComponent<Hotbar>();

			hotbarGO.transform.localScale *= .5f;
			hotbarGO.transform.parent = HudManager.Instance.transform;
			hotbarGO.transform.localPosition = new Vector3(0.6f, -2.6f, 10.0f);


			hotbarGO.layer = LayerMask.NameToLayer("UI");
			#endregion

			#region InventorySystem
			InventorySystem invSystem = PlayerControl.LocalPlayer.gameObject.AddComponent<InventorySystem>();
			GameObject invObj = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.inventory);
			Inventory inv = invObj.GetComponent<Inventory>();


			invSystem.SetInventoryGo(invObj);
			invObj.transform.parent = HudManager.Instance.transform;
			invObj.transform.localScale *= .5f;

			invObj.layer = LayerMask.NameToLayer("UI");

			AspectPosition invObjPos = invObj.AddComponent<AspectPosition>();

			invObjPos.DistanceFromEdge = new Vector3(0, -.66f, -200);
			invObjPos.Update();
			#endregion

			#region MobSpawnManager
			GameObject mobSpawnManagerObj = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.mobSpawnManager);
			#endregion

			#region PickupSoundManager
			GameObject pickupSoundManagerGo = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.pickup);
			#endregion
		}
	}
}
