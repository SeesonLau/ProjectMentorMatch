using ProjectMentorMatch.ViewModels;
using Microsoft.Maui.Controls;
namespace ProjectMentorMatch.Views;

public partial class ChatSpecific : ContentPage
{
    public ChatSpecific(string ReceiverName)
    {
        InitializeComponent();
        BindingContext = new ChatViewModel(ReceiverName);
    }
}