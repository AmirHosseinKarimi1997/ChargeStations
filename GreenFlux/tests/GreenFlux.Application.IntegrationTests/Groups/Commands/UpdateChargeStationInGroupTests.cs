using FluentAssertions;
using GreenFlux.Application.Groups.Commands.AddChargeStationToGroup;
using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Application.Groups.Commands.UpdateChargeStationInGroup;
using GreenFlux.Application.Groups.Queries.GetChargeStation;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.Application.IntegrationTests.Groups.Commands
{
    using static TestFixture;
    public class UpdateChargeStationInGroupTests
    {
        [Test]
        public async Task ShouldUpdateChargeStation()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var addCahrgeStationCommand = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(addCahrgeStationCommand);

            var command = new UpdateChargeStationInGroupCommand(1, 1, "Station22");
            await SendAsync(command);

            var getChargeStationQuery = new GetChargeStationQuery(groupId, 1);
            var chargeStation = await SendAsync(getChargeStationQuery);

            chargeStation.Should().NotBeNull();
            chargeStation.Name.Should().Be(command.Name);
        }

        [Test]
        public async Task ShouldThrowNotFoundException()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var updateChargeStationCommand = new UpdateChargeStationInGroupCommand(1, 1, "tttt");

            await FluentActions.Invoking(() =>
                SendAsync(updateChargeStationCommand)).Should().ThrowAsync<Application.Exceptions.ChargeStationNotFoundException>();
        }
    }
}
