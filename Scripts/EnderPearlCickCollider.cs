using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {

	[RegisterInIl2Cpp]
	public class EnderPearlClickCollider : MonoBehaviour {
		public EnderPearlClickCollider(IntPtr ptr) : base(ptr) { }

		public Vector3 pos;

		void OnMouseDown() {
			PlayerControl.LocalPlayer.transform.position = pos;
			EnderPearlAbility.Instance.Hide();
			InventoryStorage.Instance.RemoveInventoryItemFromStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(
			InventoryItemDatabase.InventoryItemsEnum.EnderPearl));
			Hotbar.Instance.RefreshHotbar();
		}

	}
}
