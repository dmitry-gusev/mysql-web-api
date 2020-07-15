using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repos;
using Microsoft.AspNetCore.Mvc;
using MiniProductCRUD.Models;


namespace MiniProductCRUD.Controllers
{
    [Route("[controller]")]
    [CustomAttribute]
    [ApiController]
    public class Items : ControllerBase
    {

        private IBaseRepository<Product> _prods;


        public Items(IBaseRepository<Product> products)
        {
            _prods = products;
        }

        
        [HttpGet()]
        public async Task<IActionResult> Get(string token)
        {

        
            try
            {
                var items = await _prods.GetAllAsync<object>();
                var result = new OperationResult<ProductViewModel[]>()
                {
                    Code = ResultCodes.Success,
                    ErrorMessage = "",
                    Data = items.ToViewModels().ToArray()
                };
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                //Application Error
                return new JsonResult(new OperationResult<string>()
                {
                    Code = ResultCodes.ServerError,
                    ErrorMessage = "Application Error",
                    Data = e.Message
                });
            }

        }


        [HttpPost]
        public async Task<IActionResult> Post(string token,[FromBody] ProductViewModel prod) 
        {
            if (prod==null || string.IsNullOrEmpty(prod.Name))
            {
                return new JsonResult(new OperationResult<string>()
                {
                    Code = ResultCodes.DataError,
                    ErrorMessage = "Product name must be pointed !"

                });
            }
            //Add new item, prod will implicit convert from ProductViewModel to Product
            var newItem =await _prods.AddAsync(prod);

            
            return new JsonResult(new OperationResult<ProductViewModel[]>()
            {
                Code = ResultCodes.Success,
                ErrorMessage = "",
                Data = new[] { (ProductViewModel)newItem }

            });
            
        }

    }
}
