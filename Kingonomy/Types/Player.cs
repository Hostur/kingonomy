using Kingonomy.Models;

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

        public Player(PlayerModel model)
        {
            var items = model.Items;
            var resources = model.Resources;

            Items = new Item[items.Length];

            for (int i = 0; i < items.Length; i++)
                Items[i] = new Item(items[i]);

            Resources = new Resource[resources.Length];
            for (int i = 0; i < resources.Length; i++)
                Resources[i] = new Resource(resources[i]);
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
    }
}
