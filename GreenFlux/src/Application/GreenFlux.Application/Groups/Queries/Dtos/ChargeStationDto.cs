
namespace GreenFlux.Application.Groups.Queries.Dtos;

public class ChargeStationDto
{
    public ChargeStationDto()
    {
        Connectors = new List<ConnectorDto>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public List<ConnectorDto> Connectors { get; set; }
}

