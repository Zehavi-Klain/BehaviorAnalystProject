using AutoMapper;
using Common.Dto;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Services;
using System.Net.Mail;

public class ChildService : IService<ChildDto>
{
    private readonly IRepository<Child> Repositery;
    private readonly IMapper _mapper;

    public ChildService(IRepository<Child> repositery, IMapper mapper)
    {
        Repositery = repositery;
        _mapper = mapper;
    }

    public ChildDto AddItem(ChildDto item)
    {
        try
        {
            // בדיקת תקינות הקלט
            ValidateChildDto(item);

            var child = _mapper.Map<ChildDto, Child>(item);
            var result = Repositery.AddItem(child);
            return _mapper.Map<Child, ChildDto>(result);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"שגיאה בנתונים: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"שגיאה בלתי צפויה: {ex.Message}");
        }
    }

    public void Delete(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID לא תקין");

        var existing = Repositery.GetById(id);
        if (existing == null)
            throw new KeyNotFoundException($"לא נמצא ילד עם מזהה {id}");

        Repositery.Delete(id);
    }

    public List<ChildDto> GetAll()
    {
        return _mapper.Map<List<Child>, List<ChildDto>>(Repositery.GetAll());
    }

    public ChildDto GetById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("ID לא תקין");

        var child = Repositery.GetById(id);
        if (child == null)
            throw new KeyNotFoundException($"לא נמצא ילד עם מזהה {id}");

        return _mapper.Map<Child, ChildDto>(child);
    }

    public ChildDto UpdateItem(int id, ChildDto item)
    {
        try
        {
            if (id <= 0)
                throw new ArgumentException("ID לא תקין");

            ValidateChildDto(item);

            var existing = Repositery.GetById(id);
            if (existing == null)
                throw new KeyNotFoundException($"לא נמצא ילד עם מזהה {id}");

            Repositery.UpdateItem(id, _mapper.Map<ChildDto, Child>(item));
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException($"שגיאה בנתונים: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"שגיאה בלתי צפויה: {ex.Message}");
        }
        return item;
    }

    public List<FormDto> GetChildForms(int id)
    {
        var child = Repositery.GetById(id);
        if (child == null)
            throw new ArgumentException($"Child with code {id} does not exist.");

        var commentEntities = child.ChildForms ?? new List<Form>();
        return _mapper.Map<List<Form>, List<FormDto>>(child.ChildForms.ToList());
    }
    public List<CommentDto> GetChildComments(int id)
    {
        var child = Repositery.GetById(id);
        if (child == null)
            throw new ArgumentException($"Child with code {id} does not exist.");

        var commentEntities = child.ChildComments ?? new List<Comment>();
        var comments = _mapper.Map<List<Comment>, List<CommentDto>>(commentEntities.ToList());
        return comments;
    }

    public List<LessonSummaryDto> GetLessonSummery(int id)
    {
        var child = Repositery.GetById(id);
        if (child == null)
            throw new ArgumentException($"Child with code {id} does not exist.");

        var commentEntities = child.ChildLessonsSumery ?? new List<LessonSummary>();
        var summaries = _mapper.Map<List<LessonSummary>, List<LessonSummaryDto>>(commentEntities.ToList());
        return summaries;
    }
    #region בדיקות תקינות



    // ---------- בדיקות תקינות ----------
    private void ValidateChildDto(ChildDto item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item), "לא ניתן לעבד פריט null.");

        if (string.IsNullOrWhiteSpace(item.Id))
            throw new ArgumentException("ID נדרש.");

        if (string.IsNullOrWhiteSpace(item.Fname))
            throw new ArgumentException("שם פרטי נדרש.");

        if (string.IsNullOrWhiteSpace(item.Lname))
            throw new ArgumentException("שם משפחה נדרש.");

        if (string.IsNullOrWhiteSpace(item.Email))
            throw new ArgumentException("אימייל נדרש.");

        if (!IsValidEmail(item.Email))
            throw new ArgumentException("כתובת אימייל לא תקינה.");
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
#endregion