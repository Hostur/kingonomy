using System.Text.Json.Nodes;
using Kingonomy.Models;

namespace Kingonomy
{
    /// <summary>
    /// Item is a runtime representation of <see cref="PlayerItemModel"/> that can be used to modify metadata.
    /// </summary>
    public class Item
    {
        private readonly PlayerItemModel _model;
        private JsonObject? _metaData;
        public bool Modified;
        public string? Id => _model.Item.Id;
        public int? PlayerItemId => _model.PlayerItemId;

        public Item(PlayerItemModel model)
        {
            _model = model;
            _metaData = model?.Item.MetaData != null ? JsonNode.Parse(model.Item.MetaData)?.AsObject() : new JsonObject();
            Modified = false;
        }

        public string GetMetadataForSave() => _metaData?.ToJsonString() ?? string.Empty;

        public void AddOrUpdateKey<T>(string key, T value)
        {
            if (_metaData != null)
                _metaData = new JsonObject();

            _metaData[key] = JsonValue.Create(value);
            Modified = false;
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            if (_metaData == null || !_metaData.ContainsKey(key)) return defaultValue;

            if (_metaData[key] is JsonValue jsonValue)
            {
                return jsonValue.GetValue<T>();
            }

            return defaultValue;
        }

        public void DeleteKey(string key)
        {
            if (_metaData != null)
            {
                _metaData.Remove(key);
                Modified = false;
            }
        }
    }
}
