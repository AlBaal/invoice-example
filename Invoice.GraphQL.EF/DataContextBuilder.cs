﻿using EfCore.InMemoryHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.GraphQL.EF
{
    public class DataContextBuilder
    {
        public static MyDataContext BuildDataContext()
        {
            var company1 = new Company
            {
                Id = 1,
                Content = "Company1"
            };
            var employee1 = new Employee
            {
                Id = 2,
                CompanyId = company1.Id,
                Content = "Employee1"
            };
            var employee2 = new Employee
            {
                Id = 3,
                CompanyId = company1.Id,
                Content = "Employee2"
            };
            var company2 = new Company
            {
                Id = 4,
                Content = "Company2"
            };
            var employee4 = new Employee
            {
                Id = 5,
                CompanyId = company2.Id,
                Content = "Employee4"
            };
            var company3 = new Company
            {
                Id = 6,
                Content = "Company3"
            };
            var company4 = new Company
            {
                Id = 7,
                Content = "Company4"
            };
            var myDataContext = InMemoryContextBuilder.Build<MyDataContext>();
            myDataContext.AddRange(company1, employee1, employee2, company2, company3, company4, employee4);
            myDataContext.SaveChanges();
            return myDataContext;
        }
    }
}
