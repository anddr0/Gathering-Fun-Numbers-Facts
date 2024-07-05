
namespace TestExerciseControlant
{
    public class NumbersApiServise : INumbersApiService
    {
        private static readonly HttpClient client = new HttpClient();
        private  static string baseUrl = "http://numbersapi.com/";
        private  static string defaultAnswear = "Fact: there is no fact";
        private  static string[] availableFacts = { "trivia", "math", "year" };
        private int number;
        private string option;
        private bool isRandom;

        public NumbersApiServise(int number, string option, bool isRandom)
        {
            this.number = number;
            this.option = option;
            this.isRandom = isRandom;
        }

        public async Task<string> getNumberApiResponse()
        {
            if (!availableFacts.Contains(option))
            {
                return $"There is an exception: fact option {option} does not available";
            }
            if (isRandom && number != -1)
            {
                return $"There is an exception: number should be [-1], not [{number}] while isRandom {isRandom}";
            }
            string fetchUrl = $"{baseUrl}{(isRandom ? "random" : number)}/{option}?default={defaultAnswear.Replace(" ", "+")}";

            try
            {
                HttpResponseMessage res = await client.GetAsync(fetchUrl);
                res.EnsureSuccessStatusCode();
                var data = await res.Content.ReadAsStringAsync();
                return data ?? defaultAnswear;
            }
            catch (Exception exception)
            {
                return "There is an exception: \n" + exception;
            }
        }
    }

}
