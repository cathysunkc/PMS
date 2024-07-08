using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PMS
{
    public partial class EditProperty : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string propertyID = Request.QueryString["id"]; 
                if (!string.IsNullOrEmpty(propertyID))
                {
                    LoadPropertyDetails(propertyID);
                }
            }
        }

        protected void SaveChanges_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;

            if (Page.IsValid)
            {
                double parsedPrice;
                double parsedBedNum;
                double parsedBathNum;
                DateTime parsedAvailableDate;

                if (!double.TryParse(txtPrice.Text, out parsedPrice) || parsedPrice <= 0)
                {
                    ShowErrorMessage("Please enter a valid price.");
                    return;
                }

                if (!double.TryParse(txtBedNum.Text, out parsedBedNum) || parsedBedNum <= 0)
                {
                    ShowErrorMessage("Please enter a valid number of beds.");
                    return;
                }

                if (!double.TryParse(txtBathNum.Text, out parsedBathNum) || parsedBathNum <= 0)
                {
                    ShowErrorMessage("Please enter a valid number of baths.");
                    return;
                }

                string availableDateString = txtAvailableDate.Text;

                if (string.IsNullOrEmpty(availableDateString))
                {
                    ShowErrorMessage("Available date is required.");
                    return;
                }

                //https://stackoverflow.com/questions/9760237/what-does-cultureinfo-invariantculture-mean
                if (!DateTime.TryParseExact(availableDateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedAvailableDate))
                {
                    ShowErrorMessage("Please enter a valid available date.");
                    return;
                }

                // Retrieve the logged-in user's ID from the session
                string realtorID = Session["UserID"] != null ? Session["UserID"].ToString() : "defaultRealtorID";
                DB db = new DB();

                Property property = new Property
                {
                    PropertyID = Request.QueryString["id"], 
                    Address = txtPropertyName.Text,
                    City = txtPropertyCity.Text,
                    ZipCode = txtPropertyZip.Text,
                    PropertyType = txtPropertyType.Text,
                    Description = txtDescription.Text,
                    Area = txtArea.Text,
                    BedNum = parsedBedNum,
                    BathNum = parsedBathNum,
                    ParkingType = txtParkingType.Text,
                    TransactionType = RadioButton1.Checked ? 'R' : 'S',
                    Price = parsedPrice,
                    AvailableDate = parsedAvailableDate,
                    PostedDate = DateTime.Now,
                    IsFeatured = false,
                    IsSold = false,
                    RealtorID = realtorID
                };

                db.UpdateProperty(property);

                Response.Redirect("ViewProperty.aspx?id=" + property.PropertyID); 
            }
        }

        private void LoadPropertyDetails(string propertyID)
        {
            DB db = new DB();
            Property property = db.GetPropertyByID(propertyID);
            if (property != null)
            {
                txtPropertyName.Text = property.Address;
                txtPropertyCity.Text = property.City;
                txtPropertyZip.Text = property.ZipCode;
                txtPropertyType.Text = property.PropertyType;
                txtDescription.Text = property.Description;
                txtArea.Text = property.Area;
                txtBedNum.Text = property.BedNum.ToString();
                txtBathNum.Text = property.BathNum.ToString();
                txtParkingType.Text = property.ParkingType;
                RadioButton1.Checked = property.TransactionType == 'R';
                RadioButton2.Checked = property.TransactionType == 'S';
                txtPrice.Text = property.Price.ToString();
                txtAvailableDate.Text = property.AvailableDate.ToString("yyyy-MM-dd");
            }
        }

        protected void ValidateTransactionType(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = RadioButton1.Checked || RadioButton2.Checked;
        }

        private void ShowErrorMessage(string message)
        {
            lblErrorMessage.Text = message;
            lblErrorMessage.Visible = true;
        }
    }
}
