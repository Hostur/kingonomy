using System;

namespace Kingonomy.Models
{
    [Preserve, Serializable]
    public sealed class ItemModel
    {
        [Preserve] public string Id { get; set; }
        [Preserve] public bool IsStackable { get; set; }
        [Preserve] public float Quantity { get; set; }
        [Preserve] public string? MetaData { get; set; }

        [Preserve]
        public ItemModel(string id, bool isStackable, float quantity, string metaData)
        {
            Id = id;
            IsStackable = isStackable;
            Quantity = quantity;
            MetaData = metaData;
        }
    }

    [Preserve, Serializable]
    public sealed class PlayerItemModel
    {
        [Preserve] public int PlayerItemId { get; set; }
        [Preserve] public ItemModel Item { get; set; }

        [Preserve]
        public PlayerItemModel(int playerItemId, ItemModel item)
        { PlayerItemId = playerItemId;
           Item = item;
        }
    }
}
