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
        //Testing for merge to main 3
        protected void Page_Load(object sender, EventArgs e)
        {
            Property property = new Property();
            
            DataSet ds = new DataSet();
            DataTable dt = property.GetFeaturedProperty();

            ds.Tables.Add(dt);
            
            gridProperty.DataSource = ds;
            gridProperty.DataBind();
        }
        
    }
}