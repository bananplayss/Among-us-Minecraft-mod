using AmongUs.Data.Player;
using bananplaysshu;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Networking.Attributes;
using System;
using UnityEngine;
using static bananplaysshu.ThunderzLuckyPlugin;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class InvisibleButton : CustomActionButton {

		public static bool canUse = true;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("ThunderzLuckyPlugin.Resources.InvisibleButton.png");

		public override string Name => "Invisibility";

		public override float Cooldown => 10f;

		public override float EffectDuration => 0f;

		public override int MaxUses => 0;

		public static bool isInvisible = false;

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
			isInvisible = !isInvisible;
			InvisibleRpc(PlayerControl.LocalPlayer, isInvisible);
			LocalInvisibility(PlayerControl.LocalPlayer);
		}

		[MethodRpc((uint)CustomRPC_Enum.InvisibleRpc)]
		public static void InvisibleRpc(PlayerControl pc, bool isInvisible) {
			float alpha = isInvisible == true ? 0f : 1f;
			pc.cosmetics.nameText.gameObject.SetActive(isInvisible == true ? false : true);
			pc.cosmetics.SetPhantomRoleAlpha(alpha);

		}

		public void LocalInvisibility(PlayerControl pc) {
			float alpha = isInvisible == true ? .5f : 1f;
			pc.cosmetics.SetPhantomRoleAlpha(alpha);
		}

		public override bool CanUse() {
			return canUse;
		}
	}
}
