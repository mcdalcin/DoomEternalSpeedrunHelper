using System.Threading.Tasks;

namespace DoomEternalSpeedrunHelper2.Activation
{
    public interface IActivationHandler
    {
        bool CanHandle(object args);

        Task HandleAsync(object args);
    }
}
