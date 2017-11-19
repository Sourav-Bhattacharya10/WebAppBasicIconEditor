using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppBasicIconEditor
{
    public partial class HomePage : System.Web.UI.Page
    {
        string ServerFilePath = null;
        string SelectedFileName = null;
        string OnlyFileName = null;
        string PngFileName = null;
        string PngFilePath = null;

        

        protected void Page_Load(object sender, EventArgs e)
        {
            //If the website is loaded for the first time,
            if(!IsPostBack)
            {
                //Then delete all the image files present in the UploadedImages folder
                string[] ImageFiles = Directory.GetFiles(Server.MapPath("~/UploadedImages/"));

                for (int i = 0; i < ImageFiles.Length; i++)
                {
                    File.Delete(ImageFiles[i]);
                }
            }
            else
            {
                //if (ViewState["ImageFileUpload"] != null)
                //    ImageFileUpload = (FileUpload)ViewState["ImageFileUpload"];

                if (ViewState["ServerFilePath"] != null)
                    ServerFilePath = (string)ViewState["ServerFilePath"];

                if (ViewState["OnlyFileName"] != null)
                    OnlyFileName = (string)ViewState["OnlyFileName"];

                if (ViewState["PngFileName"] != null)
                    PngFileName = (string)ViewState["PngFileName"];

                if (ViewState["PngFilePath"] != null)
                    PngFilePath = (string)ViewState["PngFilePath"];
            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (ImageFileUpload.HasFile)
            {
                try
                {
                    //ViewState["ImageFileUpload"] = ImageFileUpload;
                    SelectedFileName = Path.GetFileName(ImageFileUpload.FileName);
                    ExtractOnlyFileName(SelectedFileName);
                    ServerFilePath = Server.MapPath("~/UploadedImages/") + SelectedFileName;
                    ViewState["ServerFilePath"] = ServerFilePath;
                    ImageFileUpload.SaveAs(ServerFilePath);
                    LoadedImage.ImageUrl = "~/UploadedImages/"+ SelectedFileName;
                }
                catch (Exception ex)
                {
                    //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }

        private void ExtractOnlyFileName(string selectedFileName)
        {
            char[] nameArray = selectedFileName.ToCharArray();
            OnlyFileName = "";
            for (int i = 0; nameArray[i] != '.'; i++)
            {
                OnlyFileName += nameArray[i];
            }
            ViewState["OnlyFileName"] = OnlyFileName;
        }

        protected void TransparentButton_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(ServerFilePath, FileMode.Open, FileAccess.Read);
                Bitmap MTImage = new Bitmap(fs);
                fs.Close();
                MTImage.MakeTransparent();
                PngFileName = OnlyFileName + ".png";
                ViewState["PngFileName"] = PngFileName;
                PngFilePath = Server.MapPath("~/UploadedImages/") + PngFileName;
                ViewState["PngFilePath"] = PngFilePath;
                MTImage.Save(PngFilePath);
                LoadedImage.ImageUrl = "~/UploadedImages/" + PngFileName;
                MTImage.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        protected void ColorButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(PngFilePath == null)
                {
                    PngFileName = OnlyFileName + ".png";
                    ViewState["PngFileName"] = PngFileName;
                    PngFilePath = Server.MapPath("~/UploadedImages/") + PngFileName;
                    ViewState["PngFilePath"] = PngFilePath;
                }

                string colorvalue = Request.Form["CP1"];
                byte newR = GetRValue(colorvalue);
                byte newG = GetGValue(colorvalue);
                byte newB = GetBValue(colorvalue);

                FileStream fs = new FileStream(PngFilePath, FileMode.Open, FileAccess.Read);
                Bitmap ACImage = new Bitmap(fs);
                fs.Close();
                for (int i = 0; i < ACImage.Width; i++)
                {
                    for (int j = 0; j < ACImage.Height; j++)
                    {
                        var c = ACImage.GetPixel(i, j);
                        if(!(c.Name == "0" && c.R == 0 && c.G == 0 && c.B == 0))
                        {
                            Color newColor = Color.FromArgb(newR, newG, newB);
                            ACImage.SetPixel(i, j, newColor);
                        }
                    }
                }

                ACImage.Save(PngFilePath);
                LoadedImage.ImageUrl = "~/UploadedImages/" + PngFileName;
                ACImage.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private byte GetBValue(string colorvalue)
        {
            char[] colorArray = colorvalue.ToCharArray();

            string Bstr = "" + colorArray[5] + colorArray[6];

            byte DecimalValue = Convert.ToByte(Bstr, 16);

            return DecimalValue;
        }

        private byte GetGValue(string colorvalue)
        {
            char[] colorArray = colorvalue.ToCharArray();

            string Gstr = "" + colorArray[3] + colorArray[4];

            byte DecimalValue = Convert.ToByte(Gstr, 16);

            return DecimalValue;
        }

        private byte GetRValue(string colorvalue)
        {
            char[] colorArray = colorvalue.ToCharArray();

            string Rstr = "" + colorArray[1] + colorArray[2];

            byte DecimalValue = Convert.ToByte(Rstr, 16);

            return DecimalValue;
        }

        protected void DownloadButton_Click(object sender, EventArgs e)
        {
            Response.ClearContent();
            Response.ContentType = "image/png";
            Response.AppendHeader("Content-Disposition", "attachment; filename="+PngFileName);
            Response.TransmitFile(PngFilePath);
            Response.End();
        }
    }
}