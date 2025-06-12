
using HarmonyLib;
using System;
using UnityEngine;

namespace bananplaysshu {

	[HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.ShowRole))]
	internal class IntroPatch {
		public static void Postfix(IntroCutscene __instance) {
			LateTask.New(() => {
				if(PlayerControl.LocalPlayer.Data.RoleType == AmongUs.GameOptions.RoleTypes.Impostor) {
					__instance.RoleText.text = "Minecraft";
				}
				__instance.RoleBlurbText.text = "";


			},0.001f);
		}
	}

	[HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Update))]
	public static class GameStartManagerUpdatePatch {
		public static void Prefix(GameStartManager __instance) {
			try {
				__instance.MinPlayers = 1;
			} catch (Exception ex) {
				Debug.LogError(ex.Message);
			}
		}
	}


}
