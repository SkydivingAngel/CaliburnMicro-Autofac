using System;
using System.Windows;
using Caliburn.Micro;

namespace WpfApp1
{
    public sealed class SecondViewModel : Screen
    {
        private readonly IWindowManager windowManager;
        private readonly IEventAggregator eventAggregator;
        private readonly IRepo repo01;
        private readonly IRepo repo02;

        public SecondViewModel(IWindowManager windowManager, IEventAggregator eventAggregator)
        {
            this.windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));


            //MessageBox.Show((repo002 == repo001) + "");

            eventAggregator.Subscribe(this);
        }

        public void Login()
        {
            MessageBox.Show("Login");
        }

        public bool CanLogin
        {
            get
            {
                return Enable;
            }
        }

        private string labelText;
        public string LabelText
        {
            get
            {
                return labelText;
            }
            set
            {
                labelText = value;
                NotifyOfPropertyChange(() => LabelText);
            }
        }

        private bool enable;
        public bool Enable
        {
            get
            {
                LabelText = enable ? "Abilitato" : "Non Abilitato";
                return enable;
            }
            set
            {
                enable = value;
                NotifyOfPropertyChange(() => Enable);
                NotifyOfPropertyChange(() => CanLogin);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }
    }
}