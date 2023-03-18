using System;
using WPFApp_CaseManagement.Models.Entities;
using WPFApp_CaseManagement.Models;
using WPFApp_CaseManagement.Context;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace WPFApp_CaseManagement.Services
{
    internal class EmployeeService
    {
        private static DataContext _context = new DataContext();

        // Add a new employee and save it to the db
        public static async Task Add(EmployeeModel? _employee)
        {
            if (_employee == null)
            {
                throw new ArgumentNullException(nameof(_employee));
            }
            try
            {
                // Check if the email address is already in the db, if so - return a prompt
                var existingEmployee = await _context.Employees.FirstOrDefaultAsync(a => a.Email == _employee.Email);
                if (existingEmployee != null)
                {
                    var result = MessageBox.Show("An employee with the same email address already exists. Please change the email address to create a new account", "Duplicate Email Address", MessageBoxButton.OK);
                    if (result == MessageBoxResult.OK)
                    {
                        // Cancel, do not save the result
                        return;
                    }
                }

                // If no duplicated email address, save the new record to the db
                var _employeeEntity =  new EmployeeEntity 
                { 
                    FirstName = _employee.FirstName, 
                    LastName = _employee.LastName, 
                    Email = _employee.Email, 
                    Department = _employee.Department, 
                    EmployeeNo = Guid.NewGuid()
                };

                _context.Add(_employeeEntity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var message = ex.InnerException?.Message ?? ex.Message;
                throw new Exception("Error: " + message, ex);
            }
        }

        // Fetch all employees and display them
        public static async Task<IEnumerable<EmployeeModel>> FetchEmployeeAsync()
        {
            var _employees = new List<EmployeeModel>();

            foreach (var _employee in await _context.Employees.ToListAsync())
                _employees.Add(new EmployeeModel
                {
                    Id = _employee.Id,
                    FirstName = _employee.FirstName,
                    LastName = _employee.LastName,
                    Email = _employee.Email,
                    Department = _employee.Department,
                });

            return _employees;
        }

        // Fetch employees from the db and add to observablecollection
        public static ObservableCollection<EmployeeModel> Employees()
        {
            var employees = new ObservableCollection<EmployeeModel>();
            foreach (var _employee in _context.Employees)
            {
                employees.Add(new EmployeeModel
                {
                    FirstName = _employee.FirstName,
                    LastName = _employee.LastName,
                    Email = _employee.Email,
                    Department = _employee.Department
                });
            }
            return employees;
        }

        // Delete selected employee based on the ID as in the employeemodel and save to db
        // If I had more time: Should also added a prompt "Are you sure you want to delete X?", and update the view without having to change view and then go back to make it easier for the user to see.
        public static async Task DeleteSelectedEmployeeAsync(EmployeeModel model)
        {
            if (model != null)
            {
                var selectedEmployee = _context.Employees.FirstOrDefault(c => c.Id == model.Id);
                if (selectedEmployee != null)
                {
                    selectedEmployee.FirstName = model.FirstName;
                    selectedEmployee.LastName = model.LastName;
                    selectedEmployee.Email = model.Email;
                    selectedEmployee.Department = model.Department;

                    _context.Remove(selectedEmployee);
                    await _context.SaveChangesAsync();
                }
            }
        }

        // Find employee with the same email address as in employeemodel and edit the selected employee
        public static async Task EditSelectedEmployeeAsync(EmployeeModel model)
        {
            if (model != null)
            {
                var selectedEmployee = _context.Employees.FirstOrDefault(c => c.Email == model.Email);
                if (selectedEmployee != null)
                {
                    selectedEmployee.FirstName = model.FirstName;
                    selectedEmployee.LastName = model.LastName;
                    selectedEmployee.Email = model.Email;
                    selectedEmployee.Department = model.Department;

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}