using HarmonyLib;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

	[HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
	public static class AnotherGameManagerPatch{
		public static void Postfix() {
			GameObject pickupSoundManagerGo = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.pickup);
		}
	}
}
