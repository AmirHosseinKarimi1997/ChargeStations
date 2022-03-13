using GreenFlux.Api.Models.GroupViewModels;
using GreenFlux.Application.Groups.Commands.CreateGroup;
using GreenFlux.Application.Groups.Commands.DeleteGroup;
using GreenFlux.Application.Groups.Commands.UpdateGroup;
using GreenFlux.Application.Groups.Commands.SetGroupCapacity;
using GreenFlux.Application.Groups.Queries.GetGroup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Groups.Queries.GetAllGroups;
using GreenFlux.Api.Models.ServiceResponses;
using System.Net;

namespace GreenFlux.Api.Controllers
{
    [ApiController]
    public class GroupsController : ApiControllerBase
    {

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<GroupDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(int id)
        {
            var query = new GetGroupQuery(id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<GroupDto>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get()
        {
            var query = new GetAllGroupsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IntegerServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Create(CreateGroupViewModel model)
        {
            var command = new CreateGroupCommand(model.Name, model.CapacityInAmps);
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BaseServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Update(int id, UpdateGroupViewModel model)
        {
            var command = new UpdateGroupCommand(id, model.Name, model.CapacityInAmps);
            await Mediator.Send(command);
            return Ok();
        }

        [Route("{id}/SetCapacity")]
        [HttpPatch]
        [ProducesResponseType(typeof(BaseServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> SetCapacityInAmps(int id, SetGroupCapacityViewModel model)
        {
            var command = new SetGroupCapacityInAmpsCommand(id, model.CapacityInAmps);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteGroupCommand(id);
            await Mediator.Send(command);
            return Ok();
        }
    }
}
