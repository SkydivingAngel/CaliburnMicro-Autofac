using System;
using System.Windows;
using Caliburn.Micro;
//using Autofac.Core;

namespace WpfApp1
{
    using Autofac;

    public sealed class MainViewModel : Screen
    {
        private readonly IWindowManager windowManager;
        private readonly IEventAggregator eventAggregator;
        private readonly IRepo repo01;
        private readonly IRepo repo02;

        public MainViewModel(IWindowManager windowManager, IEventAggregator eventAggregator, IRepo Uno, IRepo Due)//
        {
            this.windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
            this.eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));

            MessageBox.Show((Uno == null) + " - " + Uno.Nome);
            MessageBox.Show((Due == null) + " - " + Due.Nome);
            eventAggregator.Subscribe(this);
        }

        public void Login()
        {
            MessageBox.Show("Login");
            var second = AutofacBootstrapper.Container.ResolveNamed<IScreen>("Second");
            second.DisplayName = "pippo";

            windowManager.ShowDialog(second);
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
