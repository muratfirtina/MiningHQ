namespace Domain.Constants;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Moderator = "Moderator";
    public const string HRAssistant = "HRAssistant";
    
    public static readonly string[] All = { Admin, Moderator, HRAssistant };
    
    public static class Claims
    {
        public const string UsersRead = "users.read";
        public const string UsersWrite = "users.write";
        public const string EmployeesRead = "employees.read";
        public const string EmployeesWrite = "employees.write";
        public const string MachinesRead = "machines.read";
        public const string MachinesWrite = "machines.write";
        public const string QuarriesRead = "quarries.read";
        public const string QuarriesWrite = "quarries.write";
        public const string AdminPanel = "admin.panel";
    }
}
