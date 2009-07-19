using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using AphelionTrigger;
using AphelionTrigger.Library;
using AphelionTrigger.Library.Security;

public partial class Includes_Footer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !Page.IsPostBack ) BindQuote();
    }

    private void BindQuote()
    {
        Quote quote;
        if ( ( Cache["QUOTE"] as Quote ) == null )
        {
            QuoteList list = QuoteList.GetQuoteList();

            Random random = new Random();
            int num = random.Next( 0, ( list.Count - 1 ) );

            quote = list[num];

            HttpContext.Current.Cache.Insert( "QUOTE", quote, null, DateTime.Now.AddMinutes( 3 ), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null );
        }
        else
        {
            quote = (Quote)Cache["QUOTE"];
        }

        QuoteLabel.Text = quote.Text;
        QuoteAuthor.Text = quote.Author;
    }
}
