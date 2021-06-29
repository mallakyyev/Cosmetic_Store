using AutoMapper;
using Cosmetics.BLL.DTO.ConfigurationModelDTO;
using Cosmetics.DAL.Data;
using Cosmetics.DAL.Models.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using models = Cosmetics.DAL.Models;

namespace Cosmetics.BLL.Services.SettingPortal
{
    public class SettingPortalService : ISettingPortalService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _appEnvironment;

        public SettingPortalService(ApplicationDbContext dbContext, IMapper mapper, IWebHostEnvironment appEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _appEnvironment = appEnvironment;
        }

        public async Task EditSettingPortal(EditSettingPortalDTO modelDTO)
        {
            var setting = await _dbContext.SettingPortals
            .Include(p => p.SettingPortalTranslates)
            .FirstOrDefaultAsync();

            if(setting != null)
            {
                setting.SettingPortalTranslates.Clear();

                setting.SettingPortalTranslates = modelDTO.SettingPortalTranslates
                .Select(p => new SettingPortalTranslate
                {
                    LanguageCulture = p.LanguageCulture,
                    SettingPortalId = p.SettingPortalId,
                    AboutPortal = p.AboutPortal,
                    DeliveryPortal = p.DeliveryPortal
                }).ToList();
                setting.PhonePortal = modelDTO.PhonePortal;
                setting.AddressPortal = modelDTO.AddressPortal;
                setting.EmailPortal = modelDTO.EmailPortal;

                if (modelDTO.FormFile != null)
                {
                    if (setting.LogoPortal != null)
                    {
                        bool deletedPictre = DeleteImage(setting.LogoPortal);
                    }
                    string fileName = await UploadImage(modelDTO.FormFile);
                    setting.LogoPortal = fileName;
                }

                _dbContext.Update(setting);
            }
            else
            {
                var addSetting = _mapper.Map<models.Configuration.SettingPortal>(modelDTO);
                if (modelDTO.FormFile != null)
                {
                    string fileName = await UploadImage(modelDTO.FormFile);
                    addSetting.LogoPortal = fileName;
                }
                await _dbContext.SettingPortals.AddAsync(addSetting);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<EditSettingPortalDTO> GetSettingPortal()
        {
            models.Configuration.SettingPortal settingPortal = await _dbContext.SettingPortals
                .Include(i => i.SettingPortalTranslates)
                .FirstOrDefaultAsync();
            EditSettingPortalDTO editSettingPortalDTO = _mapper.Map<EditSettingPortalDTO>(settingPortal);
            return editSettingPortalDTO;
        }

        public async Task<SettingPortalDTO> GetSettingPortalTranslate(string culture)
        {
            var setting = await _dbContext.SettingPortals.Include(i => i.SettingPortalTranslates)
                .FirstOrDefaultAsync();
            if (setting != null)
            {
                var translates = setting.SettingPortalTranslates.SingleOrDefault(s => s.LanguageCulture == culture);

                SettingPortalDTO settingPortalDTO = new SettingPortalDTO();
                settingPortalDTO.Id = setting.Id;
                settingPortalDTO.AboutPortal = translates.AboutPortal;
                settingPortalDTO.DeliveryPortal = translates.DeliveryPortal;
                settingPortalDTO.LogoPortal = setting.LogoPortal;
                settingPortalDTO.AddressPortal = setting.AddressPortal;
                settingPortalDTO.PhonePortal = setting.PhonePortal;
                settingPortalDTO.EmailPortal = setting.EmailPortal;

                return settingPortalDTO;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> UploadImage(IFormFile formFile)
        {
            var fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(formFile.FileName);

            string path = "/images/settings/";

            // Uncomment to save the file
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(_appEnvironment.WebRootPath + path);
            }

            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path + fileName, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return fileName;
        }

        public bool DeleteImage(string pictureName)
        {
            string path = "/images/settings/" + pictureName;

            if (!File.Exists(path)) return false;

            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
