using System.Text.Json.Serialization;

namespace Wordle.Shared;

public class Statistics {
    public int GamesPlayed {get; set;}
    public int GamesWon {get; set;}
    public int GamesLost {get; set;}
    [JsonIgnore]public float WinPercentage => ((float)GamesWon / (float)GamesPlayed) * 100f;
    [JsonIgnore]public float WinPercentageInt => (int)WinPercentage;
    [JsonIgnore]public float LossPercentage => ((float)GamesLost / (float)GamesPlayed) * 100f;
    private int winstreak;
    public int CurrentWinStreak {
        get => winstreak;
        set {
            this.winstreak = value;
            this.MaxWinStreak = Math.Max(this.MaxWinStreak, this.winstreak);
        }
    }
    public int MaxWinStreak {get; set;}

    public Dictionary<int, int>? GuessDistribution {get; set;}

    public void AddWin(int guesses) {
        this.GamesPlayed += 1;
        this.GamesWon += 1;
        this.CurrentWinStreak += 1;

        if (this.GuessDistribution == null)
            this.GuessDistribution = new Dictionary<int, int>();
        if (this.GuessDistribution.ContainsKey(guesses))
            this.GuessDistribution[guesses] += 1;
        else 
            this.GuessDistribution[guesses] = 1;
    }
    public void AddLoss() {
        this.GamesPlayed += 1;
        this.GamesLost += 1;
        this.CurrentWinStreak = 0;
    }
}