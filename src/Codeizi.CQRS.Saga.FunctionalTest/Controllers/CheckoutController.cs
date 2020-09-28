using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codeizi.CQRS.Saga.FunctionalTest.Sagas;
using Codeizi.CQRS.Saga.FunctionalTest.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Codeizi.CQRS.Saga.FunctionalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutSaga checkoutSaga;
        public CheckoutController(CheckoutSaga checkoutSaga)
        {
            this.checkoutSaga = checkoutSaga;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var initialState = new InitialStateCheckoutViewModel
            {
                CustomerViewModel = new CustomerViewModel
                {
                    FirstName = "Codeizi",
                    Id = Guid.NewGuid()
                },
                Products = new List<ProductViewModel>
                {
                    new ProductViewModel
                    {
                        Id = Guid.NewGuid(),
                        Amount = 1,
                        Description = "Iphone"
                    },
                    new ProductViewModel
                    {
                        Id = Guid.NewGuid(),
                        Amount = 1,
                        Description = "eBook Clean Code"
                    },
                }
            };

            var sagaInfo = await checkoutSaga.Start(initialState);
            return Ok(sagaInfo);
        }        
    }
}
