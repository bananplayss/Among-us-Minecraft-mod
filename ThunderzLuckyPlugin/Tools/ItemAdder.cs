using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bananplaysshu.Tools {
	public static class ItemAdder {

		static ConsoleKey key;

		static bool includeBonus = false;

		public static void HandleInput() {
			switch (key) {
				case ConsoleKey.O:
					InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Trident));
					break;
				case ConsoleKey.P:
					InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Spyglass));
					break;
				case ConsoleKey.L:
					InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Gunpowder));
					break;
				case ConsoleKey.D9:
					InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.BucketOfLava));
					break;
				case ConsoleKey.D8:
					InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.IronSword));
					break;
				case ConsoleKey.D7:
					InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.EyeOfEnder));
					break;
				case ConsoleKey.D6:
					InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.EnderPearl));
					break;
				case ConsoleKey.D5:
					Hotbar.Instance.Heal(1);
					break;
				case ConsoleKey.D4:
					InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Iron));
					break;
				case ConsoleKey.D3:
					InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Sand));
					break;
				case ConsoleKey.K:
					if (includeBonus) {
						InventoryStorage.Instance.AddItemToInventoryStorage(InventoryItemDatabase.Instance.ReturnItemByEnumName(InventoryItemDatabase.InventoryItemsEnum.Plushie));
					}
					break;
			}
		}
	}
}
