using DataAccessLayer.Interface;
using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CustomerRepository : ICustomerRepository
    {
        private IDatabaseHelper _dbHelper;
        public CustomerRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public CustomerModel LoginCustomer(string taikhoan, string matkhau)
        {
            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_login_customers",
                     "@taikhoan", taikhoan,
                     "@matkhau", matkhau
                );
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    CustomerModel customer = new CustomerModel
                    {
                        MaKH = Convert.ToInt32(row["MaKH"]),
                        TenDangNhap = row["Username"].ToString(),
                        MatKhau = row["Password"].ToString(),
                        HoTen = row["FullName"].ToString(),
                        Email = row["Email"].ToString(),
                        DiaChi = row["Address"].ToString()
                    };
                    return customer;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CustomerModel GetCustomerbyID(string MaKH)
        {

            string msgError = "";
            try
            {
                var dt = _dbHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_get_customer_id",
                                                                     "@MaKH", MaKH);
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }

                return dt.ConvertTo<CustomerModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create_Customer(CustomerModel model)
        {
            string msgError = "";
            try
            {
                var result = _dbHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_create_account_kh",
                    "@tenDangNhap", model.TenDangNhap,
                    "@matKhau", model.MatKhau,
                    "@email", model.Email,
                    "@hoTen", model.HoTen,
                    "@diaChi", model.DiaChi,
                    "@sdt", model.SDT);  // Thêm tham số cho số điện thoại

                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
