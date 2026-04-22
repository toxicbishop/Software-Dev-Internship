namespace Internship.Task5;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Done { get; set; }

    public TaskItem(int id, string title, string description)
    {
        Id = id;
        Title = title;
        Description = description;
        Done = false;
    }

    public void Print()
    {
        var mark = Done ? "[X]" : "[ ]";
        var line = $"[{Id}] {mark} {Title}";
        if (!string.IsNullOrEmpty(Description))
        {
            line += $" -- {Description}";
        }
        Console.WriteLine(line);
    }
}
