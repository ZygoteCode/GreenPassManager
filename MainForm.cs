using System.Windows.Forms;
using MetroSuite;
using System.Diagnostics;
using System.Drawing;
using System;

public partial class MainForm : MetroForm
{
    public MainForm()
    {
        InitializeComponent();
        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
        CheckForIllegalCrossThreadCalls = false;
    }

    private void gunaButton1_Click(object sender, System.EventArgs e)
    {
        if (openFileDialog1.ShowDialog().Equals(DialogResult.OK))
        {
            string json = "";

            try
            {
                json = GPSharp.GetJSON(System.IO.File.ReadAllText(openFileDialog1.FileName));
            }
            catch
            {

            }

            if (json == "" || json == null)
            {
                try
                {
                    json = GPSharp.GetJSON(Image.FromFile(openFileDialog1.FileName));
                }
                catch
                {

                }
            }

            if (json == "" || json == null)
            {
                MessageBox.Show("Invalid file loaded!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    GreenPass greenPass = new GreenPass(json);

                    if (greenPass == null)
                    {
                        MessageBox.Show("Invalid file loaded!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        LoadGreenPass(greenPass);
                    }       
                }
                catch
                {
                    MessageBox.Show("Invalid file loaded!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            openFileDialog1.FileName = "";       
        }
    }

    private void gunaButton2_Click(object sender, System.EventArgs e)
    {
        try
        {
            string json = "";

            try
            {
                json = GPSharp.GetJSON(Clipboard.GetText());
            }
            catch
            {

            }

            if (json == null || json == "")
            {
                try
                {
                    json = GPSharp.GetJSON(Clipboard.GetImage());
                }
                catch
                {

                }
            }

            if (json != "" && json != null)
            {
                GreenPass greenPass = new GreenPass(json);
                
                if (greenPass != null)
                {
                    LoadGreenPass(greenPass);
                }
            }
        }
        catch
        {
            
        }
    }

    public string GetDateWithHour(DateTime dateTime)
    {
        string sDay = dateTime.Day.ToString(), sMonth = dateTime.Month.ToString(), sYear = dateTime.Year.ToString(), sHour = dateTime.Hour.ToString(), sMinute = dateTime.Hour.ToString(), sSecond = dateTime.Second.ToString();

        if (sDay.Length == 1)
        {
            sDay = "0" + sDay;
        }

        if (sMonth.Length == 1)
        {
            sMonth = "0" + sMonth;
        }

        if (sHour.Length == 1)
        {
            sHour = "0" + sHour;
        }

        if (sMinute.Length == 1)
        {
            sMinute = "0" + sMinute;
        }

        if (sSecond.Length == 1)
        {
            sSecond = "0" + sSecond;
        }

        return sDay + "/" + sMonth + "/" + sYear + " " + sHour + ":" + sMinute + ":" + sSecond;
    }

    public string GetDate(DateTime dateTime)
    {
        string sDay = dateTime.Day.ToString(), sMonth = dateTime.Month.ToString(), sYear = dateTime.Year.ToString(), sHour = dateTime.Hour.ToString(), sMinute = dateTime.Hour.ToString(), sSecond = dateTime.Second.ToString();

        if (sDay.Length == 1)
        {
            sDay = "0" + sDay;
        }

        if (sMonth.Length == 1)
        {
            sMonth = "0" + sMonth;
        }

        if (sHour.Length == 1)
        {
            sHour = "0" + sHour;
        }

        if (sMinute.Length == 1)
        {
            sMinute = "0" + sMinute;
        }

        if (sSecond.Length == 1)
        {
            sSecond = "0" + sSecond;
        }

        return sDay + "/" + sMonth + "/" + sYear;
    }

    public void Reset()
    {
        metroLabel2.Text = "//";
        metroLabel3.Text = "//";
        metroLabel7.Text = "//";
        metroLabel5.Text = "//";
        metroLabel9.Text = "//";
        metroLabel11.Text = "//";
        metroLabel19.Text = "//";
        metroLabel17.Text = "//";
        metroLabel15.Text = "//";
        metroLabel13.Text = "//";
        metroLabel27.Text = "//";
        metroLabel25.Text = "//";
        metroLabel23.Text = "//";
        metroLabel21.Text = "//";
        metroLabel29.Text = "//";
        metroLabel35.Text = "//";
        metroLabel61.Text = "//";
        metroLabel33.Text = "//";
        metroLabel31.Text = "//";
        metroLabel59.Text = "//";
        metroLabel57.Text = "//";
        metroLabel55.Text = "//";
        metroLabel53.Text = "//";
        metroLabel51.Text = "//";
        metroLabel49.Text = "//";
        metroLabel47.Text = "//";
        metroLabel77.Text = "//";
        metroLabel75.Text = "//";
        metroLabel73.Text = "//";
        metroLabel71.Text = "//";
        metroLabel69.Text = "//";
        metroLabel67.Text = "//";
        metroLabel65.Text = "//";
        metroLabel63.Text = "//";
        metroLabel45.Text = "//";
    }

    public void LoadGreenPass(GreenPass greenPass)
    {
        Reset();

        metroLabel2.Text = greenPass.GetSchemaVersion();
        metroLabel3.Text = GetDate(greenPass.GetDateOfBirthday());
        metroLabel7.Text = greenPass.GetSurname();
        metroLabel5.Text = greenPass.GetStandardisedSurname();
        metroLabel9.Text = greenPass.GetForename();
        metroLabel11.Text = greenPass.GetStandardisedForename();
        metroLabel19.Text = greenPass.IsValid().ToString();
        metroLabel17.Text = greenPass.IsSuperGreenPass().ToString();
        metroLabel15.Text = greenPass.IsBooster().ToString();
        metroLabel13.Text = greenPass.IsVaccination().ToString();
        metroLabel27.Text = greenPass.IsTest().ToString();
        metroLabel25.Text = greenPass.IsRecovery().ToString();
        metroLabel23.Text = greenPass.GetCountryCode();
        metroLabel21.Text = greenPass.GetCOVIDVariant();
        metroLabel29.Text = greenPass.GetCertificateIdentifier();
        metroLabel35.Text = greenPass.GetCertificateIssuer();
        metroLabel61.Text = greenPass.GetCertificateCountryCode();
        metroLabel33.Text = GetDateWithHour(greenPass.GetStartDate());
        metroLabel31.Text = GetDateWithHour(greenPass.GetEndDate());

        if (greenPass.IsVaccination())
        {
            metroLabel59.Text = greenPass.GetVaccinationData().DosesNumber.ToString();
            metroLabel57.Text = greenPass.GetVaccinationData().MarketingAuthorization;
            metroLabel55.Text = greenPass.GetVaccinationData().TotalDoses.ToString();
            metroLabel53.Text = GetDate(greenPass.GetVaccinationData().VaccinationDate);
            metroLabel51.Text = greenPass.GetVaccinationData().VaccineProduct;
            metroLabel49.Text = greenPass.GetVaccinationData().VaccineType;
            metroLabel47.Text = greenPass.GetVaccinationData().GetVaccineName();
        }
        else if (greenPass.IsTest())
        {
            metroLabel77.Text = greenPass.GetTestData().TestDeviceIdentifier;
            metroLabel75.Text = greenPass.GetTestData().TestingCentre;
            metroLabel73.Text = greenPass.GetTestData().TestName;
            metroLabel71.Text = greenPass.GetTestData().TestResult;
            metroLabel69.Text = GetDateWithHour(greenPass.GetTestData().TestSampleCollection);
            metroLabel67.Text = greenPass.GetTestData().TestType;
            metroLabel39.Text = greenPass.GetTestData().GetTestResult().ToString();
            metroLabel37.Text = greenPass.GetTestData().GetTestType().ToString();
        }
        else if (greenPass.IsRecovery())
        {
            metroLabel65.Text = GetDate(greenPass.GetRecoveryData().CertificateValidFrom);
            metroLabel63.Text = GetDate(greenPass.GetRecoveryData().CertificateValidUntil);
            metroLabel45.Text = GetDate(greenPass.GetRecoveryData().FirstTest);
        }
    }

    private void gunaButton4_Click(object sender, EventArgs e)
    {
        Reset();
    }

    private void gunaButton3_Click(object sender, EventArgs e)
    {
        try
        {
            if (saveFileDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                string greenPassData = "GREEN PASS DATA\r\n" +
                    "Schema version: " + metroLabel2.Text + "\r\n" +
                    "Date of birth (DOB): " + metroLabel3.Text + "\r\n\r\n" +
                    "PERSONAL DATA\r\n" +
                    "Surname: " + metroLabel7.Text + "\r\n" +
                    "Standardised surname: " + metroLabel5.Text + "\r\n" +
                    "Forename: " + metroLabel9.Text + "\r\n" +
                    "Standardised forename: " + metroLabel11.Text + "\r\n\r\n" +
                    "CERTIFICATION DATA\r\n" +
                    "Is Valid: " + metroLabel19.Text + "\r\n" +
                    "Is Super Green Pass: " + metroLabel17.Text + "\r\n" +
                    "Is Booster: " + metroLabel15.Text + "\r\n" +
                    "Is Vaccination: " + metroLabel13.Text + "\r\n" +
                    "Is Test: " + metroLabel27.Text + "\r\n" +
                    "Is Recovery: " + metroLabel25.Text + "\r\n" +
                    "Country Code: " + metroLabel23.Text + "\r\n" +
                    "COVID-19 Variant: " + metroLabel21.Text + "\r\n" +
                    "Start Certification Validity: " + metroLabel33.Text + "\r\n" +
                    "End Certification Validity: " + metroLabel31.Text + "\r\n" +
                    "Certificate Identifier: " + metroLabel29.Text + "\r\n" +
                    "Certificate Issuer: " + metroLabel35.Text + "\r\n" +
                    "Certificate Country Code: " + metroLabel61.Text + "\r\n\r\n";

                if (metroLabel13.Text == "True")
                {
                    greenPassData += "VACCINATION DATA\r\n" +
                    "Doses Number: " + metroLabel59.Text + "\r\n" +
                    "Marketing Authorization: " + metroLabel57.Text + "\r\n" +
                    "Total Doses: " + metroLabel55.Text + "\r\n" +
                    "Vaccination Date: " + metroLabel53.Text + "\r\n" +
                    "Vaccine Product: " + metroLabel51.Text + "\r\n" +
                    "Vaccine Type: " + metroLabel49.Text + "\r\n" +
                    "Vaccine Name: " + metroLabel47.Text;
                }
                else if (metroLabel27.Text == "True")
                {
                    greenPassData += "TEST DATA\r\n" +
                    "Test Device Identifier: " + metroLabel77.Text + "\r\n" + 
                    "Testing Centre: " + metroLabel75.Text + "\r\n" + 
                    "Test Name: " + metroLabel73.Text + "\r\n" + 
                    "Test Result: " + metroLabel71.Text + "\r\n" + 
                    "Test Sample Collection: " + metroLabel69.Text + "\r\n" + 
                    "Test Type: " + metroLabel67.Text + "\r\n" + 
                    "Test Real Result: " + metroLabel39.Text + "\r\n" + 
                    "Test Real Type: " + metroLabel37.Text;
                }
                else
                {
                    greenPassData += "RECOVERY DATA\r\n" +
                    "Certificate Valid From: " + metroLabel65.Text + "\r\n" + 
                    "Certificate Valid Until: " + metroLabel63.Text + "\r\n" + 
                    "First Test: " + metroLabel45.Text;
                }

                System.IO.File.WriteAllText(saveFileDialog1.FileName, greenPassData);
            }
        }
        catch
        {
            MessageBox.Show("Failed to save your Green Pass data!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}