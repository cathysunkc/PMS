using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PMS
{
    public partial class Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            DB db = new DB();

            DataSet ds = new DataSet();
            DataTable dt = Property.GetFeaturedProperty(db);

            ds.Tables.Add(dt);

            gridProperty.DataSource = ds;
            gridProperty.DataBind();
        }

    }
}