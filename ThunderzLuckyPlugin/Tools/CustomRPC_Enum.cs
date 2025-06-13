using bananplaysshu.Buttons;
using HarmonyLib;
using InnerNet;
using JetBrains.Annotations;
using Reactor.Networking.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace bananplaysshu.Tools {
	public enum CustomRPC_Enum : uint {
		DeathRpc = 0,
		SpawnPhantomRpc = 1,
		InstantiateTridentRpc = 2,
		InvisibleRpc = 3,
		InitializeHand = 4,
		SpawnPiglinArmy = 5,
		SaveRpc = 6,
		SpeedPotionRpc = 7,
		Escape = 8,
		SendEscapistData = 8,
	}

	public class CustomRPC {

		[MethodRpc((uint)CustomRPC_Enum.DeathRpc)]
		public static void DeathRpc(PlayerControl pc) {
			pc.MurderPlayer(pc, MurderResultFlags.Succeeded);
		}

		[MethodRpc((uint)CustomRPC_Enum.SpeedPotionRpc)]
		public static void SpeedPotion(PlayerControl pc, bool speeding) {
			float speed = speeding == true ? 4f : 2.5f;
			pc.MyPhysics.Speed = speed;
		}

		[MethodRpc((uint)CustomRPC_Enum.Escape)]
		public static void Escape(PlayerControl pc, int escapists) {
			if (PlayerControl.LocalPlayer == pc) {
				escapists++;
				SendEscapistData(pc, escapists);
			}

		}

		[MethodRpc((uint)CustomRPC_Enum.SendEscapistData)]
		public static void SendEscapistData(PlayerControl pc, int escapists) {
			EscapeButton.escapists = escapists;
			if (escapists == PlayerControl.AllPlayerControls.Count && PlayerControl.LocalPlayer == pc) {
				foreach (PlayerControl _pc in PlayerControl.AllPlayerControls) {
					if (_pc.Data.RoleType == AmongUs.GameOptions.RoleTypes.Impostor) {
						AmongUsClient.Instance.KickPlayer(_pc.Data.ClientId, false);
					}
				}
			}
		}
	}
}
