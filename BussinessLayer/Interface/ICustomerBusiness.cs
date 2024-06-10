using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Interface
{
     public partial interface ICustomerBusiness
    {
        CustomerModel LoginCustomer(string taikhoan, string matkhau);
        CustomerModel GetCustomerbyID(string MaKH);
        bool Create_Customer(CustomerModel model);
    }
}
