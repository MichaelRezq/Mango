using Mango.Services.CouponAPI.Models.DTO;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CouponControler : Controller
    {

        private readonly ICouponService _couponService;
        public CouponControler(ICouponService _couponService)
        {
            _couponService = _couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();
            ResponseDto? response = await _couponService.GetAllCouponsAsync();
            if (response != null && response.IsSuccess==true)
            {
               list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            return View(response);
        }
    }
}
