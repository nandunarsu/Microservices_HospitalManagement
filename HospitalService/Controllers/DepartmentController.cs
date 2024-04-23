using HospitalService.Entity;
using HospitalService.Interface;
using HospitalService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
       
            private readonly IDepartment dept;
            public DepartmentController(IDepartment dept)
            {
                this.dept = dept;
            }
        [Authorize(Roles = "Admin")]
        [HttpPost]
            public IActionResult CreateDepartment(DepartmentRequest deptRequest)
            {
            try
            {
                var res = dept.CreateDept(deptRequest);
                var response = new ResponseModel<string>
                {
                    Message = "Department created Successful",

                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                var response = new ResponseModel<string>
                {
                    Message = ex.Message,

                };
                return BadRequest(response);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("int")]
            public IActionResult getByDeptId(int id)
            {
            try
            {
                var getres = dept.getByDeptId(id);
                var response = new ResponseModel<DepartmentEntity>
                {
                    Message = "Department Details are: ",
                    Data = getres

                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                var response = new ResponseModel<string>
                {
                    Message = ex.Message,

                };
                return BadRequest(response);
            }
            }
        [Authorize(Roles = "Admin")]
        [HttpGet("ByName")]
            public IActionResult getByDeptName(string name)
            {
            try
            {
              var results = dept.getByDeptName(name);
                var response = new ResponseModel<DepartmentEntity>
                {
                    Message = "Department Details are: ",
                    Data = results

                };
                return Ok(response);
            }
            catch(Exception ex)
            {
                var response = new ResponseModel<string>
                {
                    Message = ex.Message,

                };
                return BadRequest(response);
            }
            }

        }
    }

