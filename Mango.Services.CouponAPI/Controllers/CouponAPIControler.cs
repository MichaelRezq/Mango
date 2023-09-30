using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIControler : ControllerBase
    {
        private readonly AppDbContext _db;
        public CouponAPIControler(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public object Get() {
            try
            {
                IEnumerable<Coupon> objList = _db.coupons.ToList();
                return objList;

            }catch (Exception ex)
            {

            }
            return null;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                Coupon obj = _db.coupons.FirstOrDefault(e => e.CouponId == id);
                return obj;

            }
            catch (Exception ex)
            {

            }
            return null;
        }
    }
}
