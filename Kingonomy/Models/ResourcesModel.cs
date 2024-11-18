using System;

namespace Kingonomy.Models
{
    [Preserve, Serializable]
    public sealed class ResourceModel
    {
        [Preserve] public string? Id { get; set; }
        [Preserve] public float? Value { get; set; }
        [Preserve]
        public ResourceModel(string id, float value)
        {
            Id = id;
            Value = value;
        }

        [Preserve] public ResourceModel(){}
    }

    [Preserve, Serializable]
    public sealed class ResourcesModel
    {
        [Preserve] public ResourceModel[]? Resources { get; set; }

        [Preserve]
        public ResourcesModel(ResourceModel[]? resources)
        {
            Resources = resources;
        }

        [Preserve] public ResourcesModel(){}
    }
}
