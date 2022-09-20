using UnityEngine;
using DungeonCrawl.Actors.Characters;

namespace DungeonCrawl.Core
{
    /// <summary>
    ///     Loads the initial map and can be used for keeping some important game variables
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            Player player = ActorManager.Singleton.Spawn<Player>(0,0);
            MapLoader.LoadMap(0);
        }
    }
}
