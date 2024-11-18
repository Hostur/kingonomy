using System;

namespace Kingonomy.Models
{
    [Preserve, Serializable]
    public sealed class ItemModel
    {
        [Preserve]
        public ItemModel(string id, string metaData)
        {
            Id = id;
            MetaData = metaData;
        }
        [Preserve] public ItemModel(){}
        [Preserve] public string? Id { get; set; }
        [Preserve] public string? MetaData { get; set; }
    }

    [Preserve, Serializable]
    public sealed class PlayerItemModel
    {
        [Preserve]
        public PlayerItemModel(int? playerItemId, string? itemId, string? metaData)
        {
            PlayerItemId = playerItemId;
            ItemId = itemId;
            MetaData = metaData;
        }

        [Preserve] public PlayerItemModel(){}
        [Preserve] public int? PlayerItemId { get; set; }
        [Preserve] public string? ItemId { get; set; }
        [Preserve] public string? MetaData { get; set; }
    }

    [Preserve, Serializable]
    public sealed class ItemsModel
    {
        [Preserve]
        public ItemsModel(ItemModel[] items)
        {
            Items = items;
        }
        [Preserve] public ItemsModel(){}

        [Preserve] public ItemModel[]? Items { get; set; }
    }

    [Preserve, Serializable]
    public sealed class PlayerItemsModel
    {
        [Preserve]
        public PlayerItemsModel(PlayerItemModel[] items)
        {
            Items = items;
        }
        [Preserve] public PlayerItemsModel() { }

        [Preserve] public PlayerItemModel[]? Items { get; set; }
    }
}
