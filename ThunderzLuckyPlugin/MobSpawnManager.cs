using bananplaysshu.Tools;
using HarmonyLib;
using Il2CppInterop.Runtime.Attributes;
using Reactor.Utilities.Attributes;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ProBuilder;

namespace bananplaysshu {
	[RegisterInIl2Cpp]
	public class MobSpawnManager : MonoBehaviour {
		public MobSpawnManager(IntPtr ptr) : base(ptr) { }
		public static MobSpawnManager Instance { get; private set; }

		public int endermanIndex = 0;
		public int blazeIndex = 1;
		public int enddragonIndex = 2;

		public Vector2[] endermen;
		public Vector2[] blazes;
		public Vector2 enddragon;

		[HideFromIl2Cpp]
		private List<NPCBehaviour> npcs { get; set; }

		private void Awake() {
			Instance = this;
		}

		private void Start() {
			//refactor
			endermen = new Vector2[3];
			blazes = new Vector2[3];
			enddragon = new Vector2(0, 0);

			endermen[0] = new Vector2(-9, 1.2f);
			endermen[1] = new Vector2(14, -5);
			endermen[2] = new Vector2(-2.7f, -11.6f);

			blazes[0] = new Vector3(-52.8f, 31.2f, 0.0f);
			blazes[1] = new Vector3(-43.6f, 29.0f, 0.0f);
			blazes[2] = new Vector3(-53.8f, 27.2f, 0.0f);

			enddragon = new Vector3(48, 29, 0);

			npcs = new List<NPCBehaviour>();

			SpawnAllMobs();
		}

		public void FirstSword() {
			foreach (var npc in npcs) {
				npc.FirstSwordCrafted();
			}
		}

		private void SpawnAllMobs() {
			LateTask.New(() => {

				for (int i = 0; i < 3 + 1; i++) {
					if (i == endermanIndex) InitializeEnderman();
					if (i == blazeIndex) InitializeBlazes();
					if (i == enddragonIndex) InitializeEndDragon();
				}
			}, 0.001f);
		}

		private void InitializeEnderman() {
			for (int i = 0; i < endermen.Length; i++) {
				GameObject enderman = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.enderman,
				endermen[i], Quaternion.identity);
				NPCBehaviour nPCBehaviour = enderman.GetComponent<NPCBehaviour>();

				npcs.Add(nPCBehaviour);
				nPCBehaviour.mobType = MobsEnum.Enderman;

				enderman.GetComponent<NPCBehaviour>().SetMobItem(InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.EnderPearl), 2);
			}
		}

		private void InitializeBlazes() {
			for (int i = 0; i < blazes.Length; i++) { 
				GameObject blaze = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.blaze, blazes[i], Quaternion.identity);
				NPCBehaviour nPCBehaviour = blaze.GetComponent<NPCBehaviour>();

				npcs.Add(nPCBehaviour);
				nPCBehaviour.mobType = MobsEnum.Blaze;

				blaze.GetComponent<NPCBehaviour>().SetMobItem(InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.BlazeRod), 3);
			}
		}
		private void InitializeEndDragon() {
			GameObject enderDragon = GameObject.Instantiate(ThunderzLuckyPlugin.Instance.end_dragon, enddragon, Quaternion.identity);

			NPCBehaviour nPCBehaviour = enderDragon.GetComponent<NPCBehaviour>();

			npcs.Add(nPCBehaviour);
			nPCBehaviour.mobType = MobsEnum.EnderDragon;
		}
	}
}
