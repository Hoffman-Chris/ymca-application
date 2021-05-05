
using System;
using System.Security.Claims;
using System.Security.Principal;

namespace ymca_application.Extensions
{
    public static class IdentityExtensions
    {
        // Get first name from AspNet Identity object.
        public static string GetFirstName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FirstName");

            // Make sure claim is not null or empty before returning.
            if (claim != null && !String.IsNullOrEmpty(claim.Value))
            {
                return claim.Value;
            }

            return string.Empty;
        }

        // Get role from AspNet Identity object.
        public static int GetRole(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Role");

            // Make sure claim is not null or empty before returning.
            if (claim != null && claim.Value != null)
            {
                return int.Parse(claim.Value);
            }

            return 0;
        }

        // Get user id from AspNet Identity object.
        public static string GetUserId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserId");

            // Make sure claim is not null or empty before returning.
            if (claim != null && !String.IsNullOrEmpty(claim.Value))
            {
                return claim.Value;
            }

            return string.Empty;
        }
    }
}