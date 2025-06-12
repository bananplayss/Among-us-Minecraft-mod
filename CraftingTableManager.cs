using bananplaysshu.Buttons;
using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	internal class CraftingTableManager : MonoBehaviour{

		[HideFromIl2Cpp]
		public static CraftingTableManager Instance { get; private set; }

		[HideFromIl2Cpp]
		InventoryItem craftedItem { get; set; }

		public CraftingTableManager(IntPtr ptr) : base(ptr) { }

		[HideFromIl2Cpp]
		public OutputSlot outputSlot { get; set; }

		public static CraftingTableManager currentTable;

		private void Awake() {
			if (CraftingTableManager.Instance != null) return;
			Instance = this;
		}

		private void Start() {
			transform.localScale *= 1.25f;
		}
		
		private void Update() {
			


			if (Vector3.Distance(transform.position, PlayerControl.LocalPlayer.transform.position) <= 2f) {
				CraftingTableButton.canUse = true;
				currentTable = this;
			} else {
				if(currentTable == this) {
					CraftingTableButton.canUse = false;
					currentTable = null;
				}
			}
			
		}

		public void UpdateCraftingOutput() {
			craftedItem = null;

			foreach(Recipe recipe in RecipeDatabase.Instance.ReturnRecipeDatabase()) {
				if (CheckRecipeMatch(recipe)){
					craftedItem = recipe.result;
					outputSlot.UpdateOutput(craftedItem, recipe.quantityCrafted);
					break;
				}
			}
		}

		bool CheckRecipeMatch(Recipe recipe) {
			List<InputSlot> craftingSlots = InputSlotStorage.Instance.ReturnCraftingSlots();

			for (int i = 0; i < craftingSlots.Count; i++) {
				InventoryItem slotItem = craftingSlots[i].currentItem;
				InventoryItem requiredItem = i < recipe.ingredients.Length ? recipe.ingredients[i] : null;

				if(slotItem != requiredItem) {
					return false;
				}
			}

			return true;
		}
	}

	[HarmonyPatch(typeof(GameManager),nameof(GameManager.StartGame))]
	public static class InstantiateWorkbench {

		//IF YOU'RE LOOKING AT THIS.... I ADDED IT AFTER I DELIVERED THE PROJECT SO I DIDNT CARE MAKING A METHOD
		public static void Postfix() {
			GameObject workbench = new GameObject("Workbench");
			SpriteRenderer sr = workbench.AddComponent<SpriteRenderer>();
			sr.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CraftingTable.png",180);
			workbench.transform.position = new Vector3(6.2f, -9.3f, 0f);
			workbench.AddComponent<CraftingTableManager>();


			GameObject workbench2 = new GameObject("Workbench2");
			SpriteRenderer sr2 = workbench2.AddComponent<SpriteRenderer>();
			sr2.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CraftingTable.png", 180);
			workbench2.transform.position = new Vector3(16.9f, -5.8f);
			workbench2.AddComponent<CraftingTableManager>();

			GameObject workbench3 = new GameObject("Workbench");
			SpriteRenderer sr3 = workbench3.AddComponent<SpriteRenderer>();
			sr3.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CraftingTable.png", 180);
			workbench3.transform.position = new Vector3(4.6f, 3.0f, 0.0f);
			workbench3.AddComponent<CraftingTableManager>();

			GameObject workbench4 = new GameObject("Workbench");
			SpriteRenderer sr4 = workbench4.AddComponent<SpriteRenderer>();
			sr4.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CraftingTable.png", 180);
			workbench4.transform.position = new Vector3(-5.9f, 3.9f, 0.0f);
			workbench4.AddComponent<CraftingTableManager>();

			GameObject workbench5 = new GameObject("Workbench");
			SpriteRenderer sr5 = workbench5.AddComponent<SpriteRenderer>();
			sr5.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CraftingTable.png", 180);
			workbench5.transform.position = new Vector3(6.2f, -3.2f, 0.0f);
			workbench5.AddComponent<CraftingTableManager>();


		}
	}

	[HarmonyPatch(typeof(AmongUs.Data.Player.PlayerData), nameof(AmongUs.Data.Player.PlayerData.FileName), MethodType.Getter)]
	[HarmonyPatch(typeof(AmongUs.Data.Settings.SettingsData), nameof(AmongUs.Data.Settings.SettingsData.FileName), MethodType.Getter)]
	public static class SaveManagerPatch {
		public static void Postfix(ref string __result) {
			__result += "_dexmods";
		}
	}
	[HarmonyPatch(typeof(AmongUs.Data.Legacy.LegacySaveManager), nameof(AmongUs.Data.Legacy.LegacySaveManager.GetPrefsName))]
	public static class LegacySaveManagerPatch {
		public static void Postfix(ref string __result) {
			__result += "_dexmods";
		}
	}
}
