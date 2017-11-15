using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Security.Principal;
using Mediator.Demo.Extensions;

namespace Mediator.Demo.Controllers
{
    public class ContractorController : Controller
    {
        private readonly IPrincipal _principal;
        private readonly IMediator _mediator;

        public ContractorController(IPrincipal principal, IMediator mediator)
        {
            _principal = principal;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetContractorRequest
            {
                Token = _principal.Tokenize(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTimeOffset(new DateTime(1966, 10, 15))
            });

            await _mediator.Publish(result.Notification());

            return View(result.ViewModels.FirstOrDefault());
        }
    }
}