using bananplaysshu.Buttons;
using HarmonyLib;
using MiraAPI.Hud;
using PowerTools;
using Reactor.Networking.Attributes;
using UnityEngine;

namespace bananplaysshu {
	//refactor
	[HarmonyPatch(typeof(PlayerBodySprite), nameof(PlayerBodySprite.SetFlipX))]


	static class PlayerSpriteChange {


		public static void Postfix(PlayerBodySprite __instance) {
			__instance.BodySprite.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Skin.png", 170);
		
		}
	}
			
		[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Start))]
		static class ChangeIdleAnimation {
			public static void Postfix(PlayerControl __instance) {

			
			__instance.MyPhysics.Animations.group.IdleAnim = ThunderzLuckyPlugin.Instance.idleAnim;
			__instance.MyPhysics.Animations.group.RunAnim = ThunderzLuckyPlugin.Instance.runAnim;

			
			}
		}

	[HarmonyPatch(typeof(RoleManager), nameof(RoleManager.SelectRoles))]
	public static class LogicRolePatch {
		public static bool Prefix() {

			if (AmongUsClient.Instance.AmHost) {
				PlayerControl.LocalPlayer.RpcSetRole(AmongUs.GameOptions.RoleTypes.Impostor, false);
				return false;

			}
			PlayerControl.LocalPlayer.RpcSetRole(AmongUs.GameOptions.RoleTypes.Crewmate, false);
			
			return true;
		}
	}




	[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
	static class PlayAttackAnimation {
		public static void Postfix(PlayerControl __instance) {

			if (Input.GetKeyDown(Interaction.interactKey)) {


				HandParent handParent = HandParent.Instance;
				Hand hand = Hand.Instance;

				if (!hand.IsMining()) {
					if (handParent.GetFlipX()) {
						const string HAND_MINE_RIGHT = "HandMineRight";
						hand.animator.Play(HAND_MINE_RIGHT);

					} else {
						const string HAND_MINE_LEFT = "HandMineLeft";
						hand.animator.Play(HAND_MINE_LEFT);
					}
					hand.SetIsMining(true);

					PlayerControl.LocalPlayer.MyPhysics.Animations.Animator.Play(ThunderzLuckyPlugin.Instance.attackAnim);
				} 

			}
		}
	}
}
	
