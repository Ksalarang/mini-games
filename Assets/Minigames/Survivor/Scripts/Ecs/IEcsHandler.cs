namespace Minigames.Survivor.Scripts.Ecs
{
    public interface IEcsHandler
    {
        bool Active { get; set; }
        void Initialize();
        void Destroy();
    }
}
