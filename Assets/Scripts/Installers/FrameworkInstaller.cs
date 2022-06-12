using Assets.Scripts.Controllers;
using Assets.Scripts.Factories;
using Assets.Scripts.Services.Entities;
using Assets.Scripts.Services.Grid;
using Assets.Scripts.Services.Movement;
using Assets.Scripts.Services.Pathfinding;
using Assets.Scripts.Services.Raycast;
using Zenject;

public class FrameworkInstaller : MonoInstaller
{
   public EnemyController enemyPrefab;

   public override void InstallBindings()
   {
      Container.Bind<IRaycastService>().To<RaycastService>().AsSingle();
      Container.Bind<IMovementService>().To<MovementService>().AsSingle();
      Container.Bind<IGridBuilderService>().To<GridBuilderService>().AsSingle();
      Container.Bind<IPathfindingService>().To<PathfindingService>().AsSingle();
      Container.Bind<IEntitySpawnerService>().To<EntitySpawnerService>().AsSingle();


      Container.BindFactory<IMovementService, EnemyController, EnemyFactory>().FromComponentInNewPrefab(enemyPrefab);
   }
}