using bananplaysshu.Tools;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using Reactor.Networking.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class EscapeButton : CustomActionButton {

		#region Button Properties
		public static bool canUse = true;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("ThunderzLuckyPlugin.Resources.EscapeButton.png");

		public override string Name => "Escape";

		public override float Cooldown => 0f;

		public override float EffectDuration => 0f;

		public override int MaxUses => 1;

		public override LoadableAsset<Sprite> Sprite => buttonSprite;

		public static int escapists = 0;

		public override bool CanUse() {
			return canUse;
		}
		#endregion

		public override bool Enabled(RoleBehaviour role) {
			return role.TeamType == RoleTeamTypes.Impostor ? false : true;
		}

		public override ButtonLocation Location => ButtonLocation.BottomRight;

		protected override void OnClick() {
			PlayerControl lp = PlayerControl.LocalPlayer;

			CustomRPC.Escape(lp,escapists);
			lp.RpcSetRole(AmongUs.GameOptions.RoleTypes.CrewmateGhost);
			lp.Data.IsDead = true;
		}

		public override void SetActive(bool visible, RoleBehaviour role) {
			Button?.ToggleVisible(visible && Enabled(role));
		}
	}
}
