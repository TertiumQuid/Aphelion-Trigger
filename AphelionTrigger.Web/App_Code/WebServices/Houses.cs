using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web;
using System.Web.Security;
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;

[WebService( Namespace = "http://tempuri.org/" )]
[WebServiceBinding( ConformsTo = WsiProfiles.BasicProfile1_1 )]
[System.Web.Script.Services.ScriptService]
public class Houses : WebService
{
    public Houses()
    {
    }

    [WebMethod]
    public string[] GetNames( string prefixText, int count )
    {
        if (count == 0) count = 10;

        HouseList houses;

        if (HttpContext.Current.Cache["HOUSELIST"] == null)
        {
            houses = HouseList.GetHouseList();

            HttpContext.Current.Cache.Insert( "HOUSELIST", houses, null, DateTime.Now.AddMinutes( 10 ), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.BelowNormal, null );
        }
        else
        {
            houses = (HouseList)HttpContext.Current.Cache["HOUSELIST"];
        }

        List<string> items = new List<string>( count );
        foreach (House house in houses)
        {
            if ( house.Name.ToLower().StartsWith( prefixText.ToLower() ) && house.Name.ToLower() != prefixText.ToLower() )
            {
                items.Add( house.Name );
                if ( items.Count == count ) break;
            }
        }

        return items.ToArray();
    }
}