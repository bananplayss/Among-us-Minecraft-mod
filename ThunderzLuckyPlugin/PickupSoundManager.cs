using HarmonyLib;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class PickupSoundManager : MonoBehaviour {
		public PickupSoundManager(IntPtr ptr) : base(ptr) { }

		private AudioSource audio;


		private void Start() {
			audio = GetComponent<AudioSource>();
			InventoryStorage.Instance.OnPickupItem += Instance_OnPickupItem;
		}

		private void Instance_OnPickupItem(object sender, EventArgs e) {
			audio.Play();
		}
	}
}
