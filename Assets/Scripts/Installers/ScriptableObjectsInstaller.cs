using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ScriptableObjectsInstaller", menuName = "Installers/ScriptableObjectsInstaller")]
public class ScriptableObjectsInstaller : ScriptableObjectInstaller<ScriptableObjectsInstaller>
{
   public GridBuilderData gridBuilderData;

   public override void InstallBindings()
   {
      Container.BindInterfacesAndSelfTo<GridBuilderData>().FromInstance(gridBuilderData).AsSingle();
   }
}