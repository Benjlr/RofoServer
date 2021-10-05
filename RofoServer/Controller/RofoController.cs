using MediatR;
using Microsoft.AspNetCore.Mvc;
using RofoServer.Core.RequestModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Controller
{
    public class RofoController : ApiController
    {
        private readonly IMediator _mediator;
        public RofoController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRofoRequest roReq) {
            if (ModelState.ErrorCount > 0)
                return BadRequest();

            var response = await _mediator.Send(roReq, new CancellationToken());
            return Ok(response);
        }

        //public ActionResult Index() {
        //    throw new NotImplementedException();
        //}


        //public ActionResult Details(int id)
        //{
        //    throw new NotImplementedException();

        //}




        //public ActionResult Edit(int id)
        //{
        //    throw new NotImplementedException();

        //}

        //public ActionResult Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
