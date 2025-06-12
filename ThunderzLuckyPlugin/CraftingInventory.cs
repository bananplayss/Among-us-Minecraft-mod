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
	public class CraftingInventory : MonoBehaviour {

		public CraftingInventory(IntPtr ptr) : base(ptr) { }

		[HideFromIl2Cpp]
		public static CraftingInventory Instance { get; set; }

		private bool initialized = false;

		private void Awake() {
			Instance = this;
		}

		private void Start() {

			transform.localScale *= .5f;

			gameObject.layer = LayerMask.NameToLayer("UI");
			transform.parent = HudManager.Instance.transform;

			gameObject.SetActive(false);
		}

		public void Show() {
			gameObject.SetActive(true);
			if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inventorySystem)) {
				inventorySystem.inv.SetActive(true);
				if (!initialized) {
					initialized= true;

					GetComponent<SpriteRenderer>().sortingOrder = 1;
					transform.position = InventorySystem.Instance.inv.transform.position;
					transform.position += new Vector3(0, 2.2f, 20);
				}
				
			}

		}

		public void Hide() {
			gameObject.SetActive(false);
			if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inventorySystem)) {
				inventorySystem.inv.SetActive(false);
			}
		}
	}

	[HarmonyPatch(typeof(GameManager),nameof(GameManager.StartGame))]
	public static class InitializeCraftingInventory {
		public static void Postfix() {
			GameObject craftinginventoryGO = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.craftingInventory);
			CraftingInventory craftingInventory = craftinginventoryGO.GetComponent<CraftingInventory>();

			craftinginventoryGO.transform.parent = HudManager.Instance.transform;
			craftinginventoryGO.transform.localScale *= .5f;

			craftinginventoryGO.layer = LayerMask.NameToLayer("UI");

			CraftingInventory.Instance.Hide();


		}
	}

	[HarmonyPatch(typeof(ModManager),nameof(ModManager.LateUpdate))]
	public static class CraftinInventoryPatch{

		public static void Postfix() {
			if (CraftingInventory.Instance == null) { }
			else {
				if (Input.GetKeyDown(Interaction.toggleInventory)) {
					if (CraftingInventory.Instance == null) Debug.LogError("Null az instance");
				}
			}
			
		}
	}
}
