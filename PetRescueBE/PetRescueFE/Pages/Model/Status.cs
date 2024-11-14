using System.Runtime.Serialization;

namespace PetRescueFE.Pages.Model;
[DataContract]
public enum Status
{
    [EnumMember(Value = "ACTIVE")]
    ACTIVE,
    [EnumMember(Value = "INACTIVE")]
    INACTIVE,
    //[EnumMember(Value = "DELETED")]
    //DELETED,
    //[EnumMember(Value = "PENDING")]
    //PENDING,
    //[EnumMember(Value = "COMPLETED")]
    //COMPLETED,
    //[EnumMember(Value = "CANCELLED")]
    //CANCELLED,
    //[EnumMember(Value = "REJECTED")]
    //REJECTED,
    //[EnumMember(Value = "APPROVED")]
    //APPROVED,
    //[EnumMember(Value = "PUBLISHED")]
    //PUBLISHED,
    //[EnumMember(Value = "UNPUBLISHED")]
    //UNPUBLISHED
}