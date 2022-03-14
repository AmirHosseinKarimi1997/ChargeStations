
using Dapper;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Interfaces;
using GreenFlux.Domain.Entities.GroupAggregate;
using Microsoft.Data.SqlClient;

namespace GreenFlux.Infra.DataAccess.Queries;

public class DapperGroupQuery : IGroupQueries
{
    //For Dapper, but because I'm using in memory version of EF, I will use EF
    //for these types of queries, we can use readonly connection strings
    //Also return types should change 
    private string _connectionString = string.Empty;

    public async Task<GroupDto> GetGroupAsync(int id)
    {
        var query = @"select 
                g.Id as GroupId, g.Name as GroupName, g.CapacityInAmps as GroupCapacityInAmps, 
                cs.Id as ChargeStationId, cs.Name as ChargeStationName,
                con.Id as ConnectorId, con.ConnectorNumber as ConnectorNumber, con.MaxCurrentInAmps as ConnectorMaxCurrentInAmps
                FROM Groups g
                LEFT JOIN ChargeStations cs ON g.Id = cs.GroupId 
                LEFT JOIN Connectors con ON con.ChargeStationId = cs.Id
                WHERE g.Id=@id";

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            var result = await connection.QueryAsync<dynamic>(query, new { id });

            if (result.AsList().Count == 0)
                throw new KeyNotFoundException();

            return MapGroup(result);
        }

    }

    public DapperGroupQuery(string constr)
    {
        _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
    }

    public Task<IEnumerable<ChargeStation>> GetAllChargeStationsAsync(int groupId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Connector>> GetAllConnectorsAsync(int groupId, int chargeStationId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Group>> GetAllGroupsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ChargeStation> GetChargeStationAsync(int groupId, int id)
    {
        throw new NotImplementedException();
    }

    public Task<Connector> GetConnectorAsync(int groupId, int chargeStationId, int connectorNumber)
    {
        throw new NotImplementedException();
    }

    Task<Group> IGroupQueries.GetGroupAsync(int id)
    {
        throw new NotImplementedException();
    }

    //for dapper result mapping
    private GroupDto MapGroup(dynamic result)
    {
        var group = new GroupDto
        {
            Id = result[0].GroupId,
            CapacityInAmps = result[0].GroupCapacityInAmps,
            Name = result[0].GroupName
        };

        foreach (dynamic item in result)
        {
            var chargeStation = new ChargeStationDto
            {
                Id = item.ChargeStationId,
                Name = item.ChargeStationName
            };

            foreach (dynamic item2 in result)
            {
                var connector = new ConnectorDto
                {
                    Id = item2.ConnectorId,
                    ConnectorNumber = item2.ConnectorNumber,
                    MaxCurrentInAmps = item2.ConnectorMaxCurrentInAmps
                };

                chargeStation.Connectors.Add(connector);
            }

            group.ChargeStations.Add(chargeStation);
        }

        return group;
    }
}

