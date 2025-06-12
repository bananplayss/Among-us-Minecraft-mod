using HarmonyLib;
using Il2CppSystem.Xml;
using MiraAPI.Hud;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class ImpostorItem : MonoBehaviour{
		public ImpostorItem(IntPtr ptr) : base(ptr) { }

		private SpriteRenderer sr;
		private InventoryItem item;

		private void OnTriggerEnter2D(Collider2D c) {
			int amount = 5;
			if (item.maxStack == 1) amount = 1;
			if(c.transform == PlayerControl.LocalPlayer.transform) {
				for (int i = 0; i < amount; i++) {
					InventoryStorage.Instance.AddItemToInventoryStorage(item);
				}
				
				gameObject.SetActive(false);
			}
		}

		public void SetItem(int index) {
			InventoryItem indexedItem = ImpostorItemSpawnManager.Instance.impostorItemInventoryItems[index];
			item = indexedItem;

			sr = GetComponent<SpriteRenderer>();
			sr.sprite = item.sprite;

			transform.position = ImpostorItemSpawnManager.Instance.impostorItemInventoryItemsSpawns[index];
		}

		private void Start() {
			if(PlayerControl.LocalPlayer.Data.RoleType != AmongUs.GameOptions.RoleTypes.Impostor) {
				gameObject.SetActive(false);
			} else {
				ImpostorItemSpawnManager.Instance.impostorItemList.Add(this);
				sr.sortingOrder = 1;

				transform.localScale *= .55f;
				gameObject.SetActive(true);
			}
		}
	}
}
