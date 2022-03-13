
using GreenFlux.Application.Interfaces;

namespace GreenFlux.Infra.DataAccess.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}

