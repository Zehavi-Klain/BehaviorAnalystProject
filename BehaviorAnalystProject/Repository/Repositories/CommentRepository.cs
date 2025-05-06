using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
        public class CommentRepository : IRepository<Comment>
        {
            private readonly IContext context;

            public CommentRepository(IContext context)
            {
                this.context = context;
            }

            public Comment AddItem(Comment item)
            {
                this.context.Comment.Add(item);
                this.context.Save();
                return item;
            }

            public void Delete(int id)
            {
                this.context.Comment.Remove(GetById(id));
                context.Save();
            }

            public List<Comment> GetAll()
            {
                return this.context.Comment.ToList();
            }

            public Comment GetById(int id)
            {
                return this.context.Comment.FirstOrDefault(x => x.ID==id);
            }

            public void UpdateItem(int id, Comment item)
            {
                var comment = GetById(id);
                comment.AccessPermission = item.AccessPermission;
                comment.Comments= item.Comments;
                context.Save();
            }
        }
}
