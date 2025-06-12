using HarmonyLib;
using MiraAPI.Utilities.Assets;
using UnityEngine;
using static Il2CppSystem.Xml.XmlWellFormedWriter.AttributeValueCache;

//THIS SHITTY CLASS IS PROHIBITED TO LOOK AT, I'M SERIOUS, DON'T EXHAUST YOURSELF PLEASE


namespace bananplaysshu {
	[HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
	internal static class InitializeTree {
		#region Fields


		//IF YOU'RE LOOKING AT THIS.... I ADDED IT AFTER I DELIVERED THE PROJECT SO I DIDNT CARE MAKING A METHOD
		public static Vector2 treePos = new Vector2(2.7f, -8.5f);
		public static Vector2 treePos2 = new Vector2(2.2f, 0.7f);
		public static Vector2 treePos3 = new Vector2(-3.8f, 0.7f);
		public static Vector2 treePos4 = new Vector2(-21.2f, -3.7f);
		public static Vector2 treePos5 = new Vector2(.3f, -14f);
		public static Vector2 treePos6 = new Vector2(5.2f, -14.5f);
		public static Vector2 treePos7 = new Vector2(-8.5f, -2.4f);


		public static Transform tree;
		public static Transform tree2;
		public static Transform tree3;
		public static Transform tree4;
		public static Transform tree5;
		public static Transform tree6;
		public static Transform tree7;

		public static SpriteRenderer treeRenderer;
		public static SpriteRenderer treeRenderer2;
		public static SpriteRenderer treeRenderer3;
		public static SpriteRenderer treeRenderer4;
		public static SpriteRenderer treeRenderer5;
		public static SpriteRenderer treeRenderer6;
		public static SpriteRenderer treeRenderer7;

		#endregion


		[HarmonyPostfix]
		public static void InitializeTreeObject() {
			LateTask.New(() => {

				//IF YOU'RE LOOKING AT THIS.... I ADDED IT AFTER I DELIVERED THE PROJECT SO I DIDNT CARE MAKING A METHOD

				GameObject newTree = new GameObject();
				InitializeTree.tree = newTree.transform;
				newTree.transform.position = treePos;
				HandleTreeInteraction.treePosY = tree.position.y;
				treeRenderer = newTree.AddComponent<SpriteRenderer>();
				treeRenderer.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
				newTree.name = "Tree";

				GameObject newTree2 = new GameObject();
				InitializeTree.tree2 = newTree2.transform;
				newTree2.transform.position = treePos2;
				HandleTreeInteraction.treePosY2 = tree2.position.y;
				treeRenderer2 = newTree2.AddComponent<SpriteRenderer>();
				treeRenderer2.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
				newTree2.name = "Tree";

				GameObject newTree3 = new GameObject();
				InitializeTree.tree3 = newTree3.transform;
				newTree3.transform.position = treePos3;
				HandleTreeInteraction.treePosY3 = tree3.position.y;
				treeRenderer3 = newTree3.AddComponent<SpriteRenderer>();
				treeRenderer3.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
				newTree3.name = "Tree";

				GameObject newTree4 = new GameObject();
				InitializeTree.tree4 = newTree4.transform;
				newTree4.transform.position = treePos4;
				HandleTreeInteraction.treePosY4 = tree4.position.y;
				treeRenderer4 = newTree4.AddComponent<SpriteRenderer>();
				treeRenderer4.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
				newTree4.name = "Tree";

				GameObject newTree5 = new GameObject();
				InitializeTree.tree5 = newTree5.transform;
				newTree5.transform.position = treePos5;
				HandleTreeInteraction.treePosY5 = tree5.position.y;
				treeRenderer5 = newTree5.AddComponent<SpriteRenderer>();
				treeRenderer5.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
				newTree5.name = "Tree";

				GameObject newTree6 = new GameObject();
				InitializeTree.tree6 = newTree6.transform;
				newTree6.transform.position = treePos6;
				HandleTreeInteraction.treePosY6 = tree6.position.y;
				treeRenderer6 = newTree6.AddComponent<SpriteRenderer>();
				treeRenderer6.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
				newTree6.name = "Tree6";

				GameObject newTree7 = new GameObject();
				InitializeTree.tree7 = newTree7.transform;
				newTree7.transform.position = treePos7;
				HandleTreeInteraction.treePosY7 = tree7.position.y;
				treeRenderer7 = newTree7.AddComponent<SpriteRenderer>();
				treeRenderer7.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
				newTree7.name = "Tree7";
			}, 0.001f);
		}

		#region Sorting Methods
		public static void MoveTreeSortOrderUp(SpriteRenderer renderer) {
			renderer.sortingOrder = 10;
		}

		public static void MoveTreeSortOrderDown(SpriteRenderer renderer) {
			renderer.sortingOrder = 0;
		}
		#endregion
	}


	[HarmonyPatch(typeof(ModManager), nameof(ModManager.LateUpdate))]
	static class HandleTreeInteraction {

		#region Fields

		public static float treePosY;
		public static float treePosY2;
		public static float treePosY3;
		public static float treePosY4;
		public static float treePosY5;
		public static float treePosY6;
		public static float treePosY7;

		private static float checkInputCounter = .25f;
		private static float checkInputCounterMax = .25f;
		private static float playerPosY = 0;
		private static float ySubstractFromTreePos = .75f;
		private static float treeInteractCooldownCounter = 3f;
		private static float treeInteractCooldownCounterMax = 3f;

		private static bool canCut = true;

		public static int interactedCount = 0;
		public static int interactedCountNeeded = 3;
		#endregion

		#region Don't look
		//IF YOU'RE LOOKING AT THIS.... I ADDED IT AFTER I DELIVERED THE PROJECT SO I DIDNT CARE MAKING A METHOD
		[HarmonyPostfix]
		public static void HandleTreeBehaviourPostfix() {
			#region UpdateSortingOrder

			if (InitializeTree.tree == null || InitializeTree.tree2 == null || InitializeTree.tree3 == null || InitializeTree.tree4 == null || InitializeTree.tree5 == null || InitializeTree.tree6 == null || InitializeTree.tree7 == null) {
				return;
				
			}
			playerPosY = PlayerControl.LocalPlayer.transform.position.y;


			if (playerPosY > treePosY - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer);
			}

			if (playerPosY > treePosY - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer);
			}

			//2

			if (playerPosY > treePosY2 - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer2);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer2);
			}

			if (playerPosY > treePosY2 - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer2);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer2);
			}

			//3

			if (playerPosY > treePosY3 - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer3);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer3);
			}

			if (playerPosY > treePosY3 - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer3);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer3);
			}

			//4
			if (playerPosY > treePosY4 - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer4);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer4);
			}

			if (playerPosY > treePosY4 - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer4);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer4);
			}

			//5
			if (playerPosY > treePosY5 - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer5);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer5);
			}

			if (playerPosY > treePosY5 - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer5);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer5);
			}

			//6
			if (playerPosY > treePosY6 - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer6);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer6);
			}

			if (playerPosY > treePosY6 - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer6);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer6);
			}

			//7
			if (playerPosY > treePosY7 - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer7);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer7);
			}

			if (playerPosY > treePosY6 - ySubstractFromTreePos) {
				InitializeTree.MoveTreeSortOrderUp(InitializeTree.treeRenderer7);

			} else {
				InitializeTree.MoveTreeSortOrderDown(InitializeTree.treeRenderer7);
			}
			#endregion
			#endregion
			#region HandleInteractions For Tree one
			float distanceNeeded = .9f;
			float yPosDiff = .5f;

			Vector3 treePos = InitializeTree.tree.transform.position - new Vector3(0, ySubstractFromTreePos, 0);
			if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, treePos) < distanceNeeded &&
				(playerPosY - treePosY - ySubstractFromTreePos) <= yPosDiff) {
				if (Hand.Instance == null) return;
				if (Hand.Instance.IsMining() && canCut) {
					checkInputCounter -= Time.deltaTime;
					if (checkInputCounter < 0) {
						checkInputCounter = checkInputCounterMax;

						interactedCount++;

						if (interactedCount > interactedCountNeeded) {
							if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inv)) {
								InventoryItem wood = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Wood);

								InventoryStorage.Instance.AddItemToInventoryStorage(wood);
							} else {
								Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
							}

							InitializeTree.treeRenderer.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CutTree.png", 180);

							interactedCount = 0;
							treeInteractCooldownCounter = treeInteractCooldownCounterMax;
							canCut = false;

						}
					}
				}

				if (!canCut) {
					treeInteractCooldownCounter -= Time.deltaTime;
					if (treeInteractCooldownCounter < 0) {
						InitializeTree.treeRenderer.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
						treeInteractCooldownCounter = treeInteractCooldownCounterMax;
						canCut = true;
					}
				}
			}
			#endregion

			#region HandleInteractions For Tree two

			Vector3 treePos2 = InitializeTree.tree2.transform.position - new Vector3(0, ySubstractFromTreePos, 0);
			if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, treePos2) < distanceNeeded &&
				(playerPosY - treePosY2 - ySubstractFromTreePos) <= yPosDiff) {
				if (Hand.Instance == null) return;
				if (Hand.Instance.IsMining() && canCut) {
					checkInputCounter -= Time.deltaTime;
					if (checkInputCounter < 0) {
						checkInputCounter = checkInputCounterMax;

						interactedCount++;

						if (interactedCount > interactedCountNeeded) {
							if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inv)) {
								InventoryItem wood = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Wood);

								InventoryStorage.Instance.AddItemToInventoryStorage(wood);
							} else {
								Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
							}

							InitializeTree.treeRenderer2.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CutTree.png", 180);

							interactedCount = 0;
							treeInteractCooldownCounter = treeInteractCooldownCounterMax;
							canCut = false;

						}
					}
				}

				if (!canCut) {
					treeInteractCooldownCounter -= Time.deltaTime;
					if (treeInteractCooldownCounter < 0) {
						InitializeTree.treeRenderer2.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
						treeInteractCooldownCounter = treeInteractCooldownCounterMax;
						canCut = true;
					}
				}
			}
			#region HandleInteractions For Tree three

			Vector3 treePos3 = InitializeTree.tree3.transform.position - new Vector3(0, ySubstractFromTreePos, 0);
			if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, treePos3) < distanceNeeded &&
				(playerPosY - treePosY3 - ySubstractFromTreePos) <= yPosDiff) {
				if (Hand.Instance == null) return;
				if (Hand.Instance.IsMining() && canCut) {
					checkInputCounter -= Time.deltaTime;
					if (checkInputCounter < 0) {
						checkInputCounter = checkInputCounterMax;

						interactedCount++;

						if (interactedCount > interactedCountNeeded) {
							if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inv)) {
								InventoryItem wood = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Wood);

								InventoryStorage.Instance.AddItemToInventoryStorage(wood);
							} else {
								Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
							}

							InitializeTree.treeRenderer3.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CutTree.png", 180);

							interactedCount = 0;
							treeInteractCooldownCounter = treeInteractCooldownCounterMax;
							canCut = false;

						}
					}
				}

				if (!canCut) {
					treeInteractCooldownCounter -= Time.deltaTime;
					if (treeInteractCooldownCounter < 0) {
						InitializeTree.treeRenderer3.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
						treeInteractCooldownCounter = treeInteractCooldownCounterMax;
						canCut = true;
					}
				}
			}


			#region HandleInteractions For Tree four
			Vector3 treePos4 = InitializeTree.tree4.transform.position - new Vector3(0, ySubstractFromTreePos, 0);
			if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, treePos4) < distanceNeeded &&
				(playerPosY - treePosY4 - ySubstractFromTreePos) <= yPosDiff) {
				if (Hand.Instance == null) return;
				if (Hand.Instance.IsMining() && canCut) {
					checkInputCounter -= Time.deltaTime;
					if (checkInputCounter < 0) {
						checkInputCounter = checkInputCounterMax;

						interactedCount++;

						if (interactedCount > interactedCountNeeded) {
							if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inv)) {
								InventoryItem wood = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Wood);

								InventoryStorage.Instance.AddItemToInventoryStorage(wood);
							} else {
								Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
							}

							InitializeTree.treeRenderer4.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CutTree.png", 180);

							interactedCount = 0;
							treeInteractCooldownCounter = treeInteractCooldownCounterMax;
							canCut = false;

						}
					}
				}

				if (!canCut) {
					treeInteractCooldownCounter -= Time.deltaTime;
					if (treeInteractCooldownCounter < 0) {
						InitializeTree.treeRenderer4.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
						treeInteractCooldownCounter = treeInteractCooldownCounterMax;
						canCut = true;
					}
				}
			}

			#region HandleInteractions For Tree four
			Vector3 treePos5 = InitializeTree.tree5.transform.position - new Vector3(0, ySubstractFromTreePos, 0);
			if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, treePos5) < distanceNeeded &&
				(playerPosY - treePosY5 - ySubstractFromTreePos) <= yPosDiff) {
				if (Hand.Instance == null) return;
				if (Hand.Instance.IsMining() && canCut) {
					checkInputCounter -= Time.deltaTime;
					if (checkInputCounter < 0) {
						checkInputCounter = checkInputCounterMax;

						interactedCount++;

						if (interactedCount > interactedCountNeeded) {
							if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inv)) {
								InventoryItem wood = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Wood);

								InventoryStorage.Instance.AddItemToInventoryStorage(wood);
							} else {
								Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
							}

							InitializeTree.treeRenderer5.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CutTree.png", 180);

							interactedCount = 0;
							treeInteractCooldownCounter = treeInteractCooldownCounterMax;
							canCut = false;

						}
					}
				}

				if (!canCut) {
					treeInteractCooldownCounter -= Time.deltaTime;
					if (treeInteractCooldownCounter < 0) {
						InitializeTree.treeRenderer5.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
						treeInteractCooldownCounter = treeInteractCooldownCounterMax;
						canCut = true;
					}
				}
			}

			#region HandleInteractions For Tree four
			Vector3 treePos6 = InitializeTree.tree6.transform.position - new Vector3(0, ySubstractFromTreePos, 0);
			if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, treePos6) < distanceNeeded &&
				(playerPosY - treePosY6 - ySubstractFromTreePos) <= yPosDiff) {
				if (Hand.Instance == null) return;
				if (Hand.Instance.IsMining() && canCut) {
					checkInputCounter -= Time.deltaTime;
					if (checkInputCounter < 0) {
						checkInputCounter = checkInputCounterMax;

						interactedCount++;

						if (interactedCount > interactedCountNeeded) {
							if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inv)) {
								InventoryItem wood = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Wood);

								InventoryStorage.Instance.AddItemToInventoryStorage(wood);
							} else {
								Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
							}

							InitializeTree.treeRenderer6.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CutTree.png", 180);

							interactedCount = 0;
							treeInteractCooldownCounter = treeInteractCooldownCounterMax;
							canCut = false;

						}
					}
				}

				if (!canCut) {
					treeInteractCooldownCounter -= Time.deltaTime;
					if (treeInteractCooldownCounter < 0) {
						InitializeTree.treeRenderer6.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
						treeInteractCooldownCounter = treeInteractCooldownCounterMax;
						canCut = true;
					}
				}
			}

			#region HandleInteractions For Tree four
			Vector3 treePos7 = InitializeTree.tree7.transform.position - new Vector3(0, ySubstractFromTreePos, 0);
			if (Vector3.Distance(PlayerControl.LocalPlayer.transform.position, treePos7) < distanceNeeded &&
				(playerPosY - treePosY7 - ySubstractFromTreePos) <= yPosDiff) {
				if (Hand.Instance == null) return;
				if (Hand.Instance.IsMining() && canCut) {
					checkInputCounter -= Time.deltaTime;
					if (checkInputCounter < 0) {
						checkInputCounter = checkInputCounterMax;

						interactedCount++;

						if (interactedCount > interactedCountNeeded) {
							if (PlayerControl.LocalPlayer.TryGetComponent<InventorySystem>(out InventorySystem inv)) {
								InventoryItem wood = InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Wood);

								InventoryStorage.Instance.AddItemToInventoryStorage(wood);
							} else {
								Debug.LogError("Player didn't have an InventorySystem component on 'em. Tragic...");
							}

							InitializeTree.treeRenderer7.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.CutTree.png", 180);

							interactedCount = 0;
							treeInteractCooldownCounter = treeInteractCooldownCounterMax;
							canCut = false;

						}
					}
				}

				if (!canCut) {
					treeInteractCooldownCounter -= Time.deltaTime;
					if (treeInteractCooldownCounter < 0) {
						InitializeTree.treeRenderer7.sprite = GurgeUtils.LoadSprite("ThunderzLuckyPlugin.Resources.Tree.png", 180);
						treeInteractCooldownCounter = treeInteractCooldownCounterMax;
						canCut = true;
					}
				}
			}
		}
		#endregion
	}
	#endregion
}
	#endregion
#endregion


#endregion
#endregion
