using AgentVI.Interfaces;
using AgentVI.Models;
using AgentVI.Utils;
using AgentVI.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgentVI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDetailsPage : ContentPage, INotifyContentViewChanged
    {
        private EventDetailsViewModel eventDetailsViewModel = null;
        public event EventHandler<UpdatedContentEventArgs> RaiseContentViewUpdateEvent;

        public EventDetailsPage()
        {
            InitializeComponent();
        }

        public EventDetailsPage(EventModel i_EventModel) : this()
        {
            eventDetailsViewModel = new EventDetailsViewModel(i_EventModel);
            bindUnbindableUIFields();
        }

        private void onEventDetailsBackButtonTapped(object sender, EventArgs e)
        {
            RaiseContentViewUpdateEvent?.Invoke(this, new UpdatedContentEventArgs(null, true));
        }

        private async void bindUnbindableUIFields()
        {
            await Task.Factory.StartNew(() =>
            {
                SensorEventClipVideoPlayer.Source = eventDetailsViewModel.SensorEventClipPath;
                sensorNameLabel.Text = eventDetailsViewModel.SensorName;
                sensorEventRuleNameLabel.Text = eventDetailsViewModel.SensorEventRuleName;
                SensorEventDateTimeLabel.Text = eventDetailsViewModel.SensorEventDateTime;
                SensorEventRuleNameImage.Source = eventDetailsViewModel.RuleNameObjectPath;
                SensorEventTagLabel.Text = eventDetailsViewModel.SensorEventTag;
            });
            OnPropertyChanged();
        }
    }
}