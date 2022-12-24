﻿namespace Security.API.ViewModels
{
    public class KeycloakErrorViewModel
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }
        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; }
    }
}
