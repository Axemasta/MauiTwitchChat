using DryIoc;

namespace Axemasta.TwitchChat.Helpers
{
    internal static class ContainerHelper
    {
        public static T Resolve<T>()
        {
            try
            {
                return ContainerLocator.Container.Resolve<T>();
            }
            catch
            {
                return default;
            }
        }
    }
}
