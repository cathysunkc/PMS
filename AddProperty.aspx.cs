using System;
using System.Globalization;
using System.Web.UI;

namespace PMS
{
    public partial class AddProperty : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                double parsedPrice;
                double parsedBedNum;
                double parsedBathNum;
                DateTime parsedAvailableDate;

                if (!double.TryParse(Request.Form["price"], out parsedPrice) || parsedPrice <= 0)
                {
                    ShowErrorMessage("Please enter a valid price.");
                    return;
                }

                if (!double.TryParse(Request.Form["bedNum"], out parsedBedNum) || parsedBedNum <= 0)
                {
                    ShowErrorMessage("Please enter a valid number of beds.");
                    return;
                }

                if (!double.TryParse(Request.Form["bathNum"], out parsedBathNum) || parsedBathNum <= 0)
                {
                    ShowErrorMessage("Please enter a valid number of baths.");
                    return;
                }

                string availableDateString = Request.Form["availableDate"];

                if (string.IsNullOrEmpty(availableDateString))
                {
                    ShowErrorMessage("Available date is required.");
                    return;
                }

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
                    PropertyID = db.GenerateNewPropertyID(),
                    Address = Request.Form["propertyName"],
                    City = Request.Form["propertyCity"],
                    ZipCode = Request.Form["propertyZip"],
                    PropertyType = Request.Form["propertyType"],
                    Description = Request.Form["description"],
                    Area = Request.Form["area"],
                    BedNum = parsedBedNum,
                    BathNum = parsedBathNum,
                    ParkingType = Request.Form["parkingType"],
                    TransactionType = RadioButton1.Checked ? 'R' : 'S',
                    Price = parsedPrice,
                    AvailableDate = parsedAvailableDate,
                    PostedDate = DateTime.Now,
                    IsFeatured = false,
                    IsSold = false,
                    RealtorID = realtorID
                };

                db.AddProperty(property);

                Response.Redirect("ViewProperty.aspx");
            }


        }
        private void ShowErrorMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", $"alert('{message}');", true);
        }
    }
}