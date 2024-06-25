namespace PoppingNumbersLevel4.Services.Interfaces
{
    public interface IComputerService
    {
        public void ComputerTurn(int minGameNumber, int maxGameNumber, bool useAppearancePredictor);
        public void ReplaceStarsWithRandomNumbers(int minGameNumber, int maxGameNumber);
    }
}
