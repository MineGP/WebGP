using Newtonsoft.Json.Linq;

namespace WebGP.Application.Common.Interfaces;

public interface IFrame
{
    string Type { get; }

    JToken Data { get; }

    bool IsRequired(IFrame? lastFrame);
}
