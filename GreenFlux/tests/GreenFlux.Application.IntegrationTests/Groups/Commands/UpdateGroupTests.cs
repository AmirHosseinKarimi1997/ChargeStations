using FluentAssertions;
using GreenFlux.Application.Groups.Commands.AddChargeStationToGroup;
using GreenFlux.Application.Groups.Commands.AddConnectorToChargeStation;
using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Application.Groups.Commands.SetGroupCapacity;
using GreenFlux.Application.Groups.Commands.UpdateGroup;
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
    public class UpdateGroupTests
    {
        [Test]
        public async Task ShouldUpdateGroup()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var command = new UpdateGroupCommand(1, "Test11", 500);
            await SendAsync(command);

            var getGroupQuery = new GetGroupQuery(groupId);
            var group = await SendAsync(getGroupQuery);

            group.Should().NotBeNull();
            group.Name.Should().Be(command.Name);
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

            var command = new UpdateGroupCommand(groupId, "Test22", 50);

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<GroupCapacityLessThanSumMaxCurrentException>();
        }

        [Test]
        public async Task ShouldThrowNotFoundException()
        {
            var updateGroupCommand = new UpdateGroupCommand(1, "tttt", 1000);

            await FluentActions.Invoking(() =>
                SendAsync(updateGroupCommand)).Should().ThrowAsync<Application.Exceptions.NotFoundException>();
        }
    }
}
