using FluentAssertions;
using GreenFlux.Application.Exceptions;
using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Application.Groups.Commands.DeleteGroup;
using GreenFlux.Application.Groups.Queries.GetGroup;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenFlux.Application.IntegrationTests.Groups.Commands
{
    using static TestFixture;
    public class DeleteGroupTests
    {
        [Test]
        public async Task ShouldDeleteGroup()
        {
            var createGroupCommand = new CreateGroupCommand("Test1", 1000);
            var groupId = await SendAsync(createGroupCommand);

            var deleteGroupCommand = new DeleteGroupCommand(1);
            var isDeleted = await SendAsync(deleteGroupCommand);

            var getGroupQuery = new GetGroupQuery(groupId);
            var group = await SendAsync(getGroupQuery);

            group.Should().BeNull();
            isDeleted.Should().BeTrue();
        }

        [Test]
        public async Task ShouldThrowNotFoundException()
        {
            var deleteGroupCommand = new DeleteGroupCommand(1);

            await FluentActions.Invoking(() =>
                SendAsync(deleteGroupCommand)).Should().ThrowAsync<NotFoundException>();
        }
    }
}
