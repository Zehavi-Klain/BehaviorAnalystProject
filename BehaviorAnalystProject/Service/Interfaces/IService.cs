using Common.Dto;
using Repository.Entities;

public interface IService<T>
{
    T GetById(int id);
    List<T> GetAll();
    void Delete(int id);
    T AddItem(T item);
    T UpdateItem(int id, T item);

    // פונקציה אופציונלית עם מימוש ברירת מחדל
    List<CommentDto> GetChildComments(int id)
    {
        throw new NotImplementedException("GetChildComments לא מומשה");
    }

    List<FormDto> GetChildForms(int id)
    {
        throw new NotImplementedException("GetChildForms לא מומשה");
    }
    List<LessonSummaryDto> GetLessonSummery(int id)
    {
        throw new NotImplementedException("GetLessonSummery לא מומשה");
    }
}
