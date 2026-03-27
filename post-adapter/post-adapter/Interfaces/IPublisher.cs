using CrossPostingSystem.Models;

namespace CrossPostingSystem.Interfaces
{
    public interface IPublisher
    {
        string NetworkName { get; }
        bool Publish(Post post);
        bool IsAuthorized();
    }
}