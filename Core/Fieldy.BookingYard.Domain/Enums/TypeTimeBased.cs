using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Fieldy.BookingYard.Domain.Enums
{
	//[JsonConverter(typeof(StringEnumConverter))]
	public enum TypeTimeBased
	{
		[EnumMember(Value = "date")]
		date,

		[EnumMember(Value = "week")]
		week,

		[EnumMember(Value = "month")]
		month,

		[EnumMember(Value = "year")]
		year
	}
}
