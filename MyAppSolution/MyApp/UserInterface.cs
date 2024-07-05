namespace TestExerciseControlant
{
    public class UserInterface
    {
        private List<Option> menuOptions;
        private int selectedItem;

        public UserInterface(List<Option> menuOptions)
        {
            selectedItem = 0;
            this.menuOptions = menuOptions;
        }

        public void writeMenu(Option selectedOption)
        {
            Console.Clear();
            Console.WriteLine("Hello! This is a simple code that can give funny facts about different numbers. Please, choose your option:\n");

            foreach (var option in menuOptions)
            {
                Console.WriteLine(option == selectedOption ? $"> {option.name}" : $"  {option.name}");
            }
        }
        public void writeMenu() { writeMenu(menuOptions[selectedItem]); }

        public void exploreFacts(Dictionary<int, Dictionary<string, HashSet<string>>> responses)
        {
            Console.Clear();
            Console.WriteLine("Gotten facts:\n");

            foreach (var numberEntry in responses.OrderBy(x => x.Key))
            {
                int number = numberEntry.Key;
                var factsByType = numberEntry.Value;
                Console.WriteLine("--------------------------------------");
                Console.WriteLine($"Number [{((number != -1) ? number : "random")}]:");

                foreach (var typeEntry in factsByType)
                {
                    string type = typeEntry.Key;
                    var facts = typeEntry.Value;
                    if (facts.Any())
                    {
                        Console.WriteLine($"  Type |{type}|: ");
                        foreach (var fact in facts)
                        {
                            Console.WriteLine($"    - {fact}");
                        }
                    }
                }
                Console.WriteLine("--------------------------------------");
            }

            Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey(intercept: true);
            writeMenu(menuOptions.First());
        }

        public void arrowDownPressed()
        {
            selectedItem = Math.Min(selectedItem + 1, menuOptions.Count - 1);
            writeMenu(menuOptions[selectedItem]);
        }

        public void arrowUpPressed()
        {
            selectedItem = Math.Max(0, selectedItem - 1);
            writeMenu(menuOptions[selectedItem]);
        }

        public async Task enterPressed()
        {
            await menuOptions[selectedItem].selected.Invoke();
            selectedItem = 0;
            writeMenu(menuOptions[selectedItem]);
        }
    }
}
