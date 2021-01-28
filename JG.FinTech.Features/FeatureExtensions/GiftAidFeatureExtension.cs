namespace JG.FinTech.Features.FeatureExtensions
{
    using JG.FinTech.Features.GiftAidCalculator;
    using Microsoft.Extensions.DependencyInjection;

    public static class GiftAidFeatureExtension
    {
        public static IServiceCollection AddGiftAidFeature(this IServiceCollection feature)
        {
            return feature.AddScoped<IGiftAidCalculator, GiftAidCalculator>();
        }
    }
}
