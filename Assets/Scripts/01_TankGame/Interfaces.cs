
namespace TankGame
{

    public interface IGameSubject
    {
        void AddObserver(IGameObserver _observer);

        void RemoveObserver(IGameObserver _observer);

        void SendObserver();
    }

    public interface IGameObserver
    {
        void ReceiveSubject();
    }
}
