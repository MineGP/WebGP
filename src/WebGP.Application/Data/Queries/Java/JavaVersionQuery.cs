using MediatR;
using WebGP.Application.Common.Interfaces;

namespace WebGP.Application.Data.Queries.Java;

public record CheckJavaVersionQuery(string GameVersion, int BuildVersion) : IRequest<bool>;
public record UpdateJavaVersionQuery(string GameVersion, int BuildVersion) : IStreamRequest<IFrame>;
public record ApplyJavaVersionQuery(string GameVersion, int BuildVersion, Stream InputFile) : IStreamRequest<IFrame>;
public record GetApplyJavaVersionQuery(string Name) : IRequest<Stream?>;
public record GetSourcesJavaVersionQuery(string GameVersion, int BuildVersion) : IRequest<Stream?>;

public class JavaVersionQueryHandler :
    IRequestHandler<CheckJavaVersionQuery, bool>,
    IRequestHandler<GetApplyJavaVersionQuery, Stream?>,
    IRequestHandler<GetSourcesJavaVersionQuery, Stream?>,
    IStreamRequestHandler<UpdateJavaVersionQuery, IFrame>,
    IStreamRequestHandler<ApplyJavaVersionQuery, IFrame>
{
    private readonly IJavaService _javaService;
    public JavaVersionQueryHandler(IJavaService javaService)
        => _javaService = javaService;

    public Task<bool> Handle(CheckJavaVersionQuery request, CancellationToken cancellationToken)
        => _javaService.CheckVersion(request.GameVersion, request.BuildVersion, cancellationToken);
    public IAsyncEnumerable<IFrame> Handle(UpdateJavaVersionQuery request, CancellationToken cancellationToken)
        => _javaService.UpdateVersion(request.GameVersion, request.BuildVersion, cancellationToken);
    public IAsyncEnumerable<IFrame> Handle(ApplyJavaVersionQuery request, CancellationToken cancellationToken)
        => _javaService.ApplyVersion(request.GameVersion, request.BuildVersion, request.InputFile, cancellationToken);
    public Task<Stream?> Handle(GetApplyJavaVersionQuery request, CancellationToken cancellationToken)
        => _javaService.GetApplyVersion(request.Name, cancellationToken);
    public Task<Stream?> Handle(GetSourcesJavaVersionQuery request, CancellationToken cancellationToken)
        => _javaService.GetSourceVersion(request.GameVersion, request.BuildVersion, cancellationToken);
}