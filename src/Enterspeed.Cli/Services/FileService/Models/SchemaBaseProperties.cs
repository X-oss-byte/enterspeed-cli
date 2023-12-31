﻿using System.Text.Json.Serialization;

namespace Enterspeed.Cli.Services.FileService.Models
{

    public class SchemaBaseProperties
    {
        [JsonPropertyName("triggers")]
        public object Triggers { get; set; }

        [JsonPropertyName("sourceEntityTypes")]
        public object SourceEntityTypes { get; set; }

        [JsonPropertyName("route")]
        public object Route { get; set; }

        [JsonPropertyName("environments")]
        public object[] Environments { get; set; }

        [JsonPropertyName("actions")]
        public object[] Actions { get; set; }

        [JsonPropertyName("destinations")]
        public object[] Destinations { get; set; }

        [JsonPropertyName("properties")]
        public object Properties { get; set; }
    }
}