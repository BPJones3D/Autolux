//using Autolux.Identity.Domain.Claims;
//using Autolux.SharedKernel.SharedObjects;

//namespace Autolux.Identity.Infrastructure.Seeds;

//public static class ClaimSeed
//{
//    public static List<Claim> GenerateClaimsForAdmin()
//    {
//        var claims = new List<Claim>();

//        foreach (var permissionKey in PermissionKey.List.OrderBy(x => x.Value))
//        {
//            claims.Add(new Claim(permissionKey, true));
//        }

//        return claims;
//    }

//    public static List<Claim> GenerateClaims(IEnumerable<int> selectedClaimIds)
//    {
//        var claims = new List<Claim>();

//        foreach (var permissionKey in PermissionKey.List.OrderBy(x => x.Value))
//        {
//            claims.Add(new Claim(permissionKey, selectedClaimIds.Contains(permissionKey.Value)));
//        }

//        return claims;
//    }
//}
