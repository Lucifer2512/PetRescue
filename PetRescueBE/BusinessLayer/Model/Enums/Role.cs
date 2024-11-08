using System.Runtime.Serialization;

namespace BusinessLayer.Model.Enums;
[DataContract]
public enum Role
{
    [EnumMember(Value = "ADMIN")]
    ADMIN,
    [EnumMember(Value = "GUEST")]
    GUEST,
    [EnumMember(Value = "DONORS")]
    DONORS,
    [EnumMember(Value = "SHELTER_STAFF")]
    SHELTER_STAFF,
    [EnumMember(Value = "ADOPTERS")]
    ADOPTERS,
    [EnumMember(Value = "VOLUNTEERS")]
    VOLUNTEERS
}