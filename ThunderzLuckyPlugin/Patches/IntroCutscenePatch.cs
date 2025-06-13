using bananplaysshu.Tools;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bananplaysshu.Patches {
	[HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.ShowRole))]
	internal class IntroCutscenePatch {
		public static void Postfix(IntroCutscene __instance) {
			LateTask.New(() => {
				if (PlayerControl.LocalPlayer.Data.RoleType == AmongUs.GameOptions.RoleTypes.Impostor) {
					__instance.RoleText.text = "Minecraft";
				}
				__instance.RoleBlurbText.text = "";


			}, 0.001f);
		}
	}
}
