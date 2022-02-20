using Assets.Scripts.Services.Grid;
using Assets.Scripts.Services.Movement;
using Assets.Scripts.Services.Raycast;
using UnityEngine;
using Zenject;

public class FrameworkInstaller : MonoInstaller
{
   public override void InstallBindings()
   {
      Container.Bind<IRaycastService>().To<RaycastService>().AsSingle();
      Container.Bind<IMovementService>().To<MovementService>().AsSingle();
      Container.Bind<IGridBuilderService>().To<GridBuilderService>().AsSingle();
   }
}