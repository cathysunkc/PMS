using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static PMS.Property;

namespace PMS
{
    public partial class ViewProperty : Page
    {
        string property_id = string.Empty;
        string propertyID = string.Empty;        

        protected void Page_Load(object sender, EventArgs e)
        {
            propertyID = Request.QueryString["id"];

            

            if (propertyID != null)
            {

                //Edited by Wilson for Property static method
                Property property = Property.GetPropertyByID(propertyID);

                this.lblAddress01.Text = property.Address;

                if (!Page.IsPostBack)
                {
               //     Session["imageIndex"] = null;
                    PopulateImages();
                }

             /*   if (Session["imageIndex"] == null)
                {
                    Session["imageIndex"] = 1;
                }
                int index = Convert.ToInt16(Session["imageIndex"]);
                this.mainImage.ImageUrl = "~/Images/" + this.propertyID + "/" + this.propertyID + index.ToString("00") + ".jpg?time=" + DateTime.UtcNow;*/



                if (property.IsRent())
                    this.lblPrice.Text = property.Price.ToString("C2") + "/Month";
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
                this.lblParkingType.Text = property.ParkingType;
                // edited by Wilson for redirect to Message page
                this.property_id = property.PropertyID;


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
            Response.Redirect("Message?property_id=" + property_id);
        }

        private void PopulateImages()
        {
            List<PropertyImages> myImages = new List<PropertyImages>();
            DirectoryInfo DI = new DirectoryInfo(Server.MapPath("~/Images/" + this.propertyID + "/"));
            int index = 0;
            foreach (var file in DI.GetFiles())
            {
                if (file.Name.StartsWith(this.propertyID))
                {
                    index++;
                    myImages.Add(new PropertyImages
                    {
                        Index = index,
                        PropertyID = this.propertyID,
                        FileName = file.Name,
                        FilePath = "/Images/" + this.propertyID + "/" + file.Name
                    });
                }

            }
            listImages.DataSource = myImages;
            listImages.DataBind();

            
        }

        // redirect to editproperty page
        protected void btnEditProperty_Click(object sender, EventArgs e)
        {
            /*
            string propertyID = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(propertyID))
            {
                Server.Transfer($"EditProperty.aspx?id={propertyID}"); 
            }*/
            Response.Redirect($"EditProperty.aspx?id={this.propertyID}");
        }

        protected void ImageButton_Command(object sender, CommandEventArgs e)
        {
          /*  string value = e.CommandArgument.ToString();
            int index = Convert.ToInt16(value);
            Session["imageIndex"] = index;
            this.mainImage.ImageUrl = "~/Images/" + this.propertyID + "/" + this.propertyID + index.ToString("00") + ".jpg?time=" + DateTime.UtcNow; */
        }

    }
}