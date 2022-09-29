namespace Axemasta.TwitchChat.ViewModels
{
    internal class ViewModelBase : BindableBase
    {
        private string title;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        protected INavigationService navigationService { get; }

        public ViewModelBase(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
