﻿using Kingonomy.Models;

namespace Kingonomy
{
    public sealed class Player
    {
        public Item[] Items;
        public Resource[] Resources;

        public bool Modified
        {
            get
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i].Modified) return true;
                }

                for (int i = 0; i < Resources.Length; i++)
                {
                    if (Resources[i].Modified) return true;
                }

                return false;
            }
        }

        public Player(PlayerItemModel[] items, ResourceModel[] resources)
        {
            Items = new Item[items.Length];

            for (int i = 0; i < items.Length; i++)
                Items[i] = new Item(items[i]);

            Resources = new Resource[resources.Length];
            for (int i = 0; i < resources.Length; i++)
                Resources[i] = new Resource(resources[i]);
        }

        public Player(PlayerItemsModel items, ResourcesModel resources)
        {
            Items = new Item[items.Items.Length];

            for (int i = 0; i < items.Items.Length; i++)
                Items[i] = new Item(items.Items[i]);

            Resources = new Resource[resources.Resources.Length];
            for (int i = 0; i < resources.Resources.Length; i++)
                Resources[i] = new Resource(resources.Resources[i]);
        }
    }
}