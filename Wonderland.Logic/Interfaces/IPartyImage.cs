
namespace Wonderland.Logic.Interfaces
{
    // we are using an interface as a selected PartyImage could either be a default 'Image' or it could be an uploaded 'PartyImage'
    public interface IPartyImage
    {
        int Id { get; }

        string Url { get; }
    }
}
