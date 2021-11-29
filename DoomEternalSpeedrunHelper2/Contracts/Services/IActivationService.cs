using System.Threading.Tasks;

namespace DoomEternalSpeedrunHelper2.Contracts.Services
{
    public interface IActivationService
    {
        Task ActivateAsync(object activationArgs);
    }
}
