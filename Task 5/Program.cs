using Internship.Task5;

var manager = new TaskManager("tasks.txt");
Console.WriteLine("=== Persistent Task Manager (CRUD + File I/O) ===");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Menu:");
    Console.WriteLine("  [1] Create");
    Console.WriteLine("  [2] Read all");
    Console.WriteLine("  [3] Update");
    Console.WriteLine("  [4] Delete");
    Console.WriteLine("  [q] Quit");
    Console.Write("Pick > ");
    string choice = (Console.ReadLine() ?? "").Trim();

    if (choice == "q" || choice == "Q")
    {
        Console.WriteLine("Bye. (Data saved to tasks.txt)");
        break;
    }

    switch (choice)
    {
        case "1":
            Console.Write("Title > ");
            string title = Console.ReadLine() ?? "";
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("Title required.");
                break;
            }
            Console.Write("Description (optional) > ");
            string desc = Console.ReadLine() ?? "";
            int createdId = manager.Create(title, desc);
            Console.WriteLine($"Created task {createdId}.");
            break;

        case "2":
            manager.ReadAll();
            break;

        case "3":
            int updateId = ReadInt("Task id > ");
            var existing = manager.Find(updateId);
            if (existing == null)
            {
                Console.WriteLine($"No task with id {updateId}.");
                break;
            }
            Console.Write("Current: ");
            existing.Print();
            Console.Write("New title (blank to keep) > ");
            string newTitle = Console.ReadLine() ?? "";
            Console.Write("New description (blank to keep) > ");
            string newDesc = Console.ReadLine() ?? "";
            Console.Write("Mark done? (y/n) > ");
            string doneStr = (Console.ReadLine() ?? "").Trim().ToLower();
            bool done = doneStr.Length > 0 && doneStr[0] == 'y';
            manager.Update(updateId, newTitle, newDesc, done);
            Console.WriteLine("Updated.");
            break;

        case "4":
            int deleteId = ReadInt("Task id > ");
            if (manager.Remove(deleteId))
            {
                Console.WriteLine($"Deleted task {deleteId}.");
            }
            else
            {
                Console.WriteLine($"No task with id {deleteId}.");
            }
            break;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

static int ReadInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        string s = (Console.ReadLine() ?? "").Trim();
        if (int.TryParse(s, out int v)) return v;
        Console.WriteLine("Not a valid integer. Try again.");
    }
}
