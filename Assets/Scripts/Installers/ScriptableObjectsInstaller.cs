using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableObjectsInstaller", menuName = "Installers/ScriptableObjectsInstaller")]
public class ScriptableObjectsInstaller : ScriptableObjectInstaller<ScriptableObjectsInstaller>
{
   public GridBuilderData gridBuilderData;
   public PathfindingLayers pathfindingLayers;
   public RaycastConstants raycastConstants;
   public Directions directions;
   public CollisionConstants playerModelData;
   public PathfindingConstants pathfindingConstants;

   public override void InstallBindings()
   {
      Container.BindInterfacesAndSelfTo<GridBuilderData>().FromInstance(gridBuilderData).AsSingle();
      Container.BindInterfacesAndSelfTo<PathfindingLayers>().FromInstance(pathfindingLayers).AsSingle();
      Container.BindInterfacesAndSelfTo<RaycastConstants>().FromInstance(raycastConstants).AsSingle();
      Container.BindInterfacesAndSelfTo<Directions>().FromInstance(directions).AsSingle();
      Container.BindInterfacesAndSelfTo<CollisionConstants>().FromInstance(playerModelData).AsSingle();
      Container.BindInterfacesAndSelfTo<PathfindingConstants>().FromInstance(pathfindingConstants).AsSingle();
   }
}