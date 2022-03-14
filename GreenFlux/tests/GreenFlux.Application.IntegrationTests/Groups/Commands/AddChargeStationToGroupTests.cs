using FluentAssertions;
using GreenFlux.Application.Exceptions;
using GreenFlux.Application.Groups.Commands.AddChargeStationToGroup;
using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Application.Groups.Queries.GetGroup;
using NUnit.Framework;


namespace GreenFlux.Application.IntegrationTests.Groups.Commands
{
    using static TestFixture;
    public class AddChargeStationToGroupTests
    {
        [Test]
        public async Task ShouldAddChargeStationToGroup()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var command = new AddChargeStationToGroupCommand(groupId, "Station1");
            await SendAsync(command);

            var getGroupQuery = new GetGroupQuery(groupId);
            var group = await SendAsync(getGroupQuery);

            var cs = group.ChargeStations.First(x => x.Id == 1);

            cs.Should().NotBeNull();
            cs.Name.Should().Be(command.Name);
            cs.Connectors.Should().BeNullOrEmpty();
        }

        [Test]
        public async Task ShouldThrowValidationException()
        {
            var command = new AddChargeStationToGroupCommand(1, "");

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }
    }
}
