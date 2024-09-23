using Zenject;

public class MusicControllerInstaller : MonoInstaller
{
    public override void InstallBindings() {
        Container.Bind<MusicController>()
            .FromComponentInHierarchy()
            .AsSingle();
    }
}
