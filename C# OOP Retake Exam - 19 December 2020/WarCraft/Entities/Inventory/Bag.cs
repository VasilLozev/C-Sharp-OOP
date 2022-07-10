using System;
using System.Collections.Generic;
using System.Text;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Inventory
{
    public abstract class Bag : IBag
    {
        protected int _capacity = 100;
        public int Capacity { get { return _capacity; } set {
                _capacity = value;
            } }
        protected int _load = 0;
        public int Load { get { return _load; } set
            {
                foreach (var item in Items)
                {
                    _load += item.Weight;
                }
                value = _capacity;
            }
        }
        List<Item> _items = new List<Item>();
        public IReadOnlyCollection<Item> Items { get { return _items; } }

        public void AddItem(Item item)
        {
           _items.Add(item);
        }
        
        public Item GetItem(string name)
        {
            Item item1 = null;
            string itemName = "";
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("Bag is empty!");
            }
            foreach (var item in _items)
            {          
                if (item.GetType().Name == name)
                {
                    itemName = name;
                    item1 = item;
                }  
            }
           
            if (itemName == "")
            {
                throw new ArgumentException($"No item with name {name} in bag!");
            }
            _items.Remove(item1);
            return item1;
        }
        public Bag(int capacity)
        {
            _capacity = capacity;
        }
    }
}
