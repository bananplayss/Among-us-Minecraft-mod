using bananplaysshu;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Networking.Attributes;
using System;
using UnityEngine;
using static bananplaysshu.ThunderzLuckyPlugin;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class EscapeButton : CustomActionButton {

		public static bool canUse = true;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("ThunderzLuckyPlugin.Resources.EscapeButton.png");

		public override string Name => "Escape";

		public override float Cooldown => 0f;

		public override float EffectDuration => 0f;

		public override int MaxUses => 1;

		public override LoadableAsset<Sprite> Sprite => buttonSprite;

		public static int escapists = 0;

		public override bool Enabled(RoleBehaviour role) {
			if (role.TeamType == RoleTeamTypes.Impostor) {
				return false;
			}
			return true;

			
		}

		public override ButtonLocation Location => ButtonLocation.BottomRight;

		protected override void OnClick() {
			CustomRPC.Escape(PlayerControl.LocalPlayer,escapists);
			PlayerControl.LocalPlayer.RpcSetRole(AmongUs.GameOptions.RoleTypes.CrewmateGhost);
			PlayerControl.LocalPlayer.Data.IsDead = true;
		}


		public override bool CanUse() {
			return canUse;
		}

		public override void SetActive(bool visible, RoleBehaviour role) {
			Button?.ToggleVisible(visible && Enabled(role));
		}
	}
}
