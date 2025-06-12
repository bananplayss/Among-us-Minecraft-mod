using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	class InventorySystem : MonoBehaviour {
		[HideFromIl2Cpp]
		public static InventorySystem Instance { get; private set; }

		private int maxStorage = 64;

		public GameObject inv;

		public InventorySystem(IntPtr ptr) : base(ptr) { }

		private void Awake() {
			Instance = this;
		}


		public void SetInventoryGo(GameObject inv) {
			this.inv = inv;
		}

		private void Update() {
			if (Input.GetKeyDown(Interaction.toggleInventory)) {
				Hotbar.Instance.RefreshHotbar();
				inv.SetActive(!inv.activeSelf);
				KillAnimation.SetMovement(PlayerControl.LocalPlayer,!inv.activeSelf);
				if (CraftingInventory.Instance.gameObject.activeSelf) CraftingInventory.Instance.Hide();
				if (Hotbar.Instance.container.activeSelf) Hotbar.Instance.Hide(); else Hotbar.Instance.Show();

			}
		}
	}

	[HarmonyPatch(typeof(GameManager),nameof(GameManager.StartGame))]
	public class CreatePlayerInventory {

		[HarmonyPostfix]
		public static void AddScriptToPlayer() {
			InventorySystem invSystem = PlayerControl.LocalPlayer.gameObject.AddComponent<InventorySystem>();
			GameObject invObj = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.inventory);
			Inventory inv = invObj.GetComponent<Inventory>();
			
			
			invSystem.SetInventoryGo(invObj);
			invObj.transform.parent = HudManager.Instance.transform;
			invObj.transform.localScale *= .5f;

			invObj.layer = LayerMask.NameToLayer("UI");

			AspectPosition invObjPos = invObj.AddComponent<AspectPosition>();

			invObjPos.DistanceFromEdge = new Vector3(0,-.66f,-200);
			invObjPos.Update();
		}

	}

}
