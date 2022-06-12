using Assets.Scripts.Controllers;
using Assets.Scripts.Services.Movement;
using Zenject;

namespace Assets.Scripts.Factories
{
   public class EnemyFactory : PlaceholderFactory<IMovementService, EnemyController>
   {
      
   }
}
