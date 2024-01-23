using CustomersAPI.Contracts;
using CustomersAPI.Interface;
using CustomersAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersAPI.Service
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomersDBContext _customerDBContext;
        public CustomerRepository(CustomersDBContext customersDBContext)
        {
            this._customerDBContext = customersDBContext;
        }


        public async Task<ApiResponse<object>> GetById(int id)
        {
            var res = await _customerDBContext.Customers.Where(x => x.CustomerId == id).FirstOrDefaultAsync();
            return new ApiResponse<object> { HasError = false, Message = "", Data = res, Count = res == null ? 0 : 1 };
        }

        public async Task<ApiResponse<object>> GetCustomers() {
            var res = await _customerDBContext.Customers.ToListAsync();
            return new ApiResponse<object> { HasError = false, Message = "", Data = res, Count = res.Count };
        }
        public async Task<ApiResponse<object>> GetCustomersByEmail(string email) {
            var res = await _customerDBContext.Customers.Where(x => x.Email == email).FirstOrDefaultAsync();
            return new ApiResponse<object> { HasError = false, Message = "", Data = res, Count = res == null ? 0 : 1 };
        }
        public async Task<ApiResponse<object>> AddCustomer(CustomerDTO payload) {
            try
            {
                if (string.IsNullOrEmpty(payload.CustomerName)) return new ApiResponse<object> { HasError = true, Message = "Name Required" };
                if (string.IsNullOrEmpty(payload.Email)) return new ApiResponse<object> { HasError = true, Message = "Email Required" };
                var existingUser = await _customerDBContext.Customers.Where(x => x.Email == payload.Email).FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    return new ApiResponse<object> { HasError = true, Message = "Duplicate Email" };
                }

                var customer = new Customer()
                {
                    CustomerName = payload.CustomerName,
                    Email = payload.Email,
                    MobileNumber = payload.MobileNumber
                };

                await _customerDBContext.Customers.AddAsync(customer);
                await _customerDBContext.SaveChangesAsync();

                return new ApiResponse<object> { HasError = false, Message = "User Added Successfully" };
            }
            catch (Exception)
            {
                return new ApiResponse<object> { HasError = false, Message = "Error occured!!!" };
                //throw;
            }
        }
        public async Task<ApiResponse<object>> UpdateCustomer(CustomerDTO payload) {
            try
            {
                if (payload.Id == 0)
                {
                    return new ApiResponse<object> { HasError = true, Message = "Id required to updated user!!!!" };
                }
                if (string.IsNullOrEmpty(payload.CustomerName)) return new ApiResponse<object> { HasError = true, Message = "Name Required" };
                if (string.IsNullOrEmpty(payload.Email)) return new ApiResponse<object> { HasError = true, Message = "Email Required" };
                var existingUser = await _customerDBContext.Customers.Where(x => x.CustomerId == payload.Id).FirstOrDefaultAsync();

                if (existingUser == null)
                {
                    return new ApiResponse<object> { HasError = true, Message = "No record found!!!" };
                }

                existingUser.CustomerName = payload.CustomerName;
                existingUser.MobileNumber = payload.MobileNumber;
                await _customerDBContext.SaveChangesAsync();

                return new ApiResponse<object> { HasError = false, Message = "User Updated Successfully" };
            }
            catch (Exception)
            {
                return new ApiResponse<object> { HasError = false, Message = "Error occured!!!" };
                //throw;
            }
        }
        public async Task<ApiResponse<object>> DeleteCustomer(int id)
        {
            var res = await _customerDBContext.Customers.Where(x => x.CustomerId == id).FirstOrDefaultAsync();
            if (res != null)
            {
                _customerDBContext.Remove(res);
                await _customerDBContext.SaveChangesAsync();
                return new ApiResponse<object> { HasError = false, Message = "Customer deleted successfully." };

            }
            else
            {
                return new ApiResponse<object> { HasError = true, Message = "No record found!!!" };
            }

        }
    }
}
