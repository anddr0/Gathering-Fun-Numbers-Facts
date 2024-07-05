namespace TestExerciseControlant
{
    public class Option
    {
        public string name { get; }
        public Func<Task> selected { get; }

        public Option(string name, Func<Task> selected)
        {
            this.name = name;
            this.selected = selected;
        }
    }
}
