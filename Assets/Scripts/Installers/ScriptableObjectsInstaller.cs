using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableObjectsInstaller", menuName = "Installers/ScriptableObjectsInstaller")]
public class ScriptableObjectsInstaller : ScriptableObjectInstaller<ScriptableObjectsInstaller>
{
   public GridBuilderData gridBuilderData;
   public PathfindingLayers pathfindingLayers;

   public override void InstallBindings()
   {
      Container.BindInterfacesAndSelfTo<GridBuilderData>().FromInstance(gridBuilderData).AsSingle();
      Container.BindInterfacesAndSelfTo<PathfindingLayers>().FromInstance(pathfindingLayers).AsSingle();
   }
}