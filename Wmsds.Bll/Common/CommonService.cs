using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wmsds.Dal;
using Wmsds.Entities;
using Wmsds.Entities.Common;

namespace Wmsds.Bll.Common
{
    public class CommonService : ICommonService
    {
        public async Task<WmsdsResponse<int>> AddChannel(Channel channel)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (channel==null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }

            if (channel.DistrictId<=0 || string.IsNullOrEmpty(channel.DistrictName))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "DistrictId is a required field.";
                return wmsdResponse;
            }

            if (channel.TehsilId <= 0)
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "TehsilId is a required field.";
                return wmsdResponse;
            }

            if (string.IsNullOrEmpty(channel.ChannelName))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "Channel Name is a required field.";
                return wmsdResponse;
            }

            try
            {
                using (var _dbContext = new EntityContext())
                {
                    _dbContext.Channels.Add(channel);
                    var response = await _dbContext.SaveChangesAsync();
                    if (response > 0)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success; ;
                        wmsdResponse.ResponseMessage = "Channel has been added successfully.";
                        return wmsdResponse;
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Failed; ;
                        wmsdResponse.ResponseMessage = "Failed to add channel.";
                        return wmsdResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }

        }
        public async Task<WmsdsResponse<int>> AddDistrict(District district)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (district == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }
            if (string.IsNullOrEmpty(district.Name))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "Name is a required field.";
                return wmsdResponse;
            }
            try
            {
                using (var _dbContext = new EntityContext())
                {
                    _dbContext.Districts.Add(district);
                    var response = await _dbContext.SaveChangesAsync();
                    if (response > 0)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success; ;
                        wmsdResponse.ResponseMessage = "District has been added successfully.";
                        return wmsdResponse;
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Failed; ;
                        wmsdResponse.ResponseMessage = "Failed to add District.";
                        return wmsdResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
        public async Task<WmsdsResponse<int>> AddDivision(Division division)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (division == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }
            if (string.IsNullOrEmpty(division.Name))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "Name is a required field.";
                return wmsdResponse;
            }
            try
            {
                using (var _dbContext = new EntityContext())
                {
                    _dbContext.Divisions.Add(division);
                    var response = await _dbContext.SaveChangesAsync();
                    if (response > 0)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success; ;
                        wmsdResponse.ResponseMessage = "Division has been added successfully.";
                        return wmsdResponse;
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Failed; ;
                        wmsdResponse.ResponseMessage = "Failed to add Division.";
                        return wmsdResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
        public async Task<WmsdsResponse<int>> AddFinancialYear(FinancialYear financial)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (financial == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }
            if (string.IsNullOrEmpty(financial.Year))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "Year is a required field.";
                return wmsdResponse;
            }
            try
            {
                using (var _dbContext = new EntityContext())
                {
                    _dbContext.FinancialYears.Add(financial);
                    var response = await _dbContext.SaveChangesAsync();
                    if (response > 0)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success; ;
                        wmsdResponse.ResponseMessage = "F.Year has been added successfully.";
                        return wmsdResponse;
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Failed; ;
                        wmsdResponse.ResponseMessage = "Failed to add year.";
                        return wmsdResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
        public async Task<WmsdsResponse<int>> AddProvince(Province province)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (province == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }
            if (string.IsNullOrEmpty(province.Name))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "Name is a required field.";
                return wmsdResponse;
            }
            try
            {
                using (var _dbContext = new EntityContext())
                {
                    _dbContext.Provinces.Add(province);
                    var response = await _dbContext.SaveChangesAsync();
                    if (response > 0)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success; ;
                        wmsdResponse.ResponseMessage = "Province has been added successfully.";
                        return wmsdResponse;
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Failed; ;
                        wmsdResponse.ResponseMessage = "Operation failed.";
                        return wmsdResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
        public async Task<WmsdsResponse<int>> AddTehsil(Tehsil tehsil)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (tehsil == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }
            if (string.IsNullOrEmpty(tehsil.Name))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "Name is a required field.";
                return wmsdResponse;
            }
            try
            {
                using (var _dbContext = new EntityContext())
                {
                    _dbContext.Tehsils.Add(tehsil);
                    var response = await _dbContext.SaveChangesAsync();
                    if (response > 0)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success; ;
                        wmsdResponse.ResponseMessage = "Tehsil has been added successfully.";
                        return wmsdResponse;
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Failed; ;
                        wmsdResponse.ResponseMessage = "Operation failed.";
                        return wmsdResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
        public async Task<WmsdsResponse<int>> AddWaterCourse(WaterCourse waterCourse)
        {
            var wmsdResponse = new WmsdsResponse<int>();
            if (waterCourse == null)
            {
                wmsdResponse.ResponseCode = EnumStatus.EmptyObject;
                wmsdResponse.ResponseMessage = "Object is empty.";
                return wmsdResponse;
            }
            if (string.IsNullOrEmpty(waterCourse.WcNo))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "Watercourse is a required field.";
                return wmsdResponse;
            }
            if (string.IsNullOrEmpty(waterCourse.ChannelName))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "Name is a required field.";
                return wmsdResponse;
            }
            if (string.IsNullOrEmpty(waterCourse.TehsilName))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "Tehsil is a required field.";
                return wmsdResponse;
            }
            if (string.IsNullOrEmpty(waterCourse.DistrictName))
            {
                wmsdResponse.ResponseCode = EnumStatus.ValidationFailed; ;
                wmsdResponse.ResponseMessage = "District Name is a required field.";
                return wmsdResponse;
            }
            try
            {
                using (var _dbContext = new EntityContext())
                {
                    _dbContext.WaterCourses.Add(waterCourse);
                    var response = await _dbContext.SaveChangesAsync();
                    if (response > 0)
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Success; ;
                        wmsdResponse.ResponseMessage = "Water course has been added successfully.";
                        return wmsdResponse;
                    }
                    else
                    {
                        wmsdResponse.ResponseCode = EnumStatus.Failed; ;
                        wmsdResponse.ResponseMessage = "Operation failed.";
                        return wmsdResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                wmsdResponse.ResponseCode = EnumStatus.InternalServer;
                wmsdResponse.ResponseMessage = ex.Message;
                return wmsdResponse;
            }
        }
        public async Task<List<District>> GetAllDistricts()
        {
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    return await _dbContext.Districts.ToListAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<List<District>> GetAllDistrictsByDivId(int divisionId)
        {
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    return await _dbContext.Districts.Where(x => x.DivisionId == divisionId).ToListAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<List<Division>> GetAllDivisions()
        {
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    return await _dbContext.Divisions.ToListAsync();
                }
                catch (Exception ex)
                {
                    return null; ;
                }
            }
        }

        public async Task<Division> GetDivisionById(int Id)
        {
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    return await _dbContext.Divisions.Where(x=> x.Id == Id).FirstOrDefaultAsync();
                }
                catch (Exception ex)
                {
                    return null; ;
                }
            }
        }

        public async Task<Division> UpdateDivision(Division division)
        {
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    var dbEntity =  await _dbContext.Divisions.Where(x => x.Id == division.Id).FirstOrDefaultAsync();
                    dbEntity.Name = division.Name;
                    if (dbEntity != null)
                    {
                      var response = await _dbContext.SaveChangesAsync();
                    }
                    return dbEntity;
                }
                catch (Exception ex)
                {
                    return null; ;
                }
            }
        }

        public async Task<List<FinancialYear>> GetAllFinancialYears()
        {
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    return await _dbContext.FinancialYears.ToListAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<List<Province>> GetAllProvinces()
        {
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    return await _dbContext.Provinces.ToListAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<List<Tehsil>> GetAllTehsils()
        {
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    return await _dbContext.Tehsils.ToListAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<List<Tehsil>> GetAllTehsilsByDistrictId(int districtId)
        {
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    return await _dbContext.Tehsils.Where(x => x.DistrictId == districtId).ToListAsync();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
        public async Task<List<Channel>> GetChannelsByDistAndTehId(int districtId, int tehsilId)
        {
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    return await _dbContext.Channels.Where(x => x.DistrictId == districtId
                && x.TehsilId == tehsilId).ToListAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<List<Division>> GetDivisionByProvinceId(int provinceId)
        {
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    return await _dbContext.Divisions.Where(x => x.ProvinceId == provinceId).ToListAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<List<WaterCourse>> GetWaterCourses(int districtId=0, int tehsilId=0, int channelId=0)
        {
            using (var _dbContext = new EntityContext())
            {
                try
                {
                    var waterCourses = await _dbContext.WaterCourses
                                    .Where(c => districtId == 0 || c.DistrictId == districtId)
                                        .Where(c => tehsilId == 0 || c.TehsilId == tehsilId)
                                        .Where(c => channelId == 0 || c.ChannelId == channelId)                                                                             
                                        .ToListAsync();
                    return waterCourses;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
