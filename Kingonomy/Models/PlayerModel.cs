using System;

namespace Kingonomy.Models
{
    [Preserve, Serializable]
    public sealed class PlayerModel
    {
        [Preserve] public ResourceModel[]? Resources { get; set; }
        [Preserve] public PlayerItemModel[]? Items { get; set; }
        [Preserve] public PlayerModel() { }

        [Preserve]
        public PlayerModel(ResourceModel[] resources, PlayerItemModel[] items)
        {
            Resources = resources;
            Items = items;
        }
    }
}
