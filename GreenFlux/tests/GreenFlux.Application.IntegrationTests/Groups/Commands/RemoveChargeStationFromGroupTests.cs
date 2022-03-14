using FluentAssertions;
using GreenFlux.Domain.Exceptions;
using GreenFlux.Application.Groups.Commands.AddChargeStationToGroup;
using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Application.Groups.Commands.RemoveChargeStationFromGroup;
using GreenFlux.Application.Groups.Queries.GetChargeStation;
using GreenFlux.Application.Groups.Queries.GetGroup;
using NUnit.Framework;

namespace GreenFlux.Application.IntegrationTests.Groups.Commands
{
    using static TestFixture;
    public class RemoveChargeStationFromGroupTests
    {
        [Test]
        public async Task ShouldRemoveChargeStationFromGroup()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var addChargeStationCommand = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addChargeStationCommand);

            var removeCommand = new RemoveChargeStationFromGroupCommand(1, 1);
            await SendAsync(removeCommand);

            var getGroupQuery = new GetGroupQuery(groupId);
            var group = await SendAsync(getGroupQuery);

            var getChargeStation = new GetChargeStationQuery(1, 1);
            var cs = await SendAsync(getChargeStation);

            cs.Should().BeNull();
            group.ChargeStations.Should().BeNullOrEmpty();
        }

        [Test]
        public async Task ShouldThrowException()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var removeCommand = new RemoveChargeStationFromGroupCommand(1, 1);

            await FluentActions.Invoking(() =>
                SendAsync(removeCommand)).Should().ThrowAsync<ChargeStationNotFoundException>();
        }
    }
}
