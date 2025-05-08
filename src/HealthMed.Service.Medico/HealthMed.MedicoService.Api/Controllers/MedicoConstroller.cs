using MassTransit.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.MedicoService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoConstroller : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicoConstroller(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
