using System;
using System.Collections.Generic;
using System.Text;

namespace WebTestMessenger.BusinessLogic.Interfaces
{
    public interface IEntityModel<TEntity>
        where TEntity : class
    {
        void ToModel(TEntity entity);

        TEntity ToEntity();
    }
}
