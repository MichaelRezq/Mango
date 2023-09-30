using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIControler : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        public CouponAPIControler(AppDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }


        [HttpGet]
        public ResponseDto Get() {
            try
            {
                IEnumerable<Coupon> objList = _db.coupons.ToList();
                _response.Result = objList;
            }catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                Coupon obj = _db.coupons.First(e => e.CouponId == id);
                // INSTEAD of returning the Coupon directly from db we rerurn the Dto
                CouponDto couponDto = new CouponDto(){
                    CouponId= obj.CouponId,
                    CouponCode = obj.CouponCode,
                    DiscountAmount = obj.DiscountAmount,
                     MinAmount = obj.MinAmount,
                };
                _response.Result = couponDto;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
