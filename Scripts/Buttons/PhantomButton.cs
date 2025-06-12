using bananplaysshu;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Networking.Attributes;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using static bananplaysshu.ThunderzLuckyPlugin;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class PhantomButton : CustomActionButton {

		public static bool canUse = true;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("ThunderzLuckyPlugin.Resources.Phantom.png");

		public override string Name => "Phantom";

		public override float Cooldown => 10f;

		public override float EffectDuration => 30f;

		public override int MaxUses => 0;

		public override LoadableAsset<Sprite> Sprite => buttonSprite;

		public override bool Enabled(RoleBehaviour role) {
			if (role.TeamType == RoleTeamTypes.Impostor) {
				Button.buttonLabelText.outlineColor = Color.red;
			}
			if (role.TeamType == RoleTeamTypes.Impostor) {
				return true;
			}
			return false;
		}

		public override ButtonLocation Location => ButtonLocation.BottomLeft;

		protected override void OnClick() {
			
			PlayerControl closestPlayer = null;
			float distance = 999f;
			float closestDistance = 0;
			float maxDistance = 5f;
			foreach (PlayerControl pc in PlayerControl.AllPlayerControls) {
				if (pc == PlayerControl.LocalPlayer) continue;
				if (pc.Data.IsDead) continue;
				closestDistance = Vector3.Distance(PlayerControl.LocalPlayer.transform.position, pc.transform.position);
				if (closestDistance < distance) {
					closestPlayer = pc;
					distance = closestDistance;
				}
			}

			if (closestDistance > maxDistance) return;
			SpawnPhantomRpc(closestPlayer);


		}

		[MethodRpc((uint)CustomRPC_Enum.SpawnPhantomRpc)]
		private static void SpawnPhantomRpc(PlayerControl closestPlayer) {
			GameObject phantomObj = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.phantom);
			phantomObj.GetComponent<PhantomBehaviour>().player = closestPlayer;
			phantomObj.transform.parent = closestPlayer.transform;
			phantomObj.transform.localPosition = new Vector3(0, .8f, 0);

			PhantomManager.Instance.phantom = phantomObj.GetComponent<PhantomBehaviour>();
		}

		public override bool CanUse() {
			return canUse;
		}
	}
}
