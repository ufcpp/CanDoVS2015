using KabeDon.Packaging;
using System.Threading.Tasks;

namespace KabeDon.Sound
{
    public interface ISoundPlayerFactory
    {
        ISoundPlayer Create(IStorage soundStorage);
    }
}
