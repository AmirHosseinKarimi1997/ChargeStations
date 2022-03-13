
using Dapper;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Interfaces;
using GreenFlux.Domain.Common;
using GreenFlux.Domain.Entities.GroupAggregate;
using GreenFlux.Infra.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Infra.DataAccess.Queries;

public class GroupQueries : IGroupQueries
{
    private readonly UnitOfWork _context;
    public IUnitOfWork UnitOfWork
    {
        get
        {
            return _context;
        }
    }

    public GroupQueries(UnitOfWork context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    //For Dapper, but because I'm using in memory version of EF, I will use EF
    //for these types of query classes, we can use readonly connection strings
    //private string _connectionString = string.Empty;

    //public GroupQueries(string constr)
    //{
    //    _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
    //}

    public async Task<ChargeStation> GetChargeStationAsync(int groupId, int id)
    {
        var data = await _context.ChargeStations
                        .Include(x => x.Connectors)
                        .Where(x => x.GroupId == groupId && x.Id == id)
                        .FirstOrDefaultAsync();

        return data;
    }

    public async Task<IEnumerable<ChargeStation>> GetAllChargeStationsAsync(int groupId)
    {
        var data = await _context.ChargeStations
                .Include(x => x.Connectors)
                .Where(x => x.GroupId == groupId)
                .ToListAsync();

        return data;
    }

    public async Task<IEnumerable<Connector>> GetAllConnectorsAsync(int groupId, int chargeStationId)
    {
        var data = await _context.ChargeStations
                .Include(x => x.Connectors)
                .Where(x => x.GroupId == groupId && x.Id == chargeStationId)
                .FirstOrDefaultAsync();

        if (data != null && data.Connectors != null)
            return data.Connectors;
        
        return null;
    }

    public async Task<Connector> GetConnectorAsync(int groupId, int chargeStationId, int connectorNumber)
    {
        //because of using in memory sql, I can't use dapper and in looks ugly :\
        Connector data;
        var chargeStation = await _context.ChargeStations
                        .Include(x => x.Connectors)
                        .Where(x => x.GroupId == groupId && x.Id == chargeStationId)
                        .FirstOrDefaultAsync();

        if(chargeStation != null && chargeStation.Connectors != null) { }
            data = chargeStation.Connectors.FirstOrDefault(x => x.ConnectorNumber == connectorNumber);

        return data;
    }

    public async Task<IEnumerable<Group>> GetAllGroupsAsync()
    {
        var data = await _context.Groups
                .Include(x => x.ChargeStations)
                .ThenInclude(x => x.Connectors)
                .ToListAsync();

        return data;
    }

    public async Task<Group> GetGroupAsync(int id)
    {
        var data = await _context.Groups
                        .Include(x => x.ChargeStations)
                        .ThenInclude(x => x.Connectors)
                        .Where(x => x.Id == id)
                        .FirstOrDefaultAsync();

        return data;

        //Dapper
        //var query = @"select 
        //            g.Id as GroupId, g.Name as GroupName, g.CapacityInAmps as GroupCapacityInAmps, 
        //            cs.Id as ChargeStationId, cs.Name as ChargeStationName,
        //            con.Id as ConnectorId, con.ConnectorNumber as ConnectorNumber, con.MaxCurrentInAmps as ConnectorMaxCurrentInAmps
        //            FROM Groups g
        //            LEFT JOIN ChargeStations cs ON g.Id = cs.GroupId 
        //            LEFT JOIN Connectors con ON con.ChargeStationId = cs.Id
        //            WHERE g.Id=@id";
        //using (var connection = new SqlConnection(_connectionString))
        //{
        //    connection.Open();

        //    var result = await connection.QueryAsync<dynamic>(query, new { id });

        //    if (result.AsList().Count == 0)
        //        throw new KeyNotFoundException();

        //    return MapGroup(result);
        //}
    }

    //for dapper result mapping
    //private GroupDto MapGroup(dynamic result)
    //{
    //    var group = new GroupDto
    //    {
    //        Id = result[0].GroupId,
    //        CapacityInAmps = result[0].GroupCapacityInAmps,
    //        Name = result[0].GroupName
    //    };

    //    foreach (dynamic item in result)
    //    {
    //        var chargeStation = new ChargeStationDto
    //        {
    //            Id = item.ChargeStationId,
    //            Name = item.ChargeStationName
    //        };

    //        foreach (dynamic item2 in result)
    //        {
    //            var connector = new ConnectorDto
    //            {
    //                Id = item2.ConnectorId,
    //                ConnectorNumber = item2.ConnectorNumber,
    //                MaxCurrentInAmps = item2.ConnectorMaxCurrentInAmps
    //            };

    //            chargeStation.Connectors.Add(connector);
    //        }

    //        group.ChargeStations.Add(chargeStation);
    //    }

    //    return group;
    //}
}

