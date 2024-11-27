using System;

namespace Kingonomy.Models
{
    [Preserve, Serializable]
    public sealed class PlayerModel
    {
        [Preserve] public int Id { get; set; }
        [Preserve] public string UnityId { get; set; }
        [Preserve] public Role Role { get; set; }
        [Preserve] public string Metadata { get; set; }
        [Preserve] public PlayerItemModel[]? Items { get; set; }

        [Preserve]
        public PlayerModel(int id, string unityId, Role role, string metadata, PlayerItemModel[]? items)
        {
            Id = id;
            UnityId = unityId;
            Role = role;
            Metadata = metadata;
            Items = items;
        }
    }
}
