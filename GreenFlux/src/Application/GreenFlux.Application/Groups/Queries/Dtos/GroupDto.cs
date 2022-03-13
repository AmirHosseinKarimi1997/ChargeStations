
namespace GreenFlux.Application.Groups.Queries.Dtos;

public class GroupDto
{
    public GroupDto()
    {
        ChargeStations = new List<ChargeStationDto>();
    }
    public int Id { get; set; }

    public string Name { get; set; }

    public ulong CapacityInAmps { get; set; }

    public List<ChargeStationDto> ChargeStations {get; set; }
}

