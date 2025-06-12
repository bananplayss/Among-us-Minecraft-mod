using bananplaysshu;
using MiraAPI.Hud;
using MiraAPI.Utilities.Assets;
using System;
using TMPro;
using UnityEngine;
using static bananplaysshu.ThunderzLuckyPlugin;

namespace bananplaysshu.Buttons {

	[RegisterButton]
	internal class CraftingTableButton : CustomActionButton {

		public static bool canUse = false;

		LoadableAsset<Sprite> buttonSprite = new LoadableResourceAsset("ThunderzLuckyPlugin.Resources.CraftingTable.png");

		public override string Name => "Craft";

		public override float Cooldown => 0f;
		
		public override float EffectDuration => 0f;

		public override int MaxUses => 0;

		public override LoadableAsset<Sprite> Sprite => buttonSprite;

		public override bool Enabled(RoleBehaviour role) {

			if(role.TeamType== RoleTeamTypes.Impostor) {
				Button.buttonLabelText.outlineColor = Color.red;
			}
			
			return true;
		}

		public override ButtonLocation Location => ButtonLocation.BottomLeft;

		protected override void OnClick() {
			if (CraftingInventory.Instance == null || Inventory.Instance == null)
				Debug.LogError("CraftingInventory Instance is null, this is GAME BREAKING");
			else {
				if (!CraftingInventory.Instance.gameObject.activeSelf) {
					CraftingInventory.Instance.Show();
					Hotbar.Instance.Hide();
					KillAnimation.SetMovement(PlayerControl.LocalPlayer, false);
				}
				else {
					CraftingInventory.Instance.Hide();
					Hotbar.Instance.Show();
					KillAnimation.SetMovement(PlayerControl.LocalPlayer,true);
				}
			}
		}

		

		public override bool CanUse() {
			return canUse;
		}

		
	}
}
