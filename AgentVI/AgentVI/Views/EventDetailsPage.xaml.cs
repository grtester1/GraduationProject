using AgentVI.Interfaces;
using AgentVI.Models;
using AgentVI.Utils;
using AgentVI.ViewModels;
using System;
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

        private void bindUnbindableUIFields()
        {
            sensorNameLabel.Text = eventDetailsViewModel.EventModel.SensorName;
            sensorEventRuleNameLabel.Text = eventDetailsViewModel.EventModel.SensorEventRuleName.convertEnumToString();
            SensorEventDateTimeLabel.Text = eventDetailsViewModel.EventModel.SensorEventDateTime.ToString();
            SensorEventRuleNameImage.Source = Settings.BackButtonSVGPath;
            SensorEventTagLabel.Text = eventDetailsViewModel.EventModel.SensorEventTag.convertEnumToString();
        }
    }
}