using System;
using System.Collections.Generic;

namespace MMORPG_게임개발_시리즈_CSharp_기초_프로그래밍_입문
{
    internal class Test
    {
        // Lamda: 일회용 함수를 만드는데 사용

        enum ItemType
        {
            Weapon,
            Armor,
            Amulet,
            Ring
        }

        enum Rarity
        {
            Normal,
            Uncommon,
            Rare
        }

        class Item
        {
            public ItemType ItemType;
            public Rarity Rarity;
        }

        static List<Item> _items = new List<Item>();

        delegate bool ItemSelector(Item item); 

        static bool IsWeapon(Item item)
        {
            return item.ItemType == ItemType.Weapon;
        }

        static Item FindItem(ItemSelector selector)
        {
            foreach (Item item in _items)
            {
                if (selector(item))
                {
                    return item;
                }
            }
            return null;
        }

        static void Main()
        {
            _items.Add(new Item() { ItemType = ItemType.Weapon, Rarity = Rarity.Normal });
            _items.Add(new Item() { ItemType = ItemType.Armor, Rarity = Rarity.Uncommon });
            _items.Add(new Item() { ItemType = ItemType.Ring, Rarity = Rarity.Rare });

            ItemSelector selector = new ItemSelector((Item item) => { return item.ItemType == ItemType.Weapon; });
            Item item = FindItem(selector);
        }
    }
}
