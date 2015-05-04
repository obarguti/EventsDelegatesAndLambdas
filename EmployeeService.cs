using System;
using System.Collections.Generic;
using System.Linq;

namespace EventsDelegatesAndLambdas
{
    public delegate bool Filter(Employee employee);

    public class EmployeeService
    {
        public IEnumerable<Employee> Employees { get; set; }

        public EmployeeService(IEnumerable<Employee> employees)
        {
            Employees = employees;
        }

        public static void Main(string[] args)
        {
            var employees = new List<Employee>
            {
                new Employee() { Name = "Arch Stanton", Age = 44, IsFullTime = true, IsManager = false },
                new Employee() { Name = "Bill Carson", Age = 42, IsFullTime = true, IsManager = true },
                new Employee() { Name = "D. Harry", Age = 16, IsFullTime = false, IsManager = false }
            };

            var employeeService = new EmployeeService(employees);
            PrintEmployees(employeeService.Employees);

            var filterDelegate = new Filter(IsManager);

            var filteredEmployees = employeeService.FilterEmployees(filterDelegate);
            PrintEmployees(employeeService.FilterEmployees(filterDelegate));

            filterDelegate = IsFullTime;
            PrintEmployees(employeeService.FilterEmployees(filterDelegate));

            filterDelegate = IsMinor;
            PrintEmployees(employeeService.FilterEmployees(filterDelegate));

            Console.ReadKey();
        }

        public static void PrintEmployees(IEnumerable<Employee> employees)
        {
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Name);
            }
        }

        public IEnumerable<Employee> FilterEmployees(Filter employeesFilter)
        {
            return Employees.Where(e => employeesFilter(e));
        }

        public static bool IsManager(Employee employee)
        {
            return employee.IsManager;
        }

        public static bool IsFullTime(Employee employee)
        {
            return employee.IsFullTime;
        }

        public static bool IsMinor(Employee employee)
        {
            return employee.Age < 18;
        }
    }
}
