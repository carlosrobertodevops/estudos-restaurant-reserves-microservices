namespace Security.API.Core.ValueObjects
{
    public class Credential
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
