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
	public class HandParent : MonoBehaviour {
		public HandParent(IntPtr ptr) : base(ptr) { }

		[HideFromIl2Cpp]
		public static HandParent Instance { get; set; }

		[HideFromIl2Cpp]
		public Animator animator { get; set; }

		public Hand hand;

		private void Awake() {
			Instance = this;
			animator = GetComponent<Animator>();
		}

		public bool GetFlipX() {
			return animator.GetBool("FlipX");
		}
	}
}
