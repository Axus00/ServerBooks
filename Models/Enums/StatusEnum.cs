using System.Text.Json.Serialization;

namespace Books.Models.Enums
{
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum StatusEnum
  {
    Active,
    Removed
  }
}