using FluentAssertions;
using GreenFlux.Application.Groups.Commands.AddChargeStationToGroup;
using GreenFlux.Application.Groups.Commands.AddConnectorToChargeStation;
using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Application.Groups.Commands.SetGroupCapacity;
using GreenFlux.Application.Groups.Queries.GetGroup;
using GreenFlux.Domain.Exceptions;
using NUnit.Framework;

namespace GreenFlux.Application.IntegrationTests.Groups.Commands
{
    using static TestFixture;
    public class SetGroupCapacityTests
    {
        [Test]
        public async Task ShouldSetGroupCapacity()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var addChargeStation = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addChargeStation);

            var connectorCommand = new AddConnectorToChargeStationCommand(groupId, 1, 1, 500);
            await SendAsync(connectorCommand);

            var command = new SetGroupCapacityInAmpsCommand(groupId, 700);
            await SendAsync(command);

            var getGroupQuery = new GetGroupQuery(groupId);
            var group = await SendAsync(getGroupQuery);

            group.CapacityInAmps.Should().Be(command.CapacityInAmps);
        }

        [Test]
        public async Task ShouldThrowGroupCapacityException()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var addChargeStation = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addChargeStation);

            var connectorCommand = new AddConnectorToChargeStationCommand(groupId, 1, 1, 500);
            await SendAsync(connectorCommand);

            var command = new SetGroupCapacityInAmpsCommand(groupId, 100);

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<GroupCapacityLessThanSumMaxCurrentException>();
        }

        [Test]
        public async Task ShouldThrowGroupNotFoundException()
        {
            var command = new SetGroupCapacityInAmpsCommand(1, 100);

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<Application.Exceptions.NotFoundException>();
        }
    }
}
