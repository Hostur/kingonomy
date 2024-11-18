//using Kingonomy.Models;

using Kingonomy.Models;

namespace Kingonomy
{
    public sealed class Resource
    { 
        public string? Id { get; set; }
        public float? Value { get; set; }
        public bool Modified { get; set; }
        public Resource(ResourceModel resourceModel)
        {
            Id = resourceModel.Id;
            Value = resourceModel.Value;
            Modified = false;
        }

        public Resource() { }
    }
}
