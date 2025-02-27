﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace MvcProjectEcommerce.Controllers
{
    public class HeadingController : Controller
    {
        // GET: Heading

        HeadingManager headingManager=new HeadingManager(new EfHeadingDal());
        CategoryManager categoryManager=new CategoryManager(new EfCategoryDal());
        WriterManager writerManager=new WriterManager(new EfWriterDal());
        public ActionResult Index()
        {
            var headingValues = headingManager.GetList();
            return View(headingValues);
        }
        [HttpGet]
        public ActionResult AddHeading()
        {                                      //dropdown list icin yaptilk
            
            List<SelectListItem> valueCategory=(from x in categoryManager.GetList()

                                                select new SelectListItem
                                                {
                                                    Text=x.CategoryName,
                                                    Value=x.CategoryID.ToString(),
                                                }
                                                ).ToList();
            ViewBag.bagCategory=valueCategory;

            List<SelectListItem> valueWriter = (from x in writerManager.GetList()

                                                  select new SelectListItem
                                                  {
                                                      Text = x.WriterName+" "+x.WriterSurname,
                                                      Value = x.WriterID.ToString(),
                                                  }
                                                ).ToList();
            ViewBag.bagWriter = valueWriter;





            return View();
        }
        [HttpPost]
        public ActionResult AddHeading(Heading p)
        {
            p.HeadingDate =DateTime.Parse(DateTime.Now.ToShortDateString());
            headingManager.HeadingAdd(p);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateHeading(int id)
        {
            List<SelectListItem> valueCategory = (from x in categoryManager.GetList()

                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString(),
                                                  }
                                                ).ToList();
            ViewBag.bagCategory = valueCategory;
            var headingValue= headingManager.GetById(id);
            return View(headingValue);
        }
        [HttpPost]
        public ActionResult UpdateHeading(Heading p)
        {
            headingManager.HeadingUpdate(p);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteHeading(int id) 
        {
            var headingValue = headingManager.GetById(id);
            headingValue.HeadingStatus =false;

            headingManager.HeadingDelete(headingValue);

            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult HeadingReport()
        {
            var headingValues = headingManager.GetList();
            return View(headingValues);
        }
    }
}