using System;

namespace Kingonomy.Models
{
    [Preserve, Serializable]
    public sealed class PlayerModel
    {
        [Preserve] public ResourcesModel? Resources { get; set; }
        [Preserve] public PlayerItemsModel? Items { get; set; }
        [Preserve] public PlayerModel() { }

        [Preserve]
        public PlayerModel(ResourcesModel resources, PlayerItemsModel items)
        {
            Resources = resources;
            Items = items;
        }
    }
}
