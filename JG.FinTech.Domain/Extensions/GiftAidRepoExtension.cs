namespace JG.FinTech.Domain.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class GiftAidRepoExtension
    {
        public static IServiceCollection AddGiftAidRepository(this IServiceCollection feature)
        {
            return feature.AddScoped<IGiftAidRepository, GiftAidRepository>();
        }
    }
}
