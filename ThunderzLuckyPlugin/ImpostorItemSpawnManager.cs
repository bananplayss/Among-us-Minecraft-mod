using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class ImpostorItemSpawnManager : MonoBehaviour {
		public ImpostorItemSpawnManager(IntPtr ptr) : base(ptr) { }

		[HideFromIl2Cpp]
		public static ImpostorItemSpawnManager Instance { get; private set; }

		[HideFromIl2Cpp]
		public List<ImpostorItem> impostorItemList { get; private set; }

		public InventoryItem[] impostorItemInventoryItems;
		public Vector3[] impostorItemInventoryItemsSpawns;

		private float spawnRandomImpostorItemTimer = 50;
		private float spawnRandomImpostorItemTimerMax = 50f;


		private void Awake() {
			Instance = this;

			impostorItemList = new List<ImpostorItem>();
			impostorItemInventoryItems = new InventoryItem[10];
			impostorItemInventoryItemsSpawns = new Vector3[10];

			//refactor
			#region Bad code, no time left 
			#region are you sure?
			#region please dont
			#region nooooooo
			#region alright
			#region :c
			impostorItemInventoryItems[0] = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Spyglass);
			impostorItemInventoryItems[1] = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Spyglass);
			impostorItemInventoryItems[2] = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Gunpowder);
			impostorItemInventoryItems[3] = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Gunpowder);
			impostorItemInventoryItems[4] = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Trident);
			impostorItemInventoryItems[5] = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Trident);
			impostorItemInventoryItems[6] = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Sand);
			impostorItemInventoryItems[7] = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Sand);
			impostorItemInventoryItems[8] = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Gunpowder);
			impostorItemInventoryItems[9] = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Gunpowder);

			impostorItemInventoryItemsSpawns[0] = new Vector3(-0.8f, -8.0f);
			impostorItemInventoryItemsSpawns[1] = new Vector3(10.5f, -6.3f);
			impostorItemInventoryItemsSpawns[2] = new Vector3(8.3f, 2.3f);
			impostorItemInventoryItemsSpawns[3] = new Vector3(-19.9f, -6.6f);
			impostorItemInventoryItemsSpawns[4] = new Vector3(-6.6f, -8.4f);
			impostorItemInventoryItemsSpawns[5] = new Vector3(-16.1f, 2.4f);
			impostorItemInventoryItemsSpawns[6] = new Vector3(2.2f, -15.1f);
			impostorItemInventoryItemsSpawns[7] = new Vector3(-12.6f, -3.1f);
			impostorItemInventoryItemsSpawns[8] = new Vector3(-17.4f, -13.4f);
			impostorItemInventoryItemsSpawns[9] = new Vector3(0.5f, -9.5f, 0.0f);
			#endregion
			#endregion
			#endregion
			#endregion
			#endregion
			#endregion

		}

		private void Start() {
			spawnRandomImpostorItemTimer = spawnRandomImpostorItemTimerMax;
			int numOfImpostorItems = 10;
			for (int i = 0; i < numOfImpostorItems; i++) {
				GameObject impostorItem = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.impostorItem);
				impostorItem.GetComponent<ImpostorItem>().SetItem(i);
			}
		}

		[HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
		public static class ImpostorItemPatch {
			public static void Postfix() {
				GameObject.Instantiate(ThunderzLuckyPlugin.Instance.impostorItemSpawnManager);
			}

		}
	}
}
