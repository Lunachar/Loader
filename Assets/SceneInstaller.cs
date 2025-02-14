using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public DoorController doorController;
    public DoorButton doorButton;
    public PlayerInteraction playerInteraction;

    public override void InstallBindings()
    {
        Debug.Log("Zenject: SceneInstaller");
        Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<DoorController>().FromInstance(doorController).AsSingle();
        Container.Bind<DoorButton>().FromInstance(doorButton).AsSingle();
        Container.Bind<PlayerInteraction>().FromInstance(playerInteraction).AsSingle();
        Container.Bind<PlayerInventory>().FromComponentInHierarchy().AsSingle();

    }
}
