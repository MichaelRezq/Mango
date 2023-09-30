namespace Mango.Services.CouponAPI.Models
{
    public class Coupon
    {
        public string Id { get; set; }  
        public string CouponCode { get; set; }

        public double DiscountAmount { get; set; }

        public int  MinAmount { get; set; }
    }
}
