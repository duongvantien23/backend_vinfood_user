using BussinessLayer.Interface;
using DataAccessLayer.Interface;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private ICustomerRepository _res;
        public CustomerBusiness(ICustomerRepository res)
        {
            _res = res;
        }
        public CustomerModel LoginCustomer(string taikhoan, string matkhau)
        {
            try
            {
                return _res.LoginCustomer(taikhoan, matkhau);
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                throw ex;
            }
        }
        public CustomerModel GetCustomerbyID(string MaKH)
        {
            return _res.GetCustomerbyID(MaKH);
        }
        public bool Create_Customer(CustomerModel model)
        {
            return _res.Create_Customer(model);
        }
    }
}
