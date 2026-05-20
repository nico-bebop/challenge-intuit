namespace TurnosMedicos.Application.Helpers;

public static class DateTimeExtensions
{
    public static bool IsWithinCancellationWindow(this DateTime fechaTurno)
    {
        return fechaTurno - DateTime.UtcNow <= TimeSpan.FromHours(24);
    }
}