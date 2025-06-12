using HarmonyLib;
using Il2CppInterop.Generator.Passes;
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
	public class RecipeDatabase : MonoBehaviour {
		public RecipeDatabase(IntPtr ptr) : base(ptr) { }

		[HideFromIl2Cpp]
		private List<Recipe> recipes { get; set; }

		[HideFromIl2Cpp]
		public static RecipeDatabase Instance { get; set; }

		private void Awake() {
			Instance = this; 
			recipes = new List<Recipe>();
		}

		public void AddRecipe(Recipe recipe) {
			recipes.Add(recipe);
		}

		public List<Recipe> ReturnRecipeDatabase() {
			return recipes;
		}


	}

	[HarmonyPatch(typeof(GameManager),nameof(GameManager.StartGame))]
	public static class RecipePatch {
		[HarmonyPriority(Priority.High)]
		public static void Postfix() {
			GameObject recipeDatabaseGo = new GameObject("RecipeDatabase");
			RecipeDatabase recipeDatabase = recipeDatabaseGo.AddComponent<RecipeDatabase>();


			#region Wooden Planks Recipe

			InventoryItem woodenPlankResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.WoodenPlanks);

			InventoryItem None = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.None);
			InventoryItem Wood = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Wood);
			InventoryItem[] woodenPlankIngredients = new[]{
				None, None,None,None, Wood, None, None, None, None
			};
			Recipe woodenPlanksRecipe = new Recipe(woodenPlankResult, woodenPlankIngredients,4);
			recipeDatabase.AddRecipe(woodenPlanksRecipe);

			#endregion

			#region Stick Recipe
			InventoryItem stickResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Stick);

			InventoryItem WoodenPlanks = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.WoodenPlanks);
			InventoryItem[] stickIngredients = new[]{
				None, None,None,None, WoodenPlanks, None, None, WoodenPlanks, None
			};
			Recipe stickRecipe = new Recipe(stickResult, stickIngredients, 2);
			recipeDatabase.AddRecipe(stickRecipe);
			#endregion

			#region Wooden Pickaxe Recipe
			InventoryItem woodenPickaxeResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.WoodenPickaxe);

			InventoryItem Stick = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Stick);
			InventoryItem[] woodenPickaxeIngredients = new[]{
				WoodenPlanks, WoodenPlanks,WoodenPlanks,None, Stick, None, None, Stick, None
			};
			Recipe woodenPickaxeRecipe = new Recipe(woodenPickaxeResult, woodenPickaxeIngredients, 1);
			recipeDatabase.AddRecipe(woodenPickaxeRecipe);
			#endregion

			#region Stone Pickaxe Recipe
			InventoryItem stonePickaxeResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.StonePickaxe);

			InventoryItem Cobblestone = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Cobblestone);
			InventoryItem[] stonePickaxeIngredients = new[]{
				Cobblestone, Cobblestone,Cobblestone,None, Stick, None, None, Stick, None
			};
			Recipe stonePickaxeRecipe = new Recipe(stonePickaxeResult, stonePickaxeIngredients, 1);
			recipeDatabase.AddRecipe(stonePickaxeRecipe);
			#endregion

			#region Bucket Recipe
			InventoryItem bucketResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Bucket);

			InventoryItem Iron = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Iron);
			InventoryItem[] bucketIngredients = new[]{
				None, None,None,Iron, None, Iron, None, Iron, None
			};
			Recipe bucketRecipe = new Recipe(bucketResult, bucketIngredients, 1);
			recipeDatabase.AddRecipe(bucketRecipe);
			#endregion

			#region Iron Sword Recipe
			InventoryItem ironSwordResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.IronSword);

			InventoryItem[] ironSwordIngredients = new[]{
				None, Iron,None,None, Iron, None, None, Stick, None
			};
			Recipe ironSwordRecipe = new Recipe(ironSwordResult, ironSwordIngredients, 1);
			recipeDatabase.AddRecipe(ironSwordRecipe);
			#endregion

			#region blazePowder Recipe
			InventoryItem bpResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.BlazePowder);

			InventoryItem blazeRod = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.BlazeRod);

			InventoryItem[] bpIngredients = new[]{
				None, None,None,None, blazeRod, None, None, None, None
			};
			Recipe bpRecipe = new Recipe(bpResult, bpIngredients, 2);
			recipeDatabase.AddRecipe(bpRecipe);
			#endregion

			#region Eye of Ender Recipe
			InventoryItem eoeResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.EyeOfEnder);

			InventoryItem enderPearl = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.EnderPearl);

			InventoryItem blazePowder = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.BlazePowder);

			InventoryItem[] eoeIngredients = new[]{
				None, None,None,None, enderPearl, blazePowder, None, None, None
			};
			Recipe eoeRecipe = new Recipe(eoeResult, eoeIngredients, 1);
			recipeDatabase.AddRecipe(eoeRecipe);
			#endregion

			#region TNT Recipe
			InventoryItem tntResult = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.TNT);

			InventoryItem sand = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Sand);

			InventoryItem gunpowder = InventoryItemDatabase.Instance.ReturnItemByEnumName(
				InventoryItemDatabase.InventoryItemsEnum.Gunpowder);

			InventoryItem[] tntIngredients = new[]{
				gunpowder, sand,gunpowder,sand, gunpowder, sand, gunpowder, sand, gunpowder
			};
			Recipe tntRecipe = new Recipe(tntResult, tntIngredients, 1);
			recipeDatabase.AddRecipe(tntRecipe);
			#endregion


		}
	}
}
