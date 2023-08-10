using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Catalog_API.Enums
{
[JsonConverter(typeof(StringEnumConverter))]
public enum UserRole
{
    [EnumMember(Value = "User")]
    User,

    [EnumMember(Value = "Admin")]
    Admin
}
}