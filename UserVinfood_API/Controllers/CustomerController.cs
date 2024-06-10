using BussinessLayer;
using BussinessLayer.Interface;
using DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserVinfood_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerBusiness _user;
        public CustomerController(ICustomerBusiness user)
        {
            _user = user;
        }
        [HttpPost("create-customer")]
        public IActionResult CreateCustomer([FromBody] CustomerModel model)
        {
            try
            {
                bool result = _user.Create_Customer(model);
                if (result)
                {
                    return Ok(new { message = "Tạo tài khoản thành công." });
                }
                else
                {
                    return BadRequest(new { message = "Không thể tạo tài khoản." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Lỗi: {ex.Message}" });
            }
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                // Thực hiện đăng nhập sử dụng thông tin từ yêu cầu
                var user = _user.LoginCustomer(request.TaiKhoan, request.MatKhau);

                // Nếu đăng nhập thành công, trả về thông tin khách hàng
                return Ok(user);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về thông báo lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("get-by-id-customers/{id}")]
        public CustomerModel GetCustomerbyID(string id)
        {
            return _user.GetCustomerbyID(id);
        }
    }
    // Class để đại diện cho yêu cầu đăng nhập từ client
    public class LoginRequest
    {
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
    }
}

