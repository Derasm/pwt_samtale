using System.Runtime.Serialization;

namespace API.Models;

public enum SizesEnum
{
    [EnumMember(Value="2XS")]
    XXS,
    [EnumMember(Value="XS")]
    XS,
    [EnumMember(Value="S")]
    S,    
    [EnumMember(Value="M")]
    M,
    [EnumMember(Value="L")]
    L,
    [EnumMember(Value="XL")]
    XL,
    [EnumMember(Value="2XL")]
    XXL,
    [EnumMember(Value="3XXL")]
    XXXL,
    [EnumMember(Value="4XXL")]
    XXXXL,
    [EnumMember(Value="5XXL")]
    XXXXXL,
    
}