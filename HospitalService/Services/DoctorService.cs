using Dapper;
using HospitalService.Context;
using HospitalService.Entity;
using HospitalService.Interface;
using HospitalService.Model;
using System.Numerics;

namespace HospitalService.Services
{
    public class DoctorService : IDoctor
    {
        private readonly DapperContext _context;

        public DoctorService(DapperContext context)
        {
            _context = context;
        }
            public async Task<bool> CreateDoctor(DoctorRequest request)
            {
                try
                {
                    var query = "INSERT INTO Doctor (DoctorId, DeptId, DoctorName, DoctorAge, DoctorAvailable, Specialization, Qualifications) VALUES (@DoctorId, @DeptId, @DoctorName, @DoctorAge, @DoctorAvailable, @Specialization, @Qualifications);";
                    DoctorEntity e = MapToEntity(request);
                    //getUserById(request.DoctorId));
                    var parameters = new DynamicParameters();
                    parameters.Add("@DoctorId", e.DoctorId);
                    parameters.Add("@DeptId", e.DeptId);
                    parameters.Add("@DoctorName", e.DoctorName);
                    parameters.Add("@DoctorAge", e.DoctorAge);
                    parameters.Add("@DoctorAvailable", e.DoctorAvailable);
                    parameters.Add("@Specialization", e.Specialization);
                    parameters.Add("@Qualifications", e.Qualifications);
                    using (var connection = _context.CreateConnection())
                    {
                        var result = await connection.ExecuteAsync(query, parameters);
                        return result > 0;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        private DoctorEntity MapToEntity(DoctorRequest request)
        {
            return new DoctorEntity
            {
                DoctorId = request.DoctorId,
                DeptId = request.DeptId,
                DoctorName = request.DoctorName,
                DoctorAge = request.DoctorAge,
                DoctorAvailable = request.DoctorAvailable,
                Specialization = request.Specialization,
                Qualifications = request.Qualifications
            };
        }
        public async Task<IEnumerable<DoctorEntity>> GetDoctorById(int doctorId)
        {
            try
            {
                var query = " SELECT * FROM Doctor WHERE DoctorId = @DoctorId;";
                var parameters = new DynamicParameters();
                parameters.Add("@DoctorId", doctorId);
                using (var connection = _context.CreateConnection())
                {
                    var result =  await connection.QueryAsync<DoctorEntity>(query,parameters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<DoctorEntity>> GetAllDoctors()
        {
            try
            {
                string query = "SELECT * FROM Doctor;";
                // return  _context.CreateConnection().Query<DoctorEntity>(query).ToList();
                using (var connection = _context.CreateConnection())
                {
                    var Doctor = await connection.QueryAsync<DoctorEntity>(query);
                    return Doctor.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> UpdateDoctor(int doctorId, DoctorRequest request)
        {
            try
            {
                string query = @"
            UPDATE Doctor 
            SET Specialization = @Specialization,
                Qualifications = @Qualifications,
                DoctorName = @DoctorName,
                DoctorAge = @DoctorAge,
                DoctorAvailable = @DoctorAvailable
            WHERE DoctorId = @DoctorId";

                var parameters = new DynamicParameters();
                parameters.Add("@DoctorId", doctorId);
                parameters.Add("@Specialization", request.Specialization);
                parameters.Add("@Qualifications", request.Qualifications);
                parameters.Add("@DoctorName", request.DoctorName);
                parameters.Add("@DoctorAge", request.DoctorAge);
                parameters.Add("@DoctorAvailable", request.DoctorAvailable);

                using (var connection = _context.CreateConnection())
                {
                    int rowsAffected = await connection.ExecuteAsync(query, parameters);
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteDoctor(int doctorId)
        {
            try
            {
                string query = "DELETE FROM Doctor WHERE  DoctorId = @DoctorId;";
                //var result = _context.CreateConnection().Execute(query, new { DoctorId = doctorId });
                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.ExecuteAsync(query, new { DoctorId = doctorId });
                    return result > 0;
                }
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<IEnumerable<DoctorEntity>> GetDoctorsBySpecialization(string specialization)
        {
            try
            {
                string query = "SELECT * FROM Doctor WHERE Specialization = @Specialization;";
                return _context.CreateConnection().Query<DoctorEntity>(query, new { Specialization = specialization }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    

}
}
