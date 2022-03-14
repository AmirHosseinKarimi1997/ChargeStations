using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Application.Groups.Queries.GetGroup;
using NUnit.Framework;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Exceptions;

namespace GreenFlux.Application.IntegrationTests.Groups.Commands
{
    using static TestFixture;

    public class CreateGroupTests
    {
        [Test]
        public async Task ShouldCreateGroup()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var getGroupQuery = new GetGroupQuery(groupId);
            var group = await SendAsync(getGroupQuery);

            group.Should().NotBeNull();
            group.Name.Should().Be(createGroupCommand.Name);
            group.CapacityInAmps.Should().Be(createGroupCommand.CapacityInAmps);
            group.ChargeStations.Should().BeNullOrEmpty();
        }

        [Test]
        public async Task ShouldThrowException()
        {
            var createGroupCommand = new CreateGroupCommand("", 1000);

            await FluentActions.Invoking(() =>
                SendAsync(createGroupCommand)).Should().ThrowAsync<ValidationException>();
        }
    }
}
