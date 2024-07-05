using Moq;
using TestExerciseControlant;

public class ProgramTests
{
    private readonly FactStore factStore;
    private readonly Mock<INumbersApiService> mockApiService;
    private readonly ProgramManager programManager;
    private readonly UserInterface userInterface;

    public ProgramTests()
    {
        factStore = new FactStore();
        mockApiService = new Mock<INumbersApiService>();
        programManager = new ProgramManager();
        userInterface = new UserInterface(programManager.initializeMenuOptions());
    }

    [Theory]
    [InlineData(42, "trivia", false)]
    [InlineData(100, "math", false)]
    [InlineData(-1, "trivia", true)]
    [InlineData(1200, "year", false)]
    public async Task GetNumberApiResponse_ValidResponse_StoresFact(int number, string option, bool isRandom)
    {
        var invalidResponses = new HashSet<string>
        {
            "Fact: there is no fact",
            "No Data",
            "There is an exception"
        };

        mockApiService.Setup(service => service.getNumberApiResponse()).ReturnsAsync("Valid fact");

        string result = await new NumbersApiServise(number, option, isRandom).getNumberApiResponse();

        Assert.False(invalidResponses.Contains(result), "The response is one of the invalid responses.");
    }

    [Theory]
    [InlineData(42, "unknown", false)]
    [InlineData(1, "trivia", true)]
    public async Task GetNumberApiResponse_ExceptionHandled_ReturnsExceptionMessage(int number, string option, bool isRandom)
    {
        mockApiService.Setup(service => service.getNumberApiResponse()).ThrowsAsync(new Exception("Test exception"));

        string result = await new NumbersApiServise(number, option, isRandom).getNumberApiResponse();

        Assert.Contains("There is an exception", result);
    }

    [Theory]
    [InlineData(42, "trivia", false, "42 is the answer to life, the universe, and everything.")]
    [InlineData(42, "trivia", false, "Test")]
    [InlineData(100, "math", false, "100 is a perfect square.")]
    [InlineData(1200, "year", false, "1200 was a great year.")]
    [InlineData(7, "trivia", false, "7 is a prime number.")]
    public void StoreFact_StoresCorrectly(int number, string option, bool isRandom, string data)
    {
        factStore.storeFact(number, option, isRandom, data);

        Assert.True(factStore.responses.ContainsKey(number));
        Assert.True(factStore.responses[number].ContainsKey(option));
        Assert.Contains(data, factStore.responses[number][option]);
    }

    [Fact]
    public void ExploreGatheredFacts_DisplaysFactsCorrectly()
    {
        using (var consoleOutput = new ConsoleOutput())
        {
            var responses = new Dictionary<int, Dictionary<string, HashSet<string>>>
            {
                { 42, new Dictionary<string, HashSet<string>> { { "trivia", new HashSet<string> { "42 is the answer to life, the universe, and everything." } } } }
            };

            factStore.setResponses(responses);
            programManager.exploreGatheredFacts();

            string output = consoleOutput.getOutput();
            Assert.Contains("42 is the answer to life, the universe, and everything.", output);
        }
    }

    [Fact]
    public void UserInterface_DisplaysMenuCorrectly()
    {
        using (var consoleOutput = new ConsoleOutput())
        {
            var ui = new UserInterface(programManager.initializeMenuOptions());
            ui.writeMenu();

            string output = consoleOutput.getOutput();
            Assert.Contains("Hello! This is a simple code that can give funny facts about different numbers.", output);
        }
    }

    [Fact]
    public void ArrowNavigation_WorksCorrectly()
    {
        userInterface.arrowDownPressed();
        Assert.Equal(1, getSelectedItemIndex());

        userInterface.arrowUpPressed();
        Assert.Equal(0, getSelectedItemIndex());
    }

    private int getSelectedItemIndex()
    {
        var fieldInfo = typeof(UserInterface).GetField("selectedItem", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (int)fieldInfo.GetValue(userInterface);
    }
}

// Additional classes for capturing console output in tests
public class ConsoleOutput : IDisposable
{
    private StringWriter stringWriter;
    private TextWriter originalOutput;

    public ConsoleOutput()
    {
        stringWriter = new StringWriter();
        originalOutput = Console.Out;
        Console.SetOut(stringWriter);
    }

    public string getOutput()
    {
        return stringWriter.ToString();
    }

    public void Dispose()
    {
        Console.SetOut(originalOutput);
        stringWriter.Dispose();
    }
}
