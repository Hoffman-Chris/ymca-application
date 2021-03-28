
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace ymca_application.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetFirstName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FirstName");

            // Make sure claim is not null or empty before returning.
            if (claim != null && !String.IsNullOrEmpty(claim.Value))
            {
                return claim.Value;
            }
            else
            {
                return string.Empty;
            }
        }

        public static int GetRole(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Role");

            // Make sure claim is not null or empty before returning.
            if (claim != null && claim.Value != null)
            {
                return int.Parse(claim.Value);
            }
            else
            {
                return 0;
            }
        }

        public static string GetUserId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserId");

            // Make sure claim is not null or empty before returning.
            if (claim != null && !String.IsNullOrEmpty(claim.Value))
            {
                return claim.Value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}