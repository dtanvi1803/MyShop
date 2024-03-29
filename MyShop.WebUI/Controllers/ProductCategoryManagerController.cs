﻿using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = context.Collection().ToList();
            return View(productCategories);
        }
        public ActionResult Create()
        {
            ProductCategory productCat = new ProductCategory();
            return View(productCat);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCat)
        {
            if (!ModelState.IsValid)
            {
                return View(productCat);
            }
            else
            {
                context.Insert(productCat);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            ProductCategory productCat = context.Find(Id);
            if (productCat == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCat);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCat, string Id)
        {
            ProductCategory productCatToEdit = context.Find(Id);
            if (productCatToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCat);
                }
                productCatToEdit.Category = productCat.Category;
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCat = context.Find(Id);
            if (productCat == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCat);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCatToDelete = context.Find(Id);
            if (productCatToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}