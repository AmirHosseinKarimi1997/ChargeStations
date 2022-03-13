using GreenFlux.Api.Models.GroupViewModels;
using GreenFlux.Api.Models.ServiceResponses;
using GreenFlux.Application.Groups.Commands.AddConnectorToChargeStation;
using GreenFlux.Application.Groups.Commands.RemoveConnectorFromChargeStation;
using GreenFlux.Application.Groups.Commands.SetConnectorMaxCurrent;
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Groups.Queries.GetAllConnectors;
using GreenFlux.Application.Groups.Queries.GetConnector;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GreenFlux.Api.Controllers
{
    [Route("api/groups/{groupId}/ChargeStations/{chargeStationId}/[controller]")]
    [ApiController]
    public class ConnectorsController : ApiControllerBase
    {

        [HttpGet("{connectorNumber}")]
        [ProducesResponseType(typeof(ServiceResponse<ConnectorDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(int groupId, int chargeStationId, int connectorNumber)
        {
            var query = new GetConnectorQuery(groupId, chargeStationId, connectorNumber);
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ServiceResponse<IEnumerable<ConnectorDto>>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(int groupId, int chargeStationId)
        {
            var query = new GetAllConnectorsQuery(groupId, chargeStationId);
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpPost]
        [ProducesResponseType(typeof(BaseServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Create(int groupId, int chargeStationId, AddConnectorViewModel model)
        {
            var command = new AddConnectorToChargeStationCommand(groupId, chargeStationId
                , model.ConnectorNumber, model.MaxCurrentInAmps);

            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{connectorNumber}")]
        [ProducesResponseType(typeof(BaseServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Delete(int groupId, int chargeStationId, int connectorNumber)
        {
            var command = new RemoveConnectorFromChargeStationCommand(groupId, chargeStationId, connectorNumber);

            await Mediator.Send(command);

            return Ok();
        }

        [Route("{connectorNumber}/SetMaxCurrent")]
        [HttpPatch]
        [ProducesResponseType(typeof(BaseServiceResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> SetMaxCurrentInAmps(int groupId, int chargeStationId, int connectorNumber, SetConnectorMaxCurrentViewModel model)
        {
            var command = new SetConnectorMaxCurrentCommand(groupId, chargeStationId, connectorNumber, model.MaxCurrnetInAmps);

            await Mediator.Send(command);

            return Ok();
        }
    }
}
