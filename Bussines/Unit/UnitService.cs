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
    public class UnitService : BaseService<Unit>, IUnitService
    {
        IRepository<Unit> _repo;
        IRepository<FieldType> _fieldTypeRepo;
        private string key;
        private List<ChartModel> values;

        public UnitService(IRepository<Unit> repo
            , IRepository<FieldType> fieldTypeRepo) : base(repo)
        {
            _fieldTypeRepo = fieldTypeRepo;
            _repo = repo;

        }
        public object GetFieldForCharts(string unitId)
        {
         //TODO: fieldtype lazyloadingle gelmeli neden gelmiyor çöz
            var data = _repo.GetById(unitId)
                .Fields
                .Select(x => new UnitFieldsCharts()
                {
                    Name = x.Name,
                    fieldType = _fieldTypeRepo.GetById(x.FieldTypeId).Name,
                    fieldValue = ChartModel.create(new ChartModel()
                    {
                        key = x.Name,
                        values = x.FieldValue
                            .Select(s => new ChartValues()
                            { CreateTime = dateToLong(s.CreateTime), Value = s.Value })
                            .OrderByDescending(d => d.CreateTime)
                            .Take(10)
                            .ToList()
                    })
                });
            return data;
        }
        private long dateToLong(DateTime now)
        {
            long dateNumber = 1297380023295;
            long beginTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

            long nowTicks = now.Ticks;
            var fark = (nowTicks - beginTicks) / 10000;

            DateTime dt = new DateTime(beginTicks + dateNumber * 10000, DateTimeKind.Utc);
            // MessageBox.Show(dt.ToLocalTime().ToString());

            long unixDate = 1297380023295;
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var date = start.Ticks / 10000 + 1297380023295;
            return fark;
        }
    }
}
