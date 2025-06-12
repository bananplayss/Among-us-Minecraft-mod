using bananplaysshu;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Networking.Attributes;
using System;
using UnityEngine;
using static bananplaysshu.ThunderzLuckyPlugin;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class PiglinButton : CustomActionButton {

		public static bool canUse = true;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("ThunderzLuckyPlugin.Resources.PiglinButton.png");

		public override string Name => "Piglin-Army";

		public override float Cooldown => 0f;

		public override float EffectDuration => 30f;

		public override int MaxUses => 0;

		public override LoadableAsset<Sprite> Sprite => buttonSprite;

		private float cooldown = 30;
		private float counter;

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
			canUse = false;
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

			if (closestDistance > maxDistance) {
				Debug.Log("megakadtunk papi");
				return;
			}
			SpawnPiglinArmyRpc(closestPlayer);
		}

		public override bool CanUse() {
			return canUse;
		}

		[MethodRpc((uint)CustomRPC_Enum.SpawnPiglinArmy)]
		public static void SpawnPiglinArmyRpc(PlayerControl closestPlayer) {

			int piglinArmySize = 3;
			GameObject piglinObj = null;
			float offset = .5f;
			for (int i = 0; i < piglinArmySize + 1; i++) {
				piglinObj = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.piglin);
				piglinObj.GetComponent<NPCBehaviour>().target = closestPlayer.transform;
				piglinObj.GetComponent<NPCBehaviour>().mobType = MobsEnum.Piglin;
				piglinObj.GetComponent<NPCBehaviour>().closestPlayer = closestPlayer;
				piglinObj.transform.localPosition = closestPlayer.transform.position + new Vector3(2 + offset, -2f + offset, 0);
				offset += .4f;
			}
		}
	}
}