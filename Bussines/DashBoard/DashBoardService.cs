﻿using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Newtonsoft.Json;


namespace Bussines
{
    public class DashBoardService : BaseService<DashBoard>, IDashBoardService
    {

        IRepository<FieldValue> _fieldValueRepo;

        public DashBoardService(IRepository<DashBoard> repo
           , IRepository<FieldValue> fieldTypeRepo) : base(repo)
        {
            _fieldValueRepo = fieldTypeRepo;
            _repo = repo;

        }
        public object GetChartsByUserId(string userId)
        {
            _repo.Context.Configuration.LazyLoadingEnabled = true;
            var Id = Guid.Parse(userId);
            //var data = _repo.GetAll()
            //    .Where(x => x.UserId == Id)
            //    .ToList()
            //    .Select(x => new 
            //    {
            //        Name = x.Field.Name,
            //        fieldType = x.FieldId.ToString(),//x.Field.FieldType.Name,
            //        fieldValue = x.Field.FieldValue.ToList().Select(s=>new object[] {s.CreateTime.ToString("dd/mm HH:MM"), s.Value })

            //    });
            var data = _repo.GetAll()
              .Where(x => x.UserId == Id)
              .ToList()
              .Select(x => new
              {
                  Name = x.Field.Name,
                  fieldType = x.FieldId.ToString(),//x.Field.FieldType.Name,
                  fieldValue = new object[]
                  {
                     new {
                         label = x.Field.Name,
                         data = x.Field.FieldValue.ToList().Select(s => new object[] { s.CreateTime.ToString("dd/mm HH:MM"), s.Value }) }
                  }

              });
            return data;
        }
    }
}
