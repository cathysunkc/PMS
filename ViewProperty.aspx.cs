using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PMS
{
    public partial class ViewProperty : Page
    {
        string realtor_id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string propertyID = Request.QueryString["id"];

            if (propertyID != null)
            {
                DB db = new DB();
                Property property = new Property();
                property = Property.GetPropertyByID(db, propertyID);

                this.lblAddress01.Text = property.Address;
                this.imgProperty.ImageUrl = property.ImagePath;

                if (property.IsRent())
                    this.lblPrice.Text = property.Price.ToString("C2") + "/Monthy";
                else
                    this.lblPrice.Text = property.Price.ToString("C2");

                this.lblAddress02.Text = property.Address;
                this.lblCity.Text = property.City;
                this.lblZipCode.Text = property.ZipCode;
                this.lblArea.Text = property.Area + " Square Feet";
                this.lblDescription.Text = property.Description;
                this.lblPropertyType.Text = property.PropertyType;
                this.lblBedNum.Text = property.BedNum.ToString();
                this.lblBathNum.Text = property.BathNum.ToString();
                this.lblAvailableOn.Text = property.AvailableDate.ToString("yyyy-MM-dd");
                this.realtor_id = property.RealtorID;


                if (Session["UserID"] != null)
                {
                    User user = new User();
                    user = user.GetUserByID(Session["UserID"].ToString());

                    if (user.IsClient())
                    {
                        this.btnContactRealtor.Visible = true;
                        this.btnEditProperty.Visible = false;
                    }


                    if (user.IsRealtor())
                    {
                        this.btnContactRealtor.Visible = false;

                        if (user.UserID == property.RealtorID)
                        {
                            this.btnEditProperty.Visible = true;
                        }
                    }
                }
                else
                {
                    this.btnContactRealtor.Visible = false;
                    this.btnEditProperty.Visible = false;
                }
            }
            else
            {
                Response.Redirect("Listing");
            }
        }

        protected void ContactReatlor_Click(object sender, EventArgs e)
        {
            Response.Redirect("Message?realtor_id=" + realtor_id);
        }
    }
}