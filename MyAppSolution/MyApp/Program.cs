namespace TestExerciseControlant
{
    public class ProgramManager
    {
        private UserInterface ui;
        private FactStore factStore;

        public ProgramManager()
        {
            factStore = new FactStore();
            ui = new UserInterface(initializeMenuOptions());
        }

        public List<Option> initializeMenuOptions()
        {
            return new List<Option>
            {
                new Option("Get Trivia Fact", () => getUserNumber("trivia")),
                new Option("Get Math Fact", () => getUserNumber("math")),
                new Option("Get Year Fact\n", () => getUserNumber("year")),
                new Option("Explore gotten facts", () => { exploreGatheredFacts(); return Task.CompletedTask; }),
                new Option("Exit", () => { exit(); return Task.CompletedTask; }),
            };
        }

        public async Task launchProgram()
        {
            ui.writeMenu();

            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey(intercept: true);
                switch (keyinfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        ui.arrowDownPressed();
                        break;
                    case ConsoleKey.UpArrow:
                        ui.arrowUpPressed();
                        break;
                    case ConsoleKey.Enter:
                        await ui.enterPressed();
                        break;
                    default:
                        break;
                }
            }
            while (keyinfo.Key != ConsoleKey.Escape);
        }

        public async Task getUserNumber(string option)
        {
            Console.Clear();
            Console.WriteLine($"Okay. Enter the numbers you want to know more {option.ToUpper()} facts about\n> " +
                            "Type 'random' if you can't choose one \n> Press LeftArrow to return to the menu: ");
            
            while (true)
            {
                var (number, isRandom, exitRequested) = await GetUserInput();

                if (exitRequested)
                {
                    ui.writeMenu();
                    return;
                }

                await processUserInput(number, option, isRandom);
            }
        }

        private async Task<(int number, bool isRandom, bool exitRequested)> GetUserInput()
        {
            int number = -1;
            string input = string.Empty;
            bool isValidNumber = false;
            bool isRandom = false;
            bool exitRequested = false;

            var inputTask = Task.Run(() =>
            {
                do
                {
                    var keyInfo = Console.ReadKey(intercept: true); // To handle we are printing or pressing LeftArrow
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            exitRequested = true;
                            return;
                        case ConsoleKey.Enter:
                            if (input == "random")
                            {
                                isRandom = true;
                                isValidNumber = true;
                            }
                            else if (int.TryParse(input, out number))
                            {
                                isValidNumber = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Let's try again: ");
                                input = string.Empty;
                            }
                            break;
                        case ConsoleKey.Backspace:
                            if (input.Length > 0)
                            {
                                input = input[..^1]; // Remove last character
                                Console.Write("\b \b"); // Remove character from screen
                            }
                            break;
                        default:
                            input += keyInfo.KeyChar;
                            Console.Write(keyInfo.KeyChar);
                            break;
                    }
                } while (!isValidNumber && !exitRequested);
            });

            await inputTask;

            return (number, isRandom, exitRequested);
        }

        private async Task processUserInput(int number, string option, bool isRandom)
        {
            // Fetch the fact from the API
            string fact = await new NumbersApiServise(number, option, isRandom).getNumberApiResponse();
            factStore.storeFact(number, option, isRandom, fact);
            Console.WriteLine($"\n - {fact}");
        }


        public void exploreGatheredFacts()
        {
            ui.exploreFacts(factStore.getResponses());
        }

        private void exit()
        {
            Console.Clear();
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            ProgramManager manager = new ProgramManager();
            await manager.launchProgram();
        }
    }
}
