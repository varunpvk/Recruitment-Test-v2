namespace JG.FinTechTest.Controllers
{
    using JG.FinTech.Domain;
    using JG.FinTech.Features;
    using JG.FinTech.Features.GiftAidCalculator;
    using JG.FinTech.Models;
    using JG.FinTechTest.Routes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Linq;
    using System.Net.Mime;
    using System.Threading.Tasks;

    [Route(FinTechRoutes.basePath)]
    [ApiController]
    public class GiftAidController : BaseController
    {
        private readonly IGiftAidCalculator giftAidCalculator;
        private readonly IConfiguration config;
        private readonly IGiftAidRepository giftAidRepository;
        private readonly IDeclarationToDonorMapper declarationToDonorMapper;

        public GiftAidController(
            IGiftAidCalculator giftAidCalculator,
            IGiftAidRepository giftAidRepository,
            IDeclarationToDonorMapper declarationToDonorMapper,
            IConfiguration config)
        {
            this.giftAidCalculator = giftAidCalculator;
            this.giftAidRepository = giftAidRepository;
            this.declarationToDonorMapper = declarationToDonorMapper;
            this.config = config;
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
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

                return Ok(giftAidResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Registers the Donor Details
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route(FinTechRoutes.addDonorPath)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GiftAidResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<DonorResponse>> AddDonor(DeclarationDetails declarationDetails)
        {
            try
            {
                var donorDetails = await this.declarationToDonorMapper.GetDonorDetailsAsync(declarationDetails);
                UpdateDonorDetailsWithUserClaims(donorDetails);
                await this.giftAidRepository.AddDonorDetails(donorDetails);
                return Ok(new DonorResponse
                {
                    DonorID = donorDetails.DonorID,
                    GiftAidAmount = donorDetails.GiftAid
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void UpdateDonorDetailsWithUserClaims(DonorDetails donorDetails)
        {
            var claims = User.Claims;
            donorDetails.Name = claims.FirstOrDefault(o => o.Type.Equals("Name", StringComparison.InvariantCultureIgnoreCase))?.Value ?? string.Empty;
            donorDetails.PostCode = claims.FirstOrDefault(o => o.Type.Equals("PostCode", StringComparison.InvariantCultureIgnoreCase))?.Value ?? string.Empty;
        }
    }
}
