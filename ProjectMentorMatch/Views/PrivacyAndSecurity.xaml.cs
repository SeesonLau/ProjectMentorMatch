using CommunityToolkit.Maui.Views;
using System.IO;
using System.Reflection;
using System.Text;

namespace ProjectMentorMatch.Views;

public partial class PrivacyAndSecurity : Popup
{
    public PrivacyAndSecurity()
    {
        InitializeComponent();
        LoadPrivacyText();
    }

    private void LoadPrivacyText()
    {
        var assembly = Assembly.GetExecutingAssembly();
        Stream stream = assembly.GetManifestResourceStream("ProjectMentorMatch.Resources.Raw.eula.txt");

        if (stream != null)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string privacyText = reader.ReadToEnd();
                PrivacyLabel.Text = privacyText;
            }
        }
        else
        {
            PrivacyLabel.Text = "Error loading privacy text.";
        }
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }
}
