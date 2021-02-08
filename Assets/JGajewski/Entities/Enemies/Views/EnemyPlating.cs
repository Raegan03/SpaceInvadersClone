using JGajewski.Entities.Abstracts.Views;

namespace JGajewski.Entities.Enemies.Views
{
    public class EnemyPlating : EntityPlating
    {
        protected override EntityType EntityType => EntityType.Enemy;
    }
}
