using AmongUs.GameOptions;
using Assets.CoreScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using HarmonyLib;

namespace bananplaysshu.Patches {
	[HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
	public static class EndGameManagerPatch {
		public static void Postfix(EndGameManager __instance) {

			if (EndGameResult.CachedGameOverReason == GameOverReason.HumansByTask || EndGameResult.CachedGameOverReason == GameOverReason.HumansByVote) {
				__instance.WinText.text = "You have beaten Minecraft!";
			}
		}
	}
}
