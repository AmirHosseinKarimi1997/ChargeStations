using FluentAssertions;
using GreenFlux.Application.Exceptions;
using GreenFlux.Application.Groups.Commands.AddChargeStationToGroup;
using GreenFlux.Application.Groups.Commands.AddConnectorToChargeStation;
using GreenFlux.Application.Groups.Commands.CreateGroup;
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
    public class AddConnectorToChargeStationTests
    {
        [Test]
        public async Task ShouldAddConnector()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var addChargeStationCommand = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addChargeStationCommand);

            var command = new AddConnectorToChargeStationCommand(groupId, 1, 1, 500);
            await SendAsync(command);

            var getGroupQuery = new GetGroupQuery(groupId);
            var group = await SendAsync(getGroupQuery);

            var chargeStation = group.ChargeStations.FirstOrDefault(x => x.Id == 1);
            var connector = chargeStation.Connectors.FirstOrDefault(x => x.ConnectorNumber == 1);

            connector.Should().NotBeNull();
            connector.MaxCurrentInAmps.Should().Be(command.MaxCurrentInAmps);
            connector.ConnectorNumber.Should().Be(command.ConnectorNumber);
        }

        [Test]
        public async Task ShouldThrowAlreadyExistException()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var addChargeStationCommand = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addChargeStationCommand);

            var addConnectorCommand = new AddConnectorToChargeStationCommand(groupId, 1, 1, 500);
            await SendAsync(addConnectorCommand);

            var command = new AddConnectorToChargeStationCommand(groupId, 1, 1, 500);

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ConnectorAlreadyExistsException>();
        }

        [Test]
        public async Task ShouldThrowGroupCapacityException()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 50);
            var groupId = await SendAsync(createGroupCommand);

            var addChargeStationCommand = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addChargeStationCommand);

            var addConnectorCommand = new AddConnectorToChargeStationCommand(groupId, 1, 1, 500);

            await FluentActions.Invoking(() =>
                SendAsync(addConnectorCommand)).Should().ThrowAsync<GroupCapacityOverflowException>();
        }

        [Test]
        public async Task ShouldThrowChargeStationCapacityException()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 500);
            var groupId = await SendAsync(createGroupCommand);

            var addChargeStationCommand = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addChargeStationCommand);

            for (int i = 1; i <= 5; i++)
            {
                await SendAsync(new AddConnectorToChargeStationCommand(groupId, 1, i, 10));
            }

            var addConnectorCommand = new AddConnectorToChargeStationCommand(groupId, 1, 4, 30);

            await FluentActions.Invoking(() =>
                SendAsync(addConnectorCommand)).Should().ThrowAsync<ChargeStationCapacityOverflowException>();
        }

        [Test]
        public async Task ShouldThrowValidationException()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 500);
            var groupId = await SendAsync(createGroupCommand);

            var addChargeStationCommand = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addChargeStationCommand);

            var addConnectorCommand = new AddConnectorToChargeStationCommand(groupId, 1, 7, 30);

            await FluentActions.Invoking(() =>
                SendAsync(addConnectorCommand)).Should().ThrowAsync<ValidationException>();
        }
    }
}
