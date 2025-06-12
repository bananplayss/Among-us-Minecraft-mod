using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class GatherResource : MonoBehaviour{
		public GatherResource(IntPtr ptr) : base(ptr) { }

		[HideFromIl2Cpp]
		private InventoryItem gatherableResource {  get; set; }
		[HideFromIl2Cpp]
		private InventoryItem requiredResource {  get; set; }
		[HideFromIl2Cpp]
		private List<SpriteRenderer> srs {  get; set; }

		#region Fields

		public static float reourcePosY;

		private static float playerPosY = 0;
		private static float ySubstractFromResourcePos = .75f;
		private static float checkInputCounter = .25f;
		private static float checkInputCounterMax = .25f;
		private static float resourceInteractCooldownCounter = .5f;
		private static float resourceInteractCooldownCounterMax = .5f;

		private static bool canInteract = true;

		public static int interactedCount = 0;
		public static int interactedCountNeeded = 2;
		#endregion



		private void Start() {
			transform.localScale *= .175f;

			srs = new List<SpriteRenderer>();
			foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
				srs.Add(sr);
			}
		}

		public void SetGatherableResource(InventoryItem gatherableResource) {
			this.gatherableResource = gatherableResource;
		}

		public void SetRequiredItem(InventoryItem requiredItem) {
			requiredResource = requiredItem;
		}

		private void Update() {

			#region HandleInteractions
			float distanceNeeded = 3f;
			float yPosDiff = .7f;

			if (gatherableResource == null) return;

			Vector3 resourcePos = transform.position;

			
			if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, resourcePos) < distanceNeeded &&
				
				(playerPosY - reourcePosY - ySubstractFromResourcePos) <= yPosDiff) {

				if (Hand.Instance == null) return;
				if (Hand.Instance.IsMining() && canInteract) {
					checkInputCounter -= Time.deltaTime;
					if (checkInputCounter < 0) {
						checkInputCounter = checkInputCounterMax;
						interactedCount++;
						if (interactedCount > interactedCountNeeded) {

							if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inv)) {
								int index = 0;
								if (requiredResource != null) {
									if (InventoryStorage.Instance.HasInventoryItemInventory(requiredResource, ref index)) {
										InventoryStorage.Instance.AddItemToInventoryStorage(gatherableResource);
										InventoryStorage.Instance.RemoveInventoryItemFromStorage(requiredResource);

										
									}
								} else {
									InventoryStorage.Instance.AddItemToInventoryStorage(gatherableResource);
								}
							} else {
								Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
							}

							interactedCount = 0;

							canInteract = false;

							Hotbar.Instance.RefreshHotbar();
						}
					}
				}
				if (!canInteract) {
					resourceInteractCooldownCounter -= Time.deltaTime;
					if (resourceInteractCooldownCounter < 0) {
						resourceInteractCooldownCounter = resourceInteractCooldownCounterMax;
						canInteract = true;
					}
				}
			}
		}
	}
	#endregion

	
	[HarmonyPatch(typeof(GameManager),nameof(GameManager.StartGame))]
	public static class ResourceSpawnPatch {
		[HarmonyPriority(Priority.Normal)]
		public static void Postfix() {
			#region Patch
			//Stone pack
			GameObject stonePack = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.stonePack, new Vector3(9.2f, -12.9f, 1f), Quaternion.identity);
			InventoryItem cobblestone = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Cobblestone);
			stonePack.GetComponent<GatherResource>().SetGatherableResource(cobblestone);
			stonePack.transform.localScale *= .5f;
			stonePack.GetComponent<DynamicSorting>().SetOffset(.8f);

			GameObject stonePack2 = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.stonePack, new Vector3(-13.4f, -7f, 0.0f), Quaternion.identity);
			stonePack2.GetComponent<GatherResource>().SetGatherableResource(cobblestone);
			stonePack2.transform.localScale *= .5f;
			stonePack2.GetComponent<DynamicSorting>().SetOffset(.8f);

			//Iron pack
			GameObject ironPack = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.ironPack, new Vector3(-22.1f, -8.1f, 1f), Quaternion.identity);
			ironPack.GetComponent<GatherResource>().SetGatherableResource(InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Iron));
			ironPack.transform.localScale *= .6f;
			ironPack.GetComponent<DynamicSorting>().SetOffset(.4f);

			GameObject ironPack2 = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.ironPack, new Vector3(3f, -16f, 0.0f), Quaternion.identity);
			ironPack2.GetComponent<GatherResource>().SetGatherableResource(InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Iron));
			ironPack2.transform.localScale *= .6f;
			ironPack2.GetComponent<DynamicSorting>().SetOffset(.4f);



			//Iron pack
			GameObject lavaPool = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.lavaPool, new Vector3(-1.5f, -16.7f, 1f), Quaternion.identity);
			lavaPool.GetComponent<GatherResource>().SetGatherableResource(InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.BucketOfLava));
			lavaPool.GetComponent<GatherResource>().SetRequiredItem(
				InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Bucket));
			lavaPool.GetComponent<DynamicSorting>().SetToLavaPool();
			#endregion
		}
	}
}

