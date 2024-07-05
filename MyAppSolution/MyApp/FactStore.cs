namespace TestExerciseControlant
{
    public class FactStore 
    {
        public Dictionary<int, Dictionary<string, HashSet<string>>> responses;

        public FactStore ()
        {
            responses = new Dictionary<int, Dictionary<string, HashSet<string>>>();
        }

        public void storeFact(int number, string option, bool isRandom, string data)
        {
            if (data != "Fact: there is no fact")
            {
                if (isRandom)
                {
                    number = int.TryParse(data.Split(" ")[0], out number) ? number : -1;
                }

                if (!responses.ContainsKey(number))
                {
                    responses[number] = new Dictionary<string, HashSet<string>>();
                }

                if (!responses[number].ContainsKey(option))
                {
                    responses[number][option] = new HashSet<string>();
                }

                responses[number][option].Add(data);
            }
        }

        public Dictionary<int, Dictionary<string, HashSet<string>>> getResponses() 
        {
            return responses;
        }
        
        public void setResponses(Dictionary<int, Dictionary<string, HashSet<string>>> responses) 
        { 
            this.responses = responses; 
        }
        
    }
}