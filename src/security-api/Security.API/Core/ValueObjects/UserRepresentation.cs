namespace Security.API.Core.ValueObjects
{
    public class UserRepresentation
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("credentials")]
        public IEnumerable<Credential> Credentials { get; set; }
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }
        [JsonPropertyName("attributes")]
        public Attributes Attributes { get; set; }

        public UserRepresentation(string username,
                          string password,
                          string firstName,
                          string lastName,
                          Guid aggregateId,
                          string userType)
        {
            Username = username;
            Id = aggregateId.ToString();
            FirstName = firstName;
            LastName = lastName;
            Credentials = new List<Credential>
            {
                new Credential
                {
                    Type = "password",
                    Value = password
                }
            };
            Enabled = true;
            Attributes = new Attributes
            {
                UserType = userType
            };
        }
    }
}
