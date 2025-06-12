using bananplaysshu.Buttons;
using Il2CppSystem.Threading.Tasks;
using MiraAPI.Hud;
using Reactor.Networking.Attributes;
using Reactor.Utilities.Attributes;
using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class NPCBehaviour : MonoBehaviour {
		public NPCBehaviour(IntPtr ptr) : base(ptr) { }

		public Transform target;
		private bool knocked;
		private Vector3 originalPos;
		private int health = 5;
		private SpriteRenderer sr;
		public InventoryItem mobItem;
		private int mobItemQuantity;
		private float maxAggroDistance = 4.5f;
		private float damageDistance = .5f;
		private float damageTimer = 2f;
		private float damageTimerMax = 2f;
		private int damage = 1;
		bool started = false;
		bool dead = false;
		Vector3 destination = Vector3.zero;
		public PlayerControl closestPlayer;

		public MobsEnum mobType;

		private void Start() {
			transform.localScale *= .25f;
			if (mobType == MobsEnum.EnderDragon) health *= 3;
			if (mobType == MobsEnum.Piglin) damageTimer *= .4f;
			if (mobType == MobsEnum.Piglin) damageTimerMax *= .4f;
			sr = GetComponent<SpriteRenderer>();
			originalPos = transform.position;
		}

		public void DisableObject() {
			gameObject.SetActive(false);
		}

		public void SetMobItem(InventoryItem mobItem, int quantity) {
			this.mobItem = mobItem;
			this.mobItemQuantity = quantity;
		}

		public void FirstSwordCrafted() {
			GetComponent<Animator>().SetBool("hasSword", true);
		}

		private void Update() {
			if (target == null && mobType == MobsEnum.Piglin) Destroy(this.gameObject);
			if (dead) return;

			if (target != null && !knocked) {
				if(Vector3.Distance(target.position, transform.position) > maxAggroDistance) {
					target = null;
				}

				if(target != null) {
					if (Vector3.Distance(target.position, transform.position) < damageDistance) {
						damageTimer -= Time.deltaTime;
						if (damageTimer < 0) {
							if (mobType == MobsEnum.Piglin) {
								Die();
								closestPlayer.Die(DeathReason.Kill, true);
							} else {
								Hotbar.Instance.Damage(damage);
							}
							damageTimer = damageTimerMax;
						}
					}
				}
				

				GetComponent<Animator>().SetBool("hasSword", true);
				float x = transform.position.x;
				if (target != null && Vector3.Distance(transform.position, target.position) > .35f) {
					float moveSpeed = mobType == MobsEnum.EnderDragon ? 1.3f : 1.7f;
					transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
				}
				if(transform.position.x < x) {
					sr.flipX = true;
				} else {
					sr.flipX = false;
				}

			} else {
				if (GetComponent<Animator>().GetBool("hasSword") != true) return;
					if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, transform.position) < 5f) {
						target = PlayerControl.LocalPlayer.transform;
					}
				
			}
			if (knocked) {
				float multiplayer = 8f;
				
				if (!started) {
					if (sr.flipX) {
						started = true;

						destination = transform.position + (Vector3.right * 3.5f);
					} else {
						started = true;

						destination = transform.position + (Vector3.left * 3.5f);
					}
				}

				
					transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * multiplayer);
				

				if (Vector3.Distance(transform.position, destination) < .5f) {
					knocked = false;
					started = true;
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D other) {
			if (other.TryGetComponent<Hand>(out Hand hp)) {
				if (knocked) return;
				Damage(1);
			}
		}

		private void Damage(int dmg) {
			health -= dmg;
			knocked = true;
			if (health <= 0) { Die(); }
		}

		private void Die() {
			if (mobType == MobsEnum.EnderDragon) {
				CustomButtonSingleton<EscapeButton>.Instance.SetActive(true, PlayerControl.LocalPlayer.Data.Role);
				for (int i = 0; i < PortalManager.Instance.switchTransforms.Count; i++) {
					if (i == PortalManager.Instance.switchTransforms.Count-1) PortalManager.Instance.switchTransforms[i].gameObject.SetActive(true);
					else PortalManager.Instance.switchTransforms[i].gameObject.SetActive(false);
				}
				
			}

			target = null;
			knocked = false;
			health = 0;
			
			if (mobItem != null && !dead) {
				for (int i = 0; i < mobItemQuantity+1; i++) {
					if (mobItem == InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.EnderPearl)) EnderPearlButton.canUse = true;

					InventoryStorage.Instance.AddItemToInventoryStorage(mobItem);

				}
			}
			dead = true;
			if(mobType == MobsEnum.Piglin) {
				gameObject.SetActive(false);
			} else {
				GetComponent<Animator>().Play(mobType.ToString() + "Death");
			}
			

			
		}

		private IEnumerator RespawnCoroutine() {
			if (MobSpawnManager.Instance == null) yield return null;
			if (mobType == MobsEnum.EnderDragon)yield  return null;
			gameObject.SetActive(false);
			yield return new WaitForSeconds(40f);
			transform.position = originalPos;
			gameObject.SetActive(true);
			
		}
	}
}
