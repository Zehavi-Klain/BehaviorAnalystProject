using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Common.Dto;
using Repository.Interfaces;
using Repository.Entities;
using AutoMapper;
using System.Net.Mail;

namespace Service.Services
{
    public class AnalystService : IService<AnalystDto>
    {
        private readonly IRepository<Analyst> Repositery;
        private readonly IMapper mapper;
        private readonly MyMapper _mapper = new MyMapper();

        public AnalystService(IRepository<Analyst> repositery, IMapper mapper)
        {
            Repositery = repositery;
            this.mapper = mapper;
        }

        public AnalystDto AddItem(AnalystDto item)
        {
            ValidateAnalystDto(item);

            var existing = Repositery.GetAll().FirstOrDefault(x => x.Id == item.Id);
            if (existing != null)
                throw new InvalidOperationException("An analyst with the same ID already exists.");

            Analyst entity = _mapper.Map<AnalystDto, Analyst>(item);
            Analyst added = Repositery.AddItem(entity);
            return _mapper.Map<Analyst, AnalystDto>(added);
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            var existing = Repositery.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException($"No analyst found with ID {id}.");

            Repositery.Delete(id);
        }

        public List<AnalystDto> GetAll()
        {
            return _mapper.Map<List<Analyst>, List<AnalystDto>>(Repositery.GetAll());
        }

        public AnalystDto GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            var entity = Repositery.GetById(id);
            if (entity == null)
                throw new KeyNotFoundException($"No analyst found with ID {id}.");

            return _mapper.Map<Analyst, AnalystDto>(entity);
        }

        public void UpdateItem(int id, AnalystDto item)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid ID.");

            ValidateAnalystDto(item);

            var existing = Repositery.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException($"No analyst found with ID {id}.");

            Repositery.UpdateItem(id, _mapper.Map<AnalystDto, Analyst>(item));
        }

        // ---------- בדיקות תקינות ----------

        private void ValidateAnalystDto(AnalystDto item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Cannot process a null item.");

            if (string.IsNullOrWhiteSpace(item.Id))
                throw new ArgumentException("ID is required.");

            if (!IsValidIsraeliId(item.Id))
                throw new ArgumentException("Invalid Israeli ID format.");

            if (string.IsNullOrWhiteSpace(item.Fname))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(item.Lname))
                throw new ArgumentException("Last name is required.");

            if (string.IsNullOrWhiteSpace(item.Email))
                throw new ArgumentException("Email is required.");

            if (!IsValidEmail(item.Email))
                throw new ArgumentException("Invalid email address.");
        }

        private bool IsValidIsraeliId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;

            id = id.Trim();

            if (!id.All(char.IsDigit))
                return false;

            if (id.Length < 5 || id.Length > 9)
                return false;

            id = id.PadLeft(9, '0');
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                int digit = int.Parse(id[i].ToString());
                int mult = (i % 2 == 0) ? 1 : 2;
                int res = digit * mult;
                if (res > 9)
                    res -= 9;
                sum += res;
            }

            return sum % 10 == 0;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
