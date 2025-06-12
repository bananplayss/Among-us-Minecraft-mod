using bananplaysshu.Tools;
using HarmonyLib;
using Reactor.Networking.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class Trident : MonoBehaviour{
		public Trident(IntPtr ptr) : base(ptr) { }

		Vector3 target;

		private void Awake() {
			GetComponent<BoxCollider2D>().size *= .013f;
		}

		private void Start() {
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			target = mousePos-transform.position;
		}

		private void Update() {
			transform.position += target*.05f;
		}

		private void OnTriggerEnter2D(Collider2D other) {

			if(other.TryGetComponent<PlayerControl>(out PlayerControl pc)){
				if (pc.Data.IsDead || pc.Data.Role.IsImpostor) return;
				CustomRPC.DeathRpc(pc);
				Destroy(gameObject);
			}	
		}
	}
}
