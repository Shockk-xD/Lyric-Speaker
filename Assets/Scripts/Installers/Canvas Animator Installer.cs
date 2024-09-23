using Zenject;

public class CanvasAnimatorInstaller : MonoInstaller
{
    public override void InstallBindings() {
        InstallAnimator();
    }

    private void InstallAnimator() {
        Container.Bind<CanvasAnimator>()
                    .FromComponentInHierarchy()
                    .AsSingle();
    }
}
