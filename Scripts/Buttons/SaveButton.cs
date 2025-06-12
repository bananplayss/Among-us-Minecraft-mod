using bananplaysshu;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static bananplaysshu.ThunderzLuckyPlugin;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class SaveButton : CustomActionButton {

		public static bool canUse = false;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("ThunderzLuckyPlugin.Resources.SaveButton.png");

		public override string Name => "Save";

		public override float Cooldown => 30f;

		public override float EffectDuration => 2f;

		public override int MaxUses => 2;

		public override LoadableAsset<Sprite> Sprite => buttonSprite;

		public override bool Enabled(RoleBehaviour role) {
			if (role.TeamType == RoleTeamTypes.Impostor) {
				Button.buttonLabelText.outlineColor = Color.red;
			}
			if (role.TeamType != RoleTeamTypes.Impostor) {
				return true;
				
			}
			return false;
		}

		public override ButtonLocation Location => ButtonLocation.BottomRight;

		protected override void OnClick() {
			PhantomBehaviour p = PhantomManager.Instance.ReturnPhantom();
			p.TryToKillPhantom(PlayerControl.LocalPlayer);
			
		}

		public override bool CanUse() {
			return canUse;
		}
	}
}
