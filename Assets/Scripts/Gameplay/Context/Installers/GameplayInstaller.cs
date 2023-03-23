using Gameplay.Entities.Enemy;
using Gameplay.Entities.Player;
using Gameplay.ObjectsPool;
using SarrrGames.GoldenRush.Gameplay.Entities.Player;
using UnityEngine;
using Zenject;
using PlayerUnit = Gameplay.Entities.Player.PlayerUnit;

namespace Gameplay.Context.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
         [SerializeField] private Squad squad;
        
        public override void InstallBindings()
        {
            Container.Bind<Squad>().FromInstance(squad).AsSingle();
            Container.Bind<Pool>().AsTransient().Lazy();    
            Container.Bind<EnemyContainer>().AsSingle().NonLazy();
        }
    }
}
