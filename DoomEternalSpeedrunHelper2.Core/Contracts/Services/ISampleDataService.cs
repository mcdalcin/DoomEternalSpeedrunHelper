using System.Collections.Generic;
using System.Threading.Tasks;

using DoomEternalSpeedrunHelper2.Core.Models;

namespace DoomEternalSpeedrunHelper2.Core.Contracts.Services
{
    // Remove this class once your pages/features are using your data.
    public interface ISampleDataService
    {
        Task<IEnumerable<SampleOrder>> GetContentGridDataAsync();
    }
}
