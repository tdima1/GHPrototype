using Assets.Scripts.Services.Raycast;
using UnityEngine;
using Zenject;

public class FrameworkInstaller : MonoInstaller
{
   public override void InstallBindings()
   {
      Container.Bind<IRaycastService>().To<RaycastService>().AsSingle();
    }
}