using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        string[] results = [];

        var wordSet = words.ToHashSet();
        var usedWords = new HashSet<string>();
        
        foreach (string twoLetters in wordSet)
        {
            // Check this words hasn't already been checked
            if (usedWords.Contains(twoLetters))
            {
                continue;
            }
            
            string reversedTwoLetters = new string(twoLetters.ToCharArray().Reverse().ToArray());

            // Special case: two letters are the same: skip
            if (reversedTwoLetters == twoLetters)
            {
                continue;
            }
            if (wordSet.Contains(reversedTwoLetters))
            {
                results = results.Append($"{twoLetters} & {reversedTwoLetters}").ToArray();
                usedWords.Add(twoLetters);
                usedWords.Add(reversedTwoLetters);
            }
        }
        
        return results;
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE
            var degree = fields[3];

            if (!degrees.ContainsKey(degree)) {
                degrees[degree] = 0;
            }

            degrees[degree] += 1;
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Construct a dictionary mapping the number of occurences of each letter in word 1:
        Dictionary<char, int> word1_letters = new();
        foreach (char letter in word1.ToUpper()) {
            if (letter == ' ')
            {
                continue;
            }
            if (!word1_letters.ContainsKey(letter)) {
                word1_letters[letter] = 0;
            }
            word1_letters[letter] += 1;
        }
        
        // Do the same for word 2:
        Dictionary<char, int> word2_letters = new();
        foreach (char letter in word2.ToUpper()) {
            if (letter == ' ')
            {
                continue;
            }
            if (!word2_letters.ContainsKey(letter)) {
                word2_letters[letter] = 0;
            }
            word2_letters[letter] += 1;
        }
        
        // Check that there is the same number of keys in each dicitonary AND that there is no differences between them
        return word1_letters.Count == word2_letters.Count && !word1_letters.Except(word2_letters).Any();
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        string[] descriptions = [];
        
        foreach(Feature feature in featureCollection.Features)
        {
            descriptions = descriptions.Append($"{feature.Properties.Place} - Mag {feature.Properties.Mag}").ToArray();
        }

        return descriptions;
    }
}