using System.Diagnostics.CodeAnalysis;
using Microsoft.JSInterop;

namespace WoWsShipBuilder.Features.ShipStats;

public sealed class MouseEventInterop(IJSRuntime runtime) : IAsyncDisposable
{
    private readonly IJSRuntime runtime = runtime;

    private IJSObjectReference? module;

    public async Task PreventMiddleClickDefault(string id)
    {
        await this.InitializeModule();
        await this.module.InvokeVoidAsync("PreventMiddleClickDefault", id);
    }

    public async Task PreventMiddleClickDefaultBatched(IEnumerable<string> ids)
    {
        await this.InitializeModule();
        await this.module.InvokeVoidAsync("PreventMiddleClickDefaultBatched", ids);
    }

    [MemberNotNull(nameof(module))]
    private async Task InitializeModule()
    {
        // module is not null after this method but apparently, Roslyn does not want to recognize that.
#pragma warning disable CS8774
        this.module ??= await this.runtime.InvokeAsync<IJSObjectReference>("import", "/_content/WoWsShipBuilder.Common/scripts/MouseEventHelper.js");
#pragma warning restore CS8774
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (this.module is not null)
        {
            try
            {
                await this.module.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
                // The JS runtime is disconnected, so we don't need to dispose the module.
            }
        }
    }
}
