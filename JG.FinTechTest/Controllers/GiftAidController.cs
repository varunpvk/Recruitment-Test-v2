namespace JG.FinTechTest.Controllers
{
    using JG.FinTech.Features.GiftAidCalculator;
    using JG.FinTech.Models;
    using JG.FinTechTest.Routes;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Net.Mime;
    using System.Threading.Tasks;

    [Route(FinTechRoutes.basePath)]
    [ApiController]
    public class GiftAidController : ControllerBase
    {
        private readonly IGiftAidCalculator giftAidCalculator;

        public GiftAidController(IGiftAidCalculator giftAidCalculator)
        {
            this.giftAidCalculator = giftAidCalculator;
        }

        /// <summary>
        /// Get the amount of gift aid reclaimable for donation amount
        /// </summary>
        /// <param name="amount"></param>
        /// <returns>GiftAid the user is going to receive</returns>
        [HttpGet]
        [Route(FinTechRoutes.giftAidPath)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GiftAidResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GiftAidResponse>> GetGiftAidAmount(double amount)
        {
            try
            {
                var giftAid = await this.giftAidCalculator.CalculateGiftAidAsync(new GiftAid(amount));
                var giftAidResponse = new GiftAidResponse
                {
                    GiftAidAmount = giftAid,
                    DonationAmount = amount
                };

                return giftAidResponse;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
