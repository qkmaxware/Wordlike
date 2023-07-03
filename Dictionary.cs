namespace Wordle;

/// <summary>
/// English dictionary
/// </summary>
public class Dictionary {

    public Dictionary<int, List<string>> words_by_length = new Dictionary<int, List<string>>();

    public Dictionary() {
        var files = new string[]{
            "Wordle.data.words_3letter.json",   // 3 letter
            "Wordle.data.words_wordle.json",    // 5 letter
            "Wordle.data.words_7letter.json",   // 7 letter
        };
        foreach (var file in files) {
            Stream? stream = this.GetType().Assembly.GetManifestResourceStream(file);
            if (stream != null) {
                var dict = System.Text.Json.JsonSerializer.Deserialize<string[]>(stream);
                if (dict != null) {
                    foreach (var key in dict) {
                        var length = key.Length;
                        if (words_by_length.ContainsKey(length)) {
                            words_by_length[length].Add(key);
                        } else {
                            var list = new List<string>();
                            list.Add(key);
                            words_by_length[length] = list;
                        }
                    } 
                }
            }
        }
    }

    private static Random random = new Random();

    public string? GetRandomWordOfLength(int length) {
        List<string>? words;
        if (words_by_length.TryGetValue(length, out words)) {
            if (words.Count == 0)
                return null;
            return words[random.Next(words.Count)];
        } else {
            return null;
        }
    }

    public string Get5LetterWordOfTheDay() {
        var now = DateTime.Now;
        var seed = now.Year * (now.Month + 1) * now.Day;
        var fives = this.words_by_length[5];
        return fives[seed % fives.Count];
    }

    public int CountWordsOfLength(int length) {
        List<string>? words;
        if (words_by_length.TryGetValue(length, out words)) {
            return words.Count;
        } else {
            return 0;
        }
    }
}