using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace bananplaysshu {

	[HarmonyPatch(typeof(HudManager),nameof(HudManager.Start))]
	public static class HUDManagerPatch {

		public static void Postfix(HudManager __instance) {
			__instance.transform.Find("Buttons").Find("BottomLeft").GetComponent<GridArrange>().MaxColumns = 2;
			__instance.transform.Find("Buttons").Find("BottomLeft").GetComponent<GridArrange>().CellSize *= .8f;

			__instance.transform.Find("Buttons").Find("BottomLeft").GetComponent<GridArrange>().transform.localScale *= .9f;

		}
	}
}
