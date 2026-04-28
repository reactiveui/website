#nullable enable

using System.Threading;
using System.Threading.Tasks;
using SourceDocParser;
using SourceDocParser.Model;
using SourceDocParser.Zensical;
using SourceDocParser.Zensical.Navigation;
using SourceDocParser.Zensical.Options;

namespace ReactiveUI.Web;

/// <summary>
/// Wraps the real <see cref="ZensicalDocumentationEmitter"/> so we can
/// capture the <see cref="NavigationGraph"/> for the same <c>ApiType[]</c>
/// the emitter just rendered. The graph is built before forwarding to
/// the inner emitter, so we don't run the (more expensive) walk twice.
/// </summary>
internal sealed class NavigationCapturingEmitter : IDocumentationEmitter
{
    private readonly ZensicalDocumentationEmitter _inner;
    private readonly NavigationGraphBuilder _navBuilder;

    public NavigationCapturingEmitter(ZensicalDocumentationEmitter inner, ZensicalEmitterOptions options)
    {
        _inner = inner;
        _navBuilder = new NavigationGraphBuilder(options);
    }

    public NavigationGraph? NavigationGraph { get; private set; }

    public Task<int> EmitAsync(ApiType[] types, string outputRoot) =>
        EmitAsync(types, outputRoot, CancellationToken.None);

    public Task<int> EmitAsync(ApiType[] types, string outputRoot, CancellationToken cancellationToken)
    {
        NavigationGraph = _navBuilder.Build(types);
        return _inner.EmitAsync(types, outputRoot, cancellationToken);
    }
}
