using IWebFrame = WebGP.Application.Common.Interfaces.IFrame;
using IJavaFrame = Java.Mapper.DataLogger.Frame.IFrame;
using Newtonsoft.Json.Linq;

namespace WebGP.Java;

public record ProxyFrame(IJavaFrame Frame) : IWebFrame
{
    public string Type => Frame.Type;
    public JToken Data => Frame.Data;

    public bool IsRequired(IWebFrame? lastFrame)
        => lastFrame is not ProxyFrame proxy || Frame.IsRequired(proxy.Frame);
}
