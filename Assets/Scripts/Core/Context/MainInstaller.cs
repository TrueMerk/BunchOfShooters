using SarrrGames.GoldenRush.Gameplay.Entities.Player;
using SarrrGames.GoldenRush.Gameplay.Entities.Token;
using Zenject;

namespace Core.Context
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerProgressModel>().AsSingle().NonLazy();
            Container.Bind<TokenProgressModel>().AsSingle().NonLazy();
        }
    }
}
