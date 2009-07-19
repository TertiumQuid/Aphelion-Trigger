using System;
using System.Security.Principal;

namespace AphelionTrigger.Library.Security
{
    [Serializable()]
    public class ATPrincipal : Csla.Security.BusinessPrincipalBase
    {
        private ATPrincipal( IIdentity identity ) : base( identity ) { }

        public static bool Login( string username, string password )
        {
            ATIdentity identity = ATIdentity.GetIdentity( username, password );
            if (identity.IsAuthenticated)
            {
                ATPrincipal principal = new ATPrincipal( identity );
                Csla.ApplicationContext.User = principal;
            }
            return identity.IsAuthenticated;
        }

        public static bool ExistsUsername( string username )
        {
            return ATIdentity.ExistsUsername( username );
        }

        public static void Register( int ageId, int factionId, string name, string username, string email, string password )
        {
            ATIdentity.Register( ageId, factionId, name, username, email, password );
        }

        public static void Logout()
        {

            ATIdentity identity = ATIdentity.UnauthenticatedIdentity();
            ATPrincipal principal = new ATPrincipal( identity );
            Csla.ApplicationContext.User = principal;
        }

        public override bool IsInRole( string role )
        {
            ATIdentity identity = (ATIdentity)this.Identity;
            return identity.IsInRole( role );
        }
    }
}