using System;

namespace Faker.ValueGenerator.CleverGenerators
{
    public class CleverStringGenerator : IBaseGenerator
    {
        private string[] someWords = { "clever", "generator" ,"bruh",
                                        "War","is","peace","Freedom","is","slavery","Ignorance","is","strength",
                                        "The","world's","full","of","lonely","people","afraid","to","make","the","first","move"};
        private Random rand = new Random();

        public object Generate()
        {
            int n = rand.Next(someWords.Length + 1);
            return someWords[n];
        }

        public Type GetGeneratedType()
        {
            return typeof(string);
        }
    }
}
