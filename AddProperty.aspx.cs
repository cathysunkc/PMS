using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static PMS.Property;

namespace PMS
{
    public partial class AddProperty : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ImageList"] = null;
                Session["ImageListCount"] = 0;
                PopulateImages();
            }

            
        }

        //Load Image Gallery
        private void PopulateImages()
        {
            DB db = new DB();
            string propertyID = db.GenerateNewPropertyID();

            //Delete all old files in directory
            DirectoryInfo DI = new DirectoryInfo(Server.MapPath("~/Images/" + propertyID + "/"));

            if (DI.Exists)
            {
                foreach (FileInfo file in DI.GetFiles())
                {
                    if (file.Name.StartsWith("temp"))
                    {
                        file.Delete();
                    }
                }                
            }
            
        }

        protected void bindImages()
        {
             List<PropertyImages> tempImages = (List<PropertyImages>)Session["ImageList"];
            tempImages.Sort((x, y) => x.Index.CompareTo(y.Index));
            listImages.DataSource = tempImages;
            listImages.DataBind();
            listImages.SelectedIndex = -1;
            
        }

        //Save button
        //Logic check for input data
        protected void SubmitBtn_Click(object sender, EventArgs e)
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

                //Rename all files to start with propertyID
                DirectoryInfo DI = new DirectoryInfo(Server.MapPath("~/Images/" + property.PropertyID + "/"));
                
                int i = 0;
                foreach (FileInfo file in DI.GetFiles())
                {
                    i++;
                    string newFileName = $"{property.PropertyID}{i.ToString("00")}{Path.GetExtension(file.Name)}";
                    string newFilePath = Path.Combine(DI.FullName, newFileName);
                    string oldFilePath = file.FullName;

                    File.Move(oldFilePath, newFilePath);
                }

                db.AddProperty(property);

                //Response.Redirect("ViewProperty.aspx");
                Server.Transfer("ViewProperty.aspx?id=" + property.PropertyID);

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

        protected void deleteImageButton_Command(object sender, CommandEventArgs e)
        {
            List<PropertyImages> tempImages = (List<PropertyImages>)Session["ImageList"];

            string value = e.CommandArgument.ToString();
            int index = Convert.ToInt16(value) - 1;
            tempImages.RemoveAt(index);

			index++;

			DB db = new DB();
			string propertyID = db.GenerateNewPropertyID();

			//If the image is temp file, delete physically
			DirectoryInfo DI = new DirectoryInfo(Server.MapPath("~/Images/" + propertyID + "/"));
			foreach (FileInfo file in DI.GetFiles())
			{
				if (file.Name.StartsWith("temp" + index.ToString("00")))
				{
					file.Delete();
				}
			}

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

        protected void btnAddImage_Click(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false;

            if (!this.FileUpload.HasFile)
            {
                ShowErrorMessage("Please select an image file to upload!");
                return;
            }

            if (this.FileUpload.HasFile)
            {
                if (Path.GetExtension(this.FileUpload.FileName) != ".jpg")
                {
                    ShowErrorMessage("Please upload a JPEG file!");
                    return;
                }

                if (Convert.ToInt16(Session["ImageListCount"]) == 20)
                {
                    ShowErrorMessage("No. of Images exceeds 20!");
                    return;
                }

                List<PropertyImages> tempImages = new List<PropertyImages>();
                int index = 0;

                if (Session["ImageList"] != null)
                {
                    tempImages = (List<PropertyImages>)Session["ImageList"];
                    index = (int)Session["ImageListCount"];
                }

                index++;
                string fileName = "temp" + index.ToString("00") + ".jpg";

                DB db = new DB();
                string propertyID = db.GenerateNewPropertyID();

                if (!Directory.Exists(Server.MapPath("~/Images/" + propertyID + "/")))
                    Directory.CreateDirectory(Server.MapPath("~/Images/" + propertyID + "/"));

                FileUpload.SaveAs(Server.MapPath("~/Images/" + propertyID + "/") + fileName);
                tempImages.Add(new PropertyImages
                {
                    Index = index,
                    PropertyID = propertyID,
                    FileName = fileName,
                    FilePath = "/Images/" + propertyID + "/" + fileName
                });

                int i = 0;
                foreach (PropertyImages item in tempImages)
                {
                    i++;
                    item.FileName = "temp" + i.ToString("00") + ".jpg";
                    item.FilePath = "/Images/" + propertyID + "/" + "temp" + i.ToString("00") + ".jpg";
                }

                Session["ImageList"] = tempImages;
                Session["ImageListCount"] = index;
                System.Threading.Thread.Sleep(1000);
                bindImages();
            }
        }
    }
}
