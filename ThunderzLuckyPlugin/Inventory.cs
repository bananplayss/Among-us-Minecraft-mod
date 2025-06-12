using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class Inventory : MonoBehaviour {
		[HideFromIl2Cpp]
		private GameObject inventoryContainer { get; set; }

		public Inventory(IntPtr ptr) : base(ptr) { }

		[HideFromIl2Cpp]
		public static Inventory Instance { get; set; }

		private void Awake() {
			Instance = this;
		}

		private void Start() {
			inventoryContainer = transform.GetChild(0).gameObject;
			Debug.Log("Inventory Initialized for 'localPlayer'");

			InventorySystem.Instance.inv.SetActive(false);
		}

		public void HideIventory() {
			inventoryContainer.SetActive(false);
		}

		public void ShowInventory() {
			inventoryContainer.SetActive(true);
		}
	}

	[HarmonyPatch(typeof(HudManager),nameof(HudManager.Update))]
	public static class HudManPatch {
		public static void Postfix(HudManager __instance) {
			__instance.SabotageButton.gameObject.SetActive(false);
			__instance.ReportButton.gameObject.SetActive(false);
			__instance.KillButton.GetComponent<SpriteRenderer>().sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.KillButton.png", 100);
			__instance.ImpostorVentButton.gameObject.SetActive(false);
			__instance.KillOverlay.gameObject.SetActive(false);
		}
	}
}

