using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersAPI.Contracts
{
    public class ApiResponse<T>
    {
        public ApiResponse() { }

        public ApiResponse(T response)
        {
            Data = response;
        }

        public T Data { get; set; }

        public bool HasError { get; set; } = false;

        public string Message { get; set; } = string.Empty;
        public long Count { get; set; } = 0;
    }
}
