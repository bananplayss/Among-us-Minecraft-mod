using Il2CppInterop.Runtime.Attributes;
using Il2CppMono.Unity;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class EndPortal : MonoBehaviour{
		public EndPortal(IntPtr ptr) : base(ptr) { }

		#region Fields

		public static float reourcePosY;

		private static float playerPosY = 0;
		private static float ySubstractFromResourcePos = .75f;
		private static float checkInputCounter = .25f;
		private static float checkInputCounterMax = .25f;
		private static float resourceInteractCooldownCounter = .7f;
		private static float resourceInteractCooldownCounterMax = .7f;
		public static int index = 0;
		public static int index2 = 0;

		public static int interactedCount = 0;
		public static int interactedCountNeeded = 2;
		#endregion

		[HideFromIl2Cpp]
		private List<GameObject> eyes {  get; set; }

		private void Start() {
			eyes = new List<GameObject>();
			for (int i = 0; i < transform.GetChild(1).childCount; i++) {
				eyes.Add(transform.GetChild(1).GetChild(i).gameObject);
			}
			foreach(GameObject go in eyes) {
				go.SetActive(false);
			}

		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.O)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Trident));
			}
			if (Input.GetKeyDown(KeyCode.P)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Spyglass));
			}
			if (Input.GetKeyDown(KeyCode.L)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Gunpowder));
			}
			if (Input.GetKeyDown(KeyCode.Alpha9)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.BucketOfLava));
			}
			if (Input.GetKeyDown(KeyCode.Alpha8)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.IronSword));
			}
			if (Input.GetKeyDown(KeyCode.Alpha7)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.EyeOfEnder));
			}
			if (Input.GetKeyDown(KeyCode.Alpha6)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.EnderPearl));
			}
			if (Input.GetKeyDown(KeyCode.Alpha4)) {
				InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Iron));
			}
			if (Input.GetKeyDown(KeyCode.Alpha5)) {
				Hotbar.Instance.Heal(1);
			}


			//Bonus Content
			//if (Input.GetKeyDown(KeyCode.K)) {
			//	InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Plushie));
			//}

			#region HandleInteractions
			float distanceNeeded = 4f;
			float yPosDiff = .7f;

			Vector3 resourcePos = transform.position;


			if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, resourcePos) < distanceNeeded &&

				(playerPosY - reourcePosY - ySubstractFromResourcePos) <= yPosDiff) {

				if (Hand.Instance == null) return;
				if (Hand.Instance.IsMining()) {
					checkInputCounter -= Time.deltaTime;
					if (checkInputCounter < 0) {
						checkInputCounter = checkInputCounterMax;

							if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inv)) {
								if (InventoryStorage.Instance.HasInventoryItemInventory(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.EyeOfEnder)
								,ref index)) {
								if (index == eyes.Count) return;
								eyes[index2].SetActive(true);
								index2++;
								
								if (index2 == eyes.Count - 1) { eyes[index2].SetActive(true); GetComponent<Collider2D>().enabled = true; index2 = 0; }
								InventoryStorage.Instance.RemoveInventoryItemFromStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.EyeOfEnder));
								}
							} else {
								UnityEngine.Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
							}

							interactedCount = 0;
						}
					}
				}
			}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.transform == PlayerControl.LocalPlayer.transform) {
				PlayerControl.LocalPlayer.transform.position = PortalManager.Instance.end.transform.position;
				foreach (PlayerBodySprite sprite in PlayerControl.LocalPlayer.cosmetics.bodySprites) {
					sprite.BodySprite.rendererPriority = 5;
				}
			}
		}
	}
}
#endregion