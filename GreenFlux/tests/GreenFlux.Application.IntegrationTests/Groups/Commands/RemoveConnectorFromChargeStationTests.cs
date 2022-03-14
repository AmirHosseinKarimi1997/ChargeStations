using FluentAssertions;
using GreenFlux.Application.Groups.Commands.AddChargeStationToGroup;
using GreenFlux.Application.Groups.Commands.AddConnectorToChargeStation;
using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Application.Groups.Commands.RemoveConnectorFromChargeStation;
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
    public class RemoveConnectorFromChargeStationTests
    {
        [Test]
        public async Task ShouldThrowConnectorNotFoundException()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 50);
            var groupId = await SendAsync(createGroupCommand);

            var addChargeStationCommand = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addChargeStationCommand);

            var command = new RemoveConnectorFromChargeStationCommand(groupId, 1, 1);

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ConnectorNotFoundException>();
        }

        [Test]
        public async Task ShouldRemoveConnectorFromChargeStation()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 50);
            var groupId = await SendAsync(createGroupCommand);

            var addChargeStationCommand = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addChargeStationCommand);

            var connectorCommand = new AddConnectorToChargeStationCommand(groupId, 1, 1, 50);
            await SendAsync(connectorCommand);

            var command = new RemoveConnectorFromChargeStationCommand(groupId, 1, 1);
            await SendAsync(command);

            var getGroupQuery = new GetGroupQuery(groupId);
            var group = await SendAsync(getGroupQuery);

            var chargeStation = group.ChargeStations.FirstOrDefault(x => x.Id == 1);
            var connector = chargeStation.Connectors.FirstOrDefault(x => x.ConnectorNumber == 1);

            connector.Should().BeNull();
            chargeStation.Connectors.Should().BeNullOrEmpty();

        }

    }
}
