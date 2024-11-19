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
}
