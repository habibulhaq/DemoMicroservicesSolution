using CustomersAPI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersAPI.Interface
{
    public interface ICustomerRepository
    {
        Task<ApiResponse<object>> GetById(int id);
        Task<ApiResponse<object>> GetCustomers();
        Task<ApiResponse<object>> GetCustomersByEmail(string email);
        Task<ApiResponse<object>> AddCustomer(CustomerDTO payload);
        Task<ApiResponse<object>> UpdateCustomer(CustomerDTO payload);
        Task<ApiResponse<object>> DeleteCustomer(int id);
    }
}
