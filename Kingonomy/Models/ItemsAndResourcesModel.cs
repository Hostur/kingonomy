using System;

namespace Kingonomy.Models
{
    [Preserve, Serializable]
    public sealed class ItemsAndResourcesModel
    {
        [Preserve]
        public ItemsAndResourcesModel(ResourcesModel[] resources, ItemModel[] items)
        {
            Resources = resources;
            Items = items;
        }
        [Preserve] public ItemsAndResourcesModel(){}
        [Preserve] public ResourcesModel[]? Resources { get; set; }
        [Preserve] public ItemModel[]? Items { get; set; }
    }
}
