using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;

using Autofac.Extras.IocManager;

using Stove.Runtime.Security;

namespace Stove.Runtime.Session
{
    /// <summary>
    ///     Implements <see cref="IStoveSession" /> to get session properties from claims of
    ///     <see cref="Thread.CurrentPrincipal" />.
    /// </summary>
    public class ClaimsStoveSession : IStoveSession, ISingletonDependency
    {
        public ClaimsStoveSession()
        {
            PrincipalAccessor = DefaultPrincipalAccessor.Instance;
        }

        public IPrincipalAccessor PrincipalAccessor { get; set; } //TODO: Convert to constructor-injection

        public virtual int? TenantId
        {
            get
            {
                Claim tenantIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == StoveClaimTypes.TenantId);
                if (string.IsNullOrEmpty(tenantIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt32(tenantIdClaim.Value);
            }
        }

        public virtual long? UserId
        {
            get
            {
                Claim userIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }

                long userId;
                if (!long.TryParse(userIdClaim.Value, out userId))
                {
                    return null;
                }

                return userId;
            }
        }

        public virtual long? ImpersonatorUserId
        {
            get
            {
                Claim impersonatorUserIdClaim = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == StoveClaimTypes.ImpersonatorUserId);
                if (string.IsNullOrEmpty(impersonatorUserIdClaim?.Value))
                {
                    return null;
                }

                return Convert.ToInt64(impersonatorUserIdClaim.Value);
            }
        }
    }
}
