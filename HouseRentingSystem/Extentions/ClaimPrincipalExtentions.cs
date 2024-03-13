using System.Security.Claims;

namespace HouseRentingSystem.Extentions
{
    public static class ClaimPrincipalExtentions
    {
        public static string Id (this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
