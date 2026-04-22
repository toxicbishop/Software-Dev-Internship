#include <algorithm>
#include <cctype>
#include <iostream>
#include <string>
#include <vector>

class Task {
public:
  int id;
  std::string title;
  std::string description;
  bool done;

  Task(int id, const std::string &title, const std::string &description)
      : id(id), title(title), description(description), done(false) {}

  void print() const {
    std::cout << "[" << id << "] " << (done ? "[X] " : "[ ] ") << title;
    if (!description.empty()) {
      std::cout << " -- " << description;
    }
    std::cout << "\n";
  }
};

class TaskManager {
  std::vector<Task> tasks;
  int nextId = 1;

public:
  int create(const std::string &title, const std::string &description) {
    tasks.emplace_back(nextId, title, description);
    return nextId++;
  }

  void readAll() const {
    if (tasks.empty()) {
      std::cout << "No tasks.\n";
      return;
    }
    for (const auto &t : tasks)
      t.print();
  }

  Task *find(int id) {
    auto it = std::find_if(tasks.begin(), tasks.end(),
                           [id](const Task &t) { return t.id == id; });
    return it == tasks.end() ? nullptr : &*it;
  }

  bool update(int id, const std::string &title, const std::string &description,
              bool done) {
    Task *t = find(id);
    if (!t)
      return false;
    if (!title.empty())
      t->title = title;
    if (!description.empty())
      t->description = description;
    t->done = done;
    return true;
  }

  bool remove(int id) {
    auto it = std::find_if(tasks.begin(), tasks.end(),
                           [id](const Task &t) { return t.id == id; });
    if (it == tasks.end())
      return false;
    tasks.erase(it);
    return true;
  }
};

static std::string readLine(const std::string &prompt) {
  std::cout << prompt;
  std::string s;
  std::getline(std::cin, s);
  return s;
}

static int readInt(const std::string &prompt) {
  while (true) {
    std::string s = readLine(prompt);
    try {
      size_t pos = 0;
      int v = std::stoi(s, &pos);
      if (pos == s.size())
        return v;
    } catch (...) {
    }
    std::cout << "Not a valid integer. Try again.\n";
  }
}

static bool readYesNo(const std::string &prompt) {
  std::string s = readLine(prompt);
  if (s.empty())
    return false;
  char c = static_cast<char>(std::tolower(static_cast<unsigned char>(s[0])));
  return c == 'y';
}

int main() {
  TaskManager mgr;
  std::cout << "=== Task Manager (CRUD) ===\n";

  while (true) {
    std::cout << "\nMenu:\n"
              << "  [1] Create\n"
              << "  [2] Read all\n"
              << "  [3] Update\n"
              << "  [4] Delete\n"
              << "  [q] Quit\n";
    std::string choice = readLine("Pick > ");
    if (choice == "q" || choice == "Q") {
      std::cout << "Bye.\n";
      break;
    }

    if (choice == "1") {
      std::string title = readLine("Title > ");
      if (title.empty()) {
        std::cout << "Title required.\n";
        continue;
      }
      std::string desc = readLine("Description (optional) > ");
      int id = mgr.create(title, desc);
      std::cout << "Created task " << id << ".\n";
    } else if (choice == "2") {
      mgr.readAll();
    } else if (choice == "3") {
      int id = readInt("Task id > ");
      Task *t = mgr.find(id);
      if (!t) {
        std::cout << "No task with id " << id << ".\n";
        continue;
      }
      std::cout << "Current: ";
      t->print();
      std::string title = readLine("New title (blank to keep) > ");
      std::string desc = readLine("New description (blank to keep) > ");
      bool done = readYesNo("Mark done? (y/n) > ");
      mgr.update(id, title, desc, done);
      std::cout << "Updated.\n";
    } else if (choice == "4") {
      int id = readInt("Task id > ");
      if (mgr.remove(id)) {
        std::cout << "Deleted task " << id << ".\n";
      } else {
        std::cout << "No task with id " << id << ".\n";
      }
    } else {
      std::cout << "Invalid choice.\n";
    }
  }

  return 0;
}
