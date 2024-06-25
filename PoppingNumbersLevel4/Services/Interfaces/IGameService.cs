namespace PoppingNumbersLevel4.Services.Interfaces
{
    public interface IGameService
    {
        public void PrintBoard();
        public int ClearConnectedNumbers();
        public bool IsValidPosition(int row, int col);
        public bool IsGameOver();
    }
}
