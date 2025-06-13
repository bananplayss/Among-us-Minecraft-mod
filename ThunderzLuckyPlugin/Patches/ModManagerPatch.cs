using bananplaysshu.Tools;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace bananplaysshu.Patches {
	public class ModManagerPatch {
		[HarmonyPatch(typeof(ModManager), nameof(ModManager.LateUpdate))]
		internal static class ModManagerLateUpdatePatch {
			public static bool Prefix(ModManager __instance) {
				LateTask.Update(Time.deltaTime);
				ItemAdder.HandleInput();

				return false;
			}
		}

		[HarmonyPatch(typeof(ModManager), nameof(ModManager.LateUpdate))]
		internal static class ModManagerLateUpdate {
			public static void Postfix(ModManager __instance) {
				__instance.ShowModStamp();

				__instance.localCamera = Camera.main;

				__instance.ModStamp.transform.position = AspectPosition.ComputeWorldPosition(__instance.localCamera,
					AspectPosition.EdgeAlignments.RightTop, new Vector3(.6f, 1.2f, __instance.localCamera.nearClipPlane + .1f));
			}
		}
	}
}
