

using System.Security.Cryptography.X509Certificates;

namespace SpartaDungeonBattle
{
    // 메인 메서드
    class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }  
    }
}