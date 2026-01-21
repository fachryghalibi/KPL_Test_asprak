using System.Text.Json;
using testpraktikum.Models;

namespace testpraktikum.Repositories
{
    public class StudentRepository
    {
        private static StudentRepository? _instance;
        private static readonly object _lock = new();
        private readonly string _filePath = "students.json";

        private StudentRepository()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public static StudentRepository Instance
        {
            get
            {
                lock (_lock)
                {
                    _instance ??= new StudentRepository();
                    return _instance;
                }
            }
        }

        public List<Student> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Student>>(json)!;
        }

        public void Save(List<Student> students)
        {
            var json = JsonSerializer.Serialize(students);
            File.WriteAllText(_filePath, json);
        }
    }
}
