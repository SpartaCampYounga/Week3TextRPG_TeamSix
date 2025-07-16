using TextRPG_TeamSix.Characters;

namespace TextRPG_TeamSix.Battle.Actions
{
    internal interface IPlayerAction
    {
        void Execute(Player player, List<Enemy> enemies);
    }
}