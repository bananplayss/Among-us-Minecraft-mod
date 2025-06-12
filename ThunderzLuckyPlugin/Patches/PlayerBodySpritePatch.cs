using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bananplaysshu.Patches {
	[HarmonyPatch(typeof(PlayerBodySprite), nameof(PlayerBodySprite.SetFlipX))]
	public static class PlayerBodySpritePatch {
		public static void Postfix(PlayerBodySprite __instance) {
			if (Hand.Instance != null) {

				HandParent.Instance.animator.SetBool("FlipX", !__instance.BodySprite.flipX);
			}
		}
	}
}
