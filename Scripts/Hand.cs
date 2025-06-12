using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using MiraAPI.Patches;
using PowerTools;
using Reactor.Networking.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace bananplaysshu {

	[RegisterInIl2Cpp]
	public class Hand : MonoBehaviour {
		public Hand(IntPtr ptr) : base(ptr) { }


		[HideFromIl2Cpp]
		public static Hand Instance { get; set; }

		[HideFromIl2Cpp]
		public SpriteRenderer sr { get; set; }

		[HideFromIl2Cpp]
		public Animator animator { get; set; }

		private bool isMining = false;


		private void Awake() {
			Instance = this;

			sr = GetComponent<SpriteRenderer>();
			animator = GetComponent<Animator>();
			sr.sortingOrder = 0;
		}



		public void UpdateSprite(Sprite sprite) {
			if (transform.parent.GetComponent<SpriteRenderer>() == null) {
				transform.parent.gameObject.AddComponent<SpriteRenderer>();
			}

			GetComponentInParent<SpriteRenderer>().sprite = sprite;

			sr.sprite = sprite;
		}



		public void SetIsMining(bool isMining) {
			animator.SetBool("IsMining", isMining);
			this.isMining = isMining;
		}

		public bool IsMining() {
			return isMining;
		}

		[MethodRpc((uint)CustomRPC_Enum.InitializeHand)]
		public static void InitializeHandRpc(PlayerControl pc) {
			GameObject handObj = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.hand);
			handObj.name = "Hand";
			handObj.AddComponent<HandParent>();
			handObj.GetComponent<HandParent>().hand = handObj.transform.GetChild(0).gameObject.AddComponent<Hand>();
			handObj.transform.parent = PlayerControl.LocalPlayer.transform;
			handObj.transform.localScale *= .5f;
			handObj.transform.localPosition = Vector3.zero;
		}

	}


		[HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
		public static class HandPatch {
			public static void Postfix() {
				Hand.InitializeHandRpc(PlayerControl.LocalPlayer);

			}
		}

		[HarmonyPatch(typeof(PlayerBodySprite), nameof(PlayerBodySprite.SetFlipX))]
		public static class FlipXPatch {
			public static void Postfix(PlayerBodySprite __instance) {
				if (Hand.Instance != null) {

					HandParent.Instance.animator.SetBool("FlipX", !__instance.BodySprite.flipX);
				}
			}
		}

		[HarmonyPatch(typeof(PlayerAnimations), nameof(PlayerAnimations.PlayRunAnimation))]
		public static class HandAnimationPatch {
			public static void Postfix() {
				if (Hand.Instance != null) {
					Hand.Instance.SetIsMining(false);
					if (HandParent.Instance.GetFlipX()) {
						const string HAND_RUNNING_RIGHT = "HandRunningRight";
						Hand.Instance.animator.Play(HAND_RUNNING_RIGHT);

					} else {
						const string HAND_RUNNING_LEFT = "HandRunningLeft";
						Hand.Instance.animator.Play(HAND_RUNNING_LEFT);
					}

				}
			}
		}

		[HarmonyPatch(typeof(PlayerAnimations), nameof(PlayerAnimations.PlayIdleAnimation))]
		public static class HandAnimationPatch2 {
			public static void Postfix() {
				if (Hand.Instance != null) {
					const string HAND_IDLE = "HandIdle";
					Hand.Instance.animator.Play(HAND_IDLE);
					
					Hand.Instance.SetIsMining(false);
				}
			}
		}
	}
