using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using MudBlazor;
using PasigSystem.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasigSystem.Client.Pages
{
    public partial class ApplicationForm
    {
        private PatientRecord userDetails = new PatientRecord();

        string imgUrl = string.Empty;

        List<ImageFile> filesBase64 = new List<ImageFile>();

        bool success;
        MudTextField<string> emailField;
        MudTextField<string> lastNameField;
        MudTextField<string> firstNameField;
        MudTextField<string> middleNameField;
        MudTextField<string> maidenMiddleNameField;
        MudTextField<string> homeAddressField;
        MudTextField<string> companyField;
        MudTextField<string> companyAddressField;

        //civil status
        CivilStatus civilStatus;

        public class CivilStatus
        {
            public string Name { get; set; }
        }

        Func<CivilStatus, string> converter = p => p?.Name;

        //gender
        public string SelectedGender { get; set; }

        // photo
        IList<IBrowserFile> files = new List<IBrowserFile>();
        private async Task UploadFilesAsync(InputFileChangeEventArgs e)
        {
            var entries = e.GetMultipleFiles();

            foreach (var file in e.GetMultipleFiles())
            {
                files.Add(file);
                var resizedFile = await file.RequestImageFileAsync(file.ContentType, 150, 150); // resize the image file
                var buf = new byte[resizedFile.Size]; // allocate a buffer to fill with the file's data
                using (var stream = resizedFile.OpenReadStream())
                {
                    await stream.ReadAsync(buf); // copy the stream to the buffer
                }
                userDetails.photo = buf;
                filesBase64.Add(new ImageFile { base64data = Convert.ToBase64String(buf), contentType = file.ContentType, fileName = file.Name }); // convert to a base64 string!!
                imgUrl = Convert.ToBase64String(buf);
            }
            //Do your validations here
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            if (entries.FirstOrDefault().Size > 10000)
            {
                Snackbar.Add($"Files with {entries.FirstOrDefault().Size} bytes size are not allowed", Severity.Error);
            }
            if (entries.FirstOrDefault().Name.Split(".").Last() == "JPG")
            {

            }

            Snackbar.Add($"This file has the extension {entries.FirstOrDefault().Name.Split(".").Last()}", Severity.Info);

            //TODO upload the files to the server
        }

        BarangayX barangay;

        public class BarangayX
        {
            public string Name { get; set; }
        }

        Func<BarangayX, string> converter1 = p => p?.Name;


        public List<Barangay> barangays = new List<Barangay>();

        public string SelectedBarangay { get; set; }
        public string SelectedCategory { get; set; }

        MudListItem selectedItem;

        public string SelectedPayment { get; set; }

        //AppointmentDate
        MudDatePicker _picker;
        // APPOINTMENT DATE
        public DateTime MinDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 05);
        public DateTime MaxDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 27);

        private DateTime? appDate { get; set; } = Convert.ToDateTime(DateTime.Now).AddDays(7); //DateTime.Now + 5;
        private DateTime? bdate { get; set; } = Convert.ToDateTime(DateTime.Now).AddYears(-18); 

        //AppointmentTime
        AppointmentTime appTime;

        public class AppointmentTime
        {
            public string Name { get; set; }
        }

        Func<AppointmentTime, string> converter2 = p => p?.Name;

        MudForm form;
        
        private Guid myid { get; set; }
        private byte[] image = null;

        protected override Task OnInitializedAsync()
        {
            QRCoder.QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData qrData = qrGenerator.CreateQrCode(
                buildQRString(),
                QRCoder.QRCodeGenerator.ECCLevel.Q);
            QRCoder.PngByteQRCode qrCode = new QRCoder.PngByteQRCode(qrData);
            image = qrCode.GetGraphic(1);
            return Task.CompletedTask;
        }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        private DotNetObjectReference<ApplicationForm> objRef;

        protected override void OnInitialized()
        {
            this.objRef = DotNetObjectReference.Create(this);
        }
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initPayPalButton", objRef);
                StateHasChanged();
            }
        }

        private void HandleValidSubmitAsync()
        {
        }
        private void GoBack()
        {
            _navigationManager.NavigateTo("/");
        }
        //QRCode
        private string buildQRString()
        {
            myid = Guid.NewGuid();
            return myid.ToString();
        }


    }
}
