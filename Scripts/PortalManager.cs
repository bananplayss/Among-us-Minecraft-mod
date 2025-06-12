using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class PortalManager : MonoBehaviour {
		public PortalManager(IntPtr ptr) : base(ptr) { }

		[HideFromIl2Cpp]
		public static PortalManager Instance { get; private set; }

		public GameObject nether;
		public GameObject end;

		public List<Transform> switchTransforms;

		private void Awake() {
			Instance = this;
		}



		[HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
		public static class InitializePortalObjectPatch {
			public static void Postfix() {
				GameObject portalManager = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.portalManager);

				GameObject nether = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.nether, new Vector3(-50, 30, 0), Quaternion.identity);
				GameObject end = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.end, new Vector3(50, 30, 0), Quaternion.identity);

				PortalManager.Instance.nether = nether;
				PortalManager.Instance.end = end;

				nether.transform.localScale *= .6f;
				end.transform.localScale *= .6f;


				GameObject endPortal = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.endPortal, new Vector3(-1.2f, 4.4f, 0f), Quaternion.identity);
				GameObject waterPack = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.waterPack, new Vector3(-10.1f, -4.1f, 0), Quaternion.identity);
				GameObject netherPortal = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.netherPortal, waterPack.transform.position + new Vector3(0, .7f), Quaternion.identity);

				waterPack.transform.localScale *= .3f;
				endPortal.transform.localScale *= .26f;
				netherPortal.transform.localScale *= .35f;

				List<Transform> switchTransforms = new List<Transform>();
				for (int i = 0; i < end.transform.GetChild(0).childCount; i++) {
					switchTransforms.Add(end.transform.GetChild(0).GetChild(i).transform);
				}

				portalManager.GetComponent<PortalManager>().switchTransforms = switchTransforms;
			}
		}
	}
}
