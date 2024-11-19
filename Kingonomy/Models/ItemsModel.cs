using System;

namespace Kingonomy.Models
{
    [Preserve, Serializable]
    public sealed class ItemTemplateModel
    {
        [Preserve]
        public ItemTemplateModel(string id, string metaData)
        {
            Id = id;
            MetaData = metaData;
        }
        [Preserve] public ItemTemplateModel(){}
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
}
