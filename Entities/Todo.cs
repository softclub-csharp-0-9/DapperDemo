using DapperDemo.Enums;

namespace DapperDemo;

public class Todo
{
    public int Id { get; set; }
    public string TaskName { get; set; }
    public Status Status { get; set; }
}