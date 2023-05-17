using UnityEngine;

namespace PlatformerGame
{
    public interface ILevel
    {
        public void ReceiveMsg(Vector3 position);
    }

    public interface IBlock
    {
        public void AddLevel(ILevel level);
        public void SendMsg();
    }
}
