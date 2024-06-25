using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService
        (DiscountContext dbContext, ILogger<DiscountService> logger): DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
           var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

            if(coupon is null)
                coupon = new Models.Coupon { ProductName ="No Discount", Amount = 0, Description = "No Discount Desc" };

            var couponModle = coupon.Adapt<CouponModel>();
            return couponModle;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if(coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Coupon Data"));

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully created. ProductName: {ProductName}", coupon.ProductName);

            var coouponModel = coupon.Adapt<CouponModel>();
            return coouponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Coupon Data"));

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully updated. ProductName: {ProductName}", coupon.ProductName);

            var coouponModel = coupon.Adapt<CouponModel>();
            return coouponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = dbContext.Coupons.FirstOrDefault(c => c.ProductName.Equals(request.ProductName));
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Coupon Data"));

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully deleted. ProductName: {ProductName}", coupon.ProductName);



            return new DeleteDiscountResponse { Success = true };
        }
    }
}
