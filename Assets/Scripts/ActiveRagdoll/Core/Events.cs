namespace ActiveRagdoll.Core
{
    public enum TypeOfGameOver
    {
        Fall,
        Damage,
        Stuck
    }

    public static class Events
    {
        public static GameOverEvent GameOverEvent = new();
    }

    public class GameOverEvent : GameEvent
    {
        public TypeOfGameOver TypeOfGameOver;
    }
}