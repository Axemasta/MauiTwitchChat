using DryIoc;

namespace Axemasta.TwitchChat.Helpers
{
    internal static class ContainerHelper
    {
        internal static IContainerProvider _containerProvider;

        public static void SetContainer(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;
        }

        public static T Resolve<T>()
        {
            try
            {
                return _containerProvider.Resolve<T>();
            }
            catch
            {
                return default;
            }
        }
    }
}
