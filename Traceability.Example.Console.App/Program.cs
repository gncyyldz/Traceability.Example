
using NLog;

Logger _logger = LogManager.GetCurrentClassLogger();
System.Diagnostics.Trace.CorrelationManager.ActivityId = Guid.NewGuid();

Work1();

void Work1()
{
    _logger.Debug("Work1 metodu çalıştırıldı...");
    Work2();
}
void Work2()
{
    _logger.Debug("Work2 metodu çalıştırıldı...");
    Work3();
}
void Work3()
{
    _logger.Debug("Work3 metodu çalıştırıldı...");
}