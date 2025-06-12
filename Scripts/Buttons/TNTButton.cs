using bananplaysshu;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using System;
using UnityEngine;
using static bananplaysshu.ThunderzLuckyPlugin;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class TNTBuutton : CustomActionButton {

		public static bool canUse = false;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("ThunderzLuckyPlugin.Resources.TNTButton.png");

		public override string Name => "TNT";

		public override float Cooldown => 30f;

		public override float EffectDuration => 0f;

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
			int refInt = 0;
			if (InventoryStorage.Instance.HasInventoryItemInventory(InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.TNT), ref refInt)) {
				InventoryStorage.Instance.RemoveInventoryItemFromStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(
					InventoryItemDatabase.InventoryItemsEnum.TNT));
				Hotbar.Instance.RefreshHotbar();

				GameObject tnt= GameObject.Instantiate(ThunderzLuckyPlugin.Instance.tnt);
				tnt.transform.position = PlayerControl.LocalPlayer.transform.position;
			}
		}

		public override bool CanUse() {
			return canUse;
		}
	}
}
