using FluentAssertions;
using GreenFlux.Application.Groups.Commands.AddChargeStationToGroup;
using GreenFlux.Application.Groups.Commands.AddConnectorToChargeStation;
using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Application.Groups.Commands.SetConnectorMaxCurrent;
using GreenFlux.Application.Groups.Queries.GetGroup;
using GreenFlux.Domain.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.Application.IntegrationTests.Groups.Commands
{
    using static TestFixture;
    public class SetConnectorMaxCurrentTests
    {

        [Test]
        public async Task ShouldSetConnectorCapacity()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var addChargeStationCommand = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addChargeStationCommand);

            var addConnectorCommand = new AddConnectorToChargeStationCommand(groupId, 1, 1, 40);
            await SendAsync(addConnectorCommand);

            var command = new SetConnectorMaxCurrentCommand(groupId, 1, 1, 700);
            await SendAsync(command);

            var getGroupQuery = new GetGroupQuery(groupId);
            var group = await SendAsync(getGroupQuery);

            var chargeStation = group.ChargeStations.FirstOrDefault(x => x.Id == 1);
            var connector = chargeStation.Connectors.FirstOrDefault(x => x.ConnectorNumber == 1);

            connector.MaxCurrentInAmps.Should().Be(command.MaxCurrnetInAmps);
        }

        [Test]
        public async Task ShouldThrowGroupCapacityException()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 50);
            var groupId = await SendAsync(createGroupCommand);

            var addChargeStation = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addChargeStation);

            var addConnectorCommand = new AddConnectorToChargeStationCommand(groupId, 1, 1, 40);
            await SendAsync(addConnectorCommand);

            var command = new SetConnectorMaxCurrentCommand(groupId, 1, 1, 700);

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<GroupCapacityOverflowException>();
        }
    }
}
