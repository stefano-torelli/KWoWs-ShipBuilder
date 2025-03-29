using Microsoft.JSInterop;

using WoWsShipBuilder.Infrastructure.DataTransfer;

namespace WoWsShipBuilder.Web.Infrastructure;
/// <summary>
/// Implementation of the clipboard service for the web app.
/// Allows to set and read text from the clipboard.
/// </summary>
public class WebClipboardService(IJSRuntime jsRuntime) : IClipboardService
{
    public async Task<string> GetTextAsync()
    {
        return await jsRuntime.InvokeAsync<string>("navigator.clipboard.readText");
    }

    public async Task SetTextAsync(string text)
    {
        await jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }
    public async Task ClearAsync()
    {
        // Implement the method to clear the clipboard if needed
        await Task.CompletedTask;
    }
}
