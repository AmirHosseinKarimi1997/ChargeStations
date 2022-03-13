using GreenFlux.Api.Models.GroupViewModels;
using GreenFlux.Api.Models.ServiceResponses;
using GreenFlux.Application.Groups.Commands.AddChargeStationToGroup;
using GreenFlux.Application.Groups.Commands.RemoveChargeStationFromGroup;
using GreenFlux.Application.Groups.Commands.UpdateChargeStationInGroup;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Groups.Queries.GetAllChargeStations;
using GreenFlux.Application.Groups.Queries.GetChargeStation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GreenFlux.Api.Controllers
{
    [Route("api/groups/{groupId}/[controller]")]
    [ApiController]
    public class ChargeStationsController : ApiControllerBase
    {

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResponse<ChargeStationDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(int groupId, int id)
        {
            var query = new GetChargeStationQuery(groupId, id);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<ChargeStationDto>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(int groupId)
        {
            var query = new GetAllChargeStationsQuery(groupId);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Create(int groupId, AddChargeStationViewModel model)
        {
            var command = new AddChargeStationToGroupCommand(groupId, model.Name);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BaseServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Update(int groupId, int id, UpdateChargeStationViewModel model)
        {
            var command = new UpdateChargeStationInGroupCommand(groupId, id, model.Name);
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Delete(int groupId, int id)
        {
            var command = new RemoveChargeStationFromGroupCommand(groupId, id);
            await Mediator.Send(command);
            return Ok();
        }
    }
}
