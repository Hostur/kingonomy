using Kingonomy.Models;

namespace Kingonomy
{
    public sealed class Player
    {
        public int Id { get; set; }
        public string UnityId { get; set; }
        public Role Role { get; set; }
        public string Metadata { get; set; }
        public Item[] Items;

        public bool Modified
        {
            get
            {
                for (int i = 0; i < Items.Length; i++)
                {
                    if (Items[i].Modified) return true;
                }

                return false;
            }
        }

        public Player(PlayerModel model)
        {
            Id = model.Id;
            UnityId = model.UnityId;
            Role = model.Role;
            Metadata = model.Metadata;

            var items = model.Items;

            Items = new Item[items.Length];

            for (int i = 0; i < items.Length; i++)
                Items[i] = new Item(items[i]);
        }
    }
}
