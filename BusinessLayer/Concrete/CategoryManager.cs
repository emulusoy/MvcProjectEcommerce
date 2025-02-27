﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {


        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)//depancity injection
        {
            _categoryDal = categoryDal;
        }
        public List<Category> GetList()
        {
            return _categoryDal.List();//genericdeki metotlar buraya gelir
        }
        //service de methodu tanimla sonra burada yani manager de icini yonet doldur!
        public void CategoryAdd(Category category)
        {
            _categoryDal.Insert(category);
        }

        public Category GetById(int id)
        {
            return _categoryDal.Get(x => x.CategoryID == id);
        }

        public void CategoryDelete(Category category)
        {
            _categoryDal.Delete(category);//buradaki generic repositorydeki delete reposunu cektik
        }
        public void CategoryUpdate(Category category)
        {
            _categoryDal.Update(category);
        }
    }
}
