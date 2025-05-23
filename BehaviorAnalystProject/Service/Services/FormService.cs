using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace Service.Services
{
    public class FormService : IService<FormDto>
    {
        private readonly IRepository<Form> Repositery;
        private readonly IRepository<ChildDto> RepositeryChild;
        private readonly IRepository<AnalystDto> RepositeryAnlyst;
        private readonly IMapper mapper;

        public FormService(IRepository<Form> repositery, IMapper mapper)
        {
            Repositery = repositery ?? throw new ArgumentNullException(nameof(repositery), "Repository cannot be null.");
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper cannot be null.");
        }

        public FormDto AddItem(FormDto item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
          //  var child = RepositeryChild.GetById(item.ChildID);

            if (item.FormFile == null || item.FormFile.Length == 0)
                throw new ArgumentException("חייב לצרף קובץ");

            // יצירת שם ייחודי לקובץ
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(item.FormFile.FileName)}";

            // נתיב שמירה פיזי
            var formsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Forms");
            if (!Directory.Exists(formsPath))
            {
                Directory.CreateDirectory(formsPath);
            }

            var fullPath = Path.Combine(formsPath, uniqueFileName);

            // שמירה פיזית של הקובץ
            using (var fs = new FileStream(fullPath, FileMode.Create))
            {
                item.FormFile.CopyTo(fs);
            }

            // שמירת פרטי הקובץ
            var fileUrl = $"/Forms/{uniqueFileName}";
            var entity = mapper.Map<FormDto, Form>(item);
            entity.FileName = uniqueFileName;
            entity.FileUrl = fileUrl;

            var added = Repositery.AddItem(entity);

            // שליפת הקובץ מהדיסק והכנסה ל-ArrFile לפני ההחזרה
            var result = mapper.Map<Form, FormDto>(added);
            var physicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", added.FileUrl.TrimStart('/'));

            if (File.Exists(physicalPath))
            {
                result.ArrFile = File.ReadAllBytes(physicalPath);
            }
            else
            {
                // אם הקובץ לא נמצא - אפשר לקבוע כ-null או לזרוק שגיאה
                result.ArrFile = null;
                // או: throw new FileNotFoundException("הקובץ לא נמצא בנתיב הצפוי", physicalPath);
            }

            return result;
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            var existing = Repositery.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException($"No form found with ID {id}.");

            Repositery.Delete(id);
        }

        public List<FormDto> GetAll()
        {
            return mapper.Map<List<Form>, List<FormDto>>(Repositery.GetAll());
        }

        public FormDto GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            var entity = Repositery.GetById(id);
            if (entity == null)
                throw new KeyNotFoundException($"No form found with ID {id}.");

            return mapper.Map<Form, FormDto>(entity);
        }
        public FormDto UpdateItem(int id, FormDto item)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            if (item == null)
                throw new ArgumentNullException(nameof(item), "Cannot update a null item.");

            if (string.IsNullOrWhiteSpace(item.FileName))
                throw new ArgumentException("Form name is required.");

            var existing = Repositery.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException($"No form found with ID {id}.");

            // מוודאים שה-ID של ה־DTO תואם לזה שב־URL
            var entityToUpdate = mapper.Map<Form>(item);
            entityToUpdate.Id = id;

            var updated = Repositery.UpdateItem(id, entityToUpdate);
            return mapper.Map<FormDto>(updated);
        }

    }
}
