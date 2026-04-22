namespace Internship.Task5;

public class TaskManager
{
    private readonly List<TaskItem> tasks = new();
    private int nextId = 1;
    private readonly string filePath;

    public TaskManager(string filePath)
    {
        this.filePath = filePath;
        Load();
    }

    public void Load()
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        string[] lines;
        try
        {
            lines = File.ReadAllLines(filePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading {filePath}: {ex.Message}");
            return;
        }

        int loaded = 0, skipped = 0;
        int i = 0;
        while (i + 3 < lines.Length)
        {
            string idLine = lines[i];
            string title = lines[i + 1];
            string desc = lines[i + 2];
            string doneLine = lines[i + 3];

            if (!int.TryParse(idLine, out int id))
            {
                Console.Error.WriteLine($"Warning: invalid record (id='{idLine}'), skipping.");
                skipped++;
                i += 4;
                continue;
            }

            var task = new TaskItem(id, title, desc) { Done = doneLine == "1" };
            tasks.Add(task);
            if (id >= nextId) nextId = id + 1;
            loaded++;
            i += 4;
        }

        if (i < lines.Length)
        {
            Console.Error.WriteLine($"Warning: truncated record at end of {filePath}.");
        }

        Console.Write($"Loaded {loaded} task(s) from {filePath}");
        if (skipped > 0) Console.Write($" ({skipped} skipped)");
        Console.WriteLine(".");
    }

    public bool Save()
    {
        try
        {
            using var writer = new StreamWriter(filePath, false);
            foreach (var t in tasks)
            {
                writer.WriteLine(t.Id);
                writer.WriteLine(t.Title);
                writer.WriteLine(t.Description);
                writer.WriteLine(t.Done ? "1" : "0");
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing {filePath}: {ex.Message}");
            return false;
        }
    }

    public int Create(string title, string description)
    {
        var task = new TaskItem(nextId, title, description);
        tasks.Add(task);
        int id = nextId++;
        Save();
        return id;
    }

    public void ReadAll()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks.");
            return;
        }
        foreach (var t in tasks) t.Print();
    }

    public TaskItem? Find(int id) => tasks.FirstOrDefault(t => t.Id == id);

    public bool Update(int id, string title, string description, bool done)
    {
        var t = Find(id);
        if (t == null) return false;
        if (!string.IsNullOrEmpty(title)) t.Title = title;
        if (!string.IsNullOrEmpty(description)) t.Description = description;
        t.Done = done;
        Save();
        return true;
    }

    public bool Remove(int id)
    {
        int idx = tasks.FindIndex(t => t.Id == id);
        if (idx < 0) return false;
        tasks.RemoveAt(idx);
        Save();
        return true;
    }
}
