using System.Diagnostics.Metrics;

namespace WoWsShipBuilder.Infrastructure.Metrics;

internal sealed class Timer(Histogram<double> observer) : IDisposable
{
    private readonly ValueStopwatch stopwatch = ValueStopwatch.StartNew();

    private readonly Histogram<double> observer = observer;

    public void Dispose()
    {
        var elapsedTime = this.stopwatch.GetElapsedTime();
        this.observer.Record(elapsedTime.TotalSeconds);
    }
}
