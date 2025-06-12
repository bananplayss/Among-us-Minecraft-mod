using Reactor.Utilities.Attributes;
using System;
using UnityEngine;

namespace bananplaysshu.Tools {
	public static class ClosestPlayerFinder {

		public static PlayerControl FindClosestPlayer(PlayerControl playerFrom) {
			PlayerControl closestPlayer = null;
			float distance = 999f, closestDistance = 0f, maxDistance = 5f;
			foreach (PlayerControl pc in PlayerControl.AllPlayerControls) {
				if (pc == PlayerControl.LocalPlayer || pc.Data.IsDead) continue;
				closestDistance = Vector3.Distance(PlayerControl.LocalPlayer.transform.position, pc.transform.position);
				if (closestDistance < distance) {
					closestPlayer = pc;
					distance = closestDistance;
				}
				if (closestDistance > maxDistance) return null;
				return closestPlayer;
			}
			return null;

		}
	}
}
