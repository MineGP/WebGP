using WebGP.Application.Common.VM;
using WebGP.Domain.Entities;

namespace WebGP.Application.Common.Mapper
{
    public static partial class MapperExt
    {
        public static OnlineLogVM MapToVM(this OnlineLog value) => new OnlineLogVM()
        {
            Day = value.Day.ToString("yyyy-MM-dd"),
            Seconds = value.Seconds
        };
    }
}
