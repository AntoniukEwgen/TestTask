using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private CameraSettings _cameraSettings;

    public override void InstallBindings()
    {
        Container.Bind<ICameraSettings>().FromInstance(_cameraSettings).AsSingle();
        Container.Bind<IInputHandler>().To<KeyboardInputHandler>().AsSingle();
    }
}
