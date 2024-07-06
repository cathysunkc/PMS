using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static PMS.Property;

namespace PMS
{
    public partial class EditProperty : Page
    {
        string propertyID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            propertyID = Request.QueryString["id"];

            if (!IsPostBack)
            {                
                if (!string.IsNullOrEmpty(propertyID))
                {
                    LoadPropertyDetails(propertyID);
                    Session["ImageList"] = null;
                    Session["ImageListCount"] = 0;
                    PopulateImages(propertyID);
                   
                }
            }

            if (IsPostBack)
            {
                bindImages();
            }
        }

        protected void SaveChanges_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;

            //For Image Gallery
            if (Convert.ToInt16(Session["ImageListCount"]) == 0)
            {
                ShowErrorMessage("Please upload at least one image.");
                return;
            }


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
                    PropertyID = Request.QueryString["id"], // Update to check for "id"
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


                //Change Image File Name according to new Index
                List<PropertyImages> tempImages = (List<PropertyImages>)Session["ImageList"];

                foreach (PropertyImages item in tempImages)
                {
                    string originalFilePath = Server.MapPath("~/Images/" + property.PropertyID + "/") + item.FileName;
                    string newFilePath = Server.MapPath("~/Images/" + property.PropertyID + "/") + "temp" + item.Index.ToString("00") + Path.GetExtension(originalFilePath);
                    File.Move(originalFilePath, newFilePath);
                }

                //Delete all old files in directory
                DirectoryInfo DI = new DirectoryInfo(Server.MapPath("~/Images/" + property.PropertyID + "/"));
                foreach (FileInfo file in DI.GetFiles())
                {
                    if (file.Name.StartsWith(property.PropertyID))
                    {
                        file.Delete();
                    }
                }

                //Rename all files to start with propertyID
                int i = 0;
                foreach (FileInfo file in DI.GetFiles())
                {
                    i++;
                    string newFileName = $"{property.PropertyID}{i.ToString("00")}{Path.GetExtension(file.Name)}";
                    string newFilePath = Path.Combine(DI.FullName, newFileName);
                    string oldFilePath = file.FullName;

                    File.Move(oldFilePath, newFilePath);                    
                }

                // Response.Redirect("ViewProperty.aspx?id=" + property.PropertyID); // Update to pass "id"
                Server.Transfer("ViewProperty.aspx?id=" + property.PropertyID);
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

        //Load Image Gallery
		private void PopulateImages(string propertyID)
		{
            //Delete all old files in directory
            DirectoryInfo DI = new DirectoryInfo(Server.MapPath("~/Images/" + this.propertyID + "/"));
            foreach (FileInfo file in DI.GetFiles())
            {
                if (file.Name.StartsWith("temp"))
                {
                    file.Delete();
                }
            }

            List<PropertyImages> myImages = new List<PropertyImages>();
          
            int index = 0;
			foreach (var file in DI.GetFiles())
			{
                index++;
				myImages.Add(new PropertyImages
				{
                    Index = index,
					PropertyID = propertyID,
					FileName = file.Name,
					FilePath = "/Images/" + propertyID + "/" + file.Name
				});
			}
			
            Session["ImageList"] = myImages;
            Session["ImageListCount"] = myImages.Count;
            bindImages();

		}

        protected void bindImages()
        {
            List<PropertyImages> tempImages = (List<PropertyImages>)Session["ImageList"];
            tempImages.Sort((x, y) => x.Index.CompareTo(y.Index));
            listImages.DataSource = tempImages;
            listImages.DataBind();
            listImages.SelectedIndex = -1;
        }
  
        // Delete Specific Image
        protected void deleteImageButton_Command(object sender, CommandEventArgs e)
        {
            List<PropertyImages> tempImages = (List<PropertyImages>)Session["ImageList"];

            string value = e.CommandArgument.ToString();
            int index = Convert.ToInt16(value) - 1;
            tempImages.RemoveAt(index);
            int i = 0;
            foreach (PropertyImages item in tempImages)
            {
                i++;
                item.Index = i;
            }            
            Session["ImageList"] = tempImages;
            Session["ImageListCount"] = tempImages.Count;

            System.Threading.Thread.Sleep(1000);
            bindImages();
        }

        protected void movePreviousButton_Command(object sender, CommandEventArgs e)
        {
            
            List<PropertyImages> tempImages = (List<PropertyImages>)Session["ImageList"];

            string value = e.CommandArgument.ToString();
            int originalIndex = Convert.ToInt16(value);
            int newIndex = originalIndex - 1;

            tempImages[originalIndex-1].Index = newIndex;
            tempImages[newIndex-1].Index = originalIndex;

            Session["ImageList"] = tempImages;

            System.Threading.Thread.Sleep(1000);
            bindImages();
        }

        protected void moveNextButton_Command(object sender, CommandEventArgs e)
        {
            List<PropertyImages> tempImages = (List<PropertyImages>)Session["ImageList"];

            string value = e.CommandArgument.ToString();
            int originalIndex = Convert.ToInt16(value);
            int newIndex = originalIndex + 1;

            tempImages[originalIndex - 1].Index = newIndex;
            tempImages[newIndex - 1].Index = originalIndex;

            Session["ImageList"] = tempImages;

            System.Threading.Thread.Sleep(1000);
            bindImages();
            
        }

        protected void btnAddImage_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;

            if (!this.FileUpload.HasFile) {
                ShowErrorMessage("Please select an image file to upload!");
                return;
            }

            if (this.FileUpload.HasFile)
            {
                if(Path.GetExtension(this.FileUpload.FileName) != ".jpg")
                {
                    ShowErrorMessage("Please upload a JPEG file!");
                    return;
                }

                if (Convert.ToInt16(Session["ImageListCount"]) == 20) 
                {
                    ShowErrorMessage("No. of Images exceeds 20!");
                    return;
                }

                List<PropertyImages> tempImages = (List<PropertyImages>)Session["ImageList"];
                int index = tempImages.Count;
                index++;
                string fileName = "temp" + index.ToString("00") + ".jpg";
                
                FileUpload.SaveAs(Server.MapPath("~/Images/" + this.propertyID + "/") + fileName);
                tempImages.Add(new PropertyImages
                {
                    Index = index,
                    PropertyID = this.propertyID,
                    FileName = fileName,
                    FilePath = "/Images/" + this.propertyID + "/" + fileName
                });
                Session["ImageList"] = tempImages;
                Session["ImageListCount"] = index;
                bindImages();
            }
        }
    }
}
