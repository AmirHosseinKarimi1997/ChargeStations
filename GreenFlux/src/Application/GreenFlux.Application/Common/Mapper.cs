
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Domain.Entities.GroupAggregate;

namespace GreenFlux.Application.Common;

public static class Mapper
{
    public static GroupDto MapToDto(Group group)
    {
        if (group == null)
            return null;

        var groupDto = new GroupDto
        {
            Id = group.Id,
            Name = group.Name,
            CapacityInAmps = group.CapacityInAmps
        };

        var chargeStations = group.ChargeStations.Select(x => MapToDto(x)).ToList();

        groupDto.ChargeStations = chargeStations;

        return groupDto;
    }

    public static ChargeStationDto MapToDto(ChargeStation chargeStation)
    {
        if (chargeStation == null)
            return null;

        var chargeStationDto = new ChargeStationDto
        {
            Id = chargeStation.Id,
            Name = chargeStation.Name,
            Connectors = chargeStation.Connectors.Select(c => MapToDto(c)).ToList()
        };

        return chargeStationDto;
    }

    public static ConnectorDto MapToDto(Connector connector)
    {
        if (connector == null)
            return null;

        var connectorDto = new ConnectorDto
        {
            Id = connector.Id,
            ConnectorNumber = connector.ConnectorNumber,
            MaxCurrentInAmps = connector.MaxCurrentInAmps
        };

        return connectorDto;
    }
}

